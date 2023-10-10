namespace Blog.Core.Domain.Primitives
{
    public abstract class BaseEntity
    {
        private DateTimeOffset createdAt;
        private DateTimeOffset updatedAt;

        public virtual Guid Id { get; init; }
        public DateTimeOffset CreatedAt { get => createdAt; set => createdAt = value; }
        public DateTimeOffset UpdatedAt { get => updatedAt; set => updatedAt = value; }
    }
}
