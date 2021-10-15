
using System;
using System.Linq;
using System.Collections.Generic;
using Entities.Concrete;
using DataAccess.Abstract;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Utilities.Responses.Abstract;
using Entities.DTOs;
using AutoMapper;
using Core.Utilities.Responses.Concrete;
using Business.Constants;
using Core.Exceptions;
using Core.Utilities;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        private IProductService _productService;
        private IMapper _mapper;
        public CategoryManager(ICategoryRepository categoryRepository,IMapper mapper,IProductService productService)
        {
            _categoryRepository = categoryRepository;
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<IResponse> GetByIdAsync(int id)
        {
            var category =  await _categoryRepository.GetByIdAsync(id);
            if (category != null)
            {
                return new DataResponse<Category>(category, 200);
            }
            throw new ApiException(404, Messages.NotFound);
        }

        public async Task<IResponse> GetAllAsync()
        {
            var categories =  await _categoryRepository.GetAllAsync();
            return new DataResponse<IEnumerable<Category>>(categories, 200);
        }
        [ValidationAspect(typeof(AddCategoryValidator))]
        public async Task<IResponse> AddAsync(CategoryDTO model)
        {
            var category = _mapper.Map<Category>(model);
            category.CategoryOptions = model.OptionIds.Select(optionid => new CategoryOption()
            {
                CategoryId = category.Id,
                OptionId = optionid
            }).ToList();
            category.Slug = SlugHelper.Slugify(model.Name);
            var addedcategory =  await _categoryRepository.AddAsync(category);
            return new DataResponse<Category>(addedcategory, 200, Messages.AddedSuccesfully);
        }
        [ValidationAspect(typeof(UpdateCategoryValidator))]
        public async Task<IResponse> UpdateAsync(CategoryDTO model)
        {
            var category = await _categoryRepository.GetCategoryWithOptionsByCategoryIdAsync(model.Id);
            if (category != null)
            {
                var updatedcategory = _mapper.Map(model, category);
                updatedcategory.CategoryOptions = model.OptionIds.Select(optionid => new CategoryOption()
                {
                    CategoryId = category.Id,
                    OptionId = optionid
                }).ToList();
                updatedcategory.Slug = SlugHelper.Slugify(model.Name);
                await _categoryRepository.UpdateAsync(category);
                return new SuccessResponse(200, Messages.UpdatedSuccessfully);
            }
            else
            {
                throw new ApiException(404, Messages.NotFound);
            }
        }

        public async Task<IResponse> RemoveAsync(int id)
        {
            var exist = await _categoryRepository.GetByIdAsync(id);
            if (exist != null)
            {
                var product = await _productService.GetProductWithImagesByCategoryIdAsync(id);
                if (product!=null)
                {
                    foreach (var item in product.ProductImages)
                    {
                        FileManager.DeleteFile(item.Image);
                    }
                    FileManager.DeleteFile(product.MainImage);
                }
                await _categoryRepository.RemoveAsync(exist);
                return new SuccessResponse(200, Messages.DeletedSuccessfully);
            }
            throw new ApiException(404, Messages.NotFound);
        }

        public async Task<IResponse> GetCategoriesWithOptionsAsync()
        {
            var categorieswithoptions = await _categoryRepository.GetCategoriesWithOptionsAsync();
            return new DataResponse<IEnumerable<Category>>(categorieswithoptions, 200);
        }
        public async Task<IResponse> GetCategoryWithOptionsByCategoryIdAsync(int categoryid)
        {
            var categorywithoptions = await _categoryRepository.GetCategoryWithOptionsByCategoryIdAsync(categoryid);
            if (categorywithoptions == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            return new DataResponse<Category>(categorywithoptions, 200);
        }
        public async Task<IResponse> GetOptionsWithValuesByCategorySlug(string slug)
        {
            var optionswithvaluesbycategoryslug = await _categoryRepository.GetOptionsWithValuesByCategorySlug(slug);
            if (optionswithvaluesbycategoryslug == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            return new DataResponse<OptionsAndValuesDTO>(optionswithvaluesbycategoryslug, 200);
        }

        public  async Task<IResponse> GetOptionsWithValuesByCategoryId(int id)
        {
            var optionswithvaluesbycategoryid = await _categoryRepository.GetOptionsWithValuesByCategoryId(id);
            if (optionswithvaluesbycategoryid == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            return new DataResponse<OptionsAndValuesDTO>(optionswithvaluesbycategoryid, 200);
        }
    }
}