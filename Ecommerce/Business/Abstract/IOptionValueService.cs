
using Core.Utilities.Responses.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOptionValueService
    {
        Task<IResponse> AddAsync(OptionValueDTO model);
        Task<IResponse> GetAllAsync();
        Task<IResponse> GetOptionValuesWithOption();
        Task<IResponse> GetOptionValuesByOptionIdAsync(int optionid);
        Task<IResponse> GetByIdAsync(int id);
        Task<IResponse> UpdateAsync(OptionValueDTO model);
        Task<IResponse> RemoveAsync(int id);

        
    }
}
