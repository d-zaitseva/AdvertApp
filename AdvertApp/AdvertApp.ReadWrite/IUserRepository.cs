using AdvertApp.Domain.Entities;

namespace AdvertApp.ReadWrite;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
}
