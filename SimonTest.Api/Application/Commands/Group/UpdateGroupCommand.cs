namespace Simon_Test.Application.Commands.Group;

using MediatR;
using Microsoft.EntityFrameworkCore;
using Simon_Test.Application.Constants;
using SimonTest.Infrastructure.Persistence;
using TradePlus.ResultData;
using TradePlus.ResultData.Abstract;

public record UpdateGroupCommand(Guid Id, string Name) : IRequest<IResult>
{
    public class Handler : IRequestHandler<UpdateGroupCommand, IResult>
    {
        private readonly MyDbContext _context;

        public Handler(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IResult> Handle(
            UpdateGroupCommand request,
            CancellationToken cancellationToken)
        {
            var group = await _context.Groups
                .Where(g => g.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (group is null)
                return ResultFactory.Failure(ValidationMessage.GroupNotFound);
            
            group.Name = request.Name;

            _context.Update(group);

            await _context.SaveChangesAsync(cancellationToken);

            return ResultFactory.Success();
        }
    }
}