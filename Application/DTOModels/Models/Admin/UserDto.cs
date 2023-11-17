using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Application.DtoModels.Models.Admin
{
    public class UserDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? UserName { get; set; }
        
        [Required]
        public string? PhoneNumber { get; set; }

        [Required]
        public string? Address { get; set; }

        [Required]
        public string? City { get; set; }

        [Required]
        public string? State { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}