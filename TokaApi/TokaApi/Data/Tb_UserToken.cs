using System;
using System.Collections.Generic;

#nullable disable

namespace TokaApi.Data
{
    public partial class Tb_UserToken
    {
        public int TokenID { get; set; }
        public string Token { get; set; }
        public int UserID { get; set; }
        public bool Activo { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual Tb_User User { get; set; }
    }
}
