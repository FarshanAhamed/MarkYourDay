using MarkYourDay.Views;
using Plugin.Connectivity;
using Plugin.Geolocator;
using System;
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
       
        public async Task<bool> GetLocation(Punch punch)
        {
            if (await CrossConnectivity.Current.IsRemoteReachable("www.google.com", msTimeout: 10000))
            {
                await this.FindLocation(punch);

                if (dist <= 0.15)
                {
                    return true;
                }
                else
                    punch.Init();
                    return false;
            }
            else
            {
                await punch.DisplayAlert("There is a problem", "Cannot connect to internet", "OK");
                return false;

            }

        }

        public async Task FindLocation(Punch punch)
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
                        var position = await CrossGeolocator.Current.GetPositionAsync(TimeSpan.FromSeconds(6));
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
                    await punchobj.DisplayAlert("Unknown error", "Please try again", "OK");
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
