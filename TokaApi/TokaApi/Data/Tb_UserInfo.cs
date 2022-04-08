using System;
using System.Collections.Generic;

#nullable disable

namespace TokaApi.Data
{
    public partial class Tb_UserInfo
    {
        public int UserInfoID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual Tb_User User { get; set; }
    }
}
