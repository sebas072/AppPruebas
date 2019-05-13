using appPrueba.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace appPrueba.Services
{
    public class ApiService 
    {
        private string UrlPersonal { get; set; }
        public ApiService()
        {
            UrlPersonal = "http://directotesting.igapps.co/";
        }   
        public async Task<Response> APICosumeGet<T>(string url,string header = null)
        {
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(UrlPersonal)
                };
                if (!string.IsNullOrEmpty(header))
                {
                    client.DefaultRequestHeaders.Add("token", header);
                }
                var response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    var resulta = await response.Content.ReadAsStringAsync();
                    return new Response
                    {
                        IsSucces = false,
                        Message = "Error consumo",
                        Result = null
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                var resp = JsonConvert.DeserializeObject<T>(result);
                return new Response
                {
                    IsSucces = true,
                    Message = "Ok",
                    Result = resp
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSucces = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
