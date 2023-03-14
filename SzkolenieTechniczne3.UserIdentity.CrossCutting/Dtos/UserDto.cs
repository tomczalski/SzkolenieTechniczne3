using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzkolenieTechniczne3.UserIdentity.CrossCutting.Dtos
{
    public record UserDto
    {
        public Guid Id { get; set; }
        [MaxLength(128)]
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [MinLength(2)]
        [MaxLength(64)]
        [Required]
        public string FirstName { get; set; }
        [MinLength(2)]
        [MaxLength(64)]
        [Required]
        public string LastName { get; set; }

        [MinLength(9)]
        [MaxLength(9)]
        [Required]
        public string PhoneNumber { get; set; }
    }
}
