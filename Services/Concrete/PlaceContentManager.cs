using Entities.Models;
using Repositories.Abstract;
using Repositories.EFCore;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concrete
{
    public class PlaceContentManager : IPlaceContentService
    {
        private readonly IRepositoryManager _manager;
       // private readonly RepositoryContext _dbContext;

        public PlaceContentManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public PlaceContent CreatePlaceContent(PlaceContent placeContent)
        {
            _manager.PlaceContent.CreatePlaceContent(placeContent);
            _manager.Save();
            return placeContent;
        }

        //bu hepsini siliyor ya ben puan veya yorumu silmek istersem
        public void DeletePlaceContent(int id, bool trackChages)
        {
            var entity=_manager.PlaceContent.GetOnePlaceContent(id,trackChages);
            if (entity is null)
            {
                string message = $"Place with id:{id} could not found.";
                throw new Exception(message);
            }
            _manager.PlaceContent.DeletePlaceContent(entity);
            _manager.Save();

        }
        public void UpdatePlaceContent(int id, PlaceContent placeContent, bool trackChanges)
        {
            var entity = _manager.PlaceContent.GetOnePlaceContent(id, trackChanges);
            if (entity is null)
            {
                string message = $"Place with id:{id} could not found.";
                throw new Exception(message);
            }
            if (placeContent is null)
            {
                throw new ArgumentNullException(nameof(placeContent));
            }

            entity.Point = placeContent.Point;
            entity.Comment = placeContent.Comment;
            entity.IsVisitDate = placeContent.IsVisitDate;

            _manager.PlaceContent.UpdatePlaceContent(entity);
            _manager.Save();


        }

        public IEnumerable<PlaceContent> GetAllPlaceContent(bool trackChanges)
        {
            return _manager.PlaceContent.GetAllPlacesContent(trackChanges);
          
        }

        public PlaceContent GetOnePlaceContent(int id, bool trackChanges)
        {
            return _manager.PlaceContent.GetOnePlaceContent(id, trackChanges);
        }

        /*
        public void AddComment(PlaceContent comment, int UserId)
        {
            comment.UserId = UserId;
            comment.IsVisitDate=DateTime.Now;
          _dbContext.SaveChanges(); 
        }
        */
    }
}
