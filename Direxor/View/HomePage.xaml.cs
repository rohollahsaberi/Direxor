using Direxor.Classes;
using InstagramApiSharp;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Direxor.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //Create API
            new Classes.Ins().SetApi();
            GetMedia();
        }
        public async void GetMedia()
        {
            var media = await Ins.APi.UserProcessor.GetUserMediaAsync("beheshtstudio", PaginationParameters.MaxPagesToLoad(1));
            if (media.Succeeded)
            {
                var coms = await Ins.APi.CommentProcessor.GetMediaCommentsAsync(media.Value[5].Pk, PaginationParameters.Empty);
                var x = coms.Value.CommentsCount;
            }
        }
    }
}
