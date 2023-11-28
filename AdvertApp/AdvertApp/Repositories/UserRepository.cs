using AdvertApp.EF;
using AdvertApp.EF.Entities;
using AdvertApp.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace AdvertApp.Repositories;

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
