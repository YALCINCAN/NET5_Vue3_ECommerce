
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using Entities.DTOs.Filters;

namespace DataAccess.Abstract
{
    public interface IProductRepository : IEntityRepository<Product>
    {
        Task<IEnumerable<AdminProductDetails>> GetAdminProducts();
        Task<Product> GetWithOptionsAndImagesByProductId(int productid);
        Task<AdminProductDetail> GetAdminProduct(int productid);
        Task<Product> GetProductWithImagesByIdAsync(int id);
        Task<ProductDetail> GetProductDetail(string slug);
        IQueryable<ProductWithMainImage> GetProductsWithImage(ProductFilter filter);
        Task<IEnumerable<ProductWithMainImage>> GetLast10ProductsWithImage();
        Task<Product> GetProductWithImagesByCategoryIdAsync(int categoryid);
    }
}
