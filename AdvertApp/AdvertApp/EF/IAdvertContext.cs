using AdvertApp.EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdvertApp.EF
{
    public interface IAdvertContext
    {
        public DbSet<Advert> Adverts { get; }
        public DbSet<User> Users { get; }

        int SaveChanges();
    }
}
