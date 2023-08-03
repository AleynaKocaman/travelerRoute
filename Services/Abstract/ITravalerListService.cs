using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface ITravalerListService
    {
        IQueryable<TravelList> GetAllTravelList(bool trackChanges);
        TravelList GetOneTravelList(int id, bool trackChanges);
        TravelList CreateTravelList(TravelList travelList);
        void DeleteTravelList(int id,bool trackChanges);
        void UpdateTravelList(int id,TravelList travelList,bool trackChanges);
    }
}
