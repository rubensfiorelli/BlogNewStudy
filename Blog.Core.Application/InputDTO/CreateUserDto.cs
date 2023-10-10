using Blog.Core.Domain.Entities;

namespace Blog.Core.Application.InputDTO
{
    public record CreateUserDto
    {
        public CreateUserDto(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
            Slug = email;
            Bio = "Bio";
           
        }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Slug { get; set; }
        public string Bio { get; set; }


        public static implicit operator User(CreateUserDto dto)
            => new User(dto.Name, dto.Email, dto.Password, dto.Slug, dto.Bio);
    }
}
