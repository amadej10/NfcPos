using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NfcPos.Application.Users.Commands.Queries.Common;

namespace NfcPos.Application.Users.Commands.Queries.GetUser;
public class UserVm
{
    public UserDto User { get; set; } = new UserDto();
}
