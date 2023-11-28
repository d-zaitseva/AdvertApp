using AdvertApp.Models.Enums;
using Newtonsoft.Json;

namespace AdvertApp.Models.FormModels;

public class UpdateAdvertFormModel
{
    /// <summary>
    /// Advert Id that should be updated.
    /// </summary>
    public Guid AdvertId { get; private set; }

    /// <summary>
    /// UserId who updates advert.
    /// </summary>
    public Guid UserId { get; private set; }

    /// <summary>
    /// Text of the advert.
    /// </summary>
    public string Text { get; private set; }

    /// <summary>
    /// Advert image file.
    /// </summary>
    public IFormFile? Image { get; private set; }

    /// <summary>
    /// Advert status.
    /// </summary>
    public AdvertStatus Status { get; set; }

    public UpdateAdvertFormModel(Guid advertId, Guid userId, string text)
    {
        AdvertId = advertId;
        UserId = userId;
        Text = text;
    }

    [JsonConstructor]
    public UpdateAdvertFormModel(Guid advertId, Guid userId, string text, IFormFile image)
    {
        AdvertId = advertId;
        UserId = userId;
        Text = text;
        Image = image;
    }
}
