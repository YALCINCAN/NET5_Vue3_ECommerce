
using System;
using System.Linq;
using System.Collections.Generic;
using Entities.Concrete;
using DataAccess.Abstract;
using System.Threading.Tasks;
using Business.Abstract;

namespace Business.Concrete
{
    public class RefreshTokenManager : IRefreshTokenService
    {
        private IRefreshTokenRepository _refreshTokenRepository;
        public RefreshTokenManager(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<RefreshToken> GetByIdAsync(int id)
        {
            return await _refreshTokenRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<RefreshToken>> GetAllAsync()
        {
            return await _refreshTokenRepository.GetAllAsync();
        }

        public async Task<RefreshToken> AddAsync(RefreshToken entity)
        {
            return await _refreshTokenRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(RefreshToken entity)
        {
            await _refreshTokenRepository.UpdateAsync(entity);
        }

        public async Task RemoveAsync(RefreshToken entity)
        {
            await _refreshTokenRepository.RemoveAsync(entity);
        }

        public async Task<RefreshToken> GetByCodeAsync(string code)
        {
            return await _refreshTokenRepository.GetAsync(x => x.Code == code);
        }

        public async Task<RefreshToken> GetByUserIdAsync(string userid)
        {
            return await _refreshTokenRepository.GetAsync(x => x.UserId == userid);
        }
    }

}