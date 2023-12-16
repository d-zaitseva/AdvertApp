using AdvertApp.Application;
using AdvertApp.Contracts.Models;
using AdvertApp.Domain.Entities;
using AdvertApp.ReadWrite;
using Microsoft.Data.SqlClient;
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

    public async Task<IEnumerable<AdvertModel>> GetAllSortededAsync(FilterRequest filterRequest, CancellationToken cancellationToken)
    {
        var pageSize = new SqlParameter("@PageSize", filterRequest.PageSize);
        var page = new SqlParameter("@PageNumber", filterRequest.Page);
        var sortBy = new SqlParameter("@SortBy", filterRequest.SortBy);
        var sortAsc = new SqlParameter("@SortAsc", filterRequest.SortAsc);

        var result = await _context.SortedAdverts
                        .FromSqlRaw("GetAdverts {0}, {1}, {2}, {3}", pageSize, page, sortBy, sortAsc)
                        .ToListAsync(cancellationToken);

        return result;
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
