using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IPlaceService
    {
        IEnumerable<Place> GetAllPlaces(bool trackChanges);
        Place GetOnePlaceById(int id, bool trackChanges);
        Place CreateOnePlace(Place place);
        void DeleteOnePlace(int id, bool trackChages);
        void UpdateOnePlace(int id, Place place, bool trackChanges);
    }
}
