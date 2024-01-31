using Microsoft.AspNetCore.Mvc;
using Product.App.Contract.Dto;
using Product.App.Contract.IServices;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }



        [HttpGet]
        public IActionResult Get()
        {
            var product = productService.GetProducts();
            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create([FromForm] ProductDto dto)
        {
            if (ModelState.IsValid)
            {
                var rowAffected = productService.Add(dto);
                return Created("", null);
            }
            return BadRequest();
        }
        
    }
}