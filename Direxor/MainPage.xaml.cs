using Div.Model;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Direxor
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static Frame mainFrame;
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //Set Main frame to access from other page
            mainFrame = frm;

            using (var db = new DivxModel())
            {
                //Database here?
                try
                {
                    //Find first user:
                    var user = db.Users.FirstOrDefault();
                    //db.Users.Remove(user);
                    //db.SaveChanges();
                    if (user != null)
                    {
                        mainFrame.Navigate(typeof(View.ChatPage));
                    }
                    //Not user here, please login first
                    else
                    {
                        mainFrame.Navigate(typeof(View.LoginPage));
                    }
                }
                catch
                {
                    mainFrame.Navigate(typeof(View.LoginPage));
                }

            }
        }
    }
}
