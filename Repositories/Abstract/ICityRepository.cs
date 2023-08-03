using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Abstract
{
     public interface ICityRepository :IRepositoryBase<City>
    {
        IQueryable<City> GetAllCities(bool trackChanges);
        City GetOneCityById(int id, bool trackChanges);
        void CreateOneCity(City city);
        void DeleteOneCity(City city);
        void UpdateOneCity(City city);
   
    }
}
