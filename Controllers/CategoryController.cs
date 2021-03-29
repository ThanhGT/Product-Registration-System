using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using TT_LAB2.Models;

namespace TT_LAB2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryRepository categoryRepository;
        public CategoryController(ICategoryRepository repository)
        {
            categoryRepository = repository;
        }

        [HttpGet]
        public IEnumerable<Category> Get() => categoryRepository.Categories;

        [HttpGet("{id}")]
        public ActionResult<Category> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest("Value must be passed in the request body");
            }
            return Ok(categoryRepository[id]);
        }

        [HttpPost]
        public Category Post([FromBody] Category catPost) =>

            categoryRepository.AddCategory(new Category
            {
                CategoryId = catPost.CategoryId,
                CategoryName = catPost.CategoryName

            });

        [HttpPut]
        public Category Put([FromBody] Category catPut) =>
            categoryRepository.UpdateCategory(catPut);

        [HttpPatch("{id}")]
        public StatusCodeResult Patch(int id, [FromBody] JsonPatchDocument<Category> catPatch)
        {
            var cat = (Category)((OkObjectResult)Get(id).Result).Value;
            if (cat != null)
            {
                catPatch.ApplyTo(cat);
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public void Delete(int id) => categoryRepository.DeleteCategory(id);

    }
}
