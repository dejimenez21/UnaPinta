using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Api.Entities
{
    public partial class Role : IdentityRole<int>
    {
        

        public Role()
        {
            
        }

        //[Key]
        //public RoleEnum Id { get; set; }
        [MaxLength(20)]
        public string Description { get; set; }

    }

    public enum RoleEnum{
        Donante = 1,
        Solicitante = 2,
        Administrador = 3
    }
}
