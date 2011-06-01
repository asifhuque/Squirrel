using System;
using Squirrel.Services.Abstraction;
using Microsoft.Phone.Reactive;

namespace Squirrel.Services
{
    /// <summary>
    /// Implements venue realted operations.
    /// </summary>
    public class VenueService : Service, IVenueService
    {
        public VenueService(FourSquareContext context)
            : base(context.HttpRequestProxy)
        {
            // intentionally left blank. 
        }

        /// <summary>
        /// Searches a group of venues for a specific location and text
        /// </summary>
        /// <param name="latitude">Lattitude</param>
        /// <param name="longitude">Longitude</param>
        public IObservable<VenueResponse> Search(string text, double latitude, double longitude)
        {
            var vRequest = new VenueRequest
            {
                Latitude = latitude,
                Longitude = longitude,
                Text = text,
                Limit = 50
            };

            var func = Observable.FromAsyncPattern<VenueRequest, VenueResponse>(this.BeginAsync<VenueRequest, VenueResponse>, this.EndAsync<VenueResponse>);

            return func(vRequest);
        }
    }
}
