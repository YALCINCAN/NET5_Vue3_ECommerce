
using Core.Utilities.Responses.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ISliderService
    {
        Task<IResponse> AddAsync(SliderDTO model);
        Task<IResponse> GetAllAsync();
        Task<IResponse> GetByIdAsync(int id);
        Task<IResponse> UpdateAsync(SliderDTO model);
        Task<IResponse> RemoveAsync(int id);
    }
}
