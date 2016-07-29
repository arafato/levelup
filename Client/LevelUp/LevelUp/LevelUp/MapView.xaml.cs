using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Forms;
using System.Net.Http;
using System.Diagnostics;
using Plugin.Geolocator;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LevelUp
{
    public partial class MapView : ContentPage
    {
        public MapView()
        {
            InitializeComponent();

            


        }

        public async Task<NavigationInformation> GetNextWaypoint()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            var position = await locator.GetPositionAsync(10000);

            var lat = position.Latitude;
            var lon = position.Longitude;



            HttpClient client = new HttpClient();

            var response = await client.GetStringAsync($"http://hackclusterup.westeurope.cloudapp.azure.com/api/navigation/GetNextNavigationPoint?latitude="+lat+"&longitude="+lon);
            JObject result = JObject.Parse(response);

            NavigationInformation ni = new NavigationInformation();

            foreach(var idontknow in result)
            {
                switch(idontknow.Key)
                {
                    case "Latitude":
                        ni.Latitude = Convert.ToDouble(idontknow.Value.ToString());
                        break;
                    case "Longitude":
                        ni.Longitude = Convert.ToDouble(idontknow.Value.ToString());
                        break;
                    case "Direction":
                        ni.Direction = Convert.ToInt32(idontknow.Value.ToString());
                        break;
                }
            }
            
            //dynamic result = JsonConvert.DeserializeObject(response);

            return ni;
        }

        public async void DoSomething(object a, object b)
        {
            var response = await GetNextWaypoint();

            switch(response.Direction)
            {
                case 10:
                    lbl_Label_label.Text = "VOOOOOOORWÄÄÄÄÄRTS";
                    break;
                case 20:
                    lbl_Label_label.Text = "links";
                    break;
                case 30:
                    lbl_Label_label.Text = "RÄÄÄÄÄÄÄÄCHTS";
                    break;
                default:
                    break;
            }
        }
    }

    public class NavigationInformation
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Direction { get; set; }
    }
}
