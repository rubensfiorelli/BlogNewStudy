using Blog.Core.Application.OutputDTO;
using Blog.Core.Domain.Repositories;

namespace Blog.Core.Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;

        public LoginService(ILoginRepository loginRepository) => _loginRepository = loginRepository;

        public async Task<UserOutputDto> GetUserEmailAsync(string email)
        {

            var user = await _loginRepository.GetUserEmailAsync(email);
            if (user is null)
                return null;

            return (UserOutputDto)user;


        }
    }
}
