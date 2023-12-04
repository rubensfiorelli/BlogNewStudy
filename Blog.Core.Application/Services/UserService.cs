using Blog.Core.Application.InputDTO;
using Blog.Core.Application.OutputDTO;
using Blog.Core.Domain.Entities;
using Blog.Core.Domain.Repositories;
using SecureIdentity.Password;

namespace Blog.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserOutputDto> Add(CreateUserDto model)
        {

            var password = model.Password;

            model.Password = PasswordHasher.Hash(password);

            var addEntity = (User)model;

            await _repository.AddAsync(addEntity);

            await _repository.Commit();

            return (UserOutputDto)addEntity;
        }

        public async Task<UserOutputDto> Delete(Guid userId)
        {
            var existing = await _repository.Delete(userId);
            await _repository.Commit();

            return existing;
        }

        public async Task<UserOutputDto> GetUserEmail(string email)
        {
            var existing = await _repository.GetUserByEmail(email);

            if (existing is not null)
                return (UserOutputDto)existing;

            return null;
        }

        public async Task<UserOutputDto> GetUserId(Guid userId)
        {
            var existing = await _repository.GetUserId(userId);

            if (existing is null)
                return null;

            return (UserOutputDto)existing;
        }

        public async Task<List<UserOutputDto>> GetUsers(CancellationToken cancellationToken)
        {
            var listUsers = await _repository.GetUsers();

            return listUsers
                .Select(x => new UserOutputDto(x.Name, x.Email, x.PasswordHash))
                .ToList();
        }

        public async Task<UserOutputDto> Update(Guid userId, CreateUserDto model)
        {
            var existing = await _repository.GetUserId(userId);

            if (existing is null)
                return null;

            existing.SetupUser(model.Name, model.Email, model.Password, model.Bio);

            await _repository.Update(userId, existing);

            return (UserOutputDto)existing;
        }
    }
}
