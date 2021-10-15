
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using System.Threading.Tasks;
using Entities.DTOs;
using AutoMapper;
using Core.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCategoryRepository : EfEntityRepositoryBase<Category, ECommerceContext>, ICategoryRepository
    {
        private IMapper _mapper;
        public EfCategoryRepository(ECommerceContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Category> AddWithOptionsAsync(CategoryDTO model)
        {
            var category = _mapper.Map<Category>(model);
            category.CategoryOptions = model.OptionIds.Select(optionid => new CategoryOption()
            {
                CategoryId = category.Id,
                OptionId = optionid
            }).ToList();
            category.Slug = SlugHelper.Slugify(model.Name);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<IEnumerable<Category>> GetCategoriesWithOptionsAsync()
        {
            return await _context.Categories.Include(x => x.CategoryOptions).ThenInclude(x => x.Option).ToListAsync();
        }

        public async Task<Category> GetCategoryWithOptionsByCategoryIdAsync(int categoryid)
        {
            return await _context.Categories.Include(x => x.CategoryOptions).FirstOrDefaultAsync(x => x.Id == categoryid);
        }

        public async Task<OptionsAndValuesDTO> GetOptionsWithValuesByCategorySlug(string slug)
        {
            return await _context.Categories.Where(x => x.Slug == slug).Select(category => new OptionsAndValuesDTO
            {
                Options = category.CategoryOptions.Select(categoryoptions => new CategoryOptionDetailDTO
                {
                    Id = categoryoptions.Option.Id,
                    Name = categoryoptions.Option.Name,
                    OptionValues = categoryoptions.Option.OptionValues.Select(optionvalue => new OptionValueDetailDTO
                    {
                        Id = optionvalue.Id,
                        Value = optionvalue.Value,
                        OptionId = optionvalue.OptionId
                    })
                })
            }).FirstOrDefaultAsync();
        }

        public async Task<OptionsAndValuesDTO> GetOptionsWithValuesByCategoryId(int id)
        {
            return await _context.Categories.Where(x => x.Id == id).Select(category => new OptionsAndValuesDTO
            {
                Options = category.CategoryOptions.Select(categoryoptions => new CategoryOptionDetailDTO
                {
                    Id = categoryoptions.Option.Id,
                    Name = categoryoptions.Option.Name,
                    OptionValues = categoryoptions.Option.OptionValues.Select(optionvalue => new OptionValueDetailDTO
                    {
                        Id = optionvalue.Id,
                        Value = optionvalue.Value,
                        OptionId = optionvalue.OptionId
                    })
                })
            }).FirstOrDefaultAsync();
        }

        public async Task UpdateWithOptionsAsync(CategoryDTO model)
        {
            var category = await GetCategoryWithOptionsByCategoryIdAsync(model.Id);
            var updatedcategory = _mapper.Map(model, category);
            updatedcategory.CategoryOptions = model.OptionIds.Select(optionid => new CategoryOption()
            {
                CategoryId = category.Id,
                OptionId = optionid
            }).ToList();
            updatedcategory.Slug = SlugHelper.Slugify(model.Name);
            await _context.SaveChangesAsync();
        }
    }
}
