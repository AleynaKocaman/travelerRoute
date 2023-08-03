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
    public class TravalerListManager:ITravalerListService
    {
        private readonly IRepositoryManager _manager;

        public TravalerListManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public TravelList CreateTravelList(TravelList travelList) //seyehat listesi oluşturma
        {
            _manager.TravelList.CreateTravelList(travelList);
            _manager.Save();
            return travelList;
        }

        public void DeleteTravelList(int id, bool trackChanges) // seyehat listesi silme
        {
           var entity=_manager.TravelList.GetOneTravelList(id,trackChanges);
            if (entity is null)
            {
                string message = $"Place with id:{id} could not found ";
                throw new Exception(message);
            }

            _manager.TravelList.DeleteTravelList(entity);
            _manager.Save();
        }

        public void UpdateTravelList(int id, TravelList travelList, bool trackChanges) //seyehat listesinin adını güncelleme
        {
            var entity = _manager.TravelList.GetOneTravelList(id, trackChanges);
            if (entity is null)
            {
                string message = $"Place with id:{id} could not found ";
                throw new Exception(message);
            }

            if (travelList is null)
            {
                throw new ArgumentNullException(nameof(travelList));
            }

            entity.Name = travelList.Name;

            _manager.TravelList.UpdateTravelList(entity);
            _manager.Save();
        }

        public IQueryable<TravelList> GetAllTravelList(bool trackChanges) //seyehat listelerini sırala
        {
            return _manager.TravelList.GetAllTravelList(trackChanges);
        }

        public TravelList GetOneTravelList(int id, bool trackChanges) //id ye göre seyehat listesi sırala
        {
            return _manager.TravelList.GetOneTravelList(id, trackChanges);
        }

    }
}
