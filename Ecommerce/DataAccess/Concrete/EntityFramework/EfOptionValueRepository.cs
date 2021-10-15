
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOptionValueRepository : EfEntityRepositoryBase<OptionValue, ECommerceContext>, IOptionValueRepository
    {
        public EfOptionValueRepository(ECommerceContext context) : base(context)
        {

        }

        public async Task<IEnumerable<OptionValue>> GetOptionValuesByOptionIdAsync(int optionid)
        {
            return await _context.OptionValues.Where(x => x.OptionId == optionid).ToListAsync();
        }

        public async Task<IEnumerable<OptionValue>> GetOptionValuesWithOption()
        {
            return await _context.OptionValues.Include(x => x.Option).ToListAsync();
        }
    }
}
