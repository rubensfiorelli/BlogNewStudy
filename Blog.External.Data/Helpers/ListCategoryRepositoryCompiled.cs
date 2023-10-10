using Blog.Core.Domain.Entities;
using Blog.External.Data.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Blog.External.Data.Helpers
{
    public static class ListCategoryRepositoryHelpers
    {
        public static readonly Func<ApplicationDbContext, IAsyncEnumerable<Category?>>
            CompiledQueryCategory = EF.CompileAsyncQuery((ApplicationDbContext context) => context.Categories);
    }
}