using Entities.Exceptions;
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
    public class CategoryManager : ICategoryService
    {
        private readonly IRepositoryManager _manager;
        public CategoryManager(IRepositoryManager manager)
        {
            _manager = manager;
            
        }

        public Category CreateOneCategory(Category category)
        {
            _manager.Categories.CreateOneCategory(category);
            _manager.Save();
            return category;
        }

        public void DeleteOneCategory(int id, bool trackChages)
        {
            var entity = _manager.Categories.GetOneCategoryById(id, trackChages);

            if (entity is null)
            {
                throw new CategoryNotFoundException(id);
            }
            _manager.Categories.DeleteOneCategory(entity);
            _manager.Save();
        }

        public void UpdateOneCategory(int id, Category category, bool trackChanges)
        {
            //check entity
            var entity = _manager.Categories.GetOneCategoryById(id, trackChanges);
            if (entity is null)
            {
                throw new CategoryNotFoundException(id);
            }
            entity.Name = category.Name;
            _manager.Categories.Update(entity);
            _manager.Save();

        }

        public IEnumerable<Category> GetAllCategories(bool trackChanges)
        {
            return _manager.Categories.GetAllCategories(trackChanges);
        }

        public Category GetOneCategoryById(int id, bool trackChanges)
        {
            var category=_manager.Categories.GetOneCategoryById(id,trackChanges);
            if (category == null)
            {
                throw new CategoryNotFoundException(id);
            }
            return _manager.Categories.GetOneCategoryById(id, trackChanges);
        }

    }
}
