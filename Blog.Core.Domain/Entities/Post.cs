using Blog.Core.Domain.Primitives;

namespace Blog.Core.Domain.Entities
{
    public sealed class Post : BaseEntity
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Body { get; set; }
        public string Slug { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public Category Category { get; set; }
        public User Author { get; set; }

        public List<Tag> Tags { get; set; }
    }
}