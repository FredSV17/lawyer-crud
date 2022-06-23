using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class LawyerDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }
        
    }
}
