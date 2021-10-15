using Core.Utilities.Responses.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRoleService
    {
        Task<IResponse> AddRole(RoleDTO model);
        Task<IResponse> UpdateRole(RoleDTO model);
        Task<IResponse> RemoveRole(string id);
        Task<IResponse> GetRole(string id);
        IResponse GetRoles();
        Task<IResponse> GetAssignedRoles(string userid);
        Task<IResponse> RoleAssign(List<AssignRole> models);
    }
}
