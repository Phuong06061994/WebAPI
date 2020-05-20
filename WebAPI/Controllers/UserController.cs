using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateModel model)
        {
            var resultToken = await userService.Authenticate(model);
            if (string.IsNullOrEmpty(resultToken))
            {
                return BadRequest("User or password is not correct");
            }
            return Ok(new {token = resultToken } );
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateUserModel model)
        {
            var result = await userService.Create(model);
            if (!result)
            {
                return BadRequest("Create new user is not successed");
            }
            return Ok();
        }

    }
}