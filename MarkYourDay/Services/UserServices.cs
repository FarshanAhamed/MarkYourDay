using MarkYourDay.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MarkYourDay.Services
{
    public class UserServices
    {
      /*  public async static Task<ResponseModel<List<User>>> GetAllUsers()
        {
            var result = await CommonService<List<User>>.HttpGetOperation("");
            return result;
        }*/

         public async static Task<ResponseModel<User>> GetUser(string username,string password)
            {
            User user = new User
            {
                username = username,
                password = password
            };
            string body = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(user));
            var result = await CommonService<User>.HttpPutOperation("user/authenticate",body);
            return result;
            } 

        //public async static Task<ResponseModel<User>> CreateUser(string body)
        //{
        //    var result = await CommonService<User>.HttpPostOperation("",null,body);
        //    return result;
        //}
        //public async static Task<ResponseModel<User>> UpdateDetails(string body)
        //{
        //    var result = await CommonService<User>.HttpPutOperation("/"+Settings.Username, body);
        //    return result;
        //}
    }
}
