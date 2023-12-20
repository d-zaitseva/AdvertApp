using AdvertApp.Contracts.ValidationAttributes;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AdvertApp.Contracts.Models.FormModels;

public class CreateAdvertFormModel
{
    /// <summary>
    /// UserId who creats advert.
    /// </summary>
    /// 
    [Required(ErrorMessage = "User Id is a required property.")]
    public Guid UserId { get; set; }

    /// <summary>
    /// Text of the advert.
    /// </summary>
    [Required(ErrorMessage = "Advert text cannot be empty.")]
    [MaxLength(500)]
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Advert image file.
    /// </summary>
    [MaxFileSize(5 * 1024 * 1024)]
    [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
    public IFormFile? Image { get; set; }
}
