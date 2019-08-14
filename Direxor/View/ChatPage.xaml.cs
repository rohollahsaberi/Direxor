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


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Direxor.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ChatPage : Page
    {
        public static event EventHandler MediaChanged;



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
    }
}
