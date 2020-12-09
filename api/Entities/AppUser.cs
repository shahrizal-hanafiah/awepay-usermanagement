using API.Extensions;
using System;
using System.Collections.Generic;

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}