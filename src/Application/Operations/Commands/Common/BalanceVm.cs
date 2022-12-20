using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NfcPos.Application.Operations.Commands.Common;
public class BalanceVm
{
    public decimal OldBalance { get; set; }

    public decimal NewBalance { get; set; }
}
