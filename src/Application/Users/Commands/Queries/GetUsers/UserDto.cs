using NfcPos.Application.Common.Mappings;
using NfcPos.Domain.Entities;

namespace NfcPos.Application.Users.Commands.Queries.GetUsers;

public class UserDto : IMapFrom<User>
{
    public int Id { get; set; }
    public string NfcId { get; set; } = "";
    public string Name { get; set; } = "";
    public string Surname { get; set; } = "";
    public decimal balance { get; set; }
}