using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class AccountController:BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext context,ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;

        }
        [HttpPost("register")]
        public async Task<ActionResult<int>> Register(RegisterDto registerDto)
        {

            if (await UserExist(registerDto.Email)) return BadRequest("Username is taken");

            using var hMac = new HMACSHA512();

            var newUser = new AppUser()
            {
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                Phone = registerDto.Phone,
                DateOfBirth =registerDto.DateOfBirth,
                Created = DateTime.UtcNow,
                PasswordHash = hMac.ComputeHash(Encoding.UTF8.GetBytes("P@$$w0rd")),
                PasswordSalt = hMac.Key
            };

            _context.Users.Add(newUser);

            var result = await _context.SaveChangesAsync();

            return result;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserCredentialDto>> Login(LoginDto login)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync(x => x.Email == login.Email);

            if (user == null) return Unauthorized("Invalid username");

            using var hMac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hMac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }

            return new UserCredentialDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };
        }
        private async Task<bool> UserExist(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email.ToLower() == email.ToLower());
        }
    }
}