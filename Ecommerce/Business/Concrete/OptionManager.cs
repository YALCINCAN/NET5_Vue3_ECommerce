
using System;
using System.Linq;
using System.Collections.Generic;
using Entities.Concrete;
using DataAccess.Abstract;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Utilities.Responses.Abstract;
using AutoMapper;
using Core.Utilities.Responses.Concrete;
using Business.Constants;
using Core.Exceptions;
using Entities.DTOs;
using Core.Aspects.Autofac.Validation;
using Business.ValidationRules.FluentValidation;

namespace Business.Concrete
{
    public class OptionManager : IOptionService
    {
        private IOptionRepository _optionRepository;
        private IMapper _mapper;
        public OptionManager(IOptionRepository optionRepository, IMapper mapper)
        {
            _optionRepository = optionRepository;
            _mapper = mapper;
        }

        public async Task<IResponse> GetByIdAsync(int id)
        {
            var option = await _optionRepository.GetByIdAsync(id);
            if (option == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            return new DataResponse<Option>(option, 200);
        }

        public async Task<IResponse> GetAllAsync()
        {
            var options = await _optionRepository.GetAllAsync();
            var mappedoptions = _mapper.Map<IEnumerable<OptionDTO>>(options);
            return new DataResponse<IEnumerable<OptionDTO>>(mappedoptions, 200);
        }

        [ValidationAspect(typeof(AddOptionValidator))]
        public async Task<IResponse> AddAsync(OptionDTO model)
        {
            var option = _mapper.Map<Option>(model);
            var addedoption = await _optionRepository.AddAsync(option);
            return new DataResponse<Option>(addedoption, 200, Messages.AddedSuccesfully);
        }

        [ValidationAspect(typeof(UpdateOptionValidator))]
        public async Task<IResponse> UpdateAsync(OptionDTO model)
        {
            var option = await _optionRepository.GetByIdAsync(model.Id);
            if (option == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            var updatedoption = _mapper.Map(model, option);
            await _optionRepository.UpdateAsync(updatedoption);
            return new SuccessResponse(204, Messages.UpdatedSuccessfully);
        }

        public async Task<IResponse> RemoveAsync(int id)
        {
            var option = await _optionRepository.GetByIdAsync(id);
            if (option == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            await _optionRepository.RemoveAsync(option);
            return new SuccessResponse(200, Messages.DeletedSuccessfully);
        }

        public async  Task<IResponse> GetOptionsWithValuesAsync()
        {
            var optionswithvalues = await _optionRepository.GetOptionsWithValuesAsync();
            var mappedoptionswithvalues = _mapper.Map<IEnumerable<OptionsWithValuesDTO>>(optionswithvalues);
            return new DataResponse<IEnumerable<OptionsWithValuesDTO>>(mappedoptionswithvalues, 200);
        }
    }
}