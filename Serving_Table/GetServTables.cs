using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serving_Table
{
    public class GetServTables
    {
        private static GetServTables instance;
        public static GetServTables GetInstance()
        {
            if (instance == null)
            {
                instance = new GetServTables();
            }
            return instance;
        }
        public List<string> Tables { get; set; }

        GetServTables()
        {
            Tables = new List<string>()
            {
                "1",
                "2",
                "3",
                "4",
                "5"
            };
        }

        public void ObtenerMesas()
        {
            Tables.Add("1");
            Tables.Add("2");
            Tables.Add("3");
            Tables.Add("4");
            Tables.Add("5");
            
        }
    }
}
