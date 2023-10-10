using Blog.Core.Domain.Entities;

namespace Blog.Core.Domain.Repositories
{
    public interface IUserRepository : IUnitOfWork
    {
        Task<List<User>> GetUsers();
        Task<User> GetUserId(Guid userId);
        Task<User> GetUserByEmail(string email);
        Task AddAsync(User user);
        Task<User> Update(Guid userId, User user);
        Task<User> Delete(Guid userId);
    }
}
