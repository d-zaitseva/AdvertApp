using AdvertApp.Models.Enums;
using Newtonsoft.Json;

namespace AdvertApp.Models.FormModels;

public class UpdateAdvertFormModel
{
    /// <summary>
    /// Advert Id that should be updated.
    /// </summary>
    public Guid AdvertId { get; set; }

    /// <summary>
    /// UserId who updates advert.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Text of the advert.
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Advert image file.
    /// </summary>
    public IFormFile? Image { get; set; }

    /// <summary>
    /// Advert status.
    /// </summary>
    public AdvertStatus Status { get; set; }
}
