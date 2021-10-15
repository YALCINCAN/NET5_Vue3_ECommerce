﻿
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOrderStatusRepository : EfEntityRepositoryBase<OrderStatus, ECommerceContext>, IOrderStatusRepository
    {
        public EfOrderStatusRepository(ECommerceContext context) : base(context)
        {

        }
    }
}
