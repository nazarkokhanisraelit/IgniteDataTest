namespace SimonTest.Api.Application.Queries.Group;

using DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence;
using TradePlus.ResultData.Abstract.Generics;
using static TradePlus.ResultData.ResultFactory;

public record GroupsQuery : IRequest<IResult<List<GetGroupDto>>>
{
    public class Handler : IRequestHandler<GroupsQuery, IResult<List<GetGroupDto>>>
    {
        private readonly MyDbContext _context;

        public Handler(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IResult<List<GetGroupDto>>> Handle(
            GroupsQuery request,
            CancellationToken cancellationToken)
        {
            var groups = await _context.Groups
                .Select(
                    u => new GetGroupDto(
                        u.Id,
                        u.Name))
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return Success(groups);
        }
    }
}