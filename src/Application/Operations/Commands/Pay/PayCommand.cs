using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NfcPos.Application.Common.Exceptions;
using NfcPos.Application.Common.Interfaces;
using NfcPos.Application.Operations.Commands.Common;
using NfcPos.Domain.Entities;

namespace NfcPos.Application.Operations.Commands.Pay;
public class PayCommand : IRequest<BalanceVm>
{
    public string nfcId { get; set; }
    public decimal TotalPayAmount { get; set; } = 0m;

    // TODO: Add a list of items for logging
}

public class PayCommandHandler : IRequestHandler<PayCommand, BalanceVm>
{

    private readonly IApplicationDbContext _context;
    private readonly ILogger<PayCommandHandler> _logger;


    public PayCommandHandler(IApplicationDbContext context, ILogger<PayCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<BalanceVm> Handle(PayCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
          .Where(x => x.NfcId == request.nfcId)
          .FirstOrDefaultAsync();

        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.nfcId);


        }

        decimal oldBalance = user.Balance;

        user.Balance -= request.TotalPayAmount;

        if(user.Balance < 0)
        {
            throw new InsufficientFundsException(request.nfcId, request.TotalPayAmount, user.Balance);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return new BalanceVm
        {
            OldBalance = oldBalance,
            NewBalance = user.Balance
        };

    }

    
}
