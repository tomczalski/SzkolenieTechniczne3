using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SzkolenieTechniczne3.UserIdentity.CrossCutting.Dtos;
using SzkolenieTechniczne3.UserIdentity.Storage.Entities;
using SzkolenieTechniczne3.Common.CrossCutting.Mapping;



namespace SzkolenieTechniczne3.UserIdentity.CrossCutting.Mapping
{
    public class RoleToRoleDtoMapping : Mapping<Role, RoleDto>
    {
        public override Expression<Func<RoleDto, Role>> GetMapping()
        {
            return e => new Role
            {
                Id = e.Id,
                Description = e.Description,
                Name = e.Name,
                UserIds = e.Users.Select(u => u.Id).ToArray()
            };
        }
    }
}
