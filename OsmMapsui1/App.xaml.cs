using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OsmMapsui1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Page0();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
