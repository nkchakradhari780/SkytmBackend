namespace SkytmBackend.Dto
{
    public class RegisterDto
    {
        public string Username  { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string password { get; set; }
        public string ImageUrl { get; set; }
        public bool IsAdmin { get; set; }        
    }
}
