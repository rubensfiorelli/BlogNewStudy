using Blog.Core.Domain.Entities;
using System.Security.Claims;

namespace Blog.External.CrossCutting.Extensions
{
    public static class RoleClaimsExtensions
    {
        public static IEnumerable<Claim> GetClaims(this User user)
        {
            var result = new List<Claim>
            {
                new(ClaimTypes.Name, user.Email)
                //new(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            result.AddRange
                (user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Slug)));

            return result;
        }
    }
}
