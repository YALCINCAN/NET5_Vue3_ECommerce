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
    public class SlidersController : ControllerBase
    {
        private ISliderService _sliderService;
        public SlidersController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        [HttpGet]
        public async Task<IResponse> GetSliders()
        {
            return await _sliderService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<IResponse> GetSlider(int id)
        {
            return await _sliderService.GetByIdAsync(id);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ServiceFilter(typeof(NullFilterAttribute))]
        public async Task<IResponse> AddSlider([FromForm]SliderDTO model)
        {
            return await _sliderService.AddAsync(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [ServiceFilter(typeof(NullFilterAttribute))]
        public async Task<IResponse> UpdateSlider([FromForm]SliderDTO model)
        {
            return await _sliderService.UpdateAsync(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IResponse> RemoveSlider(int id)
        {
            return await _sliderService.RemoveAsync(id);
        }
    }
}
