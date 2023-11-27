using AdvertApp.Models.Enums;

namespace AdvertApp.EF.Entities;

public class Advert
{
    public Guid Id { get; set; }
    public int Number { get; set; }
    public User User { get; set; } = null!;
    public Guid UserId { get; set; }
    public string Text { get; set; } = string.Empty;
    public Image? Image { get; set; } = null;
    public Guid? ImageId { get; set; } = null;
    public int Rating { get; set; } = 0;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime ExpiredAt { get; set; }
    public AdvertStatus Status { get; set; }
}
