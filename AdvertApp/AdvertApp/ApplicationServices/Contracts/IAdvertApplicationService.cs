using AdvertApp.Models.FormModels;
using AdvertApp.Models.ViewModels;

namespace AdvertApp.ApplicationServices.Contracts;

public interface IAdvertApplicationService
{
    /// <summary>
    /// Returns all adverts.
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<AdvertViewModel>> GetAllAsync(CancellationToken cancellationToke);

    /// <summary>
    /// Adds advert.
    /// </summary>
    /// <param name="model">Advert's fields.</param>
    /// <returns>Created advert.</returns>
    Task<AdvertViewModel> AddAsync(CreateAdvertFormModel model);

    /// <summary>
    /// Updates advert.
    /// </summary>
    /// <param name="model">Advert's fields.</param>
    /// <returns>Updated advert.</returns>
    Task<AdvertViewModel> UpdateAsync(UpdateAdvertFormModel model);

    /// <summary>
    /// Deletes advert by Id.
    /// </summary>
    /// <param name="id">Advert Id.</param>
    /// /// <param name="userId">User Id who deletes advert.</param>
    /// <returns></returns>
    Task DeleteAsync(DeleteAdvertFormModel model);
}
