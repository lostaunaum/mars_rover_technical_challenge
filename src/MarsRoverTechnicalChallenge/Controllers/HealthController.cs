using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MarsRoverTechnicalChallenge.Controllers
{
    [Route("api/v1/[controller]")]
    public class HealthController : Controller
    {
        // GET /health
        // Returns a dictionary object with the different health check results
        // Normally we would attempt to connect to a database by getting a row of data or running a dummy store proc
        [ProducesResponseType(typeof(IDictionary<string, string>), 200)]
        [HttpGet]
        public Dictionary<string, string> Get()
        {
            var isConnected = true;
            var result = new Dictionary<string, string>();

            if (isConnected)
            {
                result.Add("Service HealthCheck", "Service is listening on port 28080");
                result.Add("Database Connection HealthCheck", "Database is alive and connected");

                return result;
            }
            else
            {
                result.Add("Service HealthCheck", "Service is listening on port 28080");
                result.Add("Database Connection HealthCheck", "Database connection failed");

                return result;
            }   
        }
    }
}
