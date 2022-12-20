using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NfcPos.Application.Operations.Commands.Common;
using NfcPos.Application.Operations.Commands.Pay;
using NfcPos.Application.Operations.Commands.TopUp;
using NfcPos.Application.Operations.Commands.WithdrawAll;

namespace NfcPos.WebUI.Controllers;
public class OperationsController : ApiControllerBase
{

    [HttpPost("TopUp")]
    public async Task<ActionResult<BalanceVm>> TopUp(TopUpCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost("Pay")]
    public async Task<ActionResult<BalanceVm>> Pay(PayCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost("WithdrawAll")]
    public async Task<ActionResult<BalanceVm>> WithdrawAll(WithdrawAllCommand command)
    {
        return await Mediator.Send(command);
    }
}
