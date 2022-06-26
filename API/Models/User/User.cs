namespace API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string role { get; set; }

        public User(){}
        public User(string name,string email,string password, string role){
            this.name = name;
            this.email = email;
            this.password = password;
            this.role = role;
        }
    }
}