using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Lawyer
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Email { get; set; }

        public DateTime CreatedAt { get; set;}

        public Lawyer(){}
        public Lawyer(int id,string name,string email,DateTime createdAt ){
            Id = id;
            Name = name;
            Email = email;
            CreatedAt = createdAt;
        }
        public Lawyer(string name,string email,DateTime createdAt ){
            Name = name;
            Email = email;
            CreatedAt = createdAt;
        }
    }
}
