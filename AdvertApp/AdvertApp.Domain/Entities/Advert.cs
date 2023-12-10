using AdvertApp.Contracts.Enums;

namespace AdvertApp.Domain.Entities;

public class Advert
{
    public Advert(Guid userId, string userName, string text, Image? image)
    {
        UserId = userId;
        AuthorName = userName;
        Text = text;
        Image = image;
        Audit = new Audit(DateTime.Now);
        Status = AdvertStatus.Active;
    }

    public Guid Id { get; }
    public int Number { get; }
    public Guid UserId { get; }
    public string AuthorName { get; }
    public string Text { get; private set; } = string.Empty;
    public Image? Image { get; private set; } = null;
    public int Rating { get; } = 0;
    public Audit Audit { get; private set; }
    public DateTime? ExpiredAt { get; private set; } = null;
    public AdvertStatus Status { get; private set; }

    public void Updtate(string text, Image? image, AdvertStatus status)
    {
        ArgumentNullException.ThrowIfNull(text);
        Text = text;
        Image = image;
        Audit.SetUpdateDate(DateTime.Now);
        Status = status;
        if (Status == AdvertStatus.Closed)
        {
            ExpiredAt = DateTime.Now;
        }
    }

    public void SoftDelete()
    {
        Status = AdvertStatus.Closed;
        ExpiredAt = DateTime.Now;
    }

    private Advert() { }
}

public record Audit
{
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; private set; }

    public Audit (DateTime date)
    {
        CreatedAt = date;
        UpdatedAt = date;
    }

    public Audit(DateTime createdAd, DateTime updatedAt)
    {
        CreatedAt = createdAd;
        UpdatedAt = updatedAt;
    }

    public void SetUpdateDate(DateTime updatedAt)
    {
        UpdatedAt = updatedAt;
    }
    private Audit() { }
}
