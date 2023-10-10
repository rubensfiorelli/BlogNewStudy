using Blog.Core.Domain.Entities;

namespace Blog.Core.Domain.Repositories
{
    public interface ILoginRepository : IUnitOfWork
    {
        Task<User> GetUserEmailAsync(string email);
    }
}
