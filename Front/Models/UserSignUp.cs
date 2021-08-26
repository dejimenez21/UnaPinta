﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Una_Pinta.Models
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
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string ProvinceCode { get; set; }

        public int? BloodTypeId { get; set; }

        public double? Weight { get; set; }
    }
}
