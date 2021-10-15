using Business.Abstract;
using Core.ActionFilters;
using Core.Utilities.Responses.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private IRoleService _roleService;
        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IResponse GetRoles()
        {
            var roles = _roleService.GetRoles();
            return roles;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IResponse> GetRole(string id)
        {
            var result = await _roleService.GetRole(id);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ServiceFilter(typeof(NullFilterAttribute))]
        public async Task<IResponse> AddRole(RoleDTO model)
        {
            var result = await _roleService.AddRole(model);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [ServiceFilter(typeof(NullFilterAttribute))]
        public async Task<IResponse> UpdateRole(RoleDTO model)
        {
            var result = await _roleService.UpdateRole(model);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IResponse> RemoveRole(string id)
        {
            var result = await _roleService.RemoveRole(id);
            return result;
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("getassignedroles/{userid}")]
        public async Task<IResponse> GetAssignedRoles(string userid)
        {
            var result = await _roleService.GetAssignedRoles(userid);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("roleassign")]
        [ServiceFilter(typeof(NullFilterAttribute))]
        public async Task<IResponse> RoleAssign(List<AssignRole> models)
        {
            var result = await _roleService.RoleAssign(models);
            return result;
        }
    }
}
