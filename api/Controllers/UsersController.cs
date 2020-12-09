using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Helpers;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UsersController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            try
            {
                var users = await _unitOfWork.UserRepository.GetUsersAsync();

                var result = _mapper.Map<IEnumerable<UserDto>>(users);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        // api/users/id
        [HttpGet("id/{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetUserByIdAsync(id);

                var result = _mapper.Map<UserDto>(user);

                return result;
            }
            catch(Exception ex)
            {
                throw(ex);
            }
        }
        // api/users/filterSort
        [HttpGet("filterSort", Name="GetUserFilteredSorted")]
        public async Task<ActionResult<UserDto>> GetUsers(FilterSortingParams sortingParams)
        {
            try
            {
                var user =  await _unitOfWork.UserRepository.GetUsersFilteredSortedAsync(sortingParams);

                var result = _mapper.Map<UserDto>(user);

                return result;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //api/user/update
        [HttpPut]
        public async Task<ActionResult> UpdateUser([FromBody] UserUpdateDto userUpdateDto)
        {
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(userUpdateDto.Id);

            _mapper.Map(userUpdateDto, user);

            _unitOfWork.UserRepository.UpdateUser(user);

            if (await _unitOfWork.Complete()) return NoContent();

            return BadRequest("Failed to update user");
        }

        //api/user
        [HttpDelete]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(id);

            if (user == null) return NotFound();

            _unitOfWork.UserRepository.DeleteUser(user);

            if (await _unitOfWork.Complete()) return NoContent();

            return BadRequest("Failed to update user");
        }
    }
}