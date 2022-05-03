using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TokaFront.Models
{
    public class UserToken
    {
        public int TokenID { get; set; }
        public string Token { get; set; }
        public int UserID { get; set; }
        public bool Activo { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }
}
