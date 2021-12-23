using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Dto.Enums;

namespace UnaPinta.Core.Exceptions.BloodType
{
    public class IncompatibleBloodTypesException : BaseDomainException
    {
        const string defaultMessage = "Some requested bloodtype are not compatible with patient's bloodtype.";

        public IncompatibleBloodTypesException(IEnumerable<BloodTypeEnumeration> requestedBloodType, BloodTypeEnumeration patientBloodType, string message) : base(message)
        {
            this.StatusCode = 400;
            this.ErrorCode = "E5000";
            this.Title = "Tipos de sangre incompatibles";
            this.spMessage = $"No se puede registrar la solicitud porque los tipos de sangre solicitados {String.Join(", ", requestedBloodType.Select(x => x.Description).ToArray())} no son compatibles con el tipo de sangre {patientBloodType.Description} del paciente.";
        }

        public IncompatibleBloodTypesException(IEnumerable<BloodTypeEnumeration> requestedBloodType, BloodTypeEnumeration patientBloodType) : this(requestedBloodType, patientBloodType, defaultMessage) { }
    }
}
