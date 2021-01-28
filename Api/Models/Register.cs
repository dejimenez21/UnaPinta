using System;
using System.ComponentModel.DataAnnotations;
using Api.Entities;

namespace Api.Models
{
    public class Register
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public bool Sex { get; set; }
        [Required]
        public DateTime? BirthDate { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Phone { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public UserTypeEnum? UserTypeId { get; set; }

        //Data solo de donante
        public string Address { get; set; }
        public int BloodTypeId { get; set; }
        public double? Weight { get; set; }
    }
}