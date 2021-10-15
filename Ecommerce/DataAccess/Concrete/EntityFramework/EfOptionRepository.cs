
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOptionRepository : EfEntityRepositoryBase<Option, ECommerceContext>, IOptionRepository
    {
        public EfOptionRepository(ECommerceContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Option>> GetOptionsWithValuesAsync()
        {
            return await _context.Options.Include(x => x.OptionValues).ToListAsync();
        }

       
    }
}
