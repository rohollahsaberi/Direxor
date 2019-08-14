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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Direxor.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private async void Login()
        {
            var result = await new Classes.Login().Loginasync(tbUsername.Text, tbPassword.Password);
            switch (result.Value)
            {
                case InstagramApiSharp.Classes.InstaLoginResult.Success:
                    {
                        SaveUser();
                    }
                    break;
                case InstagramApiSharp.Classes.InstaLoginResult.BadPassword:
                    {
                        //MessageBox.Show("رمز عبور وارد شده اشتباه است.", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                        //gLogin.IsEnabled = true;
                    }
                    break;
                case InstagramApiSharp.Classes.InstaLoginResult.InvalidUser:
                    {
                        //MessageBox.Show("نام کاربری وارد شده اشتباه است.", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                        //gLogin.IsEnabled = true;
                    }
                    break;
                case InstagramApiSharp.Classes.InstaLoginResult.TwoFactorRequired:
                    {
                        // MainWindow.mFrame.Navigate(new Uri("View/TwoFactorPage.xaml", UriKind.RelativeOrAbsolute));
                    }
                    break;
                case InstagramApiSharp.Classes.InstaLoginResult.Exception:
                    {
                        //MessageBox.Show(result.Info.Message, "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                        //gLogin.IsEnabled = true;
                    }
                    break;
                case InstagramApiSharp.Classes.InstaLoginResult.ChallengeRequired:
                    {
                        // MainWindow.mFrame.Navigate(new Uri("View/ChallengePage.xaml", UriKind.RelativeOrAbsolute));
                    }
                    break;
                case InstagramApiSharp.Classes.InstaLoginResult.LimitError:
                    {
                        //MessageBox.Show(result.Info.Message, "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                        //gLogin.IsEnabled = true;
                    }
                    break;
                case InstagramApiSharp.Classes.InstaLoginResult.InactiveUser:
                    {
                        //MessageBox.Show(result.Info.Message, "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                        //gLogin.IsEnabled = true;
                    }
                    break;
                case InstagramApiSharp.Classes.InstaLoginResult.CheckpointLoggedOut:
                    {
                        //MessageBox.Show(result.Info.Message, "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                        //gLogin.IsEnabled = true;
                    }
                    break;
                default:
                    break;
            }
        }

        private void SaveUser()
        {
            using (var db = new DivxModel())
            {
                var user = new User {
                    Username = tbUsername.Text.ToLower(),
                    Password = tbPassword.Password,
                    Session = Classes.Login.LoginAPi.GetStateDataAsString()
                };
                db.Users.Add(user);
                db.SaveChanges();

              //  Blogs.ItemsSource = db.Blogs.ToList();
            }
            MainPage.mainFrame.Navigate(typeof(View.HomePage));
        }
    }
}
