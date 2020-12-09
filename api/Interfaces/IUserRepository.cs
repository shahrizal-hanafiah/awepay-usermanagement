using api.Helpers;
using API.DTOs;
using API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        Task<AppUser> AddUser(AppUser user);
        void UpdateUser(AppUser user);
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByIdAsync(int id);
        Task<IEnumerable<AppUser>> GetUsersFilteredSortedAsync(FilterSortingParams filterSortingParams);
        void DeleteUser(AppUser user);
    }
}
