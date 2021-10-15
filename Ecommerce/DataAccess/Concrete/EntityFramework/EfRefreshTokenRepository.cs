
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRefreshTokenRepository : EfEntityRepositoryBase<RefreshToken, ECommerceContext>, IRefreshTokenRepository
    {
        public EfRefreshTokenRepository(ECommerceContext context) : base(context)
        {

        }
    }
}
