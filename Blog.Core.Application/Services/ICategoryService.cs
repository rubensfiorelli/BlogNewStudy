using Blog.Core.Application.InputDTO;
using Blog.Core.Application.OutputDTO;

namespace Blog.Core.Application.Services
{
    public interface ICategoryService
    {
        Task<CategoryDtoResult> Add(CategoryDtoCreate model);
        Task<List<CategoryDtoResult>> GetAll(CancellationToken cancellationToken);
        Task<CategoryDtoResult> GetById(Guid categoryId, CancellationToken cancellationToken);
        Task<CategoryDtoResult> Update(Guid categoryId, CategoryDtoCreate model);
        Task<CategoryDtoResult> Delete(Guid categoryId);
    }
}
