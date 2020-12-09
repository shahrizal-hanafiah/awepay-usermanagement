using API.Entities;
using System;
using System.Collections.Generic;

namespace API.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime Created { get; set; }
    }
}
