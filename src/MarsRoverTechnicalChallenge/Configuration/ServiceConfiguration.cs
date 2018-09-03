using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRoverTechnicalChallenge.Configuration
{
    public class ServiceConfiguration
    {
        public string Host { get; set; }
        public Dictionary<string, string> ServiceEndpoints { get; set; }
        public Dictionary<string, string> Settings { get; set; }
    }
}
