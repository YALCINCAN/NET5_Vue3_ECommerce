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
    public class OptionValuesController : ControllerBase
    {
        private IOptionValueService _optionValueService;
        public OptionValuesController(IOptionValueService optionValueService)
        {
            _optionValueService = optionValueService;
        }

        [HttpGet]
        public async Task<IResponse> GetOptionValues()
        {
            return await _optionValueService.GetAllAsync();
        }

        [HttpGet("option")]
        public async Task<IResponse> GetOptionValuesWithOption()
        {
            return await _optionValueService.GetOptionValuesWithOption();
        }

        [HttpGet("{id}")]
        public async Task<IResponse> GetOptionValue(int id)
        {
            return await _optionValueService.GetByIdAsync(id);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ServiceFilter(typeof(NullFilterAttribute))]
        public async Task<IResponse> AddOptionValue(OptionValueDTO model)
        {
            return await _optionValueService.AddAsync(model);
        }


        [Authorize(Roles = "Admin")]
        [HttpPut]
        [ServiceFilter(typeof(NullFilterAttribute))]
        public async Task<IResponse> UpdateOptionValue(OptionValueDTO model)
        {
            return await _optionValueService.UpdateAsync(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IResponse> RemoveOptionValue(int id)
        {
            return await _optionValueService.RemoveAsync(id);
        }
    }
}
