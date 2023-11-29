using Newtonsoft.Json;

namespace AdvertApp.Models.FormModels;

public class CreateAdvertFormModel
{
    /// <summary>
    /// UserId who creats advert.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Text of the advert.
    /// </summary>
    public string Text { get; set; } = String.Empty;

    /// <summary>
    /// Advert image file.
    /// </summary>
    public IFormFile? Image { get; set; }
}
