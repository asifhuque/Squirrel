using System;
using Squirrel.Abstraction;
using Squirrel.Attributes;

namespace Squirrel.Requests
{

    [RequestMethod("http://foursquare.com/oauth2/authenticate")]
    public class OAuthRequest : Request
    {
        /// <summary>
        /// Gets the reponse type for OAuth
        /// </summary>
        [RequestProperty("response_type")]
        public string ResponseType
        {
            get
            {
                return OAuthResponeType.Code.ToString().ToLower();
            }
        }

        /// <summary>
        /// Gets the redirect associated for the client
        /// </summary>
        /// 
        [RequestProperty("redirect_uri")]
        public string RedirectUrl
        {
            get
            {
                return Client.RedirectUrl;
            }
        }

        /// <summary>
        /// Returns the nature of reponse target.
        /// </summary>
        [RequestProperty("display")]
        public string ResponseTarget
        {
            get
            {
                return "touch";
            }
        }

        /// <summary>
        /// Gets the client secret
        /// </summary>
        [RequestProperty("client_secret")]
        public new string Secret
        {
            get { return string.Empty; }
        }

        public override System.Net.HttpWebRequest Create(IHttpRequestProxy proxy, string method)
        {
            return Create(this, proxy, method);
        }

        /// <summary>
        /// Creates the request url.
        /// </summary>
        public string Create()
        {
            if (string.IsNullOrEmpty(RedirectUrl))
            {
                throw new FourSquareException("Must provide a valid redirect url registered with foursquare");
            }
            return Create(this);
        }
    }
}
