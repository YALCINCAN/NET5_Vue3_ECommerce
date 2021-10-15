
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
using Core.Utilities;
using Core.Utilities.Responses.Concrete;
using Business.Constants;
using Core.Aspects.Autofac.Validation;
using Business.ValidationRules.FluentValidation;
using Entities.DTOs.Filters;
using Core.Utilities.Extensions;
using Core.Exceptions;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductRepository _productRepository;
        private IMapper _mapper;
        public ProductManager(IProductRepository productRepository,IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }
        [ValidationAspect(typeof(AddProductValidator))]
        public async Task<IResponse> AddAsync(ProductDTO model)
        {
            var product = _mapper.Map<Product>(model);
            product.Slug = SlugHelper.Slugify(model.Name);
            product.MainImage = FileManager.SaveFile(FolderNames.Products, model.Image);
            product.ProductOptionValues = new List<ProductOptionValue>();
            foreach (var productoptionvalue in model.OptionValues)
            {
                product.ProductOptionValues.Add(new ProductOptionValue()
                {
                    OptionId = productoptionvalue.OptionId,
                    OptionValueId = productoptionvalue.OptionValueId
                });
            }
            product.ProductImages = new List<ProductImage>();
            foreach (var productimage in model.Images)
            {
                product.ProductImages.Add(new ProductImage()
                {
                    Image = FileManager.SaveFile(FolderNames.Products,productimage)
                });
            }
            await _productRepository.AddAsync(product);
            return new SuccessResponse(200, Messages.AddedSuccesfully);
        }

        [ValidationAspect(typeof(UpdateProductValidator))]
        public async Task<IResponse> UpdateAsync(ProductDTO model)
        {
            var product = await _productRepository.GetWithOptionsAndImagesByProductId(model.Id);
            var updatedproduct = _mapper.Map(model, product);
            if (model.Image != null)
            {
                FileManager.DeleteFile(product.MainImage);
                updatedproduct.MainImage = FileManager.SaveFile(FolderNames.Products, model.Image);
            }
            else
            {
                updatedproduct.MainImage = product.MainImage;
            }
            updatedproduct.Slug = SlugHelper.Slugify(model.Name);
            product.ProductOptionValues = model.OptionValues.Select(pow => new ProductOptionValue()
            {
                OptionId = pow.OptionId,
                OptionValueId = pow.OptionValueId
            }).ToList();
            if (model.Images != null)
            {
                foreach (var productimage in model.Images)
                {
                    product.ProductImages.Add(new ProductImage()
                    {
                        Image = FileManager.SaveFile(FolderNames.Products, productimage)
                    });
                }
            }
            await _productRepository.UpdateAsync(product);
            return new SuccessResponse(200, Messages.UpdatedSuccessfully);
        }

        public async Task<IResponse> RemoveAsync(int id)
        {
            var product = await _productRepository.GetProductWithImagesByIdAsync(id);
            if (product != null)
            {
                foreach (var item in product.ProductImages)
                {
                    FileManager.DeleteFile(item.Image);
                }
                FileManager.DeleteFile(product.MainImage);
                await _productRepository.RemoveAsync(product);
            }
            return new SuccessResponse(200, Messages.DeletedSuccessfully);
        }

        public async Task<IResponse> GetAdminProducts()
        {
            var adminproducts = await _productRepository.GetAdminProducts();
            return new DataResponse<IEnumerable<AdminProductDetails>>(adminproducts, 200);
        }

        public async Task<IResponse> GetAdminProduct(int productid)
        {
            var adminproduct = await _productRepository.GetAdminProduct(productid);
            return new DataResponse<AdminProductDetail>(adminproduct, 200);
        }

        public async Task<IResponse> GetProductDetail(string slug)
        {
            var productdetail = await _productRepository.GetProductDetail(slug);
            if (productdetail == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            return new DataResponse<ProductDetail>(productdetail, 200);
        }

        public IResponse GetProductsWithImage(ProductFilter filter)
        {
            var productswithimages = _productRepository.GetProductsWithImage(filter);
            var pagedproductswithimages = productswithimages.ApplyPaging(filter.Page, filter.PageSize);
            return new PagedDataResponse<IQueryable<ProductWithMainImage>>(pagedproductswithimages, 200, productswithimages.Count());
        }

        public async Task<IResponse> GetLast10ProductsWithImage()
        {
            var last10productswithimages = await _productRepository.GetLast10ProductsWithImage();
            return new DataResponse<IEnumerable<ProductWithMainImage>>(last10productswithimages, 200);
        }

        public async Task<Product> GetProductWithImagesByCategoryIdAsync(int categoryid)
        {
            return await _productRepository.GetProductWithImagesByCategoryIdAsync(categoryid);
        }
    }
}