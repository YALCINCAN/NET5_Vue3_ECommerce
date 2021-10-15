
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBrandRepository : EfEntityRepositoryBase<Brand, ECommerceContext>, IBrandRepository
    {
        public EfBrandRepository(ECommerceContext context) : base(context)
        {

        }
    }
}
