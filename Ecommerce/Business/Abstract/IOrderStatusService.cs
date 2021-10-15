
using Core.Utilities.Responses.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOrderStatusService
    {
        Task<IResponse> GetAllAsync();
    }
}
