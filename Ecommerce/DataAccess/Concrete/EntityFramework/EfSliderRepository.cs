
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfSliderRepository : EfEntityRepositoryBase<Slider, ECommerceContext>, ISliderRepository
    {
        public EfSliderRepository(ECommerceContext context) : base(context)
        {

        }
    }
}
