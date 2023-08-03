using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface ITravelListContentService
    {
        IQueryable<TravelListContent> GetAllTravelListContent(bool trackChanges);
        TravelListContent GetOneTravelListContent(int id, bool trackChanges);
        TravelListContent CreateTravelListContent(TravelListContent travelListContent);
        void DeleteTravelListContent(int id,bool trackChages);
        void UpdateTravelListContent(int id,TravelListContent travelListContent,bool trackChages);
    }
}
