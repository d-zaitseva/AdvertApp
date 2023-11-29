﻿using AdvertApp.ApplicationServices.Contracts;
using AdvertApp.EF.Entities;
using AdvertApp.Models;
using AdvertApp.Models.Enums;
using AdvertApp.Models.FormModels;
using AdvertApp.Models.ViewModels;
using AdvertApp.Repositories.Contracts;
using AutoMapper;
using CSharpFunctionalExtensions;

namespace AdvertApp.ApplicationServices;

public class AdvertApplicationService : IAdvertApplicationService
{
    private readonly IAdvertReadRepository _advertReadRepository;
    private readonly IAdvertWriteRepository _advertWriteRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<IAdvertApplicationService> _logger;
    private readonly IConfiguration _configuration;
    private readonly SettingsPerUserOptions settingsOptions = new();
    private readonly IImageApplicationService _imageApplicationService;
    private readonly IUserApplicationService _userApplicationService;
    private readonly IMapper _mapper;

    public AdvertApplicationService(
        IAdvertReadRepository advertReadRepository,
        IAdvertWriteRepository advertWriteRepository,
        IUserRepository userRepository,
        ILogger<IAdvertApplicationService> logger,
        IConfiguration configuration,
        IImageApplicationService imageApplicationService,
        IUserApplicationService userApplicationService,
        IMapper mapper)
    {
        _advertReadRepository = advertReadRepository;
        _advertWriteRepository = advertWriteRepository;
        _userRepository = userRepository;
        _logger = logger;
        _configuration = configuration;
        _configuration.Bind(nameof(SettingsPerUserOptions), settingsOptions);
        _imageApplicationService = imageApplicationService;
        _userApplicationService = userApplicationService;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<AdvertViewModel>> GetAllAsync(CancellationToken cancellationToken)
    {
        var collection = new List<AdvertViewModel>();

        var result = await _advertReadRepository.GetAllAsync(cancellationToken);
        if (result.Any())
        {
            foreach (var item in result)
            {
                var avm = _mapper.Map<AdvertViewModel>(item);

                if (item.Image is not null)
                {
                    var image = _imageApplicationService.ConvertImageToFormFile(item.Image);
                    avm.Image = image;
                }

                collection.Add(avm);
            }
        }

        else
        {
            _logger.LogInformation("AdvertReadRepository's method GetAllAsync() returned no adverts");
        }

        return collection;
    }

    /// <inheritdoc />
    public async Task<Result> AddAsync(CreateAdvertFormModel model)
    {
        var userAdverts = await _advertReadRepository.GetByUserIdAsync(model.UserId);
        var user = await _userApplicationService.GetByIdAsync(model.UserId);
        if (user == null) 
        {
            _logger.LogError($"User with Id {model.UserId} doesn't exist");
            Result.Failure("User not found");
        }

        if (userAdverts.Count() < settingsOptions.MaxAdvertAmount)
        {
            Image? image = model.Image != null
                ? _imageApplicationService.ConvertFormFileToImage(model.Image)
                : null;

            var createdAdvert = new Advert(model.UserId, user.Name, model.Text, image);

            await _advertWriteRepository.CreateAsync(createdAdvert);
            _advertWriteRepository.CommitChanges();

            return Result.Success();
        }

        _logger.LogError($"User with Id {model.UserId} exceeded the permissible limit of published adverts ammount." +
            $"User cannot add new advert.");

        return Result.Failure("The advert cannot be created");
    }

    /// <inheritdoc />
    public async Task<Result> Update(UpdateAdvertFormModel model)
    {
        var updatedAdvert = await _advertReadRepository.GetByIdAsync(model.AdvertId);
        if (updatedAdvert is null)
        {
            return Result.Failure($"Advert with Id {model.AdvertId} was not found");
        }

        if (model.UserId != updatedAdvert.UserId)
        {
            var user = await _userRepository.GetByIdAsync(model.UserId);

            if (user is null)
            {
                return Result.Failure($"User with Id {model.UserId} was not found");
            }

            if (user.Role != UserRole.Admin)
            {
                return Result.Failure($"User with Id {model.UserId} cannot update advert with Id {model.AdvertId}");
            }
        }

        try
        {
            updatedAdvert.Updtate(
                        model.Text,
                        model.Image is not null
                            ? _imageApplicationService.ConvertFormFileToImage(model.Image)
                            : null,
                        model.Status);
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError(ex.Message);
            return Result.Failure("Some required fields were not set");
        }

        _advertWriteRepository.Update(updatedAdvert);

        return Result.Success();
    }

    /// <inheritdoc />
    public async Task<Result> DeleteAsync(DeleteAdvertFormModel model)
    {
        var deletedAdvert = await _advertReadRepository.GetByIdAsync(model.AdvertId);
        if (deletedAdvert is null)
        {
            return Result.Failure($"Advert with Id {model.AdvertId} was not found");
        }

        if (model.UserId != deletedAdvert.UserId)
        {
            var user = await _userRepository.GetByIdAsync(model.UserId);

            if (user is null)
            {
                return Result.Failure($"User with Id {model.UserId} was not found");
            }

            if (user.Role != UserRole.Admin)
            {
                return Result.Failure($"User with Id {model.UserId} cannot delete advert with Id {model.AdvertId}");
            }
        }

        deletedAdvert.SoftDelete();

        _advertWriteRepository.Update(deletedAdvert);

        return Result.Success();
    }
}
