using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnaPinta.Core.Exceptions.Request
{
    public class RequestNotFoundException : BaseDomainException
    {
        const string defaultMessage = "The request doesn't exist";
        public RequestNotFoundException(string message, int requestID) : base(message)
        {
            this.StatusCode = 404;
            this.ErrorCode = "E2000";
            this.Title = "Solicitud no existe";
            this.spMessage = $"La solicitud con el id \"{requestID}\" no existe.";
        }

        public RequestNotFoundException(int requestId) : this(defaultMessage, requestId) { }
    }
}
