using JWT.DataAccess.MSSQL.Entities;
using JWT.DataAccess.MSSQL.Repositories;
using JWT.WebAPI.Providers;
using Microsoft.AspNetCore.Mvc;

namespace JWT.WebAPI.Controllers;

[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUsersRepository _repository;
    private readonly IJwtProvider _provider;

    public UsersController(IUsersRepository repository, IJwtProvider provider)
    {
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

        return Ok(token);

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
}
