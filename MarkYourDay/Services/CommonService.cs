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
        //public const string BaseUrl = "http://punchapi.azurewebsites.net/api/user";
        public const string BaseUrl = "http://localhost:49888/api/user";
        public async static Task<Model<T>> HttpGetOperation(string url)
        {
            try
            {
                if (await CrossConnectivity.Current.IsRemoteReachable("www.google.com", msTimeout: 10000))
                {
                    using (var client = new HttpClient(new NativeMessageHandler()))
                    {
                        client.BaseAddress = new Uri(BaseUrl);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                       /* if (!string.IsNullOrWhiteSpace(key))
                        {
                            client.DefaultRequestHeaders.Add("KEY", key);
                        }
                        */
                        var response = await client.GetAsync(url);
                        if (response.IsSuccessStatusCode)
                        {
                            var json = await response.Content.ReadAsStringAsync();
                            var result = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<Model<T>>(json));
                            return result;
                        }
                        else
                        {
                            return new Model<T>() { statuscode = (int)response.StatusCode, statusmessage = response.StatusCode.ToString() };
                        }
                    }
                }
                else
                {
                    var status = new Model<T>();
                    status.statusmessage = "Cannot Connect to the Internet";
                    return status;
                }
            }
            catch (JsonException e)
            {
                var status = new Model<T>();
                status.statusmessage = e.ToString();
                return status;
            }
            catch (Exception e)
            {
                var status = new Model<T>();
                status.statusmessage = e.ToString();
                return status;
            }
        }

        public static async Task<Model<T>> HttpPostOperation(string url, MultipartFormDataContent content=null, string body=null)
        {
            try
            {
                if (await CrossConnectivity.Current.IsRemoteReachable("www.google.com", msTimeout: 10000))
                {
                    using (var client = new HttpClient(new NativeMessageHandler()))
                    {
                        client.BaseAddress = new Uri(BaseUrl);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                       /* if (!string.IsNullOrWhiteSpace(key))
                        {
                            client.DefaultRequestHeaders.Add("KEY", key);
                        }*/
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
                            var status = new Model<T>();
                            status.statusmessage = "No Data!";
                            return status;
                        }
                        if (response.IsSuccessStatusCode)
                        {
                            var json = await response.Content.ReadAsStringAsync();
                            var result = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<Model<T>>(json));
                            return result;
                        }
                        else
                        {
                            return new Model<T>() { statuscode = (int)response.StatusCode, statusmessage = response.StatusCode.ToString() };
                        }
                    }
                }
                else
                {
                    var status = new Model<T>();
                    status.statusmessage = "Cannot Connect to the Internet";
                    return status;
                }
            }
            catch (JsonException e)
            {
                var status = new Model<T>();
                status.statusmessage = e.ToString();
                return status;
            }
            catch (Exception e)
            {
                var status = new Model<T>();
                status.statusmessage = e.ToString();
                return status;
            }
        }

        public static async Task<Model<T>> HttpPutOperation(string url, string body)
        {
            try
            {
                if (await CrossConnectivity.Current.IsRemoteReachable("www.google.com", msTimeout: 10000))
                {
                    using (var client = new HttpClient(new NativeMessageHandler()))
                    {
                        client.BaseAddress = new Uri(BaseUrl);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                      /*  if (!string.IsNullOrWhiteSpace(key))
                        {
                            client.DefaultRequestHeaders.Add("KEY", key);
                        }
                        */
                        HttpResponseMessage response = null;
                        if (body != null)
                        {
                            HttpContent hc = new StringContent(body, Encoding.UTF8, "application/json");
                            response = await client.PutAsync(url, hc);
                           
                        }
                        else
                        {
                            var status = new Model<T>();
                            status.statusmessage = "No Data!";
                            return status;
                        }
                        if (response.IsSuccessStatusCode)
                        {
                            var json = await response.Content.ReadAsStringAsync();
                            var result = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<Model<T>>(json));
                            return result;
                        }
                        else
                        {
                            return new Model<T>() { statuscode = (int)response.StatusCode, statusmessage = response.StatusCode.ToString() };
                        }
                    }
                }
                else
                {
                    var status = new Model<T>();
                    status.statusmessage = "Cannot Connect to the Internet";
                    return status;
                }
            }
            catch (JsonException e)
            {
                var status = new Model<T>();
                status.statusmessage = e.ToString();
                return status;
            }
            catch (Exception e)
            {
                var status = new Model<T>();
                status.statusmessage = e.ToString();
                return status;
            }
        }
    }
}

