
using Core.Utilities.Responses.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBasketItemService
    {
        Task<IResponse> RemoveFromBasket(int basketId,int productId);
        Task<IResponse> ClearBasket(int basketId);
    }
}
