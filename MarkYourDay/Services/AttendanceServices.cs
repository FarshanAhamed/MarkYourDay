using MarkYourDay.Helpers;
using MarkYourDay.Models;
using MarkYourDay.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkYourDay.Services
{
    public class AttendanceServices
    {
        public async static Task<ResponseModel<Attendance>> GetToday()
        {
            var userid = Settings.UserId;
            var result = await CommonService<Attendance>.HttpGetOperation("attendance/today/"+userid);
            return result;
        }

        public async static Task CheckIn(Punch punch)
        {
            var result = await CommonService<Attendance>.HttpGetOperation("attendance/checkin");
            if (result.data != null)
            {
                Settings.CheckedStatus = "CHECKED_IN";
                var first = result.data.firstcheckin;
                var second = result.data.secondcheckin;
                var fos = (second != DateTime.MinValue) ? second : first;
                Settings.CheckedTime = "Last Checked in at "+fos.ToLocalTime().ToString();
                await punch.DisplayAlert("Success", result.message, "OK");
            }
            else
            {
                await punch.DisplayAlert("There is a problem", result.message, "OK");
            }
        }

        public async static Task CheckOut(Punch punch)
        {
            var result = await CommonService<Attendance>.HttpGetOperation("attendance/checkout");
            if (result.data != null)
            {
                Settings.CheckedStatus = "CHECKED_OUT";
                var first = result.data.firstcheckout;
                var second = result.data.secondcheckout;
                var fos = (second != DateTime.MinValue) ? second : first;
                Settings.CheckedTime = "Last Checked in at " + fos.ToLocalTime().ToString();
                await punch.DisplayAlert("Success", result.message, "OK");
            }
            else
            {
                await punch.DisplayAlert("There is a problem", result.message, "OK");
            }
        }
    }
}
