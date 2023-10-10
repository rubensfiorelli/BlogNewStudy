using Blog.Core.Domain.Entities;

namespace Blog.Core.Application.OutputDTO
{
    public record UserOutputDto
    {
        public UserOutputDto(string name, string email, string passwordHash)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
        }

        public string Name { get; init; }
        public string Email { get; init; }
        public string PasswordHash { get; init; }

        public static implicit operator UserOutputDto(User entity)
            => new UserOutputDto(entity.Name, entity.Email, entity.PasswordHash);

        public static implicit operator User(UserOutputDto dto)
            => new User(dto.Name, dto.Email, dto.PasswordHash);
    }
}
