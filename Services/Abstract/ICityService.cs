using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface ICityService
    {
        IEnumerable<City> GetAllCities(bool trackChanges);
        City GetOneCityById(int id, bool trackChanges);
        City CreateOneCity(City city);
        void UpdateOneCity(int id, City city, bool trackChanges);
        void DeleteOneCity(int id, bool trackChages);
    }
}
