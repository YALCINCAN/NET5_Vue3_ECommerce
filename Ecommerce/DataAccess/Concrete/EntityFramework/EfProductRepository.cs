
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using System.Threading.Tasks;
using Entities.DTOs;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Entities.DTOs.Filters;
using AutoFilterer.Extensions;
using Core.Utilities;
using LinqKit;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductRepository : EfEntityRepositoryBase<Product, ECommerceContext>, IProductRepository
    {
        public EfProductRepository(ECommerceContext context) : base(context)
        {

        }

        public async Task<AdminProductDetail> GetAdminProduct(int productid)
        {
            var product = await _context.Products.Select(product => new AdminProductDetail()
            {
                Id = product.Id,
                Description = product.Description,
                MainImage = product.MainImage,
                Name = product.Name,
                BrandId = product.BrandId,
                CategoryId = product.CategoryId,
                Price = product.Price.ToString("N"),
                ProductOptionValues = product.ProductOptionValues.Select(pov => new AdminProductOptionValueDetailDTO()
                {
                    OptionId = pov.OptionId,
                    OptionValueId = pov.OptionValueId
                }),
                ProductImages = product.ProductImages.Select(productimage => new ProductImageDTO()
                {
                    Id = productimage.Id,
                    Image = productimage.Image
                })
            }).FirstOrDefaultAsync(x => x.Id == productid);
            return product;
        }

        public async Task<IEnumerable<AdminProductDetails>> GetAdminProducts()
        {
            var product = await _context.Products.Select(product => new AdminProductDetails()
            {
                Id = product.Id,
                Description = product.Description,
                Price = product.Price.ToString("N"),
                Name = product.Name,
                BrandName = product.Brand.Name,
                CategoryName = product.Category.Name,
            }).ToListAsync();
            return product;
        }

        public async Task<Product> GetProductWithImagesByIdAsync(int id)
        {
            return await _context.Products.Include(x => x.ProductImages).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ProductDetail> GetProductDetail(string slug)
        {
            var product = await _context.Products.Select(product => new ProductDetail()
            {
                Id = product.Id,
                Description = product.Description,
                Name = product.Name,
                MainImage = product.MainImage,
                Slug = product.Slug,
                Price = product.Price.ToString("N"),
                Brand = new BrandDetailDTO()
                {
                    Name = product.Brand.Name,
                    Slug = product.Brand.Slug,
                },
                ProductOptionValues = product.ProductOptionValues.Select(pov => new ProductOptionValueDetailDTO()
                {
                    OptionName = pov.Option.Name,
                    OptionValue = pov.OptionValue.Value
                }),
                ProductImages = product.ProductImages.Select(productimage => new ProductImageDTO()
                {
                    Id = productimage.Id,
                    Image = productimage.Image
                }),
                Reviews = product.Reviews.Select(review => new ReviewDetail()
                {
                    Id = review.Id,
                    Description = review.Description,
                    ReviewDate = review.ReviewDate,
                    RatingValue = review.RatingValue,
                    User = new UserDetail()
                    {
                        Id = review.User.Id,
                        FirstName = review.User.FirstName,
                        LastName = review.User.LastName
                    }
                })
            }).FirstOrDefaultAsync(x => x.Slug == slug);
            return product;
        }

        public IQueryable<ProductWithMainImage> GetProductsWithImage(ProductFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.Search))
            {
                var terms = filter.Search.Split(" ");
                return _context.Products.WhereAll(terms, (u, m) => u.Name.Contains(m) || u.Slug.Contains(m) ||
                                    u.Description.Contains(m)).ApplyFilter(filter).Select(product => new ProductWithMainImage()
                                    {
                                        Id = product.Id,
                                        Name = product.Name,
                                        Description = product.Description,
                                        Price = product.Price.ToString("N"),
                                        BrandName = product.Brand.Name,
                                        Slug = product.Slug,
                                        MainImage = product.MainImage,
                                        RatingAverage = product.Reviews.Select(review => (int?)(review.RatingValue)).Cast<decimal>().Average()
                                    }).AsQueryable();
            }
            else if (filter.ProductOptionValues!=null)
            {
                var predicate = PredicateBuilder.New<Product>();
                foreach (string value in filter.ProductOptionValues.OptionValue.Value)
                    predicate = predicate.And((x => x.ProductOptionValues.Any(x => x.OptionValue.Value == value)));

                return _context.Products.Where(predicate).ApplyFilter(filter).Select(product => new ProductWithMainImage()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price.ToString("N"),
                    BrandName = product.Brand.Name,
                    Slug = product.Slug,
                    MainImage = product.MainImage,
                    RatingAverage = product.Reviews.Select(review => (int?)(review.RatingValue)).Cast<decimal>().Average()
                }).AsQueryable();
            }
            else
            {
                return _context.Products.ApplyFilter(filter).Select(product => new ProductWithMainImage()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price.ToString("N"),
                    BrandName = product.Brand.Name,
                    Slug = product.Slug,
                    MainImage = product.MainImage,
                    RatingAverage = product.Reviews.Select(review => (int?)(review.RatingValue)).Cast<decimal>().Average()
                }).AsQueryable();
            }
        }

        public async Task<IEnumerable<ProductWithMainImage>> GetLast10ProductsWithImage()
        {
            return await _context.Products.Select(product => new ProductWithMainImage()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price.ToString("N"),
                BrandName = product.Brand.Name,
                Slug = product.Slug,
                MainImage = product.MainImage,
                RatingAverage = product.Reviews.Select(review => (int?)(review.RatingValue)).Cast<decimal>().Average()
            }).OrderByDescending(x => x.Id).Take(10).ToListAsync();
        }

        public async Task<Product> GetWithOptionsAndImagesByProductId(int productid)
        {
            return await _context.Products
              .Include(i => i.ProductImages)
              .Include(i => i.ProductOptionValues)
              .FirstOrDefaultAsync(i => i.Id == productid);
        }

        public async Task<Product> GetProductWithImagesByCategoryIdAsync(int categoryid)
        {
            return await _context.Products.Include(x => x.ProductImages).FirstOrDefaultAsync(x => x.CategoryId == categoryid);
        }
    }
}
