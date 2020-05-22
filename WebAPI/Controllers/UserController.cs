using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Entities;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserService userService;
        UserManager<User> userManager;
        SignInManager<User> signInManager;
        public UserController(IUserService userService, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userService = userService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet("Login")]
        [Authorize]
        public async Task<IActionResult> Authenticate()
        {
            return Redirect("https://localhost:44377/user/login");
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

            var userCurrent = HttpContext.User;
            return Ok(resultToken);
            
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


        public async Task<string> HomeAdmin()
        {
            var userCurrent = await  userManager.GetUserAsync(User);

            return userCurrent.UserName;
        }

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Redirect("https://localhost:44377/user/login");
        }

    }
}