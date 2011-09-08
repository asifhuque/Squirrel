using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Squirrel.Attributes;
using Squirrel.Abstraction;

namespace Squirrel.Requests
{
 
    [RequestMethod("http://foursquare.com/oauth2/access_token")]
    public class OAuthTokenRequest : OAuthRequest
    {
        /// <summary>
        /// Gets or sets the authorization code.
        /// </summary>
        [RequestProperty("code")]
        public string Code
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the grant type for the request.
        /// </summary>
        [RequestProperty("grant_type")]
        public string GrantType
        {
            get
            {
                return OAuth_Grant_Type;
            }
        }
        
        /// <summary>
        /// Gets the client secret
        /// </summary>
        [RequestProperty("client_secret")]
        public new string Secret
        {
            get { return Client.Secret; }
        }

        private const string OAuth_Grant_Type = "authorization_code";
    }
}
