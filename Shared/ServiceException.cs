using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public class ServiceException : Exception
    {
        public string ErrorMessage { get; set; }

        public int StatusCode { get; set; }

        public List<string> Details { get; set; }
            = new List<string>();

        public ServiceException(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public ServiceException(int statusCode)
        {
            StatusCode = statusCode;
        }

        public ServiceException(int statusCode, string errorMessage)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }
    }


}
