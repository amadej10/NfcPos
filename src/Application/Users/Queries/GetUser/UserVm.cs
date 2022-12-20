using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NfcPos.Application.Users.Queries.Common;

namespace NfcPos.Application.Users.Queries.GetUser;
public class UserVm
{
    public UserDto User { get; set; } = new UserDto();
}
