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
    public class PlaceContentRepository : RepositoryBase<PlaceContent>,IPlaceContentRepository
    {
        public PlaceContentRepository(RepositoryContext context) :
            base(context)
        {
        }

        public void CreatePlaceContent(PlaceContent placeContent) => Create(placeContent);


        public void DeletePlaceContent(PlaceContent placeContent) => Delete(placeContent);


        public void UpdatePlaceContent(PlaceContent placeContent) => Update(placeContent);
      


        public IQueryable<PlaceContent> GetAllPlacesContent(bool trackChanges)
        {
           return  FindAll(trackChanges).OrderBy(b => b.Id).Include(b => b.Place);
        }

        public PlaceContent GetOnePlaceContent(int id, bool trackChanges)
        {
            return FindByCondition(b => b.Id.Equals(id), trackChanges).Include(b => b.Place)
                .SingleOrDefault();
        }

    }
}
