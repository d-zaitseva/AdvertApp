using AdvertApp.Application;
using AdvertApp.Contracts.Enums;
using AdvertApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertApp.Persistance;

public class AdvertContext : DbContext, IAdvertContext
{
    public DbSet<Advert> Adverts { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=AdvertAppDb;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Advert>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Number)
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            entity.Property(a => a.Text).HasMaxLength(255);
            entity.Property(a => a.Rating).HasDefaultValue(0);
            entity.Property(a => a.ExpiredAt).HasColumnType("datetime").HasDefaultValue(null);
            entity.Property(a => a.UserId);
            entity.Property(a => a.AuthorName).HasMaxLength(255);

            entity.OwnsOne(e => e.Audit, e => e.ConfigureAudit())
            .Navigation(e => e.Audit)
            .IsRequired();

            entity.OwnsOne(e => e.Image, e => e.ConfigureImage())
            .Navigation(e => e.Audit)
            .IsRequired(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Name).HasMaxLength(255);

            entity
                .HasMany(u => u.Adverts)
                .WithOne()
                .HasForeignKey(u => u.UserId);
        });

        modelBuilder.Entity<User>().HasData(
            new User { Id = Guid.NewGuid(), Name = "Admin", Role = UserRole.Admin },
            new User { Id = Guid.NewGuid(), Name = "User1", Role = UserRole.Consumer },
            new User { Id = Guid.NewGuid(), Name = "User2", Role = UserRole.Consumer }
        );

        base.OnModelCreating(modelBuilder);
    }
}

public static class ConfigurationExtensions
{
    public static OwnedNavigationBuilder<TOwner, Audit> ConfigureAudit<TOwner>(
    this OwnedNavigationBuilder<TOwner, Audit> builder)
    where TOwner : class
    {
        builder
            .Property(e => e.CreatedAt)
            .IsRequired(true)
            .HasColumnType("datetime");
        builder
            .Property(e => e.UpdatedAt)
            .IsRequired(true)
            .HasColumnType("datetime");

        return builder;
    }

    public static OwnedNavigationBuilder<TOwner, Image> ConfigureImage<TOwner>(
    this OwnedNavigationBuilder<TOwner, Image> builder)
    where TOwner : class
    {
        builder
            .Property(e => e.Id);
        builder
            .Property(e => e.Name);
        builder
            .Property(e => e.FileName);
        builder
            .Property(e => e.Type);
        builder
            .Property(e => e.ContentDisposition);
        builder
            .Property(e => e.Data)
            .HasColumnType("varbinary(max)");


        return builder;
    }
}