using Blog.Core.Domain.Entities;

namespace Blog.Core.Domain.Repositories
{
    public interface ICategoryRepository : IUnitOfWork
    {
        Task AddAsync(Category category);
        Task<IEnumerable<Category?>> GetAllAsync();
        Task<Category?> GetByIdAsync(Guid categoryId);
        Task Update(Guid categoryId, Category category);
        Task<Category?> Delete(Guid categoryId);
    }
}
