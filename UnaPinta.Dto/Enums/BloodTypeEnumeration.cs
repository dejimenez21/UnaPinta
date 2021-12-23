using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Dto.Helpers;

namespace UnaPinta.Dto.Enums
{
    public class BloodTypeEnumeration : Enumeration
    {

        public static readonly BloodTypeEnumeration Aplus = new BloodTypeEnumeration(1, "A+");
        public static readonly BloodTypeEnumeration Aminus = new BloodTypeEnumeration(2, "A-");
        public static readonly BloodTypeEnumeration Bplus = new BloodTypeEnumeration(3, "B+");
        public static readonly BloodTypeEnumeration Bminus = new BloodTypeEnumeration(4, "B-");
        public static readonly BloodTypeEnumeration ABplus = new BloodTypeEnumeration(5, "AB+");
        public static readonly BloodTypeEnumeration ABminus = new BloodTypeEnumeration(6, "AB-");
        public static readonly BloodTypeEnumeration Oplus = new BloodTypeEnumeration(7, "O+");
        public static readonly BloodTypeEnumeration Ominus = new BloodTypeEnumeration(8, "O-");

        public BloodTypeEnumeration(int value, string description) : base(value, description) { }

        public BloodTypeEnumeration() { }

        public static explicit operator BloodTypeEnumeration(int value) => 
            parse<BloodTypeEnumeration, int>(value, "value", item => item.Value == value);

        public static explicit operator BloodTypeEnumeration(string value) =>
            parse<BloodTypeEnumeration, string>(value, "description", item => item.Description == value);

        public IEnumerable<BloodTypeEnumeration> GetCompatibleBloodTypes()
        {
            var dictionary = new BloodTypeDictionary();

            return dictionary.GetCompatibleWith(this);
        }

        public IEnumerable<BloodTypeEnumeration> GetIncompatibleBloodTypesAsReceiverFromList(IEnumerable<BloodTypeEnumeration> bloodTypesList) => 
            bloodTypesList.Except(GetCompatibleBloodTypes());
        
    }
}
