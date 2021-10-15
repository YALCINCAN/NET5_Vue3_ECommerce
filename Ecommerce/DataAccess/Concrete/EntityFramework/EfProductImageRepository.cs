
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductImageRepository : EfEntityRepositoryBase<ProductImage, ECommerceContext>, IProductImageRepository
    {
        public EfProductImageRepository(ECommerceContext context) : base(context)
        {

        }

       
    }
}
