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

    public class EventMessage : EventArgs
    {
        public string ID { get; set; }
        public string Text { get; set; }
        public long cmID { get; set; }
    }

    public class EventSendResult : EventArgs
    {
        public string ID { get; set; }
        public bool Result { get; set; }
        public string Message { get; set; }
    }
}
