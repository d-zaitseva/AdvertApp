using Microsoft.AspNetCore.Http;
using AdvertApp.Contracts.Enums;
using System.ComponentModel.DataAnnotations;
using AdvertApp.Contracts.ValidationAttributes;

namespace AdvertApp.Contracts.Models.FormModels;

public class UpdateAdvertFormModel
{
    /// <summary>
    /// Advert Id that should be updated.
    /// </summary>
    [Required]
    public Guid AdvertId { get; set; }

    /// <summary>
    /// UserId who updates advert.
    /// </summary>
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

    /// <summary>
    /// Advert status.
    /// </summary>
    [Required]
    public AdvertStatus Status { get; set; }
}
