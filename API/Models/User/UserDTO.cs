namespace API.Models
{
    public class UserDTO
    {
        public string email { get; set; }
        public string password { get; set; }

        public string name { get; set; }
    }

    public class TokenGenDTO
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}