using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Xamarin.Forms.Maps;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Xamarin;


namespace LevelUp.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            
            Xamarin.FormsMaps.Init("dKidOFKS88rqXrM2OAKB~FAq-gvUDQHEVEQ5iTOAnkA~At7QL3qhaVj09X93QijcpfhAIpokAXEQVnL4t6P9GoYwgd3a0uzunF7eu8sClzBO");

            LoadApplication(new LevelUp.App());
        }
    }
}
