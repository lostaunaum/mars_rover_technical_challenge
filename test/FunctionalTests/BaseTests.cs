using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using MarsRoverTechnicalChallenge.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace FunctionalTests
{
    public class BaseTests
    {
        public static string GetBaseUrl()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"appsettings.json");
            using (StreamReader r = new StreamReader(path))
            {
                var json = r.ReadToEnd();
                var jobj = JObject.Parse(json);

                return jobj["ServiceConfiguration"]["Host"].Value<string>();
            }
            
        }
    }
}
