using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TokaFront.Models
{
    public class ErrorResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }

        public ErrorResponse()
        {
            Errors = new List<string>();
        }
    }
}
