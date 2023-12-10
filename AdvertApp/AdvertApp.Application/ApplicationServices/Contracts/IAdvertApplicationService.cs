using CSharpFunctionalExtensions;
using AdvertApp.Contracts.Models.FormModels;
using AdvertApp.Contracts.Models.ViewModels;

namespace AdvertApp.Application.ApplicationServices.Contracts;

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
    /// <returns>Result of the action.</returns>
    Task<Result> AddAsync(CreateAdvertFormModel model);

    /// <summary>
    /// Updates advert.
    /// </summary>
    /// <param name="model">Advert's fields.</param>
    /// <returns>Result of the action..</returns>
    Task<Result> Update(UpdateAdvertFormModel model);

    /// <summary>
    /// Deletes advert by Id.
    /// </summary>
    /// <param name="id">Advert Id.</param>
    /// /// <param name="userId">User Id who deletes advert.</param>
    /// <returns></returns>
    Task<Result> DeleteAsync(DeleteAdvertFormModel model);
}
