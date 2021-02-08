using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Auth
{
    public class RoleCreate
    {
        [Required]
        [MaxLength(30)]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Neither spaces nor special characters are allowed in the role name")]
        public string RoleName { get; set; }
    }
}
