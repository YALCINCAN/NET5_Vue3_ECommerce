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
    public class BrandsController : ControllerBase
    {
        private IBrandService _brandService;
        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<IResponse> GetBrands()
        {
            return await _brandService.GetAllAsync();
        }


        [HttpGet("{id}")]
        public async Task<IResponse> GetBrand(int id)
        {
            return await _brandService.GetByIdAsync(id);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ServiceFilter(typeof(NullFilterAttribute))]
        public async Task<IResponse> AddBrand([FromForm] BrandDTO model)
        {
            return await _brandService.AddAsync(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [ServiceFilter(typeof(NullFilterAttribute))]
        public async Task<IResponse> UpdateBrand([FromForm] BrandDTO model)
        {
            return await _brandService.UpdateAsync(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IResponse> RemoveBrand(int id)
        {
            return await _brandService.RemoveAsync(id);
        }
    }
}
