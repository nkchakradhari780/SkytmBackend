using Microsoft.Identity.Client;

namespace SkytmBackend.Dto
{
    public class LoginDto
    {
        public string PhoneNumber { get; set; }
        public string password { get; set; }

    }
}
