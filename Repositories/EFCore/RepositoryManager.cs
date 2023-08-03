using Repositories.Abstract;
using Repositories.EFCore.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
       // veritabanı işlemlerini gerçekleştirmek için kullanılır.
        private readonly RepositoryContext _context;
        private readonly Lazy<ICategoryRepository> _categoryRepository;

        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _categoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(_context));
         
        }

        public ICityRepository Cities => new CityRepository(_context);

        public ICategoryRepository Categories => _categoryRepository.Value;

        public IPlaceRepository Places => new PlaceRepository(_context);

        public ITravelList TravelList => new TravelListRepository(_context);

        public ITravelListContent TravelListContent => new TravelListContentRepository(_context);

        public IPlaceContentRepository PlaceContent => new PlaceContentRepository(_context);

        public void Save()
        {
            _context.SaveChanges();
        }
        
    }
}
