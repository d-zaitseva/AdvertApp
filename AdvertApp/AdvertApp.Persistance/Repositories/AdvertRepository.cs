using AdvertApp.Contracts.Enums;
using AdvertApp.Contracts.Models;
using AdvertApp.Domain.Entities;
using AdvertApp.ReadWrite;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

    public async Task<IEnumerable<Advert>> GetAllSortededAsync(FilterRequest filterRequest, CancellationToken cancellationToken)
    {
        IQueryable<Advert> advertQuery = _context.Adverts.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filterRequest.FullTextSearch))
        {
            advertQuery = advertQuery.Where(a => 
                a.AuthorName.Contains(filterRequest.FullTextSearch) || 
                a.Text.Contains(filterRequest.FullTextSearch));
        }

        if (filterRequest.Status is not null)
        {
            advertQuery = advertQuery.Where(a =>
                a.Status == filterRequest.Status);
        }

        if (filterRequest.CreatedAt is not null)
        {
            advertQuery = advertQuery.Where(a =>
                a.Audit.CreatedAt.Date == filterRequest.CreatedAt);
        }

        if (filterRequest.UpdatedAt is not null)
        {
            advertQuery = advertQuery.Where(a =>
                a.Audit.UpdatedAt.Date == filterRequest.UpdatedAt);
        }

        if (filterRequest.MinRating is not null)
        {
            advertQuery = advertQuery.Where(a =>
                a.Rating >= filterRequest.MinRating);
        }

        if (filterRequest.MaxRating is not null)
        {
            advertQuery = advertQuery.Where(a =>
                a.Rating <= filterRequest.MaxRating);
        }

        Expression<Func<Advert, object>> sortBy = filterRequest.SortBy switch
        {
            SortByFieldsForAdvert.Number => a => a.Number,
            SortByFieldsForAdvert.AuthorName => a => a.AuthorName,
            SortByFieldsForAdvert.Rating => a => a.Rating,
            SortByFieldsForAdvert.CreatedAtDate => a => a.Audit.CreatedAt,
            SortByFieldsForAdvert.UpdatedAtDate => a => a.Audit.UpdatedAt,
            SortByFieldsForAdvert.ExpiredAtDate => a => a.ExpiredAt,
            SortByFieldsForAdvert.Status => a => a.Status,
            _ => a => a.Number
        };

        if (filterRequest.SortAsc)
        {
            advertQuery = advertQuery.OrderByDescending(sortBy);
        }
        else
        {
            advertQuery = advertQuery.OrderBy(sortBy);
        }

        var result = await advertQuery
            .Skip((filterRequest.Page - 1) * filterRequest.PageSize)
            .Take(filterRequest.PageSize)
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
