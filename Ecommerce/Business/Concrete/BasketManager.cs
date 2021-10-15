
using System;
using System.Linq;
using System.Collections.Generic;
using Entities.Concrete;
using DataAccess.Abstract;
using System.Threading.Tasks;
using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Entities.DTOs;
using System.Security.Claims;
using Core.Utilities.Responses.Concrete;
using Core.Utilities.Responses.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Exceptions;

namespace Business.Concrete
{
    public class BasketManager : IBasketService
    {
        private IBasketRepository _basketRepository;
        private IBasketItemService _basketItemService;
        private IHttpContextAccessor _httpContextAccessor;
        public BasketManager(IBasketRepository basketRepository, IHttpContextAccessor httpContextAccessor, IBasketItemService basketItemService)
        {
            _basketRepository = basketRepository;
            _basketItemService = basketItemService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Basket> GetByIdAsync(int id)
        {
            return await _basketRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Basket>> GetAllAsync()
        {
            return await _basketRepository.GetAllAsync();
        }
        [ValidationAspect(typeof(BasketValidator))]
        public async Task<IResponse> AddToBasketAsync(AddUpdateBasketDTO model)
        {
            string userid = _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var basket = await GetBasketByUserId(userid);
            if (basket != null)
            {
                var item = basket.BasketItems.FirstOrDefault(x => x.ProductId == model.ProductId);
                if (item == null)
                {
                    basket.BasketItems.Add(new BasketItem()
                    {
                        ProductId = model.ProductId,
                        BasketId = basket.Id,
                        Quantity = model.Quantity
                    });
                }
                else
                {
                    item.Quantity += model.Quantity;
                }
                await _basketRepository.UpdateAsync(basket);
            }
            return new SuccessResponse(200, Messages.AddedSuccesfully);
        }
        [ValidationAspect(typeof(BasketValidator))]
        public async Task<IResponse> UpdateAsync(AddUpdateBasketDTO model)
        {
            string userid = _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var basket = await GetBasketByUserId(userid);
            if (basket != null)
            {
                var item = basket.BasketItems.FirstOrDefault(x => x.ProductId == model.ProductId);
                if (item != null)
                {
                    if (model.Quantity < 1)
                    {
                        item.Quantity = 1;
                    }
                    else
                    {
                        item.Quantity = model.Quantity;
                    }
                }
                await _basketRepository.UpdateAsync(basket);
            }
            return new SuccessResponse(200, Messages.UpdatedSuccessfully);
        }

        public async Task RemoveAsync(int id)
        {
            var exist = await _basketRepository.GetByIdAsync(id);
            if (exist != null)
            {
                await _basketRepository.RemoveAsync(exist);
            }

            throw new ApiException(404, Messages.NotFound);
        }

        public async Task<Basket> GetBasketByUserId(string userid)
        {
            return await _basketRepository.GetBasketByUserId(userid);
        }

        public async Task<DataResponse<BasketDetail>> GetBasketWithTotalPriceAsync()
        {
            string userid = _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var currentbasket = await GetBasketByUserId(userid);
            decimal totalprice = 0;
            foreach (var item in currentbasket.BasketItems)
            {
                totalprice += item.Quantity * item.Product.Price;
            }
            BasketDetail basketDetail = new BasketDetail()
            {
                Id = currentbasket.Id,
                BasketItems = currentbasket.BasketItems.Select(basketitem => new BasketItemDetail()
                {
                    Id = basketitem.Id,
                    ProductId = basketitem.ProductId,
                    Quantity = basketitem.Quantity,
                    Price = basketitem.Product.Price.ToString("N"),
                    MainImage = basketitem.Product.MainImage,
                    Name = basketitem.Product.Name,
                }).ToList(),
                TotalPrice = totalprice.ToString("N"),
            };
            return new DataResponse<BasketDetail>(basketDetail, 200);
        }

        public async Task CreateBasketForUserAsync(string userid)
        {
            var basket = new Basket()
            {
                UserId = userid
            };
            await _basketRepository.AddAsync(basket);
        }

        public async Task<IResponse> RemoveFromBasket(int productid)
        {
            string userid = _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var basket = await GetBasketByUserId(userid);
            return await _basketItemService.RemoveFromBasket(basket.Id, productid);
        }

        public async Task<IResponse> ClearBasket()
        {
            string userid = _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var basket = await GetBasketByUserId(userid);
            return await _basketItemService.ClearBasket(basket.Id);
        }
    }

}