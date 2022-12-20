using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NfcPos.Application.Common.Exceptions;
using NfcPos.Application.Common.Interfaces;
using NfcPos.Application.Operations.Commands.Common;
using NfcPos.Domain.Entities;

namespace NfcPos.Application.Operations.Commands.WithdrawAll;
public class WithdrawAllCommand : IRequest<BalanceVm>
{
    public string nfcId { get; set; }

}

public class WithdrawAllCommandHandler : IRequestHandler<WithdrawAllCommand, BalanceVm>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<WithdrawAllCommandHandler> _logger;

    public WithdrawAllCommandHandler(IApplicationDbContext context, ILogger<WithdrawAllCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<BalanceVm> Handle(WithdrawAllCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
          .Where(x => x.NfcId == request.nfcId)
          .FirstOrDefaultAsync();

        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.nfcId);


        }

        decimal oldBalance = user.Balance;

        user.Balance -= user.Balance;

        if (user.Balance < 0)
        {
            throw new InsufficientFundsException(request.nfcId, oldBalance, user.Balance);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return new BalanceVm
        {
            OldBalance = oldBalance,
            NewBalance = user.Balance
        };
    }
}
