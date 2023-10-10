using Blog.Core.Domain.Entities;
using Blog.Core.Domain.Repositories;
using Blog.External.Data.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Blog.External.Data.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly ApplicationDbContext _context;

        public LoginRepository(ApplicationDbContext context) => _context = context;

        public async Task<bool> Commit()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<User> GetUserEmailAsync(string email)
        {
            var user = await _context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Email.Equals(email));

            if (user is not null)
                return user;

            return null;
        }

        public Task Rollback()
        {
            return Task.CompletedTask;
        }
    }
}
