using System;
using Squirrel.Services.Abstraction;
using Squirrel.Requests;
using Microsoft.Phone.Reactive;
using Squirrel.Domain.Base;
using System.Windows;
using Squirrel.Responses;
using System.Reflection;
using System.Windows.Navigation;

namespace Squirrel.Services
{
    public class AuthService : Service, IAuthService
    {
        public AuthService(FourSquareContext context)
            : base(context.HttpRequestProxy, context.Token)
        {
            // intentionally left blank. 
        }

        /// <summary>
        /// Get request url.
        /// </summary>
        /// <returns></returns>
        public Uri GetAuthorizationUrl()
        {
            return new Uri(new OAuthRequest().Create());
        }
    
        public IObservable<string> GetAccessToken(Uri uri)
        {
            const string fragment = "?code=";
          
            string url = uri.ToString();
            int start = url.IndexOf(fragment);

            if (start > -1)
            {
                int index = start + fragment.Length;
                // You can use the accessToken for api calls now.
                string code = url.Substring(index, url.Length - index);

                var request = new OAuthTokenRequest
                {
                    Code = code
                };

                var func = Observable
                   .FromAsyncPattern<OAuthTokenRequest, AuthTokenResponse>(
                   this.BeginAsync<OAuthTokenRequest, AuthTokenResponse>,
                   this.EndAsync<AuthTokenResponse>);

                return func(request).Select(x => x.Token);
            }
            return Observable.Empty<string>();
        }


        public delegate void AuthenticationCompletedHandler(AuthTokenResponse response);
        public event AuthenticationCompletedHandler OnAuthenticaitonCompleted;
    }
}
