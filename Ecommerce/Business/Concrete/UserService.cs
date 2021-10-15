using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Exceptions;
using Core.Utilities;
using Core.Utilities.Responses.Abstract;
using Core.Utilities.Responses.Concrete;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserService : IUserService
    {
        private UserManager<User> _userManager;
        private IHttpContextAccessor _httpContextAccessor;
        private IMapper _mapper;
        private IEmailService _emailService;
        public UserService(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor,IMapper mapper,IEmailService emailService)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _emailService = emailService;
        }
        [ValidationAspect(typeof(ChangePasswordValidator))]
        public async Task<IResponse> ChangePassword(ChangePasswordDTO model)
        {
            var userid = _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userid);
            bool currentpasswordtrue = await _userManager.CheckPasswordAsync(user, model.CurrentPassword);
            if (currentpasswordtrue == true)
            {
                if (model.NewPassword != model.ConfirmPassword)
                {
                    throw new ApiException(400, Messages.PasswordDontMatchWithConfirmation);
                }
                var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    return new SuccessResponse(200, Messages.PasswordChangedSuccessfully);
                }
                else
                {
                    throw new ApiException(400, result.Errors.Select(e => e.Description).ToList());
                }
            }
            throw new ApiException(400, Messages.CurrentPasswordIsFalse);
        }

        [ValidationAspect(typeof(ForgotPasswordValidator))]
        public async Task<IResponse> ForgotPassword(ForgotPasswordDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(token);
            var tokenEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);
            string link = "http://localhost:8080/resetpassword/" + model.Email + "/" + tokenEncoded;
            await _emailService.ForgetPasswordMailAsync(link, model.Email);
            return new SuccessResponse(200, Messages.IfEmailTrue);
        }

        public async Task<IResponse> GetUser(string userid)
        {
            var user = await _userManager.FindByIdAsync(userid);
            if (user == null)
            {
               throw new ApiException(404, Messages.UserNotFound);
            }
            var mappeduser = _mapper.Map<UserDTO>(user);
            return new DataResponse<UserDTO>(mappeduser, 200);
        }

        public async Task<IResponse> GetUsersWithRoles()
        {
            var userswithroles = new List<UserWithRolesDTO>();
            foreach (var user in _userManager.Users.ToList())
            {
                var mappeduser = _mapper.Map<UserDTO>(user);
                userswithroles.Add(new UserWithRolesDTO()
                {
                    User = mappeduser,
                    Roles = await _userManager.GetRolesAsync(user)
                });
            }
            return new DataResponse<List<UserWithRolesDTO>>(userswithroles, 200);
        }

        [ValidationAspect(typeof(PasswordChangeByAdminValidator))]
        public async Task<IResponse> PasswordChangeByAdmin(PasswordChangeByAdminDTO model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
            if (result.Succeeded)
            {
                return new SuccessResponse(200, Messages.PasswordChangedSuccessfully);
            }
            throw new ApiException(400, result.Errors.Select(e => e.Description).ToList());
        }

        public async Task<IResponse> RemoveUser(string userid)
        {
            var user = await _userManager.FindByIdAsync(userid);
            if (user == null)
            {
                throw new ApiException(404, Messages.UserNotFound);
            }
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return new SuccessResponse(200, Messages.DeletedSuccessfully);
            }
            else
            {
                throw new ApiException(400, result.Errors.Select(e => e.Description).ToList());
            }
        }

        [ValidationAspect(typeof(ResetPasswordValidator))]
        public async Task<IResponse> ResetPassword(ResetPasswordDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                throw new ApiException(404, Messages.UserNotFound);
            }
            if (model.NewPassword != model.ConfirmPassword)
            {
                throw new ApiException(400, Messages.PasswordDontMatchWithConfirmation);
            }
            var tokenDecodedBytes = WebEncoders.Base64UrlDecode(model.Token);
            var tokenDecoded = Encoding.UTF8.GetString(tokenDecodedBytes);
            var result = await _userManager.ResetPasswordAsync(user, tokenDecoded, model.NewPassword);
            if (result.Succeeded)
            {
                return new SuccessResponse(200, Messages.PasswordResetSuccessfully);
            }
            else
            {
                throw new ApiException(400, result.Errors.Select(e => e.Description).ToList());
            }
        }

        [ValidationAspect(typeof(UpdateProfileValidator))]
        public async Task<IResponse> UpdateProfile(UpdateProfileDTO model)
        {
            var userid = _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userid);
            if (user == null)
            {
                throw new ApiException(404, Messages.UserNotFound);
            }
            var email = await _userManager.FindByEmailAsync(model.Email);
            if (email != null && user.Email != model.Email)
            {
                throw new ApiException(400, Messages.EmailIsAlreadyExist);
            }
            var updateduser = _mapper.Map(model, user);
            var result = await _userManager.UpdateAsync(updateduser);
            if (result.Succeeded)
            {
                return new SuccessResponse(200, Messages.UpdatedSuccessfully);
            }
            else
            {
                throw new ApiException(400, result.Errors.Select(e => e.Description).ToList());
            }
        }

        [ValidationAspect(typeof(UpdateUserByAdminValidator))]
        public async Task<IResponse> UpdateUserByAdmin(UserDTO model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                throw new ApiException(404, Messages.UserNotFound);
            }
            _mapper.Map(model, user);
            var username = await _userManager.FindByNameAsync(model.UserName);
            if (username != null && user.UserName != model.UserName)
            {
                throw new ApiException(400, Messages.UsernameIsAlreadyExist);
            }
            var email = await _userManager.FindByEmailAsync(model.Email);
            if (email != null && user.Email != model.Email)
            {
                throw new ApiException(400, Messages.EmailIsAlreadyExist);
            }
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new SuccessResponse(200, Messages.UpdatedSuccessfully);
            }
            throw new ApiException(400, result.Errors.Select(e => e.Description).ToList());
        }
    }
}
