
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface ICategoryRepository : IEntityRepository<Category>
    {
        Task<IEnumerable<Category>> GetCategoriesWithOptionsAsync();
        Task<OptionsAndValuesDTO> GetOptionsWithValuesByCategorySlug(string slug);
        Task<OptionsAndValuesDTO> GetOptionsWithValuesByCategoryId(int id);
        Task<Category> GetCategoryWithOptionsByCategoryIdAsync(int categoryid);
    }
}
