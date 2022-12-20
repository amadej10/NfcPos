using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NfcPos.Application.Operations.Commands.TopUp;

namespace NfcPos.WebUI.Controllers;
public class OperationsController : ApiControllerBase
{

    [HttpPost("TopUp")]
    public async Task<ActionResult<TopUpVm>> TopUp(TopUpCommand command)
    {
        return await Mediator.Send(command);
    }
}
