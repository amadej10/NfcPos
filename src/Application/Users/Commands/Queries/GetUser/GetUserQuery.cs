using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NfcPos.Application.Common.Interfaces;
using NfcPos.Application.Users.Commands.Queries.Common;

namespace NfcPos.Application.Users.Commands.Queries.GetUser;
public class GetUserQuery : IRequest<UserVm>
{
    public string nfcId { get; set; }
}

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserVm>
{

    private readonly IApplicationDbContext _context;
    private readonly ILogger<GetUserQueryHandler> _logger;
    private readonly IMapper _mapper;

    public GetUserQueryHandler(IApplicationDbContext context, ILogger<GetUserQueryHandler> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<UserVm> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var query = await _context.Users
           .Where(x => x.NfcId == request.nfcId)
           .FirstOrDefaultAsync();

        return new UserVm
        {
            User = _mapper.Map<UserDto>(query)
        };

    }
}
