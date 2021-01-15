using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class CodeSubmit
    {
        [StringLength(6)]
        public string Code { get; set; }

    }
}