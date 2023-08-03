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
    public class PlaceManager : IPlaceService
    {
        private readonly IRepositoryManager _manager;

        public PlaceManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public Place CreateOnePlace(Place place)
        {
            _manager.Places.CreateOnePlace(place);
            _manager.Save();
            return place;

        }

        public void DeleteOnePlace(int id, bool trackChages)
        {
            //check entity
            var entity = _manager.Places.GetOnePlaceById(id, trackChages);

            if (entity is null)
            {
                string message = $"Place with id:{id} could not found.";
                throw new Exception(message);
            }

            _manager.Places.DeleteOnePlace(entity);
            _manager.Save();
        }

        public void UpdateOnePlace(int id, Place place, bool trackChanges)
        {
            var entity = _manager.Places.GetOnePlaceById(id, trackChanges);
            if (entity is null)
            {
                string message = $"Place with id:{id} could not found.";
                throw new Exception(message);

            }
            //check params
            if (place is null)
                throw new ArgumentNullException(nameof(place));

            entity.Name = place.Name;
            entity.Region = place.Region;
            entity.Town = place.Town;
            entity.District = place.District;
            entity.Street = place.Street;
            entity.PostalCode = place.PostalCode;
            entity.CityId = place.CityId;
            entity.CategoryId = place.CategoryId;

            _manager.Places.Update(entity);
            _manager.Save();

        }

        public IEnumerable<Place> GetAllPlaces(bool trackChanges)
        {
            return _manager.Places.GetAllPlaces(trackChanges);
        }

        public Place GetOnePlaceById(int id, bool trackChanges)
        {
            return _manager.Places.GetOnePlaceById(id, trackChanges);
        }
    }
}
