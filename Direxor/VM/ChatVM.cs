using Direxor.Classes;
using Direxor.View;
using InstagramApiSharp;
using InstagramApiSharp.Classes.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp;
using Microsoft.Toolkit.Collections;
using System.Threading;
using Direxor.Model;

namespace Direxor.VM
{
    public class ChatVM : BaseViewModel
    {
        public static event EventHandler SendCommentResult;
        public static event EventHandler SendReCommentResult;

        private IncrementalLoadingCollection<MediaSource, InstaMedia> _media;
        public IncrementalLoadingCollection<MediaSource, InstaMedia> Media { get => _media; set { _media = value; OnPropertyChanged("Media"); } }

        private ObservableCollection<ChatModel> _chats;
        public ObservableCollection<ChatModel> Chat { get => _chats; set { _chats = value; OnPropertyChanged("Chat"); } }



        private string _status;


        public string Status { get => _status; set { _status = value; OnPropertyChanged("Status"); } }

        public ChatVM()
        {
            ChatPage.MediaChanged += ChatPage_MediaChanged;
            ChatPage.SendComment += ChatPage_SendComment;
            ChatPage.SendReplay += ChatPage_SendReplay;
            _status = "Loading data";
            //Create API
            new Classes.Ins().SetApi();
            Media = new IncrementalLoadingCollection<MediaSource, InstaMedia>();

            //GetMedia();
        }

        private async void ChatPage_SendReplay(object sender, EventArgs e)
        {
            var m = e as EventMessage;
            var send = await new Commenter().SendReComment(m.ID, m.cmID.ToString(), m.Text);
            SendReCommentResult(this, new EventSendResult { ID = m.ID, Result = send });
        }

        private async void ChatPage_SendComment(object sender, EventArgs e)
        {
            var m = e as EventMessage;
            var send = await new Commenter().SendComment(m.ID, m.Text);
            SendCommentResult(this, new EventSendResult { ID = m.ID, Result = send });
        }

        private void ChatPage_MediaChanged(object sender, EventArgs e)
        {
            var m = e as EventNumber;
            GetComment(m.PK);
        }

        private async void GetComment(string pk)
        {
            var coms = await Ins.APi.CommentProcessor.GetMediaCommentsAsync(pk, PaginationParameters.Empty);
            Chat = new ObservableCollection<ChatModel>();
            foreach (var c in coms.Value.Comments)
            {
                Chat.Add(new ChatModel
                {
                    CreatedAt = c.CreatedAt,
                    HasLikedComment = c.HasLikedComment,
                    LikeCount = c.LikesCount,
                    PK = c.Pk,
                    Text = c.Text,
                    UserFullName = c.User.FullName,
                    Username = c.User.UserName,
                    UserProfile = c.User.ProfilePicUrl,
                    UserPk = c.User.Pk,
                    reChats = await GetReComment(pk, c.Pk.ToString())
                });
            }
        }

        private async Task<List<ReChatModel>> GetReComment(string pk, string v)
        {
            List<ReChatModel> reChats = new List<ReChatModel>();
            var recoms = await Ins.APi.CommentProcessor.GetMediaRepliesCommentsAsync(pk, v, PaginationParameters.Empty);
            foreach (var r in recoms.Value.ChildComments)
            {
                reChats.Add(new ReChatModel
                {
                    CreatedAt = r.CreatedAt,
                    HasLikedComment = r.HasLikedComment,
                    LikeCount = r.LikesCount,
                    ParentPk = pk,
                    Text = r.Text,
                    PK = r.Pk,
                    UserFullName = r.User.FullName,
                    Username = r.User.UserName,
                    UserPk = r.User.Pk,
                    UserProfile = r.User.ProfilePicUrl,
                    ReChat = new ReChatContiner
                    {
                        PK = r.Pk,
                        Text = r.Text,
                        Username = r.User.UserName,
                        UserPk = r.User.Pk
                    }
                });
            }
            return reChats;
        }

        //private async void GetMedia()
        //{
        //    var media = await Ins.APi.UserProcessor.GetUserMediaAsync("beheshtstudio", PaginationParameters.MaxPagesToLoad(1));
        //    if (media.Succeeded)
        //    {
        //        Media = new ObservableCollection<InstaMedia>();
        //        foreach (var m in media.Value)
        //        {
        //            Media.Add(m);
        //        }
        //        var coms = await Ins.APi.CommentProcessor.GetMediaCommentsAsync(media.Value[6].Pk, PaginationParameters.Empty);
        //        Chat = new ObservableCollection<InstaComment>();
        //        foreach (var c in coms.Value.Comments)
        //        {
        //            Chat.Add(c);
        //        }
        //    }
        //}
    }


    public class MediaSource : IIncrementalSource<InstaMedia>
    {
        public static PaginationParameters Pagination = PaginationParameters.MaxPagesToLoad(1);
        public async Task<IEnumerable<InstaMedia>> GetPagedItemsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            // Gets items from the collection according to pageIndex and pageSize parameters.
            //var result = (from p in _people
            //              select p).Skip(pageIndex * pageSize).Take(pageSize);
            if (Pagination.NextMaxId != null)
            {
                var media = await Ins.APi.UserProcessor.GetUserMediaAsync("ra.sad", Pagination);
                if (media.Succeeded)
                {
                    Pagination.NextMaxId = media.Value.NextMaxId;
                }

                // Simulates a longer request...
                await Task.Delay(1);

                return media.Value;
            }
            else
                return null;
        }
    }


}
