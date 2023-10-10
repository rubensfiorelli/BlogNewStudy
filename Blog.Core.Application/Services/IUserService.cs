using Blog.Core.Application.InputDTO;
using Blog.Core.Application.OutputDTO;

namespace Blog.Core.Application.Services
{
    public interface IUserService
    {
        Task<List<UserOutputDto>> GetUsers(CancellationToken cancellationToken);
        Task<UserOutputDto> GetUserId(Guid userId);
        Task<UserOutputDto> GetUserEmail(string email);
        Task<UserOutputDto> Add(CreateUserDto model);
        Task<UserOutputDto> Update(Guid userId, CreateUserDto model);
        Task<UserOutputDto> Delete(Guid userId);
    }
}
