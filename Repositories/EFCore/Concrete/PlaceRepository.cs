using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore.Concrete
{
    public class PlaceRepository : RepositoryBase<Place>, IPlaceRepository
    {
        public PlaceRepository(RepositoryContext context) :
            base(context)
        {

        }

        public void CreateOnePlace(Place place) => Create(place);
        public void DeleteOnePlace(Place place) => Delete(place);
        public void UpdateOnePlace(Place place) => Update(place);

        public IQueryable<Place> GetAllPlaces(bool trackChanges) =>
            FindAll(trackChanges)
           // .OrderBy(b => b.Id)
            .Include(b=>b.City)
            .Include(b=>b.Category);

        public Place GetOnePlaceById(int id, bool trackChanges) =>
            FindByCondition(b => b.Id.Equals(id), trackChanges).Include(b => b.City).Include(b=>b.Category)
            .SingleOrDefault();




    }
}
