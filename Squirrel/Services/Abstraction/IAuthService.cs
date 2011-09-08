using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Squirrel.Domain;
using Squirrel.Domain.Base;
using System.Windows;
using Squirrel.Responses;

namespace Squirrel.Services.Abstraction
{
    public interface IAuthService
    {
        /// <summary>
        /// Gets the authorization url.
        ///</summary>
        Uri GetAuthorizationUrl();

        /// <summary>
        /// Gets the access token.
        /// </summary>
        IObservable<string> GetAccessToken(Uri url);
    }
}
