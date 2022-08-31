namespace SimonTest.Api.Controllers;

using Abstract;
using Application.Commands.User;
using Application.Extensions;
using Application.Queries.User;
using Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("user")]
public class UserController : ApiController
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(UserQuery.QueryResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> Test([FromQuery] UserQuery request, CancellationToken cancellationToken)
        => (await _mediator.Send(request, cancellationToken)).ToActionResult();
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Test(CreateUserCommand request, CancellationToken cancellationToken)
        => (await _mediator.Send(request, cancellationToken)).ToActionResult();
    
    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Test(UpdateUserGroupsCommand request, CancellationToken cancellationToken)
        => (await _mediator.Send(request, cancellationToken)).ToActionResult();
}