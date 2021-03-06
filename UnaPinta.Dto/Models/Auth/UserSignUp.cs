using System;
using System.ComponentModel.DataAnnotations;


namespace UnaPinta.Dto.Models
{
    public class UserSignUp
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public bool Sex { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe proveer un numero telefonico")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Numero telefonico no valido")]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }
        [Range(1, 8)]
        public int? BloodTypeId { get; set; }
        [Required]
        public string ProvinceCode { get; set; }
        public double? Weight { get; set; }
    }
}