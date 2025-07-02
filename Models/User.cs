namespace SkytmBackend.Models
{
    public class User
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }           
        public string Gender { get; set; }
        public string password { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public bool IsAdmin { get; set; } = false;
    }
}
