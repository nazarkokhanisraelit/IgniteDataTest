namespace Simon_Test.Application.Commands.User;

using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Simon_Test.Application.Constants;
using SimonTest.Domain.Entities;
using SimonTest.Infrastructure.Persistence;
using TradePlus.ResultData.Abstract;
using static TradePlus.ResultData.ResultFactory;

public record CreateUserCommand(
    string? FirstName,
    string? LastName,
    [EmailAddress] string Email,
    ICollection<Guid> Groups) : IRequest<IResult>
{
    public class Handler : IRequestHandler<CreateUserCommand, IResult>
    {
        private readonly MyDbContext _context;

        public Handler(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IResult> Handle(
            CreateUserCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Where(s => s.Email == request.Email)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            if (user is not null)
                return Failure(ValidationMessage.UserAlreadyExist);

            if (request.Groups.Count is not 0)
            {
                var groups = await _context.Groups
                    .ToListAsync(cancellationToken);

                var allGroupsExist = request.Groups.All(g => groups.Any(dbg => dbg.Id == g));

                if (!allGroupsExist)
                    return Failure(ValidationMessage.OneOfUserGroupsNotFound);
            }

            user = new User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserGroups = request.Groups
                    .Select(g => new UserGroups
                    {
                        GroupId = g
                    })
                    .ToList()
            };

            _context.Add(user);

            await _context.SaveChangesAsync(cancellationToken);

            return Success();
        }
    }
}