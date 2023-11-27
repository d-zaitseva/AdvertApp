using AdvertApp.EF.Entities;

namespace AdvertApp.Repositories.Contracts;

public interface IAdvertWriteRepository
{
    Task CreateAsync(Advert advert);

    void CommitChanges();
}
