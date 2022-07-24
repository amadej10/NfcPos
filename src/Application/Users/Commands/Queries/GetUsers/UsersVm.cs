using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NfcPos.Application.Users.Commands.Queries.GetUsers;
public class UsersVm
{
    public IList<UserDto> users { get; set; } = new List<UserDto>();
    
}
