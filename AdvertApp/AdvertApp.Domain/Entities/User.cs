using AdvertApp.Contracts.Enums;

namespace AdvertApp.Domain.Entities;

public class User
{
    /// <summary>
    /// User Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// User name.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Collection of adverts created by current user
    /// </summary>
    public ICollection<Advert> Adverts { get; set; } = new List<Advert>();

    /// <summary>
    /// User role.
    /// </summary>
    public UserRole Role { get; set; } = UserRole.Consumer;
}
