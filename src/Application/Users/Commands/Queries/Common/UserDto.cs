using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NfcPos.Application.Common.Mappings;
using NfcPos.Domain.Entities;

namespace NfcPos.Application.Users.Commands.Queries.Common;
public class UserDto : IMapFrom<User>
{
    public int Id { get; set; }
    public string NfcId { get; set; } = "";
    public string Name { get; set; } = "";
    public string Surname { get; set; } = "";
    public string? Description { get; set; } = "";
    public decimal balance { get; set; }
}
