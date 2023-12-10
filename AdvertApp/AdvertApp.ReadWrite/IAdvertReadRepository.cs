using AdvertApp.Domain.Entities;

namespace AdvertApp.ReadWrite;

public interface IAdvertReadRepository
{
    Task<IEnumerable<Advert>> GetAllAsync(CancellationToken cancellationToken);

    Task<Advert?> GetByIdAsync(Guid id);

    Task<IEnumerable<Advert>> GetByUserIdAsync(Guid userId);
}
