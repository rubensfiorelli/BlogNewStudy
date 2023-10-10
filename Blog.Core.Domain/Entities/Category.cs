using Blog.Core.Domain.Primitives;

namespace Blog.Core.Domain.Entities
{
    public sealed class Category : BaseEntity
    {
        public Category(string name, string slug) : base()
        {
            Name = name;
            Slug = slug.ToLower();
            IsDeleted = false;

            Posts = new List<Post>();
        }

        public string Name { get; set; }
        public string Slug { get; set; }
        public bool IsDeleted { get; private set; }

        public List<Post> Posts { get; set; }

        public void Delete()
        {
            IsDeleted = true;
        }

        public void SetupCategory(string name, string slug)
        {
            Name = name;
            Slug = slug;

        }
    }
}