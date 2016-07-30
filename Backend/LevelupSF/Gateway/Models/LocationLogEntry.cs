using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace Gateway.Models
{
    public class LocationLogEntry : TableEntity
    {
        public LocationLogEntry(Guid routeId, float lat, float lon)
        {
            PartitionKey = routeId.ToString();
            RowKey = lat + lon.ToString();

            RouteId = routeId;
            Lat = lat.ToString();
            Lon = lon.ToString();
        }

        public Guid RouteId { get; set; }

        public string Lat { get; set; }

        public string Lon { get; set; }
    }
}