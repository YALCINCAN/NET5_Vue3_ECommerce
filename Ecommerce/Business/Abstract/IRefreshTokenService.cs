
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRefreshTokenService
    {
        Task<RefreshToken> GetByCodeAsync(string code);
        Task<RefreshToken> GetByUserIdAsync(string userid);
        Task<RefreshToken> AddAsync(RefreshToken entity);
        Task<IEnumerable<RefreshToken>> GetAllAsync();
        Task<RefreshToken> GetByIdAsync(int id);
        Task UpdateAsync(RefreshToken entity);
        Task RemoveAsync(RefreshToken entity);
    }
}
