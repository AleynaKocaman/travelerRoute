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
    [Route("api/travelList")]
    public class TravelListController :ControllerBase
    {
        private readonly IServiceManager _manager;

        public TravelListController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult GetAllTravelList()
        {
            var travelList = _manager.TravalerListService.GetAllTravelList(false);
            return Ok(travelList);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneTravelList([FromRoute(Name = "id")] int id) 
        {
            var travelerList = _manager.TravalerListService.GetOneTravelList(id, false);
            return Ok(travelerList);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateTravelList([FromRoute(Name = "id"),] int id,
              [FromBody] TravelList travelList)
        {
            if (travelList is null)
                return BadRequest(); // 400  eğer boş ise

            _manager.TravalerListService.UpdateTravelList(id, travelList, true);
            return NoContent();//204
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteTravelList([FromRoute(Name = "id")] int id)
        {
            _manager.TravalerListService.DeleteTravelList(id, false);
            return NoContent();
        }

        [HttpPost]
        public IActionResult CreateOnePlace([FromBody] TravelList travelList)
        {

            if (travelList == null)
            {
                return BadRequest();//400 eğer boş ise
            }

            _manager.TravalerListService.CreateTravelList(travelList);
            return StatusCode(201, travelList);//oluşturuldu mesajı

        }
    }
}
