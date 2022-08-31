namespace SimonTest.Api.Application.Commands.Group;

using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Domain.Entities;
using Infrastructure.Persistence;
using TradePlus.ResultData.Abstract;
using static TradePlus.ResultData.ResultFactory;

public record CreateGroupCommand(string Name) : IRequest<IResult>
{
    public class Handler : IRequestHandler<CreateGroupCommand, IResult>
    {
        private readonly MyDbContext _context;

        public Handler(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IResult> Handle(
            CreateGroupCommand request,
            CancellationToken cancellationToken)
        {
            var group = new Group
            {
                Name = request.Name
            };

            _context.Add(group);

            await _context.SaveChangesAsync(cancellationToken);

            return Success();
        }
    }
}