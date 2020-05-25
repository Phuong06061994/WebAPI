﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebAPI.Entities;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        [AllowAnonymous]
        public IActionResult Authenticate()
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
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromBody]UserModel model)
        {
            var result = await userService.Create(model);
            if (!result)
            {
                return BadRequest("Create new user is not successed");
            }
            return Ok();
        }

        [HttpGet]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> Index()
        {
            var data = await userService.GetAll();
            return Ok(data);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await userService.GetById(id);
            return Ok(user);
        }

        [HttpPut("{id}/role")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> RoleAssign(Guid id, [FromBody]RoleAssignRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await userService.RoleAssign(id, request);
            if (!result)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody]UserChangePasswordModel request)
        {

            var result = await userService.ChangePassword(request);
            if (!result)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Redirect("https://localhost:44377/user/login");
        }

    }
}