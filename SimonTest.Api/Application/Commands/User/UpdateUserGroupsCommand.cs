namespace SimonTest.Api.Application.Commands.User;

using Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.Persistence;
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

            var groups = await _context.Groups
                .Where(g => request.Groups.Contains(g.Id))
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var allGroupsExist = request.Groups
                .All(rg => groups.Any(g => g.Id == rg));

            if (!allGroupsExist)
                return Failure(ValidationMessage.OneOfUserGroupsNotFound);
            
            user.UserGroups = groups
                .Select(
                    g => new UserGroups
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