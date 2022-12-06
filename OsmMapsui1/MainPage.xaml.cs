using Mapsui;
using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading;
using System.Threading.Tasks;
using Mapsui.UI.Forms;

namespace OsmMapsui1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            //inicjacja mapy
            var map = new Mapsui.Map
            {
                CRS = "EPSG:3857",
                Transformation = new Mapsui.Projection.MinimalTransformation()
            };

            //załadowanie warstwy z danymi OSM
            var tileLayer = Mapsui.Utilities.OpenStreetMap.CreateTileLayer();
            map.Layers.Add( tileLayer );

            //linijka skali
            var scaleBarWidget = new Mapsui.Widgets.ScaleBar.ScaleBarWidget(map)
            {
                TextColor = Mapsui.Styles.Color.Black,
                TextAlignment = Mapsui.Widgets.Alignment.Center,
                HorizontalAlignment = Mapsui.Widgets.HorizontalAlignment.Left,
                VerticalAlignment = Mapsui.Widgets.VerticalAlignment.Bottom 
            };
            map.Widgets.Add( scaleBarWidget );                       


            mapView.Map = map;

            //nowa pozycja
            var position1 = new Mapsui.UI.Forms.Position(53.1, 23.1);
            mapView.MyLocationLayer.UpdateMyLocation( position1 );

            //dane z aktualnej pozycji
            var myPosition = new Point( position1.Latitude, position1.Longitude );
            labelTytul.Text = "szer: " + myPosition.X.ToString()
                + " dł: " + myPosition.Y.ToString();

            //standardowy pin
            var position2 = new Mapsui.UI.Forms.Position(54.0, 24.0);
            var myPin1 = new Mapsui.UI.Forms.Pin
            {
                Type = PinType.Pin,
                Position = position2,
                Label = "54.0 24.0",
                Address = "pin 1",
                Color = Color.BlueViolet,
                Scale = 1f //domyślnie 1
            };           
            mapView.Pins.Add( myPin1 );




            //zmaiana koloru pinu i wiekości
            var myPin2 = new Mapsui.UI.Forms.Pin
            {
                Type = PinType.Pin,
                Position = new Mapsui.UI.Forms.Position(53.5, 23.5),
                Label = "53.5 23.5 ",
                Address = "pin 2",
                Color = Color.IndianRed,
                Scale= 0.3f          
            };
            mapView.Pins.Add(myPin2);

            //pokaż label ShowCallout
            var myPin3 = new Mapsui.UI.Forms.Pin
            {
                Type = PinType.Pin,
                Position = new Mapsui.UI.Forms.Position(55, 23),
                Label = "szer:55 dł:23",
                
                Address = "pin 3",
                Color = Color.Beige,
                Scale = 0.1f
            };
            mapView.Pins.Add(myPin3);
            myPin3.ShowCallout();


        }


        CancellationTokenSource cts;
        async Task GetCurrentLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected override void OnDisappearing()
        {
            if (cts != null && !cts.IsCancellationRequested)
                cts.Cancel();
            base.OnDisappearing();
        }




        private void EntryLatitudeLongitude_TextChanged(object sender, TextChangedEventArgs e)
        {
            double _lat = double.Parse(entryLatitude.Text);
            double _long = double.Parse(entryLongitude.Text);
            var position1 = new Mapsui.UI.Forms.Position(_lat, _long);
            mapView.MyLocationLayer.UpdateMyLocation(position1);
        }




    }
}
