using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkYourDay.Models
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        /*  public User(string username, bool atfantacode,DateTime starttime,DateTime? stoptime)
          {
              name = username;
              present = atfantacode;
              start =starttime;
              if (stoptime.HasValue)
                  stop = stoptime.Value;

          }
          */
    }

    

    public class WorkPlace
    {
        public Double latitude { get; set; }
        public Double longitude { get; set; }

        public WorkPlace(double lat, double lon)
        {
            latitude = lat;
            longitude = lon;
        }
    }

}