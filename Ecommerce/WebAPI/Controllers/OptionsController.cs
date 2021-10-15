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
    public class OptionsController : ControllerBase
    {
        private IOptionService _optionService;
        public OptionsController(IOptionService optionService)
        {
            _optionService = optionService;
        }

        [HttpGet]
        public async Task<IResponse> GetOptions()
        {
            return await _optionService.GetAllAsync();
        }

        [HttpGet("values")]
        public async Task<IResponse> GetOptionsWithValues()
        {
            return await _optionService.GetOptionsWithValuesAsync();
        }


        [HttpGet("{id}")]
        public async Task<IResponse> GetOption(int id)
        {
            return await _optionService.GetByIdAsync(id);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ServiceFilter(typeof(NullFilterAttribute))]
        public async Task<IResponse> AddOption(OptionDTO model)
        {
            return await _optionService.AddAsync(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [ServiceFilter(typeof(NullFilterAttribute))]
        public async Task<IResponse> UpdateOption(OptionDTO model)
        {
            return await _optionService.UpdateAsync(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IResponse> RemoveOption(int id)
        {
            return await _optionService.RemoveAsync(id);
        }
    }
}
