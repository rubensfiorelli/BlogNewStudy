using Blog.Core.Domain.Entities;

namespace Blog.Core.Application.OutputDTO
{
    public record CategoryDtoResult
    {
        public CategoryDtoResult(Guid id, string name, string slug, DateTimeOffset createdAt)
        {
            Id = id;
            Name = name;
            Slug = slug;
            CreatedAt = createdAt;
        }

        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Slug { get; init; }
        public DateTimeOffset CreatedAt { get; init; }

        public static implicit operator CategoryDtoResult(Category entity)
            => new(entity.Id,
                   entity.Name,
                   entity.Slug,
                   entity.CreatedAt);
    }
}
