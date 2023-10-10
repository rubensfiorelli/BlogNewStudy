using Blog.Core.Domain.Primitives;

namespace Blog.Core.Domain.Entities
{
    public class User : BaseEntity
    {
        public User()
        {
            
        }
        public User(string name, string email)
        {
            Name = name;
            Email = email;
            Slug = email.Replace("@", "-").Replace(".", "-");
        }

        public User(string name, string email, string passwordHash)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
        }

        public User(string name,
                    string email,
                    string passwordHash,
                    string slug,
                    string bio)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Slug = slug;
            Bio = bio;
            IsDeleted = false;

            Posts = new List<Post>();
            Roles = new List<Role>();
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string Slug { get;  private set; }
        public string? Bio { get; private set; }
        public bool IsDeleted { get; private set; }

        public List<Post> Posts { get; set; }
        public List<Role> Roles { get; set; }

       public void Delete()
        {
            IsDeleted = true;
        }
        
        public void SetupUser(string name, string email, string passwordHash, string bio)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Slug = email;
            Bio = bio;
        }       

    }
}