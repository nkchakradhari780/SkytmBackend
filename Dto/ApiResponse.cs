using SkytmBackend.Models;

namespace SkytmBackend.Dto
{
    public class ApiResponse
    {
        public User Result { get; set; }
        public string Response { get; set; }
        public string ResponseCode { get; set; }

    }
}
