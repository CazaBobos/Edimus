using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Core.Features.CreateUser;
using Users.Core.Features.GetUsers;
using Users.Core.Model;
using Users.Input;

namespace Users.Controllers;

[Route("api/users")]
[ApiController]
//[Authorize]
public class UsersController : ControllerBase
{
    public readonly IMediator _mediator;
    public UsersController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Finds all users that match a given criteria
    /// </summary>
    [HttpGet]
    public async Task<List<UserModel>> FindMany([FromQuery] GetUsersInput input, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetUsersRequest
        {
            Limit = input.Limit,
            Page = input.Page,
            Username = input.Username,
            Email = input.Email,
        }, cancellationToken);

        return response.Users;
    }

    /// <summary>
    /// Creates a new user
    /// </summary>
    [HttpPost]
    public async Task<UserModel> Create([FromBody] CreateUserInput input, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new CreateUserRequest
        {
            Username = input.Username,
            Email = input.Email,
            Password = input.Password,
            Companies = input.Companies,
        }, cancellationToken);

        return response.User;
    }
}
