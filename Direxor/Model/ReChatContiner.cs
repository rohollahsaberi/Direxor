using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Direxor.Model
{
    public class ReChatContiner
    {
        public long PK { get; set; }
        public string Text { get; set; }
        public long UserPk { get; set; }
        public string Username { get; set; }
    }
}
