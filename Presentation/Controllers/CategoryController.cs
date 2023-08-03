using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public CategoryController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public ActionResult GetAllCategories()
        {
            var category = _manager.CategoryService.GetAllCategories(false);
            return Ok(category);
        }

        [HttpGet("{id:int}")]
        public ActionResult GetOneCategory([FromRoute(Name = "id")] int id)
        {
            var category = _manager.CategoryService.GetOneCategoryById(id, false);
            return Ok(category);

        }

        [HttpPost]
        public IActionResult CreateOneCategory([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest();//400 eğer boş ise
            }
            _manager.CategoryService.CreateOneCategory(category);
            return StatusCode(201, category);//oluşturuldu mesajı
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneCategory([FromRoute(Name = "id")] int id,
           [FromBody] Category category)
        {
            if (category is null)
                return BadRequest(); // 400  eğer boş ise

            _manager.CategoryService.UpdateOneCategory(id, category, true);
            return NoContent();//204
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneCity([FromRoute(Name = "id")] int id)
        {
            _manager.CategoryService.DeleteOneCategory(id, false);
            return NoContent();
        }


    }
}
