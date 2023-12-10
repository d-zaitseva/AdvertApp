using AdvertApp.Application;
using AdvertApp.Domain.Entities;
using AdvertApp.ReadWrite;
using Microsoft.EntityFrameworkCore;
namespace AdvertApp.Persistance.Repositories;

public class AdvertRepository : IAdvertReadRepository, IAdvertWriteRepository
{
    private IAdvertContext _context;

    public AdvertRepository(IAdvertContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Advert>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Adverts.ToListAsync(cancellationToken);
    }

    public async  Task<Advert?> GetByIdAsync(Guid id)
    {
        return await _context.Adverts.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Advert>> GetByUserIdAsync(Guid userId)
    {
        return await _context.Adverts.Where(a => a.UserId == userId).ToListAsync();
    }

    public async Task CreateAsync(Advert advert)
    {
        await _context.Adverts.AddAsync(advert);
        
        _context.SaveChanges();
    }

    public void Update(Advert advert)
    {
        _context.Adverts.Update(advert);

        _context.SaveChanges();
    }

    public async Task DeleteAsync(Guid id)
    {
        var advert = await _context.Adverts.FirstOrDefaultAsync(a => a.Id == id).ConfigureAwait(false);
        if (advert != null)
        {
            _context.Adverts.Remove(advert);
            _context.SaveChanges();
        }
    }

    public void CommitChanges()
    {
        _context.SaveChanges();
    }
}
