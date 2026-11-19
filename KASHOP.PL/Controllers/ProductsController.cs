using KASHOP.BLL;
using KASHOP.DAL.DTO.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([FromForm] ProductRequest request, CancellationToken cancellationToken)
        {
            var result = await _service.CreateAsync(request, cancellationToken);
            return result ? Ok() : BadRequest();
        }
        [HttpGet("")]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var result = await _service.GetAllAsync(cancellationToken);
            return Ok(result);
        
        }

    }
}

