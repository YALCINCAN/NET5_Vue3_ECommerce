
using System;
using System.Linq;
using System.Collections.Generic;
using Entities.Concrete;
using DataAccess.Abstract;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Utilities.Responses.Abstract;
using Entities.DTOs;
using Core.Utilities.Responses.Concrete;
using AutoMapper;
using Core.Exceptions;
using Business.Constants;
using Core.Utilities;
using Core.Aspects.Autofac.Validation;
using Business.ValidationRules.FluentValidation;

namespace Business.Concrete
{
    public class SliderManager : ISliderService
    {
        private ISliderRepository _sliderRepository;
        private IMapper _mapper;
        public SliderManager(ISliderRepository sliderRepository,IMapper mapper)
        {
            _sliderRepository = sliderRepository;
            _mapper = mapper;
        }

        public async Task<IResponse> GetByIdAsync(int id)
        {
            var slider = await _sliderRepository.GetByIdAsync(id);
            if (slider == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            return new DataResponse<Slider>(slider,200);
        }

        public async Task<IResponse> GetAllAsync()
        {
            var sliders=  await _sliderRepository.GetAllAsync();
            return new DataResponse<IEnumerable<Slider>>(sliders, 200);
        }

        [ValidationAspect(typeof(AddSliderValidator))]
        public async Task<IResponse> AddAsync(SliderDTO model)
        {
            var image = FileManager.SaveFile(FolderNames.Sliders,model.ImageFile);
            model.Image = image;
            var slider = _mapper.Map<Slider>(model);
            var addedslider = await _sliderRepository.AddAsync(slider);
            return new DataResponse<Slider>(addedslider, 200,Messages.AddedSuccesfully);
        }

        [ValidationAspect(typeof(UpdateSliderValidator))]
        public async Task<IResponse> UpdateAsync(SliderDTO model)
        {
            var slider = await _sliderRepository.GetByIdAsync(model.Id);
            if (slider == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            if (model.ImageFile != null)
            {
                FileManager.DeleteFile(slider.Image);
                var image = FileManager.SaveFile(FolderNames.Sliders, model.ImageFile);
                model.Image = image;
                var updatedslider = _mapper.Map(model, slider);
                await _sliderRepository.UpdateAsync(updatedslider);
                return new SuccessResponse(204, Messages.UpdatedSuccessfully);
            }
            else
            {
                model.Image = slider.Image;
                var updatedslider = _mapper.Map(model, slider);
                await _sliderRepository.UpdateAsync(updatedslider);
                return new SuccessResponse(204, Messages.UpdatedSuccessfully);
            }
        }

        public async Task<IResponse> RemoveAsync(int id)
        {
            var slider = await _sliderRepository.GetByIdAsync(id);
            if (slider == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            FileManager.DeleteFile(slider.Image);
            await _sliderRepository.RemoveAsync(slider);
            return new SuccessResponse(200,Messages.DeletedSuccessfully);
        }
    }

}