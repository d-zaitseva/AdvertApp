using AdvertApp.EF.Entities;

namespace AdvertApp.Repositories.Contracts;

public interface IAdvertWriteRepository
{
    Task Create(Advert advert);

    void CommitChanges();
}
