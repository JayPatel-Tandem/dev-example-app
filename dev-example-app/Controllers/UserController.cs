using dev_example_app.Commands;
using dev_example_app.Models;
using dev_example_app.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace dev_example_app.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{

    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [EnableCors("AllowAll")]
    [Authorize]
    [HttpGet]
    public async Task<IEnumerable<UserModel>> GetAllUsers()
    {
        return await _mediator.Send(new GetAllUsersQuery());
    }

    [HttpPost]
    public async Task<UserModel> CreateUser([FromBody] UserModel user)
    {

        return await _mediator.Send(new CreateUserCommand { User = user });
    }
}

