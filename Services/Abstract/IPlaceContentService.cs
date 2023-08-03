using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IPlaceContentService
    {
        IEnumerable<PlaceContent> GetAllPlaceContent(bool trackChanges);
        PlaceContent GetOnePlaceContent(int id, bool trackChanges);
        PlaceContent CreatePlaceContent(PlaceContent placeContent);
        void DeletePlaceContent(int id, bool trackChages);
        void UpdatePlaceContent(int id, PlaceContent placeContent, bool trackChanges);
        //void AddComment(PlaceContent placeContent, int UserId);
    }
}
