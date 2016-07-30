using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using RouteActor.Interfaces;
using RouteActor.Interfaces.Entities;

namespace Gateway.Controllers
{
    public class NavigationController : ApiController
    {
        [HttpGet]
        public NavigationPoint GetNextNavigationPoint(int routeId, float latitude, float longitude)
        {
            //return InitializeRoute();

            return new NavigationPoint
            {
                Direction = Direction.NORTH,
                Latitude = 42.4242f,
                Longitude = 84.8484f,
            };
        }

        [HttpGet]
        private RouteEntity InitializeRoute(float lat, float longitude, string destination)
        {
            //TODO: Create if not exists to location 
            //unigque Actor
            var id = Guid.NewGuid();
            var proxy = ActorProxy.Create<IRouteActor>(new ActorId(id), "fabric:/Application1");
            var initializeRoute = proxy.GetRoute(destination).Result;
            if (initializeRoute != null) return initializeRoute;
            throw new HttpException("Bad Request");
        }


    }

}
