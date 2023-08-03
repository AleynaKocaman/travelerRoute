using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repositories.Abstract;
using Repositories.EFCore;
using Services.Abstract;
using Services.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICategoryService> _categoryService;
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly IRepositoryManager _repositoryManager;
 
        public ServiceManager(IRepositoryManager repositoryManager,
            IMapper mapper,
            IConfiguration configuration,
            UserManager<User> userManager,
            ILoggerService logger)
        {
           
            _repositoryManager = repositoryManager;
            _categoryService = new Lazy<ICategoryService>(() => new CategoryManager(repositoryManager));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationManager(userManager, mapper, configuration));
          
        }

       // public ICityService CityService => _cityService.Value;
        public ICityService CityService => new CityManager(_repositoryManager);

        public ICategoryService CategoryService => _categoryService.Value;

        public IPlaceService PlaceService => new PlaceManager(_repositoryManager);

        public IAuthenticationService AuthenticationService => _authenticationService.Value;

        public ITravalerListService TravalerListService => new TravalerListManager(_repositoryManager);

        public ITravelListContentService TravalerListContentService => new TravelListContentManager(_repositoryManager);

        public IPlaceContentService PlaceContentService => new PlaceContentManager(_repositoryManager);
    }
}
