using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnaPinta.Core.Errors
{
    public class BaseDomainException : Exception
    {
        public int StatusCode { get; set; }
        public string Title { get; set; }
        public string ErrorCode { get; set; }
        public string spMessage { get; set; }

        public BaseDomainException() { }
        public BaseDomainException(string message) : base(message) { }

        public object ToResponseObject()
        {
            return new { ErrorCode, Title, Message, spMessage };
        }
    }
}
