using Blog.Core.Application.InputDTO;
using Blog.Core.Application.OutputDTO;
using Blog.Core.Domain.Entities;
using Blog.Core.Domain.Repositories;

namespace Blog.Core.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository) => _categoryRepository = categoryRepository;
        public async Task<CategoryDtoResult> Add(CategoryDtoCreate model)
        {
            var addEntity = (Category)model;

            await _categoryRepository.AddAsync(addEntity);
            await _categoryRepository.Commit();

            return (CategoryDtoResult)addEntity;

        }
        public async Task<CategoryDtoResult> Delete(Guid categoryId)
        {
            var existing = await _categoryRepository.Delete(categoryId);
            await _categoryRepository.Commit();

            return existing;
        }

        public async Task<List<CategoryDtoResult>> GetAll(CancellationToken cancellationToken)
        {
            var listtCategories = await _categoryRepository.GetAllAsync();



            return listtCategories
                .Select(x => new CategoryDtoResult(x.Id, x.Name, x.Slug, x.CreatedAt))
                .ToList();
        }

        public async Task<CategoryDtoResult> GetById(Guid categoryId, CancellationToken cancellationToken)
        {
            var existing = await _categoryRepository.GetByIdAsync(categoryId);

            if (existing is null)
                return null;

            return (CategoryDtoResult)existing;
        }

        public async Task<CategoryDtoResult> Update(Guid categoryId, CategoryDtoCreate model)
        {
            var existing = await _categoryRepository.GetByIdAsync(categoryId);

            if (existing is null)
                return null;

            existing.SetupCategory(model.Name, model.Slug);

            await _categoryRepository.Update(categoryId, existing);
            await _categoryRepository.Commit();

            return (CategoryDtoResult)existing;

        }

    }
}
