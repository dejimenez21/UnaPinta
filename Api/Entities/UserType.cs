using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Api.Entities
{
    public partial class UserType : IdentityRole<int>
    {
        

        public UserType()
        {
            Users = new HashSet<User>();
        }

        //[Key]
        //public UserTypeEnum Id { get; set; }
        [MaxLength(20)]
        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }

    public enum UserTypeEnum{
        Donante = 1,
        Solicitante = 2,
        Administrador = 3
    }
}
