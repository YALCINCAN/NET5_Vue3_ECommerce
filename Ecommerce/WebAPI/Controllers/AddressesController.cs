using Business.Abstract;
using Core.ActionFilters;
using Core.Utilities.Responses.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private IAddressService _addressService;
        public AddressesController(IAddressService addressService)
        {
            _addressService = addressService;
        }


        [Authorize]
        [HttpGet]
        public async Task<IResponse> GetAddressesByUserId()
        {
            return await _addressService.GetUserAddresses();
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IResponse> GetAddress(int id)
        {
            return await _addressService.GetByIdAsync(id);
        }

        [Authorize]
        [HttpPost]
        [ServiceFilter(typeof(NullFilterAttribute))]
        public async Task<IResponse> AddAddress(AddressDTO model)
        {
            return await _addressService.AddAsync(model);
        }

        [Authorize]
        [HttpPut]
        [ServiceFilter(typeof(NullFilterAttribute))]
        public async Task<IResponse> UpdateAddress(AddressDTO model)
        {
            return await _addressService.UpdateAsync(model);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IResponse> RemoveAddress(int id)
        {
            return await _addressService.RemoveAsync(id);
        }
    }
}
