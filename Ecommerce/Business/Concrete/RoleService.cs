using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Exceptions;
using Core.Utilities.Responses.Abstract;
using Core.Utilities.Responses.Concrete;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RoleService : IRoleService
    {
        private RoleManager<Role> _roleManager;
        private UserManager<User> _userManager;
        private IMapper _mapper;
        public RoleService(RoleManager<Role> roleManager, IMapper mapper, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _userManager = userManager;
        }
        [ValidationAspect(typeof(AddRoleValidator))]
        public async Task<IResponse> AddRole(RoleDTO model)
        {
            var rolefromdb = await _roleManager.FindByNameAsync(model.Name);
            if (rolefromdb == null)
            {
                var role = new Role() { Name = model.Name };
                var Identityresult = await _roleManager.CreateAsync(role);
                if (Identityresult.Succeeded)
                {
                    return new DataResponse<Role>(role, 200, Messages.AddedSuccesfully);
                }
                throw new ApiException(400, Identityresult.Errors.Select(x => x.Description).ToString());
            }
            throw new ApiException(400, Messages.RoleNameIsAlreadyExist);
        }

        [ValidationAspect(typeof(UpdateRoleValidator))]
        public async Task<IResponse> UpdateRole(RoleDTO model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            _mapper.Map(model, role);
            var Identityresult = await _roleManager.UpdateAsync(role);
            if (Identityresult.Succeeded)
            {
                return new SuccessResponse(200, Messages.UpdatedSuccessfully);
            }
            throw new ApiException(400, Messages.RoleNameIsAlreadyExist);
        }

        public async Task<IResponse> RemoveRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return new SuccessResponse(200, Messages.DeletedSuccessfully);
            }
            throw new ApiException(400, result.Errors.Select(x => x.Description).ToString());
        }
        public async Task<IResponse> GetAssignedRoles(string userid)
        {
            var user = await _userManager.FindByIdAsync(userid);
            IQueryable<Role> roles = _roleManager.Roles;
            List<string> userroles = _userManager.GetRolesAsync(user).Result as List<string>;
            List<AssignRole> AssignedRole = new List<AssignRole>();
            foreach (var role in roles)
            {
                AssignRole r = new AssignRole();
                r.UserId = user.Id;
                r.RoleId = role.Id;
                r.RoleName = role.Name;
                if (userroles.Contains(role.Name))
                {
                    r.Exist = true;
                }
                else
                {
                    r.Exist = false;
                }
                AssignedRole.Add(r);
            }
            return new DataResponse<List<AssignRole>>(AssignedRole, 200);
        }

        public async Task<IResponse> GetRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if(role == null)
            {
                throw new ApiException(404, Messages.NotFound);
            }
            var mappedrole = _mapper.Map<RoleDTO>(role);
            return new DataResponse<RoleDTO>(mappedrole, 200);
        }

        public IResponse GetRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return new DataResponse<List<Role>>(roles, 200);
        }

      
        public async Task<IResponse> RoleAssign(List<AssignRole> models)
        {
            foreach (var item in models)
            {
                var user = await _userManager.FindByIdAsync(item.UserId);
                if (item.Exist)

                {
                    await _userManager.AddToRoleAsync(user, item.RoleName);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, item.RoleName);
                }
            }
            return new SuccessResponse(200, Messages.RoleAssignedSuccessfully);
        }
        
    }
}
