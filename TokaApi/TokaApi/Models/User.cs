using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TokaApi.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Pasword { get; set; }
        public string Token { get; set; }
        public UserInfo Info { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
