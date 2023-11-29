using AdvertApp.EF;
using AdvertApp.EF.Entities;
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

    public async Task<IEnumerable<Advert>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Adverts
            .Include(a => a.Image)
            .ToListAsync(cancellationToken);
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

        if (advert.Image != null)
        {
            await _context.Images.AddAsync(advert.Image);
        }
        
        _context.SaveChanges();
    }

    public void Update(Advert advert)
    {
        _context.Adverts.Update(advert);
        // TO DO: case when Image was deleted from advert
        if (advert.Image != null)
        {
            _context.Images.Update(advert.Image);
        }

        _context.SaveChanges();
    }

    public async Task DeleteAsync(Guid id)
    {
        var advert = await _context.Adverts.FirstOrDefaultAsync(a => a.Id == id).ConfigureAwait(false);
        if (advert != null)
        {
            _context.Adverts.Remove(advert);
        }
    }

    public void CommitChanges()
    {
        _context.SaveChanges();
    }
}
