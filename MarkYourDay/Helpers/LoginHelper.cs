using MarkYourDay.Models;
using MarkYourDay.Services;
using MarkYourDay.Views;
using System;
using System.Threading.Tasks;

namespace MarkYourDay.Helpers
{
    public class LoginHelper
    {
        public static bool CheckLoggedIn()
        {
            if (string.IsNullOrWhiteSpace(Settings.Username))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void ChangeUser(User data)
        { 
            if (!string.IsNullOrWhiteSpace(data.name))
            {
                Settings.Username = data.name;
            }
          /*  if (data.present)
            {
                Settings.AtFantacode = "Yes";
            }
            else
                Settings.AtFantacode = "No";
                */
        }

        public static void SaveUser(User data)
        {
           
            if (!string.IsNullOrWhiteSpace(data.name))
            {
                Settings.Username = data.name;
            }
            /*
            if (data.present)
            {
                Settings.AtFantacode = "Yes";
            }
            else
                Settings.AtFantacode = "No";
                */
        }
        public static async Task<bool> IsUserValid(string username, string password,Login login )
        {
           
            var item = await UserServices.GetUser(username,password);
            if (item.data != null)
            {
                
                Settings.Token = item.token;
                Settings.Username = item.data.name;
                Settings.UserId = item.data.id.ToString();
                return true;
            }
            else
            {
                await login.DisplayAlert("There is a problem", item.message.ToString(), "OK");
                return false;
            }
           
        }

        public static async Task GetToday()
        {
            var item = await AttendanceServices.GetToday();
            if (item.status == "NETWORK_DISABLED")
            {
                Settings.CheckedTime = "Can't connect to the network";
                Settings.CheckedStatus = "DEFAULT";
            }
            else if (item.data == null)
            {
                Settings.CheckedTime = "Nothing to display";
                Settings.CheckedStatus = "CHECKS_NULL";
            }
            else if (item.data.firstcheckin == DateTime.MinValue)
            {
                Settings.CheckedTime = "No Check-in found";
                Settings.CheckedStatus = "CHECKED_OUT";
            }
            else if (item.data.firstcheckout == DateTime.MinValue)
            {
                var date = item.data.firstcheckin;
                Settings.CheckedTime = "Last Checked in at " + date.ToLocalTime().ToString();
                Settings.CheckedStatus = "CHECKED_IN";
            }
            else if (item.data.secondcheckin == DateTime.MinValue)
            {
                var date = item.data.firstcheckout;
                Settings.CheckedTime = "Last Checked out at " + date.ToLocalTime().ToString();
                Settings.CheckedStatus = "CHECKED_OUT";
            }
            else if (item.data.secondcheckout == DateTime.MinValue)
            {
                var date = item.data.secondcheckin;
                Settings.CheckedTime = "Last Checked in at " + date.ToLocalTime().ToString();
                Settings.CheckedStatus = "CHECKED_IN";
            }
            else
            {
                var date = item.data.secondcheckout;
                Settings.CheckedTime = "Limits exceeded! "+Environment.NewLine+"Last Checked out at " + date.ToLocalTime().ToString();
                Settings.CheckedStatus = "CHECKS_FULL";
            }
        }
        
    }
}
