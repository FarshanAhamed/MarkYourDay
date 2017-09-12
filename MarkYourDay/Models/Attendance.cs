using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkYourDay.Models
{
    public class Attendance
    {
        public int id { get; set; }
        public int userid { get; set; }
        public DateTime date { get; set; }
        public DateTime firstcheckin { get; set; }
        public DateTime firstcheckout { get; set; }
        public DateTime secondcheckin { get; set; }
        public DateTime secondcheckout { get; set; }
        
        public Attendance()
        {

        }
    }
}
