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
    public class AuthManager : IAuthService
    {
        private UserManager<User> _userManager;
        private IRefreshTokenService _refreshTokenService;
        private ITokenService _tokenService;
        private IMapper _mapper;
        private IEmailService _emailService;
        private IBasketService _basketService;
        private IHttpContextAccessor _httpContextAccessor;
        public AuthManager(UserManager<User> userManager, IMapper mapper, IEmailService emailService, IRefreshTokenService refreshTokenService, ITokenService tokenService, IHttpContextAccessor httpContextAccessor,IBasketService basketService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _emailService = emailService;
            _refreshTokenService = refreshTokenService;
            _tokenService = tokenService;
            _basketService = basketService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IResponse> ConfirmEmail(ConfirmEmailDTO model)
        {
            if (model.UserId == null || model.Token == null)
            {
                throw new ApiException(404, Messages.TokenOrUserNotFound);
            }
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                throw new ApiException(404, Messages.UserNotFound);
            }
            if (user.EmailConfirmed)
            {
                throw new ApiException(400, Messages.AlreadyAccountConfirmed);
            }
            var tokenDecodedBytes = WebEncoders.Base64UrlDecode(model.Token);
            var tokenDecoded = Encoding.UTF8.GetString(tokenDecodedBytes);
            var result = await _userManager.ConfirmEmailAsync(user, tokenDecoded);
            if (result.Succeeded)
            {
                await _basketService.CreateBasketForUserAsync(user.Id);
                return new SuccessResponse(200, Messages.SuccessfullyAccountConfirmed);
            }
            throw new ApiException(400, Messages.AccountDontConfirmed);
        }

        public async Task<IResponse> CreateTokenByRefreshToken(RefreshTokenDTO model)
        {
            var existRefreshToken = await _refreshTokenService.GetByCodeAsync(model.RefreshToken);
            if (existRefreshToken == null)
            {
                throw new ApiException(404, Messages.RefreshTokenNotFound);
            }
            var user = await _userManager.FindByIdAsync(existRefreshToken.UserId);
            if (user == null)
            {
                throw new ApiException(404, Messages.UserNotFound);
            }
            if (existRefreshToken.Expiration < DateTime.Now)
            {
                throw new ApiException(404, Messages.RefreshTokenExpired);
            }
            var tokenDto = await _tokenService.CreateToken(user);
            existRefreshToken.Code = tokenDto.RefreshToken;
            existRefreshToken.Expiration = tokenDto.RefreshTokenExpiration;
            await _refreshTokenService.UpdateAsync(existRefreshToken);
            return new DataResponse<TokenDTO>(tokenDto, 200);
        }

        public async Task<IResponse> GetAuthenticatedUserWithRoles()
        {
            var userid = _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userid);
            if (user == null)
            {
                throw new ApiException(404, Messages.UserNotFound);
            }
            var userroles = await _userManager.GetRolesAsync(user);
            var mappeduser = _mapper.Map<UserDTO>(user);
            var userwithroles = new UserWithRolesDTO() { User = mappeduser, Roles = userroles };
            return new DataResponse<UserWithRolesDTO>(userwithroles, 200);
        }

        [ValidationAspect(typeof(LoginValidator))]
        public async Task<IResponse> Login(LoginDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                throw new ApiException(404, Messages.UserNameOrPasswordIsIncorrect);
            }
            if (!user.EmailConfirmed)
            {
                throw new ApiException(400, Messages.ConfirmYourAccount);
            }
            var identityResult = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!identityResult)
            {
                throw new ApiException(400, Messages.UserNameOrPasswordIsIncorrect);
            }
            var token = await _tokenService.CreateToken(user);
            var userRefreshToken = await _refreshTokenService.GetByUserIdAsync(user.Id);
            if (userRefreshToken == null)
            {
                await _refreshTokenService.AddAsync(new RefreshToken { UserId = user.Id, Code = token.RefreshToken, Expiration = token.RefreshTokenExpiration });
            }
            else
            {
                userRefreshToken.Code = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;
                await _refreshTokenService.UpdateAsync(userRefreshToken);
            }
            return new DataResponse<TokenDTO>(token, 200);
        }

       
        [ValidationAspect(typeof(RegisterValidator))]
        public async Task<IResponse> Register(RegisterDTO model)
        {
            var email = await _userManager.FindByEmailAsync(model.Email);
            if (email != null)
            {
                throw new ApiException(400, Messages.EmailIsAlreadyExist);
            }
            var username = await _userManager.FindByNameAsync(model.UserName);
            if (username != null)
            {
                throw new ApiException(400, Messages.UsernameIsAlreadyExist);
            }
            var user = _mapper.Map<User>(model);
            var IdentityResult = await _userManager.CreateAsync(user, model.Password);
            if (IdentityResult.Succeeded)
            {
                string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(token);
                var tokenEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);
                string link = "http://localhost:8080/confirmemail/" + user.Id + "/" + tokenEncoded;
                await _emailService.ConfirmationMailAsync(link, model.Email);
                return new SuccessResponse(200, Messages.RegisterSuccessfully);
            }
            else
            {
                throw new ApiException(400, IdentityResult.Errors.Select(e => e.Description).ToList());
            }
        }
    }
}
