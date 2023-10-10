using Blog.Core.Domain.Primitives;

namespace Blog.Core.Domain.Entities
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public string Slug { get; set; }

        public List<Post> Posts { get; set; }
    }
}