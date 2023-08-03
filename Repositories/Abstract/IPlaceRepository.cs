using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Abstract
{
    public interface IPlaceRepository :IRepositoryBase<Place>
    {
        IQueryable<Place> GetAllPlaces(bool trackChanges);
        Place GetOnePlaceById(int id, bool trackChanges);
        void CreateOnePlace(Place place);
        void DeleteOnePlace(Place place);
        void UpdateOnePlace(Place place);
    }
}
