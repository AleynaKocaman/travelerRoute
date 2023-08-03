using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IServiceManager
    {
        ICityService CityService { get; }
        ICategoryService CategoryService { get; }
        IPlaceService PlaceService { get; }
        IAuthenticationService AuthenticationService { get; }
        ITravalerListService TravalerListService { get; }
        ITravelListContentService TravalerListContentService { get; }
        IPlaceContentService PlaceContentService { get; }
    }
}
