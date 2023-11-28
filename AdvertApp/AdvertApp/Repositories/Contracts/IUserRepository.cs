using AdvertApp.EF.Entities;

namespace AdvertApp.Repositories.Contracts;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
}
