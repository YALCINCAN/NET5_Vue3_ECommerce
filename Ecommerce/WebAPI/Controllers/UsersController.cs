using Business.Abstract;
using Core.ActionFilters;
using Core.Utilities.Responses.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet("getuser/{userid}")]
        public async Task<IResponse> GetUser(string userid)
        {
            var user = await _userService.GetUser(userid);
            return user;
        }

        [Authorize]
        [HttpGet("getuserswithroles")]
        public async Task<IResponse> GetUsersWithRoles()
        {
            var userswithroles = _userService.GetUsersWithRoles();
            return await userswithroles;
        }

        [Authorize]
        [HttpPut]
        [ServiceFilter(typeof(NullFilterAttribute))]
        public async Task<IResponse> UpdateProfile(UpdateProfileDTO model)
        {
            return await _userService.UpdateProfile(model);
        }

        [Authorize]
        [HttpPut("changepassword")]
        [ServiceFilter(typeof(NullFilterAttribute))]
        public async Task<IResponse> ChangePassword(ChangePasswordDTO model)
        {
            return await _userService.ChangePassword(model);
        }

        [HttpPost("forgotpassword")]
        [ServiceFilter(typeof(NullFilterAttribute))]
        public async Task<IResponse> ForgotPassword(ForgotPasswordDTO model)
        {
            var result = await _userService.ForgotPassword(model);
            return result;
        }
        [HttpPost("resetpassword")]
        [ServiceFilter(typeof(NullFilterAttribute))]
        public async Task<IResponse> ResetPassword(ResetPasswordDTO model)
        {
            var result = await _userService.ResetPassword(model);
            return result;
        }

        [Authorize]
        [HttpPut("updateuserbyadmin")]
        [ServiceFilter(typeof(NullFilterAttribute))]
        public async Task<IResponse> UpdateUserByAdmin(UserDTO model)
        {
            var result = await _userService.UpdateUserByAdmin(model);
            return result;
        }

        [Authorize]
        [HttpPut("passwordchangebyadmin")]
        [ServiceFilter(typeof(NullFilterAttribute))]
        public async Task<IResponse> PasswordChangeByAdmin(PasswordChangeByAdminDTO model)
        {
            var result = await _userService.PasswordChangeByAdmin(model);
            return result;
        }
        
        [Authorize]
        [HttpDelete("{userid}")]
        public async Task<IResponse> RemoveUser(string userid)
        {
            var result = await _userService.RemoveUser(userid);
            return result;
        }
    }
}
