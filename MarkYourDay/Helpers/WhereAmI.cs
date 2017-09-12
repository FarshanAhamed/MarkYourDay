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
       
        public async static Task<bool> GetLocation(Punch punch)
        {
            await FindLocation(punch);

            if (dist <= 0.15)
            {
                return true;
            }
            else
                return false;

        }

        public static async Task FindLocation(Punch punch)
        {
            var punchobj = punch;
            try
            {
                if (CrossGeolocator.Current.IsListening)
                    return;
                else if (!CrossGeolocator.Current.IsGeolocationAvailable)
                {
                    await punchobj.DisplayAlert("Sorry", "We Couldn't locate you, Enable Location Access", "OK");
                    return;
                }
                else if (!CrossGeolocator.Current.IsGeolocationEnabled)
                {
                    await punchobj.DisplayAlert("Location", "Go to Settings , Enable Location Access ", "OK");
                    return;
                }
                else
                {
                    CrossGeolocator.Current.DesiredAccuracy = 1;
                    var position = await CrossGeolocator.Current.GetPositionAsync(TimeSpan.FromSeconds(1));
                    if (position != null)
                    {
                        time = position.Timestamp;
                        lat = position.Latitude;
                        lon = position.Longitude;


                        dist = Distance(LATITUDE, LONGITUDE, lat, lon);
                        if (dist > 0.15)
                        {
                            await punchobj.DisplayAlert("Sorry", "You are not at Fantacode", "OK");
                        }

                    }
                }
               
            }
            catch (Exception ex)
            {
                await punchobj.DisplayAlert("Unknown error", "Error: " + ex.Message.ToString(), "OK");
                return;
            }
        }
            
 
       /* public static void PositionChanged(object sender, PositionEventArgs e)
        {
            // Device.BeginInvokeOnMainThread(() =>
            //{
            var pos = e.Position;
            time = pos.Timestamp;
            lat = pos.Latitude;
            lon = pos.Longitude;
          
            dist = Distance(LATITUDE, LONGITUDE, lat, lon);
          
            //  });
        }*/
        
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
