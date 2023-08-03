using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface ICategoryService 
    {
        IEnumerable<Category> GetAllCategories(bool trackChanges);
        Category GetOneCategoryById(int id, bool trackChanges);
        Category CreateOneCategory(Category category);
        void UpdateOneCategory(int id, Category category, bool trackChanges);
        void DeleteOneCategory(int id, bool trackChages);
    }
}
