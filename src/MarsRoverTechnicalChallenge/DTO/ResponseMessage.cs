
using System.Net;

namespace MarsRoverTechnicalChallenge.DTO
{
    public class ReponseMessage
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
    }
}
