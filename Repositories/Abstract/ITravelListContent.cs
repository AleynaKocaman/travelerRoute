using Entities.Models;
using Repositories.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Abstract
{
    public interface ITravelListContent : IRepositoryBase<TravelListContent>
    {
        IQueryable<TravelListContent> GetAllTravelListContent(bool trackChanges);
        TravelListContent GetOneTravelListContent(int id, bool trackChanges);
        void CreateTravelListContent(TravelListContent travelListContent);
        void DeleteTravelListContent(TravelListContent travelListContent);
        void UpdateTravelListContent(TravelListContent travelListContent);
    }
}
