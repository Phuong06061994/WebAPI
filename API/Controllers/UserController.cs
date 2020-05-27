using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DAL.Constant;
using DAL.Repository;
using DAL.Request.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]AuthenticateRequest model)
        {   
           
            var result = await _userRepository.Create(model);
            if (!result)
            {
                return BadRequest("Create new user is not successed");
            }
            return Ok();
                
            
           
        }
    }
}