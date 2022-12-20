using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NfcPos.Application.Operations.Commands.TopUp;
public class TopUpVm
{
    public decimal OldBalance { get; set; }

    public decimal NewBalance { get; set; }
}
