
using Core.Utilities.Responses.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAddressService
    {
        Task<IResponse> AddAsync(AddressDTO model);
        Task<IResponse> GetAllAsync();
        Task<IResponse> GetUserAddresses();
        Task<IResponse> GetByIdAsync(int id);
        Task<IResponse> UpdateAsync(AddressDTO model);
        Task<IResponse> RemoveAsync(int id);
    }
}
