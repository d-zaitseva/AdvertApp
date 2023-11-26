using AdvertApp.EF;
using AdvertApp.EF.Entities;
using AdvertApp.Models;
using AdvertApp.Models.Enums;
using AdvertApp.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
namespace AdvertApp.Repositories;

public class AdvertRepository : IAdvertReadRepository, IAdvertWriteRepository
{
    private IAdvertContext _context;

    public AdvertRepository(IAdvertContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Advert>> GetAllAsync()
    {
        //return await _context.Adverts.ToListAsync();
        var collection = new List<Advert>();

        collection.Add(new Advert
        {
            Id = Guid.NewGuid(),
            Number = 1,
            UserId = Guid.NewGuid(),
            Text = "Some text here",
            Rating = 3,
            CreatedAt = DateTime.Now,
            Status = AdvertStatus.Active
        });
        collection.Add(new Advert
        {
            Id = Guid.NewGuid(),
            Number = 2,
            UserId = Guid.NewGuid(),
            Text = "Some other text here",
            Rating = 5,
            CreatedAt = DateTime.Now,
            Status = AdvertStatus.Active
        });

        return collection;
    }

    public async  Task<Advert?> GetByIdAsync(Guid id)
    {
        //return await _context.Adverts.FirstOrDefaultAsync(a => a.Id == id);
        return new Advert
        {
            Id = id,
            Number = 1,
            UserId = Guid.NewGuid(),
            Text = "Some text here",
            Rating = 3,
            CreatedAt = DateTime.Now,
            Status = AdvertStatus.Active
        };
    }

    public async Task<IEnumerable<Advert>> GetByUserIdAsync(Guid userId)
    {
        //return await _context.Adverts.Where(a => a.UserId == userId).ToListAsync();
        var collection = new List<Advert>();

        collection.Add(new Advert
        {
            Id = Guid.NewGuid(),
            Number = 1,
            UserId = userId,
            Text = "Some text here",
            Rating = 3,
            CreatedAt = DateTime.Now,
            Status = AdvertStatus.Active
        });
        collection.Add(new Advert
        {
            Id = Guid.NewGuid(),
            Number = 2,
            UserId = userId,
            Text = "Some other text here",
            Rating = 5,
            CreatedAt = DateTime.Now,
            Status = AdvertStatus.Active
        });

        return collection;
    }

    public async Task Create(Advert advert)
    {
        await _context.Adverts.AddAsync(advert);
        _context.SaveChanges();
    }

    public void CommitChanges()
    {
        _context.SaveChanges();
    }
}
