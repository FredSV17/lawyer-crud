using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreDapper.Models
{
    public class LawyerDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }
        
    }
}
