using Entities.Models;
using Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore.Concrete
{
    public class TravelListRepository : RepositoryBase<TravelList>,ITravelList
    {
        public TravelListRepository(RepositoryContext context) : 
            base(context)
        {
        }

        public void CreateTravelList(TravelList travelList) => Create(travelList);
        public void DeleteTravelList(TravelList travelList) => Delete(travelList);
        public void UpdateTravelList(TravelList travelList) => Update(travelList);
        public IQueryable<TravelList> GetAllTravelList(bool trackChanges) =>
             FindAll(trackChanges).OrderBy(b => b.Id);
        public TravelList GetOneTravelList(int id, bool trackChanges) =>
            FindByCondition(b => b.Id.Equals(id), trackChanges).SingleOrDefault();

    }
}
