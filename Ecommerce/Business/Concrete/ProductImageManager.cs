
using System;
using System.Linq;
using System.Collections.Generic;
using Entities.Concrete;
using DataAccess.Abstract;
using System.Threading.Tasks;
using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Core.Utilities.Responses.Abstract;
using Core.Utilities;
using Business.Constants;
using Core.Utilities.Responses.Concrete;
using Core.Exceptions;

namespace Business.Concrete
{
    public class ProductImageManager : IProductImageService
    {
        private IProductImageRepository _productImageRepository;
        public ProductImageManager(IProductImageRepository productImageRepository)
        {
            _productImageRepository = productImageRepository;
        }

        public async Task<IResponse> RemoveAsync(int id)
        {
            var exist = await _productImageRepository.GetByIdAsync(id);
            if (exist != null)
            {
                FileManager.DeleteFile(exist.Image);
                await _productImageRepository.RemoveAsync(exist);
                return new SuccessResponse(200, Messages.DeletedSuccessfully);
            }
            else
            {
                throw new ApiException(404, Messages.NotFound);
            }
        }

    }

}