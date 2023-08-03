using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.EFCore;
using Services.Abstract;

namespace WebApi.Controllers
{


    [ApiController]
    [Route("api/city")]
    public class CityController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public CityController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet("getall")]
        public IActionResult GetAllCity()
        {
            var city = _manager.CityService.GetAllCities(false);
            return Ok(city);
        }

     
        [HttpGet("{id:int}")]
        public IActionResult GetOneCity([FromRoute(Name = "id")] int id)
        {
            var city = _manager.CityService.GetOneCityById(id, false);
            return Ok(city);
        }
        [HttpPost]
        public IActionResult CreateOneCity([FromBody] City city)
        {
            if (city == null)
            {
                return BadRequest();//400 eğer boş ise
            }
            _manager.CityService.CreateOneCity(city);
            return StatusCode(201, city);//oluşturuldu mesajı

        }
        [HttpPut("{id:int}")]
        public IActionResult UpdateOneCity([FromRoute(Name = "id")] int id,
            [FromBody] City city)
        {
            if (city is null)
                return BadRequest(); // 400  eğer boş ise

            _manager.CityService.UpdateOneCity(id, city, true);
            return NoContent();//204

        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneCity([FromRoute(Name = "id")] int id)
        {
            _manager.CityService.DeleteOneCity(id, false);
            return NoContent();
        }


    }
}
