using System.ComponentModel.DataAnnotations;

namespace AdvertApp.Contracts.Models.FormModels;

public class DeleteAdvertFormModel
{
    /// <summary>
    /// Advert Id that should be deleted.
    /// </summary>
    [Required]
    public Guid AdvertId { get; set; }

    /// <summary>
    /// UserId who deletes advert.
    /// </summary>
    [Required]
    public Guid UserId { get; set; }
}
