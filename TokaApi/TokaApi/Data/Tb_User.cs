using System;
using System.Collections.Generic;

#nullable disable

namespace TokaApi.Data
{
    public partial class Tb_User
    {
        public Tb_User()
        {
            Tb_UserTokens = new HashSet<Tb_UserToken>();
        }

        public int UserID { get; set; }
        public string Email { get; set; }
        public string Pasword { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaActualizacion { get; set; }

        public virtual ICollection<Tb_UserToken> Tb_UserTokens { get; set; }
    }
}
