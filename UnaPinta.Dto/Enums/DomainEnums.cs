using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UnaPinta.Dto.Enums
{
    public enum BloodComponentEnum
    {
        Plasma = 1,
        Plaquetas = 2,
        Globulos_Blancos = 3,
        Globulos_Rojos = 4
    }

    public enum BloodTypeEnum
    {   
        Aplus = 1,
        Aminus = 2,
        Bplus = 3,
        Bminus = 4,
        ABplus = 5,
        ABminus = 6,
        Oplus = 7,
        Ominus = 8
    }

    public enum ConditionEnum
    {
        Tatuaje = 1,
        Piercing = 2,
        Dental = 3,
        Transfusion = 4,
        Donado = 5,
        Embarazada = 6,
        Lactando = 7,
        Inaceptable = 8,
        SinCondicion = 9
    }

    public enum RoleEnum
    {
        Donante = 1,
        Solicitante = 2,
        Administrador = 3
    }
}
