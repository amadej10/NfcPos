using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using NfcPos.Application.Common.Interfaces;
using NfcPos.Domain.Entities;

namespace NfcPos.Application.Users.Commands.CreateUser;
public class CreateUserCommand : IRequest<int>
{
    public string Name { get; set; } = "";
    public string Surname { get; set; } = "";
    public string? NfcId { get; set; }
    public string? Description { get; set; }
    public decimal Balance { get; set; } = 0;
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {

        var entity = new User()
        {
            NfcId = request.NfcId,
            Name = request.Name,
            Balance = request.Balance,
            Surname = request.Surname,
            Description = request.Description
        };

        await _context.Users.AddAsync(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}