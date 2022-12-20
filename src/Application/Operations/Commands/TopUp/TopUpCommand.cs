using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NfcPos.Application.Common.Exceptions;
using NfcPos.Application.Common.Interfaces;
using NfcPos.Domain.Entities;

namespace NfcPos.Application.Operations.Commands.TopUp;
public class TopUpCommand : IRequest<TopUpVm>
{
    public string nfcId { get; set; }
    public decimal TopUpAmount { get; set; } = 0m;

}

public class TopUpCommandHandler : IRequestHandler<TopUpCommand, TopUpVm>
{

    private readonly IApplicationDbContext _context;
    private readonly ILogger<TopUpCommandHandler> _logger;
    private readonly IMapper _mapper;

    public TopUpCommandHandler(IApplicationDbContext context, ILogger<TopUpCommandHandler> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<TopUpVm> Handle(TopUpCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
           .Where(x => x.NfcId == request.nfcId)
           .FirstOrDefaultAsync();

        if(user == null)
        {
            throw new NotFoundException(nameof(User), request.nfcId);

        }

        decimal oldBalance = user.Balance;

        user.Balance += request.TopUpAmount;

        await _context.SaveChangesAsync(cancellationToken);

        return new TopUpVm
        {
            OldBalance = oldBalance,
            NewBalance = user.Balance
        };
    }
}
