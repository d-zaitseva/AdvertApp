using AdvertApp.ApplicationServices.Contracts;
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

    public AdvertApplicationService(
        IAdvertReadRepository advertReadRepository,
        IAdvertWriteRepository advertWriteRepository,
        IUserRepository userRepository,
        ILogger<IAdvertApplicationService> logger,
        IConfiguration configuration)
    {
        _advertReadRepository = advertReadRepository;
        _advertWriteRepository = advertWriteRepository;
        _userRepository = userRepository;
        _logger = logger;
        _configuration = configuration;
        _configuration.Bind(nameof(SettingsPerUserOptions), settingsOptions);
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
        // get all adverts by userId and count amount
        
        //save new advert
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
