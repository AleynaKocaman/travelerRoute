using Entities.Models;
using Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore.Concrete
{
    //repositorybase --> abstract sınıf
    public class CityRepository : RepositoryBase<City>, ICityRepository
    {
        public CityRepository(RepositoryContext context) :
            base(context)
        {

        }

        public IQueryable<City> GetAllCities(bool trackChanges) =>
            FindAll(trackChanges).OrderBy(b => b.Id);

        public City GetOneCityById(int id, bool trackChanges) =>
            FindByCondition(b => b.Id.Equals(id), trackChanges).SingleOrDefault();

        public void CreateOneCity(City city) => Create(city);

        public void DeleteOneCity(City city) => Delete(city);

        public void UpdateOneCity(City city) => Update(city);




    }
}
