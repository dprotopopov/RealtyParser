using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace RealtyParser
{
    public class HierarhialItem
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string ParentId { get; set; }
        public int Level { get; set; }
    }
}
