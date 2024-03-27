using JWT.DataAccess.MSSQL.Entities;
using JWT.DataAccess.MSSQL.Repositories;
using JWT.WebAPI.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWT.WebAPI.Controllers;

[ApiController]
public class UsersController : ControllerBase
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUsersRepository _repository;
    private readonly IJwtProvider _provider;

    public UsersController(IUsersRepository repository, IJwtProvider provider, IHttpContextAccessor httpContextAccesso)
    {
        _httpContextAccessor = httpContextAccesso;
        _repository = repository;
        _provider = provider;
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login(string email, string password)
    {
        var user = await _repository.GetByEmail(email);
        if (user is null)
        {
            return BadRequest("User not Found");
        }

        if (user.Password != password)
        {
            return BadRequest("Wrong Credentials");
        }

        var token = _provider.GenerateToken(user);

        var responseCookies = _httpContextAccessor.HttpContext.Response.Cookies;
        responseCookies.Append("tasty", token);

        return Ok();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(string name, string password, string email, string role)
    {
        _repository.Add(new User
        {
            Id = Guid.NewGuid(),
            Name = name,
            Password = password,
            Email = email,
            Role = role
        });

        return Ok();
    }

    [HttpGet("home")]
    [Authorize]
    public async Task<IActionResult> Home()
    {
        return Ok();
    }
}
