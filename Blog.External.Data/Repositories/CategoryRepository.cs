using Blog.Core.Domain.Entities;
using Blog.Core.Domain.Repositories;
using Blog.External.Data.DataContext;
using Blog.External.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Blog.External.Data.Repositories
{
    public sealed class CategoryRepository : ICategoryRepository
    {

        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) => _context = context;

        public async Task AddAsync(Category category)
        {
            try
            {
                var addCategory = await _context.Categories.AddAsync(category);

            }
            catch (DbException)
            {

                throw new Exception("Erro - 1001EDb - Falha ao acessar banco de dados");

            }
        }

        public async Task<Category?> Delete(Guid categoryId)
        {
            try
            {
                var existing = await _context.Categories
                    .AsTracking()
                    .SingleOrDefaultAsync(cat => cat.Id.Equals(categoryId));

                if (existing is null)
                    return null;

                existing.Delete();

                return existing;

            }
            catch (DbException)
            {

                throw new Exception("Erro - 1001EDb - Falha ao acessar banco de dados");

            }
        }

        public async Task<Category?> GetByIdAsync(Guid categoryId)
        {
            try
            {
                var getId = await Task.Run(() => 
                    CategoryIdRepositoryHelpers
                        .CompiledGetByIdAsync(_context, categoryId))
                            .ConfigureAwait(false);

                if (getId is not null)
                    return getId;

                return null;
            }
            catch (Exception)
            {
                throw new Exception("Erro - 1001EDb - Falha ao acessar banco de dados");
            }
        }

        public async Task Update(Guid categoryId, Category category)
        {
            try
            {
                var existing = await _context.Categories
                    .AsTracking()
                    .SingleOrDefaultAsync(cat => cat.Id.Equals(category.Id));

                if (existing is not null)
                {
                    category.UpdatedAt = DateTime.UtcNow;
                    category.CreatedAt = existing.CreatedAt;

                    existing.SetupCategory(category.Name, category.Slug);
                    _context.Entry(existing).CurrentValues.SetValues(existing);
                }

            }
            catch (DbException)
            {
                throw new Exception("Erro - 1001E-Db - Falha ao acessar banco de dados");
            }
        }
        public async Task<bool> Commit()
        {
            return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        public Task Rollback()
        {
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Category?>> GetAllAsync()
        {

            try
            {
                var listCategories = _context.Categories
                    .Include(p => p.Posts)
                    .Where(x => !x.IsDeleted)
                    .Take(20)
                    .ToListAsync();

                if (listCategories is null)
                    return Enumerable.Empty<Category?>();

                return await Task.Run(() => listCategories);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }      

    }
}
