using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace TokaApi.Models.Exceptions
{
    public class HttpCustomException : Exception
    {
        public int StatusCode { get; set; }
        public string ContentType { get; set; } = @"text/plain";

        public HttpCustomException(int statusCode)
        {
            this.StatusCode = statusCode;
        }

        public HttpCustomException(int statusCode, string message) : base(message)
        {
            this.StatusCode = statusCode;
        }

        public HttpCustomException(int statusCode, Exception inner) : this(statusCode, inner.ToString()) { }

        public HttpCustomException(int statusCode, JObject errorObject) : this(statusCode, errorObject.ToString())
        {
            this.ContentType = @"application/json";
        }
    }
}
