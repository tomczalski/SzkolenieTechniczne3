using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SzkolenieTechniczne3.UserIdentity.CrossCutting.Dtos;
using SzkolenieTechniczne3.Common.CrossCutting.Mapping;
using SzkolenieTechniczne3.UserIdentity.Storage.Entities;
using System.Linq.Expressions;

namespace SzkolenieTechniczne3.UserIdentity.CrossCutting.Mapping
{
    public class RoleDtoToRoleMapping : Mapping<RoleDto, Role>
    {
        public override Expression<Func<RoleDto, Role>> GetMapping()
        {
            return d => new Role
            {
                Id = d.Id,
                Description = d.Description,
                Name = d.Name
            };
        }

        
}
