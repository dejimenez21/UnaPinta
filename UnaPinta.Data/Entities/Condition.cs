using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace UnaPinta.Data.Entities
{
    public partial class Condition
    {
        public Condition()
        {
            WaitLists = new HashSet<WaitList>();
        }

        [Key]
        public ConditionEnum Id { get; set; }
        [MaxLength(500)]
        public string Decription { get; set; }
        public int MonthsToWait { get; set; }

        public virtual ICollection<WaitList> WaitLists { get; set; }
    }

    public enum ConditionEnum{
        Tatuaje = 1,
        Piercing = 2,
        Dental = 3,
        Transfusion = 4,
        Donado = 5,
        Embarazada = 6,
        Lactando = 7,
        Inaceptable = 8,
        SinCondicion = 9
    }
}
