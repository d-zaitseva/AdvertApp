using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Microsoft.AspNetCore.Http;
using AdvertApp.Contracts.Enums;

namespace AdvertApp.Contracts.Models.ViewModels;

public class AdvertViewModel
{
    /// <summary>
    /// Advert Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Advert Number.
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// UserId who created advert.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Advert text.
    /// </summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Image file.
    /// </summary>
    public IFormFile? Image { get; set; }

    /// <summary>
    /// Advert rating.
    /// </summary>
    public int Rating { get; set; }

    /// <summary>
    /// Creation date.
    /// </summary>
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// Update date. Equals to CreatedDate by default
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Date when Advert change status to Closed.
    /// </summary>
    public DateTime? ExpiredAt { get; set; }

    /// <summary>
    /// Advert status.
    /// </summary>
    /// 
    [JsonConverter(typeof(StringEnumConverter))]
    public AdvertStatus Status { get; set; }
}
