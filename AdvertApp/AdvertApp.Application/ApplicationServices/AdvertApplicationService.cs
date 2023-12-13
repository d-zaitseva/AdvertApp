using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using CSharpFunctionalExtensions;
using AdvertApp.Application.ApplicationServices.Contracts;
using AdvertApp.Contracts.Enums;
using AdvertApp.Contracts.Models;
using AdvertApp.Contracts.Models.FormModels;
using AdvertApp.Contracts.Models.ViewModels;
using AdvertApp.ReadWrite;
using AdvertApp.Domain.Entities;
using AutoMapper;

namespace AdvertApp.Application.ApplicationServices;

public class AdvertApplicationService : IAdvertApplicationService
{
    private readonly IAdvertReadRepository _advertReadRepository;
    private readonly IAdvertWriteRepository _advertWriteRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<IAdvertApplicationService> _logger;
    private readonly IConfiguration _configuration;
    private readonly SettingsPerUserOptions settingsOptions = new();
    private readonly IImageApplicationService _imageApplicationService;
    private readonly IMapper _mapper;

    public AdvertApplicationService(
        IAdvertReadRepository advertReadRepository,
        IAdvertWriteRepository advertWriteRepository,
        IUserRepository userRepository,
        ILogger<IAdvertApplicationService> logger,
        IConfiguration configuration,
        IImageApplicationService imageApplicationService,
        IMapper mapper)
    {
        _advertReadRepository = advertReadRepository;
        _advertWriteRepository = advertWriteRepository;
        _userRepository = userRepository;
        _logger = logger;
        _configuration = configuration;
        _configuration.Bind(nameof(SettingsPerUserOptions), settingsOptions);
        _imageApplicationService = imageApplicationService;
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

                if (!string.IsNullOrEmpty(item.FilePath))
                {
                    var image = _imageApplicationService.GetImageFile(item.FilePath);
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

        var user = await _userRepository.GetByIdAsync(model.UserId);
        if (user == null) 
        {
            _logger.LogError($"User with Id {model.UserId} doesn't exist");
            return Result.Failure("User not found");
        }

        if (userAdverts.Count() < settingsOptions.MaxAdvertAmount)
        {
            var path = model.Image is not null 
                ? await _imageApplicationService.UploadAsync(model.Image) 
                : string.Empty;

            var createdAdvert = new Advert(model.UserId, user.Name, model.Text, path);

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

        if (string.IsNullOrEmpty(model.Text))
        {
            return Result.Failure("Text is a required field.");
        }

        if (updatedAdvert.FilePath != null)
        {
            _imageApplicationService.DeleteImageFile(updatedAdvert.FilePath);
        }

        var path = model.Image is not null
                ? await _imageApplicationService.UploadAsync(model.Image)
                : string.Empty;

        updatedAdvert.Updtate(
                        model.Text,
                        path,
                        model.Status);

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
