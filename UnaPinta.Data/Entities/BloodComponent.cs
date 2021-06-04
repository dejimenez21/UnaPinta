using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace UnaPinta.Data.Entities
{
    public class BloodComponent
    {
        public BloodComponent()
        {
            Requests = new HashSet<Request>();
        }

        [Key]
        public BloodComponentEnum Id { get; set; }

        [MaxLength(50)]
        public string Description { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }

    public enum BloodComponentEnum
    {
        Plasma = 1,
        Plaquetas = 2,
        [Display(Name="Globulos Blancos")]
        GlobulosBlancos = 3,
        [Display(Name="Globulos Rojos")]
        GlobulosRojos = 4
    }
}
