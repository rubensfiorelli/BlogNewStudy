using Blog.Core.Application.OutputDTO;

namespace Blog.Core.Application.Services
{
    public interface ILoginService
    {
        Task<UserOutputDto> GetUserEmailAsync(string email);

    }
}
