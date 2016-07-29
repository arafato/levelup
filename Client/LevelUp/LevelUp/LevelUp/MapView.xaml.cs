using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Forms;
using System.Net.Http;
using System.Diagnostics;

namespace LevelUp
{
    public partial class MapView : ContentPage
    {
        public MapView()
        {
            InitializeComponent();

            


        }

        public async Task<string> GetNextWaypoint()
        {
            HttpClient client = new HttpClient();

            var response = await client.GetStringAsync($"http://hackclusterup.westeurope.cloudapp.azure.com/api/navigation/GetNextNavigationPoint?latitude=1&longitude=1");

            Device.BeginInvokeOnMainThread(() => {
                Debug.WriteLine("Hello");
                }
            );

            return response;
        }

        public async void DoSomething(object a, object b)
        {
            var response = await GetNextWaypoint();
        }
    }
}
