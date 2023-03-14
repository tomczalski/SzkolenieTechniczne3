using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzkolenieTechniczne3.UserIdentity.CrossCutting.Dtos
{
    public record RoleDto
    {
        public Guid Id { get; set; }

        [MinLength(2)]
        [MaxLength(64)]
        [Required]
        public string Name { get; set; } = null!;

        [MaxLength(300)]
        public string Description { get; set; } = null!;

        public Guid[]? UserIds { get; set; }
    }
}
