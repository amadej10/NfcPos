using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NfcPos.Application.Users.Commands.CreateUser;
using NfcPos.Application.Users.Commands.Queries.GetUsers;

namespace NfcPos.WebUI.Controllers;
public class UsersController : ApiControllerBase
{

    [HttpGet("GetUsers")]
    public async Task<ActionResult<UsersVm>> GetUsers()
    {
        return await Mediator.Send(new GetUsersQuery());
    }


    [HttpPost("CreateUser")]
    public async Task<ActionResult<int>> CreateUser(CreateUserCommand command)
    {
        return await Mediator.Send(command);
    }

}
