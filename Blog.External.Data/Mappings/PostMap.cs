using Blog.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.External.Data.Mappings
{
    public class PostMap : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            // Tabela
            builder.ToTable("Post");

            // Chave Primária
            builder.HasKey(x => x.Id);

            builder
              .Property(c => c.CreatedAt)
              .HasDefaultValueSql("GETDATE()")
              .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore
              .Metadata.PropertySaveBehavior.Ignore);

            builder
              .Property(c => c.UpdatedAt)
              .HasDefaultValueSql("GETDATE()")
              .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore
              .Metadata.PropertySaveBehavior.Ignore);

            // Índices
            builder
                .HasIndex(x => x.Slug, "IX_Post_Slug")
                .IsUnique();

            // Relacionamentos
            builder
                .HasOne(x => x.Author)
                .WithMany(x => x.Posts)
                .HasConstraintName("FK_Post_Author")
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.Category)
                .WithMany(x => x.Posts)
                .HasConstraintName("FK_Post_Category")
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamentos
            builder
                .HasMany(x => x.Tags)
                .WithMany(x => x.Posts)
                .UsingEntity<Dictionary<string, object>>(
                    "PostTag",
                    post => post
                        .HasOne<Tag>()
                        .WithMany()
                        .HasForeignKey("PostId")
                        .HasConstraintName("FK_PostRole_PostId")
                        .OnDelete(DeleteBehavior.Cascade),
                    tag => tag
                        .HasOne<Post>()
                        .WithMany()
                        .HasForeignKey("TagId")
                        .HasConstraintName("FK_PostTag_TagId")
                        .OnDelete(DeleteBehavior.Cascade));
        }
    }
}