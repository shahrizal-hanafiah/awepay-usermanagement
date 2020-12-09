using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users
                .ToListAsync();
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByEmailAsync(string searchString,string sortOrder)
        {
            var users =  _context.Users.AsQueryable();

            if (searchString != null)
            {
                users = users.Where(x => x.Email.ToLower() == searchString.ToLower() || x.Phone == searchString).AsQueryable();
            }

            switch (sortOrder)
            {
                case "email_desc":
                    users = users.OrderByDescending(s => s.Email).ToList();
                    break;
                case "FullName":
                    users = users.OrderBy(s => s.FullName).ToList();
                    break;
                case "fullname_desc":
                    users = users.OrderByDescending(s => s.FullName).ToList();
                    break;
                default:
                    users = users.OrderBy(s => s.Email).ToList();
                    break;
            }
        }

        public void UpdateUser(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public void DeleteUser(AppUser user)
        {
            _context.Remove(user);
        }
    }
}
