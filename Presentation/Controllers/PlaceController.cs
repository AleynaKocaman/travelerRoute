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
    [Route("api/place")]
    public class PlaceController :ControllerBase
    {
        private readonly IServiceManager _manager;

        public PlaceController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet("getall")]
        public IActionResult GetAllPlaces()
        {
            var place = _manager.PlaceService.GetAllPlaces(false);
            return Ok(place);
          
        }
        /*  [FromQuery(Name = "city")] string city,
           [FromQuery(Name = "category")] string category
         * */
        [HttpGet("{id:int}")]
        public IActionResult GetOnePlaces([FromRoute(Name = "id")] int id)
        {
            var place = _manager.PlaceService.GetOnePlaceById(id, false);
            return Ok(place);
        }


        [HttpPut("{id:int}")]
        public IActionResult UpdateOnePlace([FromRoute(Name = "id"),] int id,
           [FromBody] Place place)
        {
            if (place is null)
                return BadRequest(); // 400  eğer boş ise

            _manager.PlaceService.UpdateOnePlace(id, place, true);
            return NoContent();//204
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOnePlace([FromRoute(Name = "id")] int id)
        {
            _manager.PlaceService.DeleteOnePlace(id, false);
            return NoContent();
        }

        [HttpPost]
        public IActionResult CreateOnePlace([FromBody] Place place)
        {

            if (place == null)
            {
                return BadRequest();//400 eğer boş ise
            }

            _manager.PlaceService.CreateOnePlace(place);
            return StatusCode(201, place);//oluşturuldu mesajı

        }


        [HttpGet("byCityIdOrCategoryId")]
        public IActionResult GetPlacesByIdCityOrCategoryId([FromQuery(Name = "cityId")] int cityId,
           [FromQuery(Name = "categoryId")] int categoryId)
        {
            try
            {
                if (cityId == 0 && categoryId == 0)
                {
                    // İki parametre de verilmediyse tüm yerleri getir
                    var places = _manager.PlaceService.GetAllPlaces(false);
                    return Ok(places);
                }
                else if (cityId != null && categoryId == 0)
                {
                    // Sadece city adı verildiyse city'ye göre filtrele
                    var places = _manager.PlaceService.GetAllPlaces(false)
                        .Where(p => p.City.Id == cityId);
                    return Ok(places);
                }
                else if (categoryId != null && cityId == 0)
                {
                    // Sadece category adı verildiyse category'ye göre filtrele
                    var places = _manager.PlaceService.GetAllPlaces(false)
                        .Where(p => p.Category.Id == categoryId);
                    return Ok(places);
                }
                else
                {
                    // Hem city hem de category adı verildiyse ikisini de dikkate alarak filtrele
                    var places = _manager.PlaceService.GetAllPlaces(false)
                        .Where(p => p.City.Id == cityId && p.Category.Id == categoryId);
                    return Ok(places);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpGet("byCityOrCategory")]
        public IActionResult GetPlacesByCityOrCategory([FromQuery(Name = "city")] string? cityName, [FromQuery(Name = "category")] string? categoryName)
        {

            try
            {
                
                var places = _manager.PlaceService.GetAllPlaces(false);
                /*
                int  cityId=places.Where(a=>a.City.Name == cityName).Select(p => p.City.Id).ToList().FirstOrDefault();
                int categoryId = places.Where(a => a.Category.Name == categoryName).Select(p => p.Category.Id).ToList().FirstOrDefault();
                if (cityId == 0 && categoryId == 0)
                {
                    // İki parametre de verilmediyse tüm yerleri getir
                    places = _manager.PlaceService.GetAllPlaces(false);
                    return Ok(places);
                }
                else if (cityId != null && categoryId == 0)
                {
                    // Sadece city adı verildiyse city'ye göre filtrele
                     places = _manager.PlaceService.GetAllPlaces(false)
                        .Where(p => p.City.Id == cityId);
                    return Ok(places);
                }
                else if (categoryId != null && cityId == 0)
                {
                    // Sadece category adı verildiyse category'ye göre filtrele
                     places = _manager.PlaceService.GetAllPlaces(false)
                        .Where(p => p.Category.Id == categoryId);
                    return Ok(places);
                }
                else
                {
                    // Hem city hem de category adı verildiyse ikisini de dikkate alarak filtrele
                    places = _manager.PlaceService.GetAllPlaces(false)
                        .Where(p => p.City.Id == cityId && p.Category.Id == categoryId);
                    return Ok(places);
                }
                */
                if (string.IsNullOrEmpty(cityName) && string.IsNullOrEmpty(categoryName))
                {
                    // İki parametre de verilmediyse tüm yerleri getir
                    return Ok(places);
                }
                else if (!string.IsNullOrEmpty(cityName) && string.IsNullOrEmpty(categoryName))
                {
                    // Sadece city adı verildiyse city'ye göre filtrele
                    var cityPlaces = places.Where(p => p.City.Name == cityName);
                    return Ok(cityPlaces);
                }
                else if (!string.IsNullOrEmpty(categoryName) && string.IsNullOrEmpty(cityName))
                {
                    // Sadece category adı verildiyse category'ye göre filtrele
                    var categoryPlaces = places.Where(p => p.Category.Name == categoryName);
                    return Ok(categoryPlaces);
                }
                else
                {
                    // Hem city hem de category adı verildiyse ikisini de dikkate alarak filtrele
                    var cityCategoryPlaces = places.Where(p => p.City.Name == cityName && p.Category.Name == categoryName);
                    return Ok(cityCategoryPlaces);
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        [HttpGet("byRegion")]
        public IActionResult GetPlacesRegion([FromQuery(Name = "region")] string? region)
        {
            var places = _manager.PlaceService.GetAllPlaces(false);

            var regionPlace = places.Where(place => place.Region == region)
            .Select(place => place.City);
            return Ok(regionPlace);

        }


    }

}

