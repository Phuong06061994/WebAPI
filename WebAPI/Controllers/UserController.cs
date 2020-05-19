using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public IActionResult Login(AuthenticateModel model)
        {
            if (ModelState.IsValid)
            {
                var result = userService.FindForLogin(model.Username, model.Password);
            }
            else
            {
                ModelState.AddModelError("","Login fail")
            }
            return null;
        }
    }
}