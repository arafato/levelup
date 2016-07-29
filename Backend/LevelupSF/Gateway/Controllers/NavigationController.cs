using System.Collections.Generic;
using System.Web.Http;

namespace Gateway.Controllers
{
    public class NavigationController : ApiController
    {
        [HttpGet]
        public NavigationPoint GetNextNavigationPoint(int routeId, float latitude, float longitude)
        {
            return new NavigationPoint
            {
                Direction = Direction.NORTH,
                Latitude = 42.4242f,
                Longitude = 84.8484f,
            };
        }


    }
}
