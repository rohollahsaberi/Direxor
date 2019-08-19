using Direxor.Classes;
using InstagramApiSharp.Classes.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Toolkit.Uwp;
using Direxor.VM;
using Direxor.Model;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Direxor.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ChatPage : Page
    {
        public static event EventHandler MediaChanged;
        public static event EventHandler SendComment;
        public static event EventHandler SendReplay;
        private bool isReplay = false;
        private ReChatContiner reChatContiner;

        public ChatPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            lvMedia.SelectionChanged += LvMedia_SelectionChanged;
        }



        private void LvMedia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var m = lvMedia.SelectedItem as InstaMedia;
            MediaChanged(this, new EventNumber { ID = 0, PK = m.Pk });
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            if (isReplay)
            {
                var m = lvMedia.SelectedItem as InstaMedia;
                SendReplay(this, new EventMessage { ID = m.Pk, cmID = reChatContiner.PK, Text = tbMesssage.Text });
                ChatVM.SendReCommentResult += ChatVM_SendReCommentResult;
            }
            else
            {
                var m = lvMedia.SelectedItem as InstaMedia;
                SendComment(this, new EventMessage { ID = m.Pk, Text = tbMesssage.Text });
                ChatVM.SendCommentResult += ChatVM_SendCommentResult;
            }
        }

        private void ChatVM_SendReCommentResult(object sender, EventArgs e)
        {
            var ok = e as EventSendResult;
            if (ok.Result)
            {
                ChatVM.SendCommentResult -= ChatVM_SendCommentResult;
                var m = lvMedia.SelectedItem as InstaMedia;
                MediaChanged(this, new EventNumber { ID = 0, PK = m.Pk });
                tbMesssage.Text = "";
                tbUsername.Text = "";
                grReply.Visibility = Visibility.Collapsed;
            }

        }

        private void ChatVM_SendCommentResult(object sender, EventArgs e)
        {
            var ok = e as EventSendResult;
            if (ok.Result)
            {
                ChatVM.SendCommentResult -= ChatVM_SendCommentResult;
                var m = lvMedia.SelectedItem as InstaMedia;
                MediaChanged(this, new EventNumber { ID = 0, PK = m.Pk });
                tbMesssage.Text = "";
            }
        }

        private void BtnReplay_Click(object sender, RoutedEventArgs e)
        {
            isReplay = true;
            var id = (sender as Button).Tag.ToString();
            reChatContiner = (sender as Button).CommandParameter as ReChatContiner;
            tbParentText.Text = reChatContiner.Text;
            tbUsername.Text = reChatContiner.Username;
            grReply.Visibility = Visibility.Visible;
            tbMesssage.Text = "@" + reChatContiner.Username + " ";
            tbMesssage.Focus(FocusState.Programmatic);
            tbMesssage.Select(tbMesssage.Text.Length, 0);
        }
    }
}
