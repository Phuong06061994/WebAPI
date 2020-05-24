using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Entities;
using WebAPI.Services;

namespace WebAPI.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="admin")]
    public class NewsController : ControllerBase
        
    {
        private readonly INewsService newsService;

        public NewsController(INewsService newsService)
        {
            this.newsService = newsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await newsService.GetAll();
            return Ok(data);
        }
    }
}