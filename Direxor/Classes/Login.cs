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
    public class Login
    {
        //api for all login prossess
        public static IInstaApi LoginAPi;

        public async Task<IResult<InstaLoginResult>> Loginasync(string username, string password)
        {
            //Create user info
            var userSession = new UserSessionData
            {
                UserName = username,
                Password = password
            };

            //Create api
            LoginAPi = InstaApiBuilder.CreateBuilder()
                    .SetUser(userSession)
                    .UseLogger(new DebugLogger(LogLevel.Exceptions))
                    .Build();

            //login
            IResult<InstaLoginResult> result = await LoginAPi.LoginAsync();
            return result;
        }
    }
}
