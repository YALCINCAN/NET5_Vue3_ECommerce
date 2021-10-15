
using Core.Utilities.Responses.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductImageService
    {
        Task<IResponse> RemoveAsync(int id);
    }
}
