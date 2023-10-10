using Blog.Core.Application.InputDTO;
using Blog.Core.Application.Notifications;
using Blog.Core.Application.OutputDTO;
using Blog.Core.Application.Services;
using Blog.Core.Domain.Entities;
using Blog.External.CrossCutting.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace Blog.Api.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly IUserService _userService;

        public AccountController(TokenService tokenService, IUserService userService = null)
        {
            _tokenService = tokenService;
            _userService = userService;
        }

        [HttpGet("accounts/{userId}")]
        public async Task<IActionResult> GetById(Guid userId)
        {
            try
            {
                var existing = await _userService.GetUserId(userId);

                if (existing is null)
                    return NotFound(new Notification<UserOutputDto>("User não localizado"));

                return Ok(existing);

            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new Notification<CreateUserDto>("Falha interna no servidor"));
            }

        }

        [HttpPost("v1/accounts/")]
        public async Task<IActionResult> Post(CreateUserDto model)
        {

            await _userService.Add(model);

            return Ok(new Notification<dynamic>(new
            {
                model.Email,
                model.Password

            }));

        }

        [HttpPost("v1/accounts/login")]
        public async Task<IActionResult> Login(CreateLoginDto model)
        {
            if (!ModelState.IsValid)
                BadRequest(new Notification<string>("Não foi possivel verificar o Login"));

            var existing = await _userService.GetUserEmail(model.Email);

            if (existing is null)
                return StatusCode(401, new Notification<string>("Usuário ou senha inválidos"));

            if (!PasswordHasher.Verify(existing.PasswordHash, model.Password))
                return StatusCode(401, new Notification<string>("Usuário ou senha inválidos"));

            try
            {
                _ = (User)existing;

                var token = _tokenService.GenerateToken(existing);

                return Ok(new Notification<string>(token, null));

            }
            catch
            {

                return StatusCode(500, new Notification<string>("Erro interno no servidor"));
            }

        }

        //[HttpGet]
        //public IActionResult GetUser() => Ok(User.Identity.Name);
        // Metodo para testar usuario logando
    }
}
