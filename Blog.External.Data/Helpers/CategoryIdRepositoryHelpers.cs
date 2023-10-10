using Blog.Core.Domain.Entities;
using Blog.External.Data.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Blog.External.Data.Helpers
{
    internal static class CategoryIdRepositoryHelpers
    {

        public static readonly Func<ApplicationDbContext, Guid, Task<Category>>
            CompiledGetByIdAsync = EF.CompileAsyncQuery
                ((ApplicationDbContext context, Guid categoryId)
                    => context.Categories.First(l => l.Id == categoryId));
    }
}