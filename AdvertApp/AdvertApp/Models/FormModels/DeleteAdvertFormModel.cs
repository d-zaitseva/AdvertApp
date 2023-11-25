namespace AdvertApp.Models.FormModels;

public class DeleteAdvertFormModel
{
    /// <summary>
    /// Advert Id that should be deleted.
    /// </summary>
    public Guid AdvertId { get; private set; }

    /// <summary>
    /// UserId who deletes advert.
    /// </summary>
    public Guid UserId { get; private set; }

    public DeleteAdvertFormModel(Guid advertId, Guid userId)
    {
        AdvertId = advertId;
        UserId = userId;
    }
}
