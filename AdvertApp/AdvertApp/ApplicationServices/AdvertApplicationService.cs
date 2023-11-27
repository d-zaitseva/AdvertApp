using AdvertApp.ApplicationServices.Contracts;
using AdvertApp.EF.Entities;
using AdvertApp.Models;
using AdvertApp.Models.Enums;
using AdvertApp.Models.FormModels;
using AdvertApp.Models.ViewModels;
using AdvertApp.Repositories.Contracts;

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

    public AdvertApplicationService(
        IAdvertReadRepository advertReadRepository,
        IAdvertWriteRepository advertWriteRepository,
        IUserRepository userRepository,
        ILogger<IAdvertApplicationService> logger,
        IConfiguration configuration,
        IImageApplicationService imageApplicationService)
    {
        _advertReadRepository = advertReadRepository;
        _advertWriteRepository = advertWriteRepository;
        _userRepository = userRepository;
        _logger = logger;
        _configuration = configuration;
        _configuration.Bind(nameof(SettingsPerUserOptions), settingsOptions);
        _imageApplicationService = imageApplicationService;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<AdvertViewModel>> GetAllAsync(CancellationToken cancellationToke)
    {
        var collection = new List<AdvertViewModel>();
        // get list of all adverts.
        var result = await _advertReadRepository.GetAllAsync();
        if (result.Any())
        {
            foreach (var item in result)
            {
                // add Image convert to IFormFile
                collection.Add(new AdvertViewModel
                {
                    Id = item.Id,
                    Number = item.Number,
                    UserId = item.UserId,
                    Text = item.Text,
                    Image = null,
                    Rating = item.Rating,
                    CreatedAt = item.CreatedAt,
                    UpdatedAt = item.UpdatedAt,
                    ExpiredAt = item.ExpiredAt,
                    Status = item.Status
                });
            }
        }

        else
        {
            _logger.LogInformation("AdvertReadRepository's method GetAllAsync() returned no adverts");
        }

        return collection;
    }

    /// <inheritdoc />
    public async Task<AdvertViewModel> AddAsync(CreateAdvertFormModel model)
    {
        // check if user with userId can add advert (compare with max Advert amount from settings)
        var userAdverts = await _advertReadRepository.GetByUserIdAsync(model.UserId);
        if (userAdverts.Count() < settingsOptions.MaxAdvertAmount)
        {
            Image? image = model.Image != null 
                ? _imageApplicationService.ConvertFormFileToImage(model.Image)
                : null;

            var advert = new Advert
            {
                Id = Guid.NewGuid(),
                UserId = model.UserId,
                Text = model.Text,
                ImageId = image?.Id,
                Image = image,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = AdvertStatus.Active
            };

            await _advertWriteRepository.CreateAsync(advert);
            _advertWriteRepository.CommitChanges();

            var newAdvert = await _advertReadRepository.GetByIdAsync(advert.Id);
            return newAdvert != null
                ? new AdvertViewModel
                {
                    Id = newAdvert.Id,
                    Number = newAdvert.Number,
                    UserId = newAdvert.UserId,
                    Text = newAdvert.Text,
                    Rating = newAdvert.Rating,
                    CreatedAt = newAdvert.CreatedAt,
                    UpdatedAt = newAdvert.UpdatedAt,
                    ExpiredAt = newAdvert.ExpiredAt,
                    Status = newAdvert.Status
                }
                : new AdvertViewModel();

        }
        _logger.LogError($" User with Id {model.UserId} exceeded the permissible limit of published adverts ammount." +
            $"User cannot add new advert.");

        return new AdvertViewModel();
    }

    /// <inheritdoc />
    public async Task<AdvertViewModel> UpdateAsync(UpdateAdvertFormModel model)
    {
        //get advert by Id
        // if model.UserId != advert.UserId -> get user by Id to check UserRole
        // check userId to validate if can be updated

        // update existing advert with new fields
        // change UpdatedAt property
        // save changes

        var advert = new AdvertViewModel
        {
            Id = Guid.NewGuid(),
            Number = 3,
            UserId = model.UserId,
            Text = model.Text,
            Rating = 5,
            CreatedAt = DateTime.Now,
            Status = AdvertStatus.Active
        };

        return advert;
    }

    /// <inheritdoc />
    public async Task DeleteAsync(DeleteAdvertFormModel model)
    {
        //get advert by Id
        // if model.UserId != advert.UserId -> get user by Id to check UserRole
        // check userId to validate if can be deleted

        // change advert status to closed
        // save changes
    }
}
