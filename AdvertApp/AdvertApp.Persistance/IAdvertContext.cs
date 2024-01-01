using AdvertApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdvertApp.Persistance;

public interface IAdvertContext
{
    public DbSet<Advert> Adverts { get; }
    public DbSet<User> Users { get; }

    DbSet<AdvertModel> SortedAdverts { get; }

    int SaveChanges();
}
