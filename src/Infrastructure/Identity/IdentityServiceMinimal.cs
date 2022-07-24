using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NfcPos.Application.Common.Interfaces;

namespace NfcPos.Infrastructure.Identity;
internal class IdentityServiceMinimal : IIdentityService
{
    public Task<string> GetUserNameAsync(string userId)
    {
        return Task.FromResult("TESTUSER");
    }
}
