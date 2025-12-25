
using Microsoft.AspNetCore.Mvc;
using OnionPronia.Application.DTOs;
using OnionPronia.Application.Interfaces.Services;

namespace OnionPronia.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page, int take)
        {
            return Ok(await _service.GetAllAsync(page, take));

            //return StatusCode(StatusCodes.Status200OK, categories);
        }

        [HttpGet("{id}")]
        //[Route("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id is null || id < 1) return BadRequest();

            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PostCategoryDto categoryDto)
        {
            await _service.CreateAsync(categoryDto);
            return Created();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] PutCategoryDto categoryDto)
        {
            if (id < 1) return BadRequest();
            await _service.UpdateAsync(id, categoryDto);
            return NoContent();
            //return Ok(existed);

        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Remove(int id)
        {
            if (id < 1) return BadRequest();

            await _service.DeleteAsync(id);

            return NoContent();



            //IEnumerable, --> Lazy desteklemir
            //IQueryable,
            //ICollection,
            //    IList

        }



    }
}
