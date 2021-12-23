using System.Collections.Generic;
using UnaPinta.Dto.Enums;

namespace UnaPinta.Dto.Helpers
{
    public class BloodTypeDictionary
    {
        IDictionary<BloodTypeEnumeration, List<BloodTypeEnumeration>> CompatibilityDictionary;

        public BloodTypeDictionary()
        {
            CompatibilityDictionary = new Dictionary<BloodTypeEnumeration,  List<BloodTypeEnumeration>>{
               { BloodTypeEnumeration.Ominus, new List<BloodTypeEnumeration>{ BloodTypeEnumeration.Ominus} },
               { BloodTypeEnumeration.Oplus, new List<BloodTypeEnumeration>{ BloodTypeEnumeration.Ominus, BloodTypeEnumeration.Oplus} },
               { BloodTypeEnumeration.Aminus, new List<BloodTypeEnumeration>{ BloodTypeEnumeration.Ominus, BloodTypeEnumeration.Aminus} },
               { BloodTypeEnumeration.Aplus, new List<BloodTypeEnumeration>{ BloodTypeEnumeration.Ominus, BloodTypeEnumeration.Aminus, BloodTypeEnumeration.Oplus, BloodTypeEnumeration.Aplus} },
               { BloodTypeEnumeration.Bminus, new List<BloodTypeEnumeration>{ BloodTypeEnumeration.Ominus, BloodTypeEnumeration.Bminus} },
               { BloodTypeEnumeration.Bplus, new List<BloodTypeEnumeration>{ BloodTypeEnumeration.Ominus, BloodTypeEnumeration.Bminus, BloodTypeEnumeration.Oplus, BloodTypeEnumeration.Bplus } },
               { BloodTypeEnumeration.ABminus, new List<BloodTypeEnumeration>{ BloodTypeEnumeration.Ominus, BloodTypeEnumeration.Aminus, BloodTypeEnumeration.Bminus, BloodTypeEnumeration.ABminus} },
               { BloodTypeEnumeration.ABplus, new List<BloodTypeEnumeration>{ BloodTypeEnumeration.Ominus, BloodTypeEnumeration.Aminus, BloodTypeEnumeration.Bminus, BloodTypeEnumeration.ABminus, BloodTypeEnumeration.Oplus, BloodTypeEnumeration.Aplus, BloodTypeEnumeration.Bplus, BloodTypeEnumeration.ABplus} },
            };
        }

        public List<BloodTypeEnumeration> GetCompatibleWith(BloodTypeEnumeration bloodType)
        {
            return CompatibilityDictionary[bloodType];
        }
    }
}