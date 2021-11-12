using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnaPinta.Dto.Models.User
{
    public class UpdateUserProfileDto
    {
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Numero telefonico no valido")]
        public string PhoneNumber { get; set; }
        public string ProvinceCode { get; set; }
    }
}
