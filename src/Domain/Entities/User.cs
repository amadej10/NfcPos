using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NfcPos.Domain.Entities;
public class User : BaseAuditableEntity
{
    public string Name { get; set; } = "";
    public string Surname { get; set; } = "";
    public string? NfcId { get; set; }
    public string? Description { get; set; }
    public decimal Balance { get; set; } = 0;
}
