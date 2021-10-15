using Core.Utilities.Responses.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task<IResponse> UpdateProfile(UpdateProfileDTO model);
        Task<IResponse> ChangePassword(ChangePasswordDTO model);
        Task<IResponse> ForgotPassword(ForgotPasswordDTO model);
        Task<IResponse> ResetPassword(ResetPasswordDTO model);
        Task<IResponse> GetUsersWithRoles();
        Task<IResponse> RemoveUser(string userid);
        Task<IResponse> PasswordChangeByAdmin(PasswordChangeByAdminDTO model);
        Task<IResponse> UpdateUserByAdmin(UserDTO model);
        Task<IResponse> GetUser(string userid);
    }
}
