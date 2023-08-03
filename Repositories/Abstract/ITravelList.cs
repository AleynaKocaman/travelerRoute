using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Abstract
{
    public interface ITravelList :IRepositoryBase<TravelList>
    {
        IQueryable<TravelList> GetAllTravelList(bool trackChanges);
        TravelList GetOneTravelList(int id, bool trackChanges);
        void CreateTravelList(TravelList travelList);
        void DeleteTravelList(TravelList travelList);
        void UpdateTravelList(TravelList travelList);
    }
}
