using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Abstract
{
    public interface IPlaceContentRepository: IRepositoryBase<PlaceContent>
    {
        IQueryable<PlaceContent> GetAllPlacesContent(bool trackChanges);
        PlaceContent GetOnePlaceContent(int id, bool trackChanges);
        void CreatePlaceContent(PlaceContent placeContent);
        void DeletePlaceContent(PlaceContent placeContent);
        void UpdatePlaceContent(PlaceContent placeContent);
    }
}
