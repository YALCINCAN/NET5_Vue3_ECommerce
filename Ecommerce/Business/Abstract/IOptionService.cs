
using Core.Utilities.Responses.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOptionService
    {
        Task<IResponse> AddAsync(OptionDTO model);
        Task<IResponse> GetAllAsync();
        Task<IResponse> GetOptionsWithValuesAsync();
        Task<IResponse> GetByIdAsync(int id);
        Task<IResponse> UpdateAsync(OptionDTO model);
        Task<IResponse> RemoveAsync(int id);
    }
}
