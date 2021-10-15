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
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IResponse> GetAuthenticatedUserWithRoles()
        {
           return await _authService.GetAuthenticatedUserWithRoles();
        }

        [HttpPost("register")]
        [ServiceFilter(typeof(NullFilterAttribute))]
        public async Task<IResponse> Register(RegisterDTO model)
        {
            return await _authService.Register(model);
        }

        [HttpPost("login")]
        [ServiceFilter(typeof(NullFilterAttribute))]
        public async Task<IResponse> Login(LoginDTO model)
        {
            return await _authService.Login(model);
        }

        [HttpPost("confirmemail")]
        [ServiceFilter(typeof(NullFilterAttribute))]
        public async Task<IResponse> ConfirmEmail(ConfirmEmailDTO model)
        {
            return await _authService.ConfirmEmail(model);
        }


        [HttpPost("refreshtoken")]
        [ServiceFilter(typeof(NullFilterAttribute))]
        public async Task<IResponse> RefreshToken(RefreshTokenDTO model)
        {
            return await _authService.CreateTokenByRefreshToken(model);
        }
    }
}
