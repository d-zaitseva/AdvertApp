using AdvertApp.EF.Entities;

namespace AdvertApp.Repositories.Contracts;

public interface IAdvertReadRepository
{
    Task<IEnumerable<Advert>> GetAllAsync();

    Task<Advert?> GetByIdAsync(Guid id);

    Task<IEnumerable<Advert>> GetByUserIdAsync(Guid userId);
}
