using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnaPinta.Data.Entities
{
    public class File
    {
        [Key]
        public long Id { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public byte[] FileContent { get; set; }
    }
}
