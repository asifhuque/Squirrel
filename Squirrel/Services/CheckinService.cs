using System;
using Squirrel.Services.Abstraction;

namespace Squirrel.Services
{
    public class CheckinService : Service, ICheckInService
    {
        /// <summary>
        /// Initializes the new instance of the <see cref="CheckinService"/> class.
        /// </summary>
        public CheckinService(FourSquareContext context)
            : base(context.HttpRequestProxy, context.Token)
        {
            // intentionally left blank. 
        }

        /// <summary>
        /// Allows user to check in to a place. 
        /// </summary>
        public IObservable<CheckinResponse> CheckIn(double latitude, double longitude, Broadcast broadcast, string venue)
        {
            var request = new CheckinRequest
            {
                Latitude = latitude,
                Longitude = longitude,
                Broadcast = broadcast,
                Venue = venue
            };
            return CheckIn(request);
        }

        /// <summary>
        /// Allows user to check in to a place. 
        /// </summary>
        public IObservable<CheckinResponse> CheckIn(CheckinRequest request)
        {
            return CreateObservablePattern<CheckinRequest, CheckinResponse>(HttpRequestMethod.POST)(request);
        }
    }
}
