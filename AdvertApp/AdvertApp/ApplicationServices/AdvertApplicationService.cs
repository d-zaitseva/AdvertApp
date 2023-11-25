using AdvertApp.ApplicationServices.Contracts;
using AdvertApp.Models.Enums;
using AdvertApp.Models.FormModels;
using AdvertApp.Models.ViewModels;

namespace AdvertApp.ApplicationServices;

public class AdvertApplicationService : IAdvertApplicationService
{
    private readonly ILogger<IAdvertApplicationService> _logger;
    private readonly IConfiguration _configuration;

    public AdvertApplicationService(ILogger<IAdvertApplicationService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<AdvertViewModel>> GetAllAsync(CancellationToken cancellationToke)
    {
        // get list of all adverts.

        var collection = new List<AdvertViewModel>();

        collection.Add(new AdvertViewModel
        {
            Id = Guid.NewGuid(),
            Number = 1,
            UserId = Guid.NewGuid(),
            Text = "Some text here",
            Rating = 3,
            CreatedAt = DateTime.Now,
            Status = AdvertStatus.Active
        });
        collection.Add(new AdvertViewModel
        {
            Id = Guid.NewGuid(),
            Number = 2,
            UserId = Guid.NewGuid(),
            Text = "Some other text here",
            Rating = 5,
            CreatedAt = DateTime.Now,
            Status = AdvertStatus.Active
        });

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
