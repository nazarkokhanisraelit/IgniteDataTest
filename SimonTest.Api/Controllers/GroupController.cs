namespace SimonTest.Api.Controllers;

using Abstract;
using Application.Commands.Group;
using Application.DTOs;
using Application.Queries.Group;
using Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> Test(CancellationToken cancellationToken)
        => (await _mediator.Send(new GroupsQuery(), cancellationToken)).ToActionResult();
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Test(CreateGroupCommand request, CancellationToken cancellationToken)
        => (await _mediator.Send(request, cancellationToken)).ToActionResult();
    
    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Test(UpdateGroupCommand request, CancellationToken cancellationToken)
        => (await _mediator.Send(request, cancellationToken)).ToActionResult();
}