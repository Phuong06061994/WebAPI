using API.Filter;
using DAL.Repository;
using DAL.Request;
using Microsoft.AspNetCore.Authorization;
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
        [ClaimRequirement(FunctionCode.ADMIN, ActionCode.CREATE)]
        public async Task<IActionResult> GetAll()
        {
            var data = await _newsRepository.GetAll();
            return Ok(data);
        }


        [HttpGet("{id}")]
       
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _newsRepository.GetById(id);
            return Ok(data);
        }

        [HttpPost]
        [ClaimRequirement(FunctionCode.ADMIN, ActionCode.CREATE)]
        public async Task<IActionResult> Create([FromBody]NewsRequest request)
        {
            var data = await _newsRepository.Create(request);
            if(data < 0)
            {
                return BadRequest();
            }
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] NewsRequest request)
        {
            var data = await _newsRepository.Update(request);
            if (data < 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Detele( int id)
        {
            var data = await _newsRepository.Delete(id);
            if (data < 0)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}