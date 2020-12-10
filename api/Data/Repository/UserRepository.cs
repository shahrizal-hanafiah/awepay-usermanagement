using api.Extensions;
using api.Helpers;
using API.Entities;
using API.Interfaces;
using AutoMapper;
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

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<AppUser> AddUser(AppUser user)
        {
            await _context.AddAsync(user);
            return user;
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

        public async Task<IEnumerable<AppUser>> GetUsersFilteredSortedAsync(FilterSortingParams filterSortingParams)
        {
            var users = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(filterSortingParams.SearchByEmail)) users = users.Where(x => EF.Functions.Like(x.Email.ToLower(),
                "%" + filterSortingParams.SearchByEmail.ToLower() + "%"));

            if (!string.IsNullOrEmpty(filterSortingParams.SearchByPhone)) users = users.Where(x => EF.Functions.Like(x.Phone.ToLower(),
                "%" + filterSortingParams.SearchByPhone.ToLower() + "%"));

            if (!string.IsNullOrEmpty(filterSortingParams.SortBy)) users = users.Sort(filterSortingParams.SortBy);

            var latestUsers = await users.ToListAsync();

            return latestUsers;

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
