using Newtonsoft.Json;

namespace AdvertApp.Models.FormModels;

public class CreateAdvertFormModel
{
    /// <summary>
    /// UserId who creats advert.
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

    public CreateAdvertFormModel(Guid userId, string text)
    {
        UserId = userId;
        Text = text;
    }

    [JsonConstructor]
    public CreateAdvertFormModel(Guid userId, string text, IFormFile image)
    {
        UserId = userId;
        Text = text;
        Image = image;
    }
}
