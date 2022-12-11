using Mapsui;
using Mapsui.UI.Forms;

using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OsmMapsui1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page0 : ContentPage
	{
        //inicjacja mapy
        Mapsui.Map map = new Mapsui.Map
        {
            CRS = "EPSG:3857",
            Transformation = new Mapsui.Projection.MinimalTransformation()
        };

        Pin pin0= new Pin();


        public Page0 ()
		{
			InitializeComponent ();

            map = MapLayerOSM(map);
            map = MapWiggets(map);
            mapView.Map = map;

            //czy pokazywać kółeczko/strzałkę obecnego położenia
            mapView.MyLocationEnabled= true;

            //czy wyśrodkować mapę na mojej lokalizacji
            mapView.MyLocationFollow= true;

            //jak tak to pokazuje ruszającą się strzałkę
            //jak nie to wyświetla kółeczko lokalizacji
            mapView.MyLocationLayer.IsMoving= true;




            //  labelTytul.Text = "" + mapView.MyLocationLayer.Resolutions.ToString();


            /*
            mapView.Navigator.NavigateTo(
  //              new Mapsui.UI.Forms.Position(51,23 ) , 50.0

                );
  */


            //powiększanie/zmniejszanie skali mapy
            //zakres od 0/cały świat/ do 20/średni budynek/ 


            //mapView.Navigator.ZoomTo(20);
            //MapTransferLocation(50, 23);



            //

            PinAdd(52,22, "label01", "adres01", Color.Red, true);
            PinAdd(53,23, "label02", "adres02", Color.Blue, true);
            PinAdd(53,24, "label03", "adres03", Color.Yellow, false);

            //pin bez label
            mapView.Pins.Add(new Pin
                {
                    Position = new Mapsui.UI.Forms.Position(50, 20),
                    Label = "label1",
                    Address = "adress1",
                    Color = Color.Blue,
                    Scale = .6f
                }
            );

            //pin z labelem
            mapView.Pins.Add(new Pin
            {
                Position = new Mapsui.UI.Forms.Position(51, 20),
                Label = "label2",
                Address = "adress2",
                Color = Color.Blue,
                Scale = .5f
            }
           );
            mapView.Pins[mapView.Pins.Count - 1].ShowCallout();
           
                     

                

        }

        void PinAdd(double latitude, double longitude, 
            string label, string adress, 
            Color color, bool showCallout)  
        {
            Pin pin= new Pin();
            pin.Position = new Mapsui.UI.Forms.Position(latitude, longitude);
            pin.Label = label;            
            pin.Address = adress;
            pin.Color = color;
            
            mapView.Pins.Add(pin);
            if (showCallout) mapView.Pins[mapView.Pins.Count - 1].ShowCallout();
        }




     //
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var location = await Geolocation.GetLastKnownLocationAsync();

            if (location != null)
            {
                Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
            }
            else
            {
                labelTytul.Text = "brak sygnału GPS i WiFi";
            }

            mapView.MyLocationLayer.UpdateMyLocation(new Position(location.Latitude, location.Longitude), true);
        }

      







        void MapTransferLocation(double latitude, double longitude)
        {
            //wymusza i przenosi koleczko pozycji na nowe miejsce
            mapView.MyLocationLayer.UpdateMyLocation(
                new Mapsui.UI.Forms.Position(latitude, longitude)
                );
        }





        Mapsui.Map MapWiggets(Mapsui.Map map)
        {
            //linijka skali
            var scaleBarWidget = new Mapsui.Widgets.ScaleBar.ScaleBarWidget(map)
            {
                TextColor = Mapsui.Styles.Color.Red,
                TextAlignment = Mapsui.Widgets.Alignment.Center,
                HorizontalAlignment = Mapsui.Widgets.HorizontalAlignment.Left,
                VerticalAlignment = Mapsui.Widgets.VerticalAlignment.Bottom
            };
            map.Widgets.Add(scaleBarWidget);

            //przyciski powiększania/zmniejszania mapy
            // var plusMinusZoom = new ZoomInOutWidget(map);
           // Mapsui.Widgets.Zoom.ZoomInOutWidget(map) { };



            return map;
        }


        Mapsui.Map MapLayerOSM( Mapsui.Map map )
        {
            //załadowanie warstwy z danymi OSM
            var tileLayer = Mapsui.Utilities.OpenStreetMap.CreateTileLayer();
            map.Layers.Add(tileLayer);
            return map;
        }




        private void mapView_MapClicked(object sender, MapClickedEventArgs e)
        {

            //połozenie klikniętego punktu
            labelTytul.Text = e.Point.Latitude.ToString() + " "+
                 e.Point.Longitude.ToString();
            
            //labelTytul.Text = "" + mapView.MyLocationLayer.ViewingDirection;                       

        }

        private void mapView_PinClicked(object sender, PinClickedEventArgs e)
        {
            labelTytul.Text = "kliknięto pin"+ e.Pin.Address;
        }
    }
}