
using Core.Utilities.Responses.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        Task<IResponse> GetCategoriesWithOptionsAsync();
        Task<IResponse> GetCategoryWithOptionsByCategoryIdAsync(int categoryid);
        Task<IResponse> GetOptionsWithValuesByCategorySlug(string slug);
        Task<IResponse> GetOptionsWithValuesByCategoryId(int id);
        Task<IResponse> GetAllAsync();
        Task<IResponse> GetByIdAsync(int id);
        Task<IResponse> RemoveAsync(int id);
        Task<IResponse> AddAsync(CategoryDTO model);
        Task<IResponse> UpdateAsync(CategoryDTO model);
    }
}
