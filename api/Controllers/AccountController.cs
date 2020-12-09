using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(DataContext context,ITokenService tokenService, IUnitOfWork unitOfWork)
        {
            _context = context;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }
        [HttpPost("register")]
        public async Task<ActionResult<int>> Register(RegisterDto registerDto)
        {

            if (await UserExist(registerDto.Email)) return BadRequest("Email already exist");

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

            var user = await _unitOfWork.UserRepository.AddUser(newUser);

            if (await _unitOfWork.Complete()) return Ok(user.Id);

            return BadRequest("Something went wrong during adding the user");
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