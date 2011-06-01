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
            : base(context.HttpRequestProxy)
        {
            // intentionally left blank. 
        }

        public IObservable<TipResponse> Search(double latitude, double longitude, int page, int limit)
        {
            var request = new TipRequest
            {
                Latitude = latitude,
                Longitude = longitude,
                Offset = page,
                Limit = limit
            };


            var func = Observable.FromAsyncPattern<TipRequest, TipResponse>(this.BeginAsync<TipRequest, TipResponse>, this.EndAsync<TipResponse>);

            return func(request);
        }
    }
}
