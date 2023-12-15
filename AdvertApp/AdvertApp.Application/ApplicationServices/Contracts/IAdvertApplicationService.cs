using CSharpFunctionalExtensions;
using AdvertApp.Contracts.Models.FormModels;
using AdvertApp.Contracts.Models.ViewModels;
using AdvertApp.Contracts.Enums;
using AdvertApp.Contracts.Models;

namespace AdvertApp.Application.ApplicationServices.Contracts;

public interface IAdvertApplicationService
{
    /// <summary>
    /// Returns all adverts.
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<AdvertViewModel>> GetAllAsync(FilterRequest filterRequest, CancellationToken cancellationToke);

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
    /// <param name="model">Advert Id and User Id who deletes advert.</param>
    /// <returns></returns>
    Task<Result> DeleteAsync(DeleteAdvertFormModel model);

    /// <summary>
    /// Change advert status to deleted by Id.
    /// </summary>
    /// <param name="model">Advert Id and User Id who deletes advert.</param>
    /// <returns></returns>
    Task<Result> SoftDeleteAsync(DeleteAdvertFormModel model);
}
