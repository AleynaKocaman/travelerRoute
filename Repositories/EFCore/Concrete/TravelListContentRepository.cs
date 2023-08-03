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
    public class TravelListContentRepository : RepositoryBase<TravelListContent>,ITravelListContent
    {
        public TravelListContentRepository(RepositoryContext context) :
            base(context)
        {
        }

        public void CreateTravelListContent(TravelListContent travelListContent)=> Create(travelListContent);

        public void DeleteTravelListContent(TravelListContent travelListContent)=> Delete(travelListContent);
        public void UpdateTravelListContent(TravelListContent travelListContent) => Update(travelListContent);

        public IQueryable<TravelListContent> GetAllTravelListContent(bool trackChanges)=>
            FindAll(trackChanges).OrderBy(b => b.Id).Include(b => b.TravelList).Include(b => b.Place);
        public TravelListContent GetOneTravelListContent(int id, bool trackChanges)=>
              FindByCondition(b => b.Id.Equals(id), trackChanges).Include(b => b.TravelList).Include(b => b.Place).SingleOrDefault();


    }
}
