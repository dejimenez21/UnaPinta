using Microsoft.AspNetCore.Mvc.Rendering;
using UnaPinta.Dto.Enums;
using System.Collections.Generic;

namespace Una_Pinta.Helpers.BloodComponentFill
{
    public static class BloodComponentFill
    {
        public static List<SelectListItem> BloodTypes { get; set; } = new List<SelectListItem>();
        public static List<SelectListItem> BloodComponent { get; set; } = new List<SelectListItem>();

        public static List<SelectListItem> LoadBloodTypes()
        {
            BloodTypes.Add(new SelectListItem { Text = "A+", Value = ((int)BloodTypeEnum.Aplus).ToString()});
            BloodTypes.Add(new SelectListItem { Text = "A-", Value = ((int)BloodTypeEnum.Aminus).ToString()});
            BloodTypes.Add(new SelectListItem { Text = "B+", Value = ((int)BloodTypeEnum.Bplus).ToString()});
            BloodTypes.Add(new SelectListItem { Text = "B-", Value = ((int)BloodTypeEnum.Bminus).ToString()});
            BloodTypes.Add(new SelectListItem { Text = "AB+", Value = ((int)BloodTypeEnum.ABplus).ToString()});
            BloodTypes.Add(new SelectListItem { Text = "AB-", Value = ((int)BloodTypeEnum.ABminus).ToString()});
            BloodTypes.Add(new SelectListItem { Text = "O+", Value = ((int)BloodTypeEnum.Oplus).ToString()});
            BloodTypes.Add(new SelectListItem { Text = "O-", Value = ((int)BloodTypeEnum.Ominus).ToString()});

            return BloodTypes;
        }

        public static List<SelectListItem> LoadBloodComponent()
        {
            BloodComponent.Add(new SelectListItem { Text = "Plasma", Value = ((int)BloodComponentEnum.Plasma).ToString()});
            BloodComponent.Add(new SelectListItem { Text = "Plaquetas", Value = ((int)BloodComponentEnum.Plaquetas).ToString()});
            BloodComponent.Add(new SelectListItem { Text = "Globulos Blancos", Value = ((int)BloodComponentEnum.Globulos_Blancos).ToString()});
            BloodComponent.Add(new SelectListItem { Text = "Globulos Rojos", Value = ((int)BloodComponentEnum.Globulos_Rojos).ToString()});

            return BloodComponent;
        }
    }
}
