using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TokaFront.Models
{
    public class TableDataSet
    {
        public int Draw { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public List<Cliente> Data { get; set; }

        public TableDataSet()
        {
            Data = new List<Cliente>();
        }
    }
}
