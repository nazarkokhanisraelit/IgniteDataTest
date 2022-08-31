namespace Simon_Test.Controllers;

using Abstract;
using Application.Commands.Group;
using Application.Commands.User;
using Application.DTOs;
using Application.Queries.Group;
using Application.Queries.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Simon_Test.Application.Extensions;

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

[Route("user-group")]
public class GroupController : ApiController
{
    private readonly IMediator _mediator;

    public GroupController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<GetGroupDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Test([FromQuery] GroupsQuery request, CancellationToken cancellationToken)
        => (await _mediator.Send(request, cancellationToken)).ToActionResult();
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Test(CreateGroupCommand request, CancellationToken cancellationToken)
        => (await _mediator.Send(request, cancellationToken)).ToActionResult();
    
    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Test(UpdateGroupCommand request, CancellationToken cancellationToken)
        => (await _mediator.Send(request, cancellationToken)).ToActionResult();
}