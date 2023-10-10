using Blog.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.External.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                  .ToTable("User");

            builder
               .HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("NVARCHAR")
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

            builder.Property(x => x.Bio)
                .HasMaxLength(2000)
                .IsRequired(false);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnType("VARCHAR")
                .HasMaxLength(160);

            builder.Property(x => x.PasswordHash)
                .IsRequired()
                .HasColumnType("VARCHAR")
                .HasMaxLength(255);

            builder.Property(x => x.Slug)
                .IsRequired(false)
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

            builder
                .HasIndex(x => x.Slug, "IX_User_Slug")
                .IsUnique();

            builder
               .HasIndex(x => x.Email, "IX_User_Email")
               .IsUnique();

            //n:n muitos para muitos
            builder
                .HasMany(x => x.Roles)
                .WithMany(x => x.Users)
                .UsingEntity<Dictionary<string, object>>
                (
                "UserRole", role => role.HasOne<Role>()
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .HasConstraintName("FK_UserRole_RoleId")
                    .OnDelete(DeleteBehavior.ClientCascade),
                user => user.HasOne<User>()
                    .WithMany()
                    .HasForeignKey("UserId")
                    .HasConstraintName("FK_UserRole_UserId")
                    .OnDelete(DeleteBehavior.ClientCascade));
        }
    }
}