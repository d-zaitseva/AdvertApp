using AdvertApp.EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdvertApp.EF;

public class AdvertContext : DbContext, IAdvertContext
{
    public DbSet<Advert> Adverts { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Image> Images { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=AdvertAppDb;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Advert>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Number).ValueGeneratedOnAdd();
            entity.Property(a => a.Text).HasMaxLength(255);
            entity.Property(a => a.Rating).HasDefaultValue(0);
            entity.Property(a => a.CreatedAt).HasColumnType("datetime");
            entity.Property(a => a.UpdatedAt).HasColumnType("datetime");
            entity.Property(a => a.ExpiredAt).HasColumnType("datetime").HasDefaultValue(null);
            entity.Property(a => a.ImageId).IsRequired(false);

            entity.HasOne(a => a.User)
                .WithMany(a => a.Adverts)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(a => a.Image)
                .WithOne(i => i.Advert)
                .HasForeignKey<Image>(i => i.Id)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(i => i.Id);
            entity.Property(i => i.Data).HasColumnType("varbinary(max)");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
        });

        modelBuilder.Entity<User>().HasData(
            new User { Id = Guid.NewGuid(), Name = "Admin", Role = Models.Enums.UserRole.Admin },
            new User { Id = Guid.NewGuid(), Name = "User1", Role = Models.Enums.UserRole.Consumer },
            new User { Id = Guid.NewGuid(), Name = "User2", Role = Models.Enums.UserRole.Consumer }
        );

        base.OnModelCreating(modelBuilder);
    }
}
