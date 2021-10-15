
using Core.Utilities.Responses.Abstract;
using Core.Utilities.Responses.Concrete;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBasketService
    {
        Task<IResponse> AddToBasketAsync(AddUpdateBasketDTO model);
        Task<IResponse> RemoveFromBasket(int productid);
        Task<IResponse> ClearBasket();
        Task CreateBasketForUserAsync(string userid);
        Task<DataResponse<BasketDetail>> GetBasketWithTotalPriceAsync();
        Task<Basket> GetBasketByUserId(string userid);
        Task<IEnumerable<Basket>> GetAllAsync();
        Task<Basket> GetByIdAsync(int id);
        Task<IResponse> UpdateAsync(AddUpdateBasketDTO model);
        Task RemoveAsync(int id);
    }
}
