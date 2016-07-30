using System;
using System.Web.Http;
using Gateway.Models;
using Microsoft.Azure;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using RouteActor.Interfaces;
using RouteActor.Interfaces.Entities;

namespace Gateway.Controllers
{
    public class LiveController : ApiController
    {
        [HttpPost]
        public Waypoint UpdateLocation(Guid routId, float latitude, float longitude)
        {
            var storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the table client.
            var tableClient = storageAccount.CreateCloudTableClient();

            // Retrieve a reference to the table.
            var table = tableClient.GetTableReference("locationLog");

            // Create the table if it doesn't exist.
            table.CreateIfNotExists();

            var log = new LocationLogEntry(routId, latitude, longitude);
            log.Lat = latitude.ToString();
            log.Lon = longitude.ToString();

            // Create the TableOperation object that inserts the customer entity.
            var insertOperation = TableOperation.Insert(log);

            // Execute the insert operation.
            table.Execute(insertOperation);

            var proxy = ActorProxy.Create<IRouteActor>(new ActorId(routId), "fabric:/Application1");
            
            return proxy.GetNextWaypoint();
        }
    }
}