using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Lawyer
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }
        
        public Lawyer(){}
        public Lawyer(int id,string name,string email ){
            Id = id;
            Name = name;
            Email = email;
        }
        public Lawyer(string name,string email ){
            Name = name;
            Email = email;
        }
    }
}
