using Blog.Core.Application.InputDTO;
using Blog.Core.Application.Notifications;
using Blog.Core.Application.OutputDTO;
using Blog.Core.Application.Services;
using Blog.External.Data.DataContext;
using Blog.External.Data.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api.Controllers
{
    [Route("v1/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService) => _categoryService = categoryService;

        [HttpGet]
        public async Task<IActionResult> GetAllCompiled(ApplicationDbContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                using (var db = context)

                    context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

                var existing = new List<CategoryDtoResult>();
                await foreach (var category in ListCategoryRepositoryHelpers.CompiledQueryCategory(context)
                    .WithCancellation(cancellationToken))
                {
                    existing.Add(category);
                }
                return Ok(new Notification<List<CategoryDtoResult>>(existing));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new Notification<CategoryDtoResult>("Erro interno no Servidor"));
            }

        }           

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetId(Guid categoryId, CancellationToken cancellationToken)
        {
            try
            {
                var existing = await _categoryService.GetById(categoryId, cancellationToken);

                if (existing is null)
                    return NotFound(new Notification<CategoryDtoResult>("Categoria não localizada"));

                return Ok(new Notification<CategoryDtoResult>(existing));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new Notification<CategoryDtoResult>("Erro interno no Servidor"));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(CategoryDtoCreate model)
        {
            try
            {
                var addCategory = await _categoryService.Add(model);

                if (addCategory != null)
                    return Created($"categories/{addCategory.Id}", new Notification<CategoryDtoResult>(addCategory));

                return StatusCode(500, new Notification<CategoryDtoCreate>("Erro ao adicionar categoria"));

            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new Notification<CategoryDtoCreate>("Erro interno no Servidor"));
            }
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> Put(Guid categoryId, CategoryDtoCreate model, CancellationToken cancellationToken)
        {
            try
            {
                var existing = await _categoryService.GetById(categoryId, cancellationToken);

                await _categoryService.Update(categoryId, model);

                return Ok(new Notification<CategoryDtoResult>(existing));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new Notification<CategoryDtoResult>("Erro interno no Servidor"));
            }
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> Delete(Guid categoryId)
        {
            try
            {
                _ = await _categoryService.Delete(categoryId);

                return NoContent();

            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new Notification<CategoryDtoResult>("Erro interno no Servidor"));
            }

        }
    }
}