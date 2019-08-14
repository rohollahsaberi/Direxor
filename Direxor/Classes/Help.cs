using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Direxor.Classes
{
    public class EventNumber : EventArgs
    {
        public int ID { get; set; }
        public string PK { get; set; }
    }
}
