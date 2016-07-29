using System.Collections.Generic;
using System.Web.Http;

namespace Gateway.Controllers
{
    public class NavigationController : ApiController
    {
        [HttpGet]
        [ActionName("next")]
        public NavigationCommand GetNextNavigationPoint(float latitude, float longitude)
        {
            return new NavigationCommand
            {
                Direction = Direction.STRAIGHT,
                Latitude = 42.4242f,
                Longitude = 84.8484f,
            };
        }
    }
}
