using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TT_LAB2.Models;

namespace TT_LAB2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository productRepository;
        public ProductController(IProductRepository repository)
        {
            productRepository = repository;
        }

        [HttpGet]
        public IEnumerable<Product> Get() => productRepository.Products;

        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest("Value must be passed in the request body");
            }
            return Ok(productRepository[id]);
        }

        [HttpPost]
        public Product Post([FromBody] Product prodPost) =>
                productRepository.AddProduct(new Product
                {
                    ProductId = prodPost.ProductId,
                    ProductName = prodPost.ProductName,
                    Price = prodPost.Price,
                    CategoryId = prodPost.CategoryId
                });

        [HttpPut]
        public Product Put([FromBody] Product prodPut) =>
            productRepository.UpdateProduct(prodPut);

        [HttpPatch("{id}")]
        public StatusCodeResult Patch(int id, [FromBody] JsonPatchDocument<Product> prodPatch)
        {
            var prod = (Product)((OkObjectResult)Get(id).Result).Value;
            if (prod != null)
            {
                prodPatch.ApplyTo(prod);
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public void Delete(int id) => productRepository.DeleteProduct(id);

    }
}
