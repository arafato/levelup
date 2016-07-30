using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using RouteActor.Interfaces;
using RouteActor.Interfaces.Entities;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace RouteActor
{
    /// <remarks>
    /// This class represents an actor.
    /// Every ActorID maps to an instance of this class.
    /// The StatePersistence attribute determines persistence and replication of actor state:
    ///  - Persisted: State is written to disk and replicated.
    ///  - Volatile: State is kept in memory only and replicated.
    ///  - None: State is kept in memory only and not replicated.
    /// </remarks>
    [StatePersistence(StatePersistence.Persisted)]
    internal class RouteActor : Actor, IRouteActor
    {
        private const string BingMapsKey = "Aqwu5og5QECPomMpIne-33shXpCHewspjoXWZM4y0dTq4xvNB8nkm0xAZUC_54u9";

        /// <summary>
        /// This method is called whenever an actor is activated.
        /// An actor is activated the first time any of its methods are invoked.
        /// </summary>
        protected override Task OnActivateAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "Actor activated.");

            // The StateManager is this actor's private state store.
            // Data stored in the StateManager will be replicated for high-availability for actors that use volatile or persisted state storage.
            // Any serializable object can be saved in the StateManager.
            // For more information, see http://aka.ms/servicefabricactorsstateserialization

            this.StateManager.TryAddStateAsync("waypoints", new List<Waypoint>());
            return this.StateManager.TryAddStateAsync("index", 0);
        }

        /// <summary>
        /// TODO: Replace with your own actor method.
        /// </summary>
        /// <returns></returns>
        Task<int> IRouteActor.GetCountAsync()
        {
            return this.StateManager.GetStateAsync<int>("count");
        }

        /// <summary>
        /// TODO: Replace with your own actor method.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        Task IRouteActor.SetCountAsync(int count)
        {
            // Requests are not guaranteed to be processed in order nor at most once.
            // The update function here verifies that the incoming count is greater than the current count to preserve order.
            return this.StateManager.AddOrUpdateStateAsync("count", count, (key, value) => count > value ? count : value);
        }

        public async Task<RouteEntity> GetRoute(float longitude, float latitude, string tolocation, Guid idGuid)
        {
            return await InitializeRoute(longitude,latitude, tolocation, idGuid);
        }

        private async Task<RouteEntity> InitializeRoute(float longitude, float latitude, string tolocation, Guid idGuid)
        {
            //Go to Bing check for Location?
            HttpClient client = new HttpClient();

            //TODO: Set startpoint and endpoint

            string uri = $"http://dev.virtualearth.net/REST/V1/Routes/Walking?wp.0=Eiffel%20Tower&wp.1=louvre%20museum&optmz=distance&output=json&key={BingMapsKey}";
            //client.BaseAddress = new Uri(uri);
            var response = await client.GetStringAsync(uri);

            var value = JObject.Parse(response);
            IList<JToken> waypoints = value["resourceSets"].Children()["resources"].Children()["routeLegs"].Children()["itineraryItems"].Values().ToList();

            RouteEntity re = new RouteEntity();
            re.Id = idGuid;

            decimal coordinate;

            var dummy = value["resourceSets"].Children()["resources"].Children()["routeLegs"].Children()["actualStart"].Children().ToList();
            var longt = dummy[1].First[0];
            var latit = dummy[1].First[1];
            if (longt.Type == JTokenType.Float || longt.Type == JTokenType.Integer)
            {
                re.StartLongitude = longt.ToObject<decimal>();
            }
            if (latit.Type == JTokenType.Float || latit.Type == JTokenType.Integer)
            {
                re.StartLatitude = latit.ToObject<decimal>();
            }

            var enddummy = value["resourceSets"].Children()["resources"].Children()["routeLegs"].Children()["actualEnd"].Children().ToList();
            var longe = dummy[1].First[0];
            var latie = dummy[1].First[1];
            if (longe.Type == JTokenType.Float || longe.Type == JTokenType.Integer)
            {
                re.EndLongitue = longe.ToObject<decimal>();
            }
            if (latie.Type == JTokenType.Float || latie.Type == JTokenType.Integer)
            {
                re.EndLatitude = latit.ToObject<decimal>();
            }

            var distancedummy = value["resourceSets"].Children()["resources"].Children()["routeLegs"].Children()["actualEnd"].Children().ToList();

            List<Waypoint> wpList = new List<Waypoint>();

            foreach (var wpoint in waypoints)
            {
                Waypoint w = new Waypoint();

                var comp = wpoint["compassDirection"].ToObject<string>();
                w.CompassDirection = comp;

                var p = wpoint["maneuverPoint"].ToList();
                var longi = p[1].First[0];
                var lati = p[1].First[1];

                w.ManeuverPointLong = longi.ToObject<decimal>();
                w.ManeuverPointLat = lati.ToObject<decimal>();

                wpList.Add(w);

            }

            re.Waypoints = wpList;

            //Return Route
            return re;
        }
    }
}
