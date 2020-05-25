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
    [Authorize]
    public class NewsController : ControllerBase
        
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _newsService.GetAll();
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> Create(News model)
        {
            var data = await _newsService.Save(model);
            if(data < 0)
            {
                return  BadRequest("Cap nhat khong thanh cong");
            }
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(News model)
        {
           
            var data = await _newsService.Save(model);
            if (data < 0)
            {
                return BadRequest("Cap nhat khong thanh cong");
            }
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userCurrent = HttpContext.User.Identity.Name;

            var news = await _newsService.GetById(id);

            if (userCurrent.Equals(news.CreatedBy) || userCurrent.Equals("admin"))
            {
                return Ok(news);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _newsService.Delete(id);
            if (data < 0)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}