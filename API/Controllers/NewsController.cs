﻿using DAL.Repository;
using DAL.Request;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsRepository _newsRepository;

        public NewsController(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _newsRepository.GetAll();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]NewsRequest request)
        {
            var data = await _newsRepository.Create(request);
            if(data > 0)
            {
                return BadRequest();
            }
            return Ok(data);
        }
    }
}