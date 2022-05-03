using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TokaApi.Models
{
    public class AuthenticationSettings
    {

        public string KeySecret { get; set; }

        public short Expires { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

       
    }
}
