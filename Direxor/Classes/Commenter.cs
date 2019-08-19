using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Direxor.Classes
{
    public class Commenter
    {
        public async Task<bool> SendComment(string mediaID, string text)
        {
            var send = await Ins.APi.CommentProcessor.CommentMediaAsync(mediaID, text);
            return send.Succeeded;
        }

        public async Task<bool> SendReComment(string mediaID, string cmID, string text)
        {
            var send = await Ins.APi.CommentProcessor.ReplyCommentMediaAsync(mediaID, cmID, text);
            return send.Succeeded;
        }
    }
}
