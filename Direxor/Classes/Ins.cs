using Div.Model;
using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Direxor.Classes
{

    public class Ins
    {
        public static IInstaApi APi;

        public void SetApi()
        {
            PSetApi();
        }

        private void PSetApi()
        {
            //Find last login user
            using (var db = new DivxModel())
            {
                var user = db.Users.FirstOrDefault();
                if (user != null)
                {
                    //Create user info
                    var userSession = new UserSessionData
                    {
                        UserName = user.Username,
                        Password = "password"
                    };

                    //Create api
                    APi = InstaApiBuilder.CreateBuilder()
                            .SetUser(userSession)
                            .UseLogger(new DebugLogger(LogLevel.Exceptions))
                            .Build();

                    //Set Session
                    APi.LoadStateDataFromString(user.Session);
                }
                //Not user here, please login first
                else
                {
                    MainPage.mainFrame.Navigate(typeof(View.LoginPage));
                }
            }
        }
    }

}
