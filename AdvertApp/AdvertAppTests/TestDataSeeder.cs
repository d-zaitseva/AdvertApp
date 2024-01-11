using AdvertApp.Contracts.Enums;
using AdvertApp.Domain.Entities;
using AdvertApp.Persistance;
using AdvertAppTests.Fixtures;

namespace AdvertAppTests;

public class TestDataSeeder
{
    private readonly TestFixture _fixture;
    private readonly IAdvertContext? _context;

    public TestDataSeeder()
    {
        _fixture = new TestFixture();
        _context = _fixture.GetService<IAdvertContext>();
    }

    public void SeedTestData()
    {
        if (_context is not null && !_context.Users.Any())
        {
            _context?.Users.AddRangeAsync(
                new User() { Id = new Guid("003AE0EC-3374-4DEB-94C8-56035E01BC44"), Name = "Admin", Role = UserRole.Admin },
                new User() { Id = new Guid("DB7F24C1-A46B-4BC2-A36A-B0D550F4D647"), Name = "User1", Role = UserRole.Consumer },
                new User() { Id = new Guid("A8B329B0-FC1E-4170-A82C-6C0714ADBB8F"), Name = "User2", Role = UserRole.Consumer });
        }

        if (_context is not null && !_context.Adverts.Any())
        {
            _context?.Adverts.AddRangeAsync(
                new Advert(new Guid("003AE0EC-3374-4DEB-94C8-56035E01BC44"), "Admin", "advert text 1", string.Empty),
                new Advert(new Guid("DB7F24C1-A46B-4BC2-A36A-B0D550F4D647"), "User1", "advert text 2", string.Empty),
                new Advert(new Guid("DB7F24C1-A46B-4BC2-A36A-B0D550F4D647"), "User1", "advert text 3", string.Empty),
                new Advert(new Guid("A8B329B0-FC1E-4170-A82C-6C0714ADBB8F"), "User2", "advert text 4", string.Empty));
        }

        _context?.SaveChanges();
    }
}
