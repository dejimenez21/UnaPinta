using System.Collections.Generic;
using UnaPinta.Dto.Enums;

namespace UnaPinta.Dto.Helpers
{
    public class BloodTypeDictionary
    {
        IDictionary<BloodTypeEnum, List<BloodTypeEnum>> CompatibilityDictionary;

        public BloodTypeDictionary()
        {
            CompatibilityDictionary = new Dictionary<BloodTypeEnum,  List<BloodTypeEnum>>{
               { BloodTypeEnum.Ominus, new List<BloodTypeEnum>{BloodTypeEnum.Ominus} },
               { BloodTypeEnum.Oplus, new List<BloodTypeEnum>{BloodTypeEnum.Ominus, BloodTypeEnum.Oplus} },
               { BloodTypeEnum.Aminus, new List<BloodTypeEnum>{BloodTypeEnum.Ominus, BloodTypeEnum.Aminus} },
               { BloodTypeEnum.Aplus, new List<BloodTypeEnum>{BloodTypeEnum.Ominus, BloodTypeEnum.Aminus, BloodTypeEnum.Oplus, BloodTypeEnum.Aplus} },
               { BloodTypeEnum.Bminus, new List<BloodTypeEnum>{BloodTypeEnum.Ominus, BloodTypeEnum.Bminus} },
               { BloodTypeEnum.Bplus, new List<BloodTypeEnum>{BloodTypeEnum.Ominus, BloodTypeEnum.Bminus, BloodTypeEnum.Oplus, BloodTypeEnum.Bplus } },
               { BloodTypeEnum.ABminus, new List<BloodTypeEnum>{BloodTypeEnum.Ominus, BloodTypeEnum.Aminus, BloodTypeEnum.Bminus, BloodTypeEnum.ABminus} },
               { BloodTypeEnum.ABplus, new List<BloodTypeEnum>{BloodTypeEnum.Ominus, BloodTypeEnum.Aminus, BloodTypeEnum.Bminus, BloodTypeEnum.ABminus, BloodTypeEnum.Oplus, BloodTypeEnum.Aplus, BloodTypeEnum.Bplus, BloodTypeEnum.ABplus} },
            };
        }

        public List<BloodTypeEnum> GetCompatibleWith(BloodTypeEnum bloodType)
        {
            return CompatibilityDictionary[bloodType];
        }
    }
}