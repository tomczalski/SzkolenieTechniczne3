using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SzkolenieTechniczne3.Common.CrossCutting.Mapping;
using SzkolenieTechniczne3.UserIdentity.CrossCutting.Dtos;
using SzkolenieTechniczne3.UserIdentity.Storage.Entities;

namespace SzkolenieTechniczne3.UserIdentity.CrossCutting.Mapping
{
    public class UserDtoToUserMapping : Mapping<UserDto, User>
    {
        public override Expression<Func<UserDto, User>> GetMapping()
        {
            return d => new User
            {
                Id = d.Id,
                Email = d.Email,
                FirstName = d.FirstName,
                LastName = d.LastName,
                PhoneNumber = d.PhoneNumber
            };
        }
    }
}
