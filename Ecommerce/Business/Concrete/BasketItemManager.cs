
using System;
using System.Linq;
using System.Collections.Generic;
using Entities.Concrete;
using DataAccess.Abstract;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Utilities.Responses.Abstract;
using Core.Utilities.Responses.Concrete;
using Business.Constants;
using Core.Exceptions;

namespace Business.Concrete
{
    public class BasketItemManager : IBasketItemService
    {
        private IBasketItemRepository _basketItemRepository;
        public BasketItemManager(IBasketItemRepository basketItemRepository)
        {
            _basketItemRepository = basketItemRepository;
        }

        public async Task<IResponse> ClearBasket(int basketId)
        {
            var basketitems = await _basketItemRepository.GetAllAsync(x => x.BasketId == basketId);
            if (basketitems.Count() > 0)
            {
                await _basketItemRepository.RemoveRangeAsync(basketitems);
                return new SuccessResponse(200, Messages.DeletedSuccessfully);
            }
            else
            {
                throw new ApiException(404, Messages.NotFound);
            }
        }

        public async Task<IResponse> RemoveFromBasket(int basketId, int productId)
        {
            var basketitem = await _basketItemRepository.GetAsync(x => x.BasketId == basketId && x.ProductId == productId);
            if (basketitem != null)
            {
                await _basketItemRepository.RemoveAsync(basketitem);
                return new SuccessResponse(200, Messages.DeletedSuccessfully);
            }
            else
            {
                throw new ApiException(404, Messages.NotFound);
            }
        }
    }
}