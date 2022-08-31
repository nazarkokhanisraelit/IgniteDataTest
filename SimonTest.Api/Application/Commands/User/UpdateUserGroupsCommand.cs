namespace Simon_Test.Application.Commands.User;

using MediatR;
using Microsoft.EntityFrameworkCore;
using Simon_Test.Application.Constants;
using SimonTest.Domain.Entities;
using SimonTest.Infrastructure.Persistence;
using TradePlus.ResultData.Abstract;
using static TradePlus.ResultData.ResultFactory;

public record UpdateUserGroupsCommand(
    Guid Id,
    ICollection<Guid> Groups) : IRequest<IResult>
{
    public class Handler : IRequestHandler<UpdateUserGroupsCommand, IResult>
    {
        private readonly MyDbContext _context;

        public Handler(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IResult> Handle(
            UpdateUserGroupsCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Where(u => u.Id == request.Id)
                .Include(u => u.UserGroups)
                .FirstOrDefaultAsync(cancellationToken);

            if (user is null)
                return Failure(ValidationMessage.UserNotFound);

            if (request.Groups.Count is not 0)
            {
                var testGroups = await _context.Groups
                    .ToListAsync(cancellationToken);

                var allGroupsExist = request.Groups.All(g => testGroups.Any(dbg => dbg.Id == g));

                if (!allGroupsExist)
                    return Failure(ValidationMessage.OneOfUserGroupsNotFound);
            }

            var groups = await _context.Groups
                .Where(g => request.Groups.Contains(g.Id))
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            user.UserGroups = groups
                .Select(g => new UserGroups
                {
                    GroupId = g.Id
                    
                })
                .ToList();

            _context.Update(user);

            await _context.SaveChangesAsync(cancellationToken);

            return Success();
        }
    }
}