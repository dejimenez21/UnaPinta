using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Una_Pinta.Helpers.BloodComponentFill
{
    public static class BloodComponentFill
    {
        public static List<SelectListItem> BloodTypes { get; set; } = new List<SelectListItem>();
        public static List<SelectListItem> BloodComponent { get; set; } = new List<SelectListItem>();

        public static List<SelectListItem> LoadBloodTypes()
        {
            BloodTypes.Add(new SelectListItem { Text = "A+", Value = "1" });
            BloodTypes.Add(new SelectListItem { Text = "A-", Value = "2" });
            BloodTypes.Add(new SelectListItem { Text = "B+", Value = "3" });
            BloodTypes.Add(new SelectListItem { Text = "B-", Value = "4" });
            BloodTypes.Add(new SelectListItem { Text = "AB+", Value = "5" });
            BloodTypes.Add(new SelectListItem { Text = "AB-", Value = "6" });
            BloodTypes.Add(new SelectListItem { Text = "O+", Value = "7" });
            BloodTypes.Add(new SelectListItem { Text = "O-", Value = "8" });

            return BloodTypes;
        }

        public static List<SelectListItem> LoadBloodComponent()
        {
            BloodComponent.Add(new SelectListItem { Text = "Plasma", Value = "1" });
            BloodComponent.Add(new SelectListItem { Text = "Plaquetas", Value = "2" });
            BloodComponent.Add(new SelectListItem { Text = "Globulos Blancos", Value = "3" });
            BloodComponent.Add(new SelectListItem { Text = "Globulos Rojos", Value = "4" });

            return BloodComponent;
        }
    }
}
