using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Exceptions
{
    public class ServiceException : Exception
    {
        public ErrorResponse response { get; set; }
        public string ErrorMessage { get; set; }

        public int StatusCode { get; set; }

        public List<string> Details { get; set; }
            = new List<string>();

        public ServiceException(int statusCode, string errorMessage)
        {
            response = new ErrorResponse(statusCode, errorMessage);
        }
    }


}
