using AdvertApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdvertApp.Application;

public interface IAdvertContext
{
    public DbSet<Advert> Adverts { get; }
    public DbSet<User> Users { get; }

    int SaveChanges();
}
