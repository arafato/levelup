using System;
using System.Web.Http;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;

namespace Gateway.Controllers
{
    internal class LiveController : ApiController
    {
        public void UpdateLocation(Guid routId, float latitude, float longitude)
        {
            var storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));
        }
    }
}