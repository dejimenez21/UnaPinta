using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnaPinta.Core.Exceptions.Request
{
    public class StringDateNotFoundException : BaseDomainException
    {
        const string defaultMessage = "The StringDate doesn't exist";
        public StringDateNotFoundException(string message, int stringDateId) : base(message)
        {
            this.StatusCode = 404;
            this.ErrorCode = "E3005";
            this.Title = "Intervalo de fecha no existe";
            this.spMessage = $"La fecha estimada de respuesta con el id \"{stringDateId}\" no existe.";
        }

        public StringDateNotFoundException(int stringDateId) : this(defaultMessage, stringDateId) { }
    }
}
