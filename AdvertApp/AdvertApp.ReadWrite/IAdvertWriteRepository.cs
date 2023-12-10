using AdvertApp.Domain.Entities;

namespace AdvertApp.ReadWrite;

public interface IAdvertWriteRepository
{
    Task CreateAsync(Advert advert);

    void Update(Advert advert);

    Task DeleteAsync(Guid id);

    void CommitChanges();
}
