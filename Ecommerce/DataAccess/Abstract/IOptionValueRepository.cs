
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;
namespace DataAccess.Abstract
{
    public interface IOptionValueRepository : IEntityRepository<OptionValue>
    {
        Task<IEnumerable<OptionValue>> GetOptionValuesWithOption();
        Task<IEnumerable<OptionValue>> GetOptionValuesByOptionIdAsync(int optionid);
    }
}
