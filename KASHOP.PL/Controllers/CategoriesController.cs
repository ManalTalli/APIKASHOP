using KASHOP.BLL;
using KASHOP.DAL;
using KASHOP.DAL.Data;
using KASHOP.DAL.DTO.Requests;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace KASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;
        public CategoriesController(ICategoryService service)
        {

            _service = service;
        }
        [HttpGet("")]
        public async Task<IActionResult> Index(CancellationToken cancellationToken=default)
        {
            var categories =await _service.GetAllAsync(cancellationToken);

            return Ok(categories);
        }
        //[HttpGet("{id}")]
        //public IActionResult GetById (int id)
        //{
        //    var category = context.Categories.Find(id);
        //    return Ok(category);
        //}
        [HttpPost("")]
        public async Task<IActionResult> Create (CategoryRequest request,CancellationToken cancellationToken)
        {
            var result = await _service.CreateAsync(request, cancellationToken);
            return result ?Ok() : BadRequest();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken = default)
        {
            var result= await _service.GetByIdAsync(id, cancellationToken);
            return result is not null? Ok(result):BadRequest();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete( int id,CancellationToken cancellationToken)
        {
            var result = await _service.DeleteAsync(id, cancellationToken);
            return result?NoContent():BadRequest();
        }


    } 
}
