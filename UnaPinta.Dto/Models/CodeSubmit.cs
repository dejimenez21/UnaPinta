using System.ComponentModel.DataAnnotations;

namespace UnaPinta.Dto.Models
{
    public class CodeSubmit
    {
        [StringLength(6)]
        public string Code { get; set; }

    }
}