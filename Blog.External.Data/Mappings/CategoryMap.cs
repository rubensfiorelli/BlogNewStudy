using Blog.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.External.Data.Mappings
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Tabela
            builder.ToTable("Category");

            // Chave Primária
            builder.HasKey(x => x.Id);

            // Propriedades
            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.Slug)
                .IsRequired()
                .HasColumnName("Slug")
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

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
                .HasIndex(x => x.Slug, "IX_Category_Slug")
                .IsUnique();
        }
    }
}