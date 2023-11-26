using AdvertApp.Models.Enums;

namespace AdvertApp.EF.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<Advert> Adverts { get; set; } = new List<Advert>();
    public UserRole Role { get; set; } = UserRole.Consumer;
}
