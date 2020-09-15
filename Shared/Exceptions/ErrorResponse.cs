using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Exceptions
{
    public class ErrorResponse
    {
        public int HttpStatus { get; set; }
        public string Message { get; set; }

        public ErrorResponse(int status, string message)
        {
            HttpStatus = status;
            Message = message;
        }

        public override string ToString()
        {
            return HttpStatus + " - " + Message;
        }

    }
}
