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
    [Route("api/travelListContent")]
    public class TravelListContentController :ControllerBase
    {
        private readonly IServiceManager _manager;

        public TravelListContentController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult GetAllTravelListContent()
        {
            var travelListContent = _manager.TravalerListContentService.GetAllTravelListContent(false);
            return Ok(travelListContent);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneTravelerListContent([FromRoute(Name = "id")] int id)
        {
            var travelListContent = _manager.TravalerListContentService.GetOneTravelListContent(id, false);
            return Ok(travelListContent);
        }
        [HttpPut("{id:int}")]
        public IActionResult UpdateOneTravelListContent([FromRoute(Name = "id"),] int id,
          [FromBody] TravelListContent travelListContent)
        {
            if (travelListContent is null)
            {
                return BadRequest();
            }

            _manager.TravalerListContentService.UpdateTravelListContent(id, travelListContent, true);
            return NoContent();//204 
        }
        

        [HttpDelete("{id:int}")]
        public IActionResult DeleteTravelerListContent([FromRoute(Name ="id")] int id)
        {
            _manager.TravalerListContentService.DeleteTravelListContent(id, false);
            return NoContent();
        }


        [HttpPost]
        public IActionResult CreateTravelListContent([FromBody]TravelListContent travelListContent)
        {
            if (travelListContent is null)
            {
                return BadRequest();
            }
            _manager.TravalerListContentService.CreateTravelListContent(travelListContent);
            return NoContent();

        }

        


    }
}
