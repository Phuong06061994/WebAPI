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
    [Authorize("Admin")]
    public class NewsController : ControllerBase
        
    {
        private readonly INewsService newsService;

        public NewsController(INewsService newsService)
        {
            this.newsService = newsService;
        }

        [HttpGet]
        public IEnumerable<News> GetAll()
        {
            return newsService.GetAll();
        }
    }
}