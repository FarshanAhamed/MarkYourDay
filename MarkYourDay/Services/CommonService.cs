using MarkYourDay.Helpers;
using MarkYourDay.Models;
using ModernHttpClient;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;



namespace MarkYourDay.Services
{
    public class CommonService<T>
    {
//if DEBUG
  //      public const string BaseUrl = "http://192.168.31.6:49888/api/user";
//#else
        public const string BaseUrl = "http://punchapi.azurewebsites.net/api/";
        public const string apiKey = "c6ae8c42-5452-46ec-af3e-03a621ae43e0";
//#endif
        public async static Task<ResponseModel<T>> HttpGetOperation(string url)
        {
            try
            {
                if (await CrossConnectivity.Current.IsRemoteReachable("www.google.com", msTimeout: 10000))
                {
                    using (var client = new HttpClient(new NativeMessageHandler()))
                    {
                        client.BaseAddress = new Uri(BaseUrl);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Add("API-KEY", apiKey);
                        if (!string.IsNullOrWhiteSpace(Settings.Token))
                        {
                            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Settings.Token);
                        }

                        var response = await client.GetAsync(url);
                        if (response.IsSuccessStatusCode)
                        {
                            var json = await response.Content.ReadAsStringAsync();
                            var result = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<ResponseModel<T>>(json));
                            return result;
                        }
                        else
                        {
                            return new ResponseModel<T>(response.StatusCode.ToString(), response.StatusCode.ToString());
                        }
                    }
                }
                else
                {
                    var status = new ResponseModel<T>(null, "Cannot Connect to the Internet");
                    return status;
                } 
            }
            catch (JsonException e)
            {
                var status = new ResponseModel<T>(null,e.ToString());
                return status;
            }
            catch (Exception e)
            {
                var status = new ResponseModel<T>(null,e.ToString());
                return status;
            }
        }

        public static async Task<ResponseModel<T>> HttpPostOperation(string url, MultipartFormDataContent content=null, string body=null)
        {
            try
            {
                if (await CrossConnectivity.Current.IsRemoteReachable("www.google.com", msTimeout: 10000))
                {
                    using (var client = new HttpClient(new NativeMessageHandler()))
                    {
                        client.BaseAddress = new Uri(BaseUrl);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Add("API-KEY", apiKey);
                        if (!string.IsNullOrWhiteSpace(Settings.Token))
                        {
                            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Settings.Token);
                        }
                        HttpResponseMessage response = null;
                        if (content != null)
                        {
                            response = await client.PostAsync(url, content);
                        }
                        else if (body != null)
                        {
                            HttpContent hc = new StringContent(body, Encoding.UTF8, "application/json");
                            response = await client.PostAsync(url, hc);
                        }
                        else
                        {
                            var status = new ResponseModel<T>(null,"No Data");
                            return status;
                        }
                        if (response.IsSuccessStatusCode)
                        {
                            var json = await response.Content.ReadAsStringAsync();
                            var result = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<ResponseModel<T>>(json));
                            return result;
                        }
                        else
                        {
                            return new ResponseModel<T>(response.StatusCode.ToString(),response.StatusCode.ToString() );
                        }
                    }
                }
                else
                {
                    var status = new ResponseModel<T>(null, "Cannot Connect to the Internet");
                    return status;
                }
            }
            catch (JsonException e)
            {
                var status = new ResponseModel<T>(null,e.ToString());
                return status;
            }
            catch (Exception e)
            {
                var status = new ResponseModel<T>(null,e.ToString());
                return status;
            }
        }

        public static async Task<ResponseModel<T>> HttpPutOperation(string url, string body)
        {
            try
            {
                if (await CrossConnectivity.Current.IsRemoteReachable("www.google.com", msTimeout: 10000))
                {
                    using (var client = new HttpClient(new NativeMessageHandler()))
                    {
                        client.BaseAddress = new Uri(BaseUrl);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Add("API-KEY", apiKey);
                        if (!string.IsNullOrWhiteSpace(Settings.Token))
                        {
                            client.DefaultRequestHeaders.Add("Authorization", "Bearer "+Settings.Token);
                            client.DefaultRequestHeaders.Add("API_KEY", apiKey);
                        }
                        
                        HttpResponseMessage response = null;
                        if (body != null)
                        {
                            HttpContent hc = new StringContent(body, Encoding.UTF8, "application/json");
                            response = await client.PutAsync(url, hc);
                           
                        }
                        else
                        {
                            var status = new ResponseModel<T>(null,"No Data");
                            return status;
                        }
                        if (response.IsSuccessStatusCode)
                        {
                            var json = await response.Content.ReadAsStringAsync();
                            var result = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<ResponseModel<T>>(json));
                            return result;
                        }
                        else
                        {
                            return new ResponseModel<T>(null,"Incorrect login details" );
                        }
                    }
                }
                else
                {
                    var status = new ResponseModel<T>(null, "Cannot Connect to the Internet");
                    return status;
                }
            }
            catch (JsonException e)
            {
                var status = new ResponseModel<T>(null,e.ToString());
                return status;
            }
            catch (Exception e)
            {
                var status = new ResponseModel<T>(null,e.ToString());
                return status;
            }
        }
    }
}

