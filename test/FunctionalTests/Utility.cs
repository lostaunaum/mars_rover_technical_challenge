using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalTests
{
    public static class Utility
    {
        public static async Task<HttpResponseMessage> GetAsync(string uri)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(uri);
            
            return response;
        }
        public static async Task<HttpResponseMessage> PostAsync(string uri)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync(uri, null);

            return response;
        }

        public static async Task<HttpResponseMessage> PutAsync(string uri)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PutAsync(uri, null);

            return response;
        }

        public static async Task<HttpResponseMessage> DeleteAsync(string uri)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.DeleteAsync(uri);

            return response;
        }
    }
}
