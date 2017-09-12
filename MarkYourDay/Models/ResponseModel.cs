using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkYourDay.Models
{
    public class ResponseModel<T>
    {
        public string status { get; set; }
        public string message { get; set; }
        public T data { get; set; }
        public string token { get; set; }

        public ResponseModel()
        {

        }

        public ResponseModel(string status, string message)
        {
            this.status = status;
            this.message = message;
        }

        public ResponseModel(string status, string message, T data)
        {
            this.data = data;
            this.status = status;
            this.message = message;
        }
        public ResponseModel(string status, string message, T data, string token)
        {
            this.data = data;
            this.status = status;
            this.message = message;
            this.token = token;
        }
    }
}
