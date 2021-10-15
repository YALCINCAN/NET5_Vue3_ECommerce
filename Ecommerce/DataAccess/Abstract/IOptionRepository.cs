
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;
namespace DataAccess.Abstract
{
    public interface IOptionRepository : IEntityRepository<Option>
    {
        Task<IEnumerable<Option>> GetOptionsWithValuesAsync();
    }
}
