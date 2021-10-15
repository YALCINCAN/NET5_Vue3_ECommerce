
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfAddressRepository : EfEntityRepositoryBase<Address, ECommerceContext>, IAddressRepository
    {
        public EfAddressRepository(ECommerceContext context) : base(context)
        {

        }
    }
}
