using System.ComponentModel.DataAnnotations;

namespace UnaPinta.Core.Models
{
    public class CodeSubmit
    {
        [StringLength(6)]
        public string Code { get; set; }

    }
}