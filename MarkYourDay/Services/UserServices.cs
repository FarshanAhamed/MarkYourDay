using MarkYourDay.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkYourDay.Services
{
    public class UserServices
    {
      /*  public async static Task<Model<List<User>>> GetAllUsers()
        {
            var result = await CommonService<List<User>>.HttpGetOperation("");
            return result;
        }*/

         public async static Task<Model<User>> GetUser(string username,string password)
            {
               var result = await CommonService<User>.HttpGetOperation("/"+username+"/"+password);
                return result;
            } 

        public async static Task<Model<User>> CreateUser(string body)
        {
            var result = await CommonService<User>.HttpPostOperation("",null,body);
            return result;
        }
        public async static Task<Model<User>> UpdateDetails(string body)
        {
            var result = await CommonService<User>.HttpPutOperation("/"+Settings.Username, body);
            return result;
        }
    }
}
