using Core.Utilities.Responses.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Entities.DTOs.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
        Task<Product> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetProductWithImagesByCategoryIdAsync(int categoryid);
        Task<IResponse> AddAsync(ProductDTO model);
        Task<IResponse> UpdateAsync(ProductDTO model);
        Task<IResponse> RemoveAsync(int id);
        Task<IResponse> GetProductDetail(string slug);
        Task<IResponse> GetAdminProducts();
        Task<IResponse> GetAdminProduct(int productid);
        IResponse GetProductsWithImage(ProductFilter filter);
        Task<IResponse> GetLast10ProductsWithImage();
    }
}
