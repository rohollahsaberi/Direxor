using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Direxor.Model
{
    public class ChatModel
    {
        public DateTime CreatedAt { get; set; }
        public long PK { get; set; }
        public string Text { get; set; }
        public string UserFullName { get; set; }
        public long UserPk { get; set; }
        public string Username { get; set; }
        public string UserProfile { get; set; }
        public bool HasLikedComment { get; set; }
        public int LikeCount { get; set; }
        public List<ReChatModel> reChats { get; set; }
    }

    public class ReChatModel
    {
        public string ParentPk { get; set; }
        public DateTime CreatedAt { get; set; }
        public long PK { get; set; }
        public string Text { get; set; }
        public string UserFullName { get; set; }
        public long UserPk { get; set; }
        public string UserProfile { get; set; }
        public string Username { get; set; }
        public bool HasLikedComment { get; set; }
        public int LikeCount { get; set; }
        public ReChatContiner ReChat { get; set; }
        public ICommand Command { get; set; }
    }
}
