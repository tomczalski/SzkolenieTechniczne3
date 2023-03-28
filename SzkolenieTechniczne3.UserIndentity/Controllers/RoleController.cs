using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SzkolenieTechniczne3.UserIdentity.CrossCutting.Dtos;
using SzkolenieTechniczne3.UserIdentity.CrossCutting.Services.Interfaces;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace SzkolenieTechniczne3.UserIndentity.Controllers
{
    [Route("/identity")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("roles")]
        public async Task<IEnumerable<RoleDto>> Read()
        {
            var dto = await _roleService.Read();
            return dto;
        }

        [HttpGet("roles/id")]
        public async Task<RoleDto> ReadById(Guid id)
        {
            var dto = await _roleService.ReadById(id);
            return dto;
        }

        [HttpPost("role")]
        public async Task<IActionResult> Create([FromBody] RoleDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dto = await _roleService.Create(dto);
            return Ok(dto);
        }

        [HttpPut("role")]
        public async Task<IActionResult> Update([FromBody] RoleDto dto) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dto = await _roleService.Update(dto);
            return Ok(dto);
        }

        [HttpDelete("role")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _roleService.Delete(id);
            return NoContent();
        }

        /// <summary>
        /// Assigns role to the user
        /// </summary>
        /// <param name="userId">Identifier of the user</param>
        /// <param name="roleIds">Identifiers of the roles</param>
        /// <returns></returns>
        [HttpPost("roles/assign-to-user")]
        public async Task<IActionResult> AssignRolesToUser(Guid userId, [FromBody] Guid[] roleIds)
        {
            await _roleService.AssignRolesToUser(userId, roleIds);
            return Ok();
        }
    }
}
