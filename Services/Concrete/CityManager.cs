using Entities.Models;
using Repositories.Abstract;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concrete
{
    public class CityManager : ICityService
    {
        private readonly IRepositoryManager _manager;
        public CityManager(IRepositoryManager manager)
        {
            _manager = manager;

        }
        public City CreateOneCity(City city)
        {
            _manager.Cities.CreateOneCity(city);
            _manager.Save();
            return city;
        }
        public void DeleteOneCity(int id, bool trackChages)
        {
            //check entity
            var entity = _manager.Cities.GetOneCityById(id, trackChages);

            if (entity is null)
            {
                string message = $"City with id:{id} could not found.";
                throw new Exception(message);
            }

            _manager.Cities.DeleteOneCity(entity);
            _manager.Save();

        }
        public void UpdateOneCity(int id, City city, bool trackChanges)
        {
            //check entity
            var entity = _manager.Cities.GetOneCityById(id, trackChanges);
            if (entity is null)
            {
                string message = $"City with id:{id} could not found.";
                throw new Exception(message);
            }
            //check params
            if (city is null)
                throw new ArgumentNullException(nameof(city));
            entity.Name = city.Name;
            _manager.Cities.Update(entity);
            _manager.Save();
        }
        public IEnumerable<City> GetAllCities(bool trackChanges)
        {
            return _manager.Cities.GetAllCities(trackChanges);
        }
        public City GetOneCityById(int id, bool trackChanges)
        {
            return _manager.Cities.GetOneCityById(id, trackChanges);
        }

    }
}
