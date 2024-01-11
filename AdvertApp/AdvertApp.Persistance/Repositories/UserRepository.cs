using AdvertApp.Domain.Entities;
using AdvertApp.ReadWrite;
using Microsoft.EntityFrameworkCore;

namespace AdvertApp.Persistance.Repositories;

public class UserRepository : IUserRepository
{
    private IAdvertContext _context;

    public UserRepository(IAdvertContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users.FirstOrDefaultAsync(a => a.Id == id);
    }
}
