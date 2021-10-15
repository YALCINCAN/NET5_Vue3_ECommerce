
using System;
using System.Linq;
using System.Collections.Generic;
using Entities.Concrete;
using DataAccess.Abstract;
using System.Threading.Tasks;
using Business.Abstract;
using AutoMapper;
using Core.Utilities.Responses.Abstract;
using Core.Utilities.Responses.Concrete;
using Entities.DTOs;
using Business.Constants;
using Core.Exceptions;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Business.Concrete
{
    public class AddressManager : IAddressService
    {
        private IAddressRepository _addressRepository;
        private IHttpContextAccessor _httpContextAccessor;
        private IMapper _mapper;
        public AddressManager(IAddressRepository addressRepository,IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IResponse> GetByIdAsync(int id)
        {
            var address = await _addressRepository.GetByIdAsync(id);
            if (address == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            return new DataResponse<Address>(address, 200);
        }

        public async Task<IResponse> GetAllAsync()
        {
            var addresses = await _addressRepository.GetAllAsync();
            return new DataResponse<IEnumerable<Address>>(addresses, 200);
        }

        public async Task<IResponse> GetUserAddresses()
        {
            string userid = _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var addresses = await _addressRepository.GetAllAsync(x=>x.UserId==userid);
            return new DataResponse<IEnumerable<Address>>(addresses, 200);
        }
        [ValidationAspect(typeof(AddAddressValidator))]
        public async Task<IResponse> AddAsync(AddressDTO model)
        {
            string userid = _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var address = _mapper.Map<Address>(model);
            address.UserId = userid;
            var addedaddress = await _addressRepository.AddAsync(address);
            return new DataResponse<Address>(addedaddress, 200, Messages.AddedSuccesfully);
        }
        [ValidationAspect(typeof(UpdateAddressValidator))]
        public async Task<IResponse> UpdateAsync(AddressDTO model)
        {
            var address = await _addressRepository.GetByIdAsync(model.Id);
            if (address == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            var updatedaddress = _mapper.Map(model, address);
            await _addressRepository.UpdateAsync(updatedaddress);
            return new SuccessResponse(204, Messages.UpdatedSuccessfully);
        }

        public async Task<IResponse> RemoveAsync(int id)
        {
            var address = await _addressRepository.GetByIdAsync(id);
            if (address == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            await _addressRepository.RemoveAsync(address);
            return new SuccessResponse(200, Messages.DeletedSuccessfully);
        }
    }

}