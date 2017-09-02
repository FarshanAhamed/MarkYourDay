using MarkYourDay.Views;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkYourDay.Helpers
{
    public class WhereAmI
    {
        public static Double LATITUDE = 11.284108;
        public static Double LONGITUDE = 75.792672;
        static Double lat, lon;
        static DateTimeOffset time;
        public static double? dist = null;
       
        public static bool GetLocation()
        {
            FindLocation();

            if (dist <= 0.15)
            {
                Settings.OK = "Yes";
                return true;
            }
            else
                Settings.OK = "No";
            return false;
          
        }

        public static async void FindLocation()
        {
            var punchobj = new Punch(Settings.Username);
            
            if (CrossGeolocator.Current.IsListening)
                return;
            if (!CrossGeolocator.Current.IsGeolocationAvailable)
            {
                await punchobj.DisplayAlert("Sorry", "We Couldn't locate you, Enable Location Access", "OK");
                return;
            }
            if (!CrossGeolocator.Current.IsGeolocationEnabled)
            {
                await punchobj.DisplayAlert("Location", "Go to Settings , Enable Location Access ", "OK");
                return;
            }
            CrossGeolocator.Current.DesiredAccuracy = 1;
            var position = await CrossGeolocator.Current.GetPositionAsync(TimeSpan.FromSeconds(1));
            if (position != null)
            {
                time = position.Timestamp;
                lat = position.Latitude;
                lon = position.Longitude;
               
                
                dist = Distance(LATITUDE, LONGITUDE, lat, lon);
                
                //  await DisplayAlert("Distance", "Your distance from FC: " + dist, "OK");

            }
//CrossGeolocator.Current.PositionChanged += PositionChanged;

            bool result = await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(1), 1, true, new Plugin.Geolocator.Abstractions.ListenerSettings
            {
                ActivityType = Plugin.Geolocator.Abstractions.ActivityType.AutomotiveNavigation,
                AllowBackgroundUpdates = true,
                DeferLocationUpdates = true,
                DeferralDistanceMeters = 1,
                DeferralTime = TimeSpan.FromSeconds(1),
                ListenForSignificantChanges = true,
                PauseLocationUpdatesAutomatically = false
            });
            if (!result)
            {
                await punchobj.DisplayAlert("Sorry", "Unable to get your location, Try Again! ", "OK");
                return;
            }
            else
            {
                return;
            }
        }

        public static void PositionChanged(object sender, PositionEventArgs e)
        {
            // Device.BeginInvokeOnMainThread(() =>
            //{
            var pos = e.Position;
            time = pos.Timestamp;
            lat = pos.Latitude;
            lon = pos.Longitude;
          
            dist = Distance(LATITUDE, LONGITUDE, lat, lon);
          
            //  });
        }
        
        public static double Distance(double Latitude1, double Longitude1, double Latitude2, double Longitude2)
        {
            double R = 6371.0;          // R is earth radius.
            double dLat = toRadian(Latitude2 - Latitude1);
            double dLon = toRadian(Longitude2 - Longitude1);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(toRadian(Latitude1)) * Math.Cos(toRadian(Latitude2)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Asin(Math.Min(1, Math.Sqrt(a)));
            double d = R * c;

            return d;
        }

        private static double toRadian(double val)
        {
            return (Math.PI / 180) * val;
        }

    }
}
