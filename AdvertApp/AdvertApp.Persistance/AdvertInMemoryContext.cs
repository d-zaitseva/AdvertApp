using Microsoft.EntityFrameworkCore;

namespace AdvertApp.Persistance;

public class AdvertInMemoryContext : AdvertContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "AdvertDbInMemory");
    }
}
