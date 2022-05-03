using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TokaApi.Models
{
    public class UserInfo
    {
        public int UserInfoID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }
}
