using Blog.Core.Domain.Entities;
using Blog.Core.Domain.Repositories;
using Blog.External.Data.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Blog.External.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context) => _context = context;
       

        public async Task AddAsync(User user)
        {
            try
            {
                await _context.AddAsync(user);

            }
            catch (DbUpdateException)
            {

                throw new Exception("Erro ao inserir Usuário");
            }
        }

        public async Task<bool> Commit() => await _context.SaveChangesAsync() > 0;

        public async Task<User> Delete(Guid userId)
        {
            try
            {
                var existing = await _context.Users
                     .AsTracking()
                     .FirstOrDefaultAsync(x => x.Id.Equals(userId));

                if (existing is null)
                    return null;

                existing.Delete();

                return existing;
            }
            catch (DbUpdateException)
            {

                throw new Exception("Erro ao remover Usuário");
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var existing = await _context.Users
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Email.Equals(email));

            if (existing is null)
                return null;

            return existing;
        }

        public async Task<User> GetUserId(Guid userId)
        {
            var existing = await _context.Users
              .AsTracking()
              .SingleOrDefaultAsync(x => x.Id.Equals(userId));

            if (existing is null)
                return null;

            return existing;
        }

        public async Task<List<User>> GetUsers()
        {
            try
            {
                var listUsers = await _context.Users
               .Where(x => !x.IsDeleted)
               .ToListAsync();

                return listUsers;
            }
            catch (DbUpdateException)
            {
                throw new Exception("Erro desconhecido ao acessar banco de dados");
            }
        }

        public Task Rollback()
        {
            return Task.CompletedTask;
        }

        public async Task<User> Update(Guid userId, User user)
        {
            try
            {
                var existing = await _context.Users
                    .AsTracking()
                    .SingleOrDefaultAsync(x => x.Id.Equals(user.Id));

                if (existing is null)
                    return null;

                user.UpdatedAt = DateTimeOffset.UtcNow;
                user.CreatedAt = existing.CreatedAt;

                existing.SetupUser(user.Name, user.Email, user.PasswordHash, user.Bio);

                _context.Update(existing);

                return existing;
            }
            catch (DbUpdateException)
            {
                throw new Exception("Erro ao atualizar User");
            }
        }
    }
}
