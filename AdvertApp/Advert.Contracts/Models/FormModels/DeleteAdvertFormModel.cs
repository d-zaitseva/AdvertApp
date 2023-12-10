namespace AdvertApp.Contracts.Models.FormModels;

public class DeleteAdvertFormModel
{
    /// <summary>
    /// Advert Id that should be deleted.
    /// </summary>
    public Guid AdvertId { get; set; }

    /// <summary>
    /// UserId who deletes advert.
    /// </summary>
    public Guid UserId { get; set; }
}
