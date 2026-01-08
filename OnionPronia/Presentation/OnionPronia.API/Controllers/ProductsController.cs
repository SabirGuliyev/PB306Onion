using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnionPronia.Application.DTOs.Products;
using OnionPronia.Application.Interfaces.Services;

namespace OnionPronia.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync(int page=0,int take=0)
        {
            return Ok(await _service.GetAllAsync(page, take));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(long id)
        {
            if (id < 1) return BadRequest();

            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm] PostProductDto productDto)
        {
           await _service.CreateProductAsync(productDto);

            return Created();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(long id,[FromBody] PutProductDto productDto)
        {
            await _service.UpdateProductAsync(id,productDto);

            return NoContent();
        }

    }
}
