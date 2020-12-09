using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class UserUpdateDto
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [EmailAddress,Required]
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
    }
}