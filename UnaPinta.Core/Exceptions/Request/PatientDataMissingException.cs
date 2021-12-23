using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnaPinta.Core.Exceptions.Request
{
    public class PatientDataMissingException : BaseDomainException
    {
        const string NameMsg = "El nombre del paciente es requerido. ";
        const string BirthDateMsg = "La fecha de nacimiento del paciente es requerida. ";
        const string BloodTypeMsg = "El tipo de sangre del paciente es requerido. ";

        public PatientDataMissingException(bool name = false, bool birthDate = false, bool bloodType = false)
        {
            this.StatusCode = 400;
            this.ErrorCode = "E2000";
            this.Title = "Información de paciente incompleta";
            this.spMessage = string.Empty;

            if (name) spMessage += NameMsg;
            if (birthDate) spMessage += BirthDateMsg;
            if (bloodType) spMessage += BloodTypeMsg;
        }
    }
}
