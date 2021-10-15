
using System;
using System.Linq;
using System.Collections.Generic;
using Entities.Concrete;
using DataAccess.Abstract;
using System.Threading.Tasks;
using Business.Abstract;
using AutoMapper;
using Core.Utilities.Responses.Concrete;
using Business.Constants;
using Core.Exceptions;
using Core.Utilities.Responses.Abstract;
using Entities.DTOs;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        private IBrandRepository _brandRepository;
        private IMapper _mapper;
        public BrandManager(IBrandRepository brandRepository,IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<IResponse> GetByIdAsync(int id)
        {
            var brand = await _brandRepository.GetByIdAsync(id);
            if (brand == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            return new DataResponse<Brand>(brand, 200);
        }

        public async Task<IResponse> GetAllAsync()
        {
            var brands = await _brandRepository.GetAllAsync();
            return new DataResponse<IEnumerable<Brand>>(brands, 200);
        }
        [ValidationAspect(typeof(AddBrandValidator))]
        public async Task<IResponse> AddAsync(BrandDTO model)
        {
            var image = FileManager.SaveFile(FolderNames.Brands, model.ImageFile);
            model.Image = image;
            var brand = _mapper.Map<Brand>(model);
            brand.Slug = SlugHelper.Slugify(model.Name);
            var addedbrand = await _brandRepository.AddAsync(brand);
            return new DataResponse<Brand>(addedbrand, 200, Messages.AddedSuccesfully);
        }

        [ValidationAspect(typeof(UpdateBrandValidator))]
        public async Task<IResponse> UpdateAsync(BrandDTO model)
        {
            var brand = await _brandRepository.GetByIdAsync(model.Id);
            if (brand == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            if (model.ImageFile != null)
            {
                FileManager.DeleteFile(brand.Image);
                var image = FileManager.SaveFile(FolderNames.Brands, model.ImageFile);
                model.Image = image;
                var updatedbrand = _mapper.Map(model, brand);
                brand.Slug = SlugHelper.Slugify(model.Name);
                await _brandRepository.UpdateAsync(updatedbrand);
                return new SuccessResponse(204, Messages.UpdatedSuccessfully);
            }
            else
            {
                model.Image = brand.Image;
                var updatedbrand = _mapper.Map(model, brand);
                brand.Slug = SlugHelper.Slugify(model.Name);
                await _brandRepository.UpdateAsync(updatedbrand);
                return new SuccessResponse(204, Messages.UpdatedSuccessfully);
            }
        }

        public async Task<IResponse> RemoveAsync(int id)
        {
            var brand = await _brandRepository.GetByIdAsync(id);
            if (brand == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            FileManager.DeleteFile(brand.Image);
            await _brandRepository.RemoveAsync(brand);
            return new SuccessResponse(200, Messages.DeletedSuccessfully);
        }
    }

}