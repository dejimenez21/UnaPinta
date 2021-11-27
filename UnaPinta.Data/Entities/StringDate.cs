using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnaPinta.Data.Entities
{
    public class StringDate
    {
        [Key]
        public int Id { get; set; }
        public string String { get; set; }
        public int Hours { get; set; }

        public DateTime ToDateTime(DateTime initialDate)
        {
            return initialDate.AddHours(Hours);
        }
    }
}
