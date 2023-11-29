﻿using AdvertApp.EF.Entities;

namespace AdvertApp.Repositories.Contracts;

public interface IAdvertWriteRepository
{
    Task CreateAsync(Advert advert);

    void Update(Advert advert);

    Task DeleteAsync(Guid id);

    void CommitChanges();
}
