using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Dto;
using DAL.Repository;
using DAL.Request.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPermissionRepository _permissionRepository;
        public AccountController(IUserRepository userRepository, IPermissionRepository permissionRepository)
        {
            _userRepository = userRepository;
            _permissionRepository = permissionRepository;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateRequest model)
        {
            var resultToken = await _userRepository.Authenticate(model);
            if (string.IsNullOrEmpty(resultToken))
            {
                return BadRequest("User or password is not correct");
            }
            return Ok(resultToken);

        }

        [HttpGet]
        [AllowAnonymous]
        public  IActionResult Login()
        {
            return Redirect("https://localhost:44377/user/login");

        }
    }
}