using System;
using System.Web.Http;
using Gateway.Models;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Gateway.Controllers
{
    public class LiveController : ApiController
    {
        [HttpPost]
        public void UpdateLocation(Guid routId, float latitude, float longitude)
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
        }
    }
}