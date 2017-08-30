using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkYourDay
{
    public class User
    {
        public string name { get; set; }
        public bool present { get; set; }
        public DateTime start { get; set; }
        public DateTime stop { get; set; }

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

    public class Model<T>
    {
        public int statuscode { get; set; }
        public string statusmessage { get; set; }
        public T data { get; set; }
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