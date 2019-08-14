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

namespace Direxor.VM
{
    public class ChatVM : BaseViewModel
    {
        //private ObservableCollection<ProjectShowModel> _items;
        //public ObservableCollection<ProjectShowModel> Items { get => _items; set { _items = value; OnPropertyChanged("Items"); } }

        private IncrementalLoadingCollection<MediaSource, InstaMedia> _media;
        public IncrementalLoadingCollection<MediaSource, InstaMedia> Media { get => _media; set { _media = value; OnPropertyChanged("Media"); } }

        private ObservableCollection<InstaComment> _chats;
        public ObservableCollection<InstaComment> Chat { get => _chats; set { _chats = value; OnPropertyChanged("Chat"); } }

        private string _status;


        public string Status { get => _status; set { _status = value; OnPropertyChanged("Status"); } }

        public ChatVM()
        {
            ChatPage.MediaChanged += ChatPage_MediaChanged;
            _status = "Loading data";
            //Create API
            new Classes.Ins().SetApi();
            Media = new IncrementalLoadingCollection<MediaSource, InstaMedia>();

            //GetMedia();
        }

        private void ChatPage_MediaChanged(object sender, EventArgs e)
        {
            var m = e as EventNumber;
            GetComment(m.PK);
        }

        private async void GetComment(string pk)
        {
            var coms = await Ins.APi.CommentProcessor.GetMediaCommentsAsync(pk, PaginationParameters.Empty);
            Chat = new ObservableCollection<InstaComment>();
            foreach (var c in coms.Value.Comments)
            {
                Chat.Add(c);
            }
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
