using MarkYourDay.Services;
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
        public async Task<bool> IsUserValid(string username, string password)
        {
            bool result = true;
            var item = await UserServices.GetUser(username,password);
            if (item == null)
            {
                result = false;
            }
            return result;
        }
        
    }
}
