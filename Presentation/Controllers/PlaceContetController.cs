using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/placeContent")]
    public class PlaceContetController :ControllerBase
    {
        private readonly IServiceManager _manager;

        public PlaceContetController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        public IActionResult CreateOnePlaceContent([FromBody] PlaceContent placeContent)
        {
            if (placeContent == null)
            {
                return BadRequest();//400 eğer boş ise
            }
            _manager.PlaceContentService.CreatePlaceContent(placeContent);
            return StatusCode(201, placeContent);


        }

        [HttpPut("{id:int}")]
        public IActionResult UpdatePlaceContent([FromRoute(Name = "id"),] int id,
           [FromBody] PlaceContent placeConntent)
        {
            if (placeConntent is null)
            {
                return BadRequest();
            }

            _manager.PlaceContentService.UpdatePlaceContent(id, placeConntent,true);
            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeleteOnePlaceContent([FromRoute(Name = "id")] int id)
        {
            _manager.PlaceContentService.DeletePlaceContent(id, false);
            return NoContent();
        }

        [HttpGet]
        public IActionResult GetAllPlaceContent()
        {
            var placeContent = _manager.PlaceContentService.GetAllPlaceContent(false);
            return Ok(placeContent);

        }

        [HttpGet("{id:int}")]
        public IActionResult GetOnePlaceContent([FromRoute(Name = "id")] int id)
        {
            var placeContent = _manager.PlaceContentService.GetOnePlaceContent(id,false);
            return Ok(placeContent);

        }

        /*
        [HttpPost("{comment:string}")]
        public IActionResult AddComment(PlaceContent comment, int userId)
        {
            _manager.PlaceContentService.AddComment(comment, userId);
            return RedirectToAction("Index", "Home"); // Yorum eklendikten sonra başka bir sayfaya yönlendirilebilirsiniz.
        }
        */
    }
}
