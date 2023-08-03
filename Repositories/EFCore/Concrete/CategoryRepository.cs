using Entities.Models;
using Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore.Concrete
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateOneCategory(Category category) => Create(category);
        public void DeleteOneCategory(Category category) => Delete(category);
        public void UpdateOneCategory(Category category) => Update(category);

        public IQueryable<Category> GetAllCategories(bool trackChanges) =>
            FindAll(trackChanges).OrderBy(b => b.Id);

        public Category GetOneCategoryById(int id, bool trackChanges) =>
            FindByCondition(b => b.Id.Equals(id), trackChanges).SingleOrDefault();


    }
}
