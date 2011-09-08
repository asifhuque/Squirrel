using System;
using Squirrel.Services.Abstraction;
using Microsoft.Phone.Reactive;
using Squirrel.Abstraction;

namespace Squirrel.Services
{
    /// <summary>
    /// Implements tip realted operations.
    /// </summary>
    public class TipService : Service, ITipService
    {
        public TipService(FourSquareContext context)
            : base(context.HttpRequestProxy, context.Token)
        {
            // intentionally left blank. 
        }

        public IObservable<TipResponse> Search(double latitude, double longitude, int page, int limit)
        {
            var request = new Requests.Tips.SearchRequest
            {
                Latitude = latitude,
                Longitude = longitude,
                Offset = page,
                Limit = limit
            };

            var func = Observable.FromAsyncPattern<Requests.Tips.SearchRequest, TipResponse>(this.BeginAsync<Requests.Tips.SearchRequest, TipResponse>, this.EndAsync<TipResponse>);

            return func(request);
        }

        public IObservable<TipResponse> Add(string venueId, string text)
        {
            return Add(venueId, text, string.Empty, Broadcast.Private);
        }

        public IObservable<TipResponse> Add(string venueId, string text, string url, Broadcast broadCast)
        {
            var request = new Requests.Tips.AddRequest
            {
                VenueId = venueId,
                Text = text,
                Url = url,
                Broadcast = broadCast
            };

            return CreateObservablePattern<Requests.Tips.AddRequest, TipResponse>(HttpRequestMethod.POST)(request);
        }
    }
}
