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
    public class TravelListContentManager : ITravelListContentService
    {
        private readonly IRepositoryManager _manager;

        public TravelListContentManager(IRepositoryManager manager)
        {
            _manager = manager;
        }
        
        public TravelListContent CreateTravelListContent(TravelListContent travelListContent)
        {
            _manager.TravelListContent.Create(travelListContent);
            _manager.Save();
            return travelListContent;
        } 

     
        public void DeleteTravelListContent(int id, bool trackChages)
        {
            var entity=_manager.TravelListContent.GetOneTravelListContent(id, trackChages);
            if (entity is null)
            {
                string message = $"TravelListContent with id:{id} could not found.";
                throw new Exception(message);
            }
            _manager.TravelListContent.Delete(entity);
            _manager.Save();
        }
        
        
        public void UpdateTravelListContent(int id, TravelListContent travelListContent, bool trackChages)
        {
            var entity = _manager.TravelListContent.GetOneTravelListContent(id, trackChages);

            if (entity is null)
            {
                string message = $"TravelListContent with id:{id} could not found.";
                throw new Exception(message);
            }

            if (travelListContent is null)
            {
                throw new ArgumentNullException(nameof(travelListContent));

            }

            entity.TravelListId = travelListContent.TravelListId;
            entity.PlaceId = travelListContent.PlaceId;
            entity.VisitDate= travelListContent.VisitDate;

            _manager.TravelListContent.Update(entity);
            _manager.Save();
        }
        //eklenen verileri sırala
        public IQueryable<TravelListContent> GetAllTravelListContent(bool trackChanges)
        {
            return _manager.TravelListContent.GetAllTravelListContent(trackChanges);
        }

        //istenilen id ye göre getir
        public TravelListContent GetOneTravelListContent(int id, bool trackChanges)
        {
            return _manager.TravelListContent.GetOneTravelListContent(id,trackChanges);
        }

    
    }
}
