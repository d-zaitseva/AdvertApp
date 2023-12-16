using AdvertApp.Contracts.Enums;

namespace AdvertApp.Domain.Entities;

public class AdvertModel
{
    public Guid Id { get; set; }
    public int Number { get; set; }
    public Guid UserId { get; set; }
    public string AuthorName { get; set; }
    public string Text { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public int Rating { get; } = 0;
    public DateTime Audit_CreatedAt { get; set; }
    public DateTime Audit_UpdatedAt { get; set; }
    public DateTime? ExpiredAt { get; set; } = null;
    public AdvertStatus Status { get; set; }
}
