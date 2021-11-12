using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Una_Pinta.Models
{
    public class Component
    {
        public int id { get; set; }
        public string description { get; set; }
        public List<object> requests { get; set; }
    }
}
