using Core.Utilities.Responses.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        Task<IResponse> Register(RegisterDTO model);
        Task<IResponse> Login(LoginDTO model);
        Task<IResponse> ConfirmEmail(ConfirmEmailDTO model);
        Task<IResponse> CreateTokenByRefreshToken(RefreshTokenDTO model);
        Task<IResponse> GetAuthenticatedUserWithRoles();
    }
}
