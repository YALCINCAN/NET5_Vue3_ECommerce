
using System;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;
namespace DataAccess.Abstract
{
    public interface IBasketRepository : IEntityRepository<Basket>
    {
        Task<Basket> GetBasketByUserId(string userid);
    }
}
