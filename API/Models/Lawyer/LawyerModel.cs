using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreDapper.Models
{
    public class Lawyer
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }
        
        public Lawyer(string name,string email ){
            Name = name;
            Email = email;
        }
    }
}
