using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Abstract
{
    public interface IRepositoryManager 
    {
        //verideki tabloları tanımlamak 
        ICityRepository Cities { get; } 
        ICategoryRepository Categories { get; }
        IPlaceRepository Places { get; }
        ITravelList TravelList { get; }
        ITravelListContent TravelListContent { get; }
        IPlaceContentRepository PlaceContent { get; }
   
        void Save();
    }
}
