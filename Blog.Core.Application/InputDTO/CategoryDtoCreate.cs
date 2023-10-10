using Blog.Core.Domain.Entities;

namespace Blog.Core.Application.InputDTO
{
    public record CategoryDtoCreate
    {
        public CategoryDtoCreate(string name, string slug)
        {
            Name = name;
            Slug = slug;
        }

        public string Name { get; set; }
        public string Slug { get; set; }

        public static implicit operator Category(CategoryDtoCreate model)
            => new(model.Name,
                   model.Slug);

    }
}
