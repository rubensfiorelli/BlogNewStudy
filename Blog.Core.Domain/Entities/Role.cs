using Blog.Core.Domain.Primitives;

namespace Blog.Core.Domain.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public string Slug { get; set; }

        public List<User> Users { get; set; }
    }
}