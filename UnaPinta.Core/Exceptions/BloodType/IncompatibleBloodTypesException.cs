using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnaPinta.Core.Exceptions.BloodType
{
    public class IncompatibleBloodTypesException : BaseDomainException
    {
        const string defaultMessage = "Some requested bloodtype are not compatible with patient's bloodtype.";

        public IncompatibleBloodTypesException(IEnumerable<string> requestedBloodType, string patientBloodType, string message) : base(message)
        {
            this.StatusCode = 400;
            this.ErrorCode = "EXXX";
            this.Title = "Tipos de sangre incompatibles";
            this.spMessage = $"No se puede registrar la solicitud porque los tipos de sangre solicitados {String.Join(", ", requestedBloodType.ToArray())} no son compatibles con el tipo de sangre {patientBloodType} del paciente.";
        }

        public IncompatibleBloodTypesException(IEnumerable<string> requestedBloodType, string patientBloodType) : this(requestedBloodType, patientBloodType, defaultMessage) { }
    }
}
