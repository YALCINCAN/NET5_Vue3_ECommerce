
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBasketRepository : EfEntityRepositoryBase<Basket, ECommerceContext>, IBasketRepository
    {
        public EfBasketRepository(ECommerceContext context) : base(context)
        {

        }

        public async Task<Basket> GetBasketByUserId(string userid)
        {
            return await _context.Baskets
                    .Include(x => x.BasketItems)
                    .ThenInclude(x => x.Product)
                    .FirstOrDefaultAsync(x => x.UserId == userid);
        }
    }
}
