using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Squirrel.Domain;

namespace Squirrel.Services.Abstraction
{
    public interface ITipService
    {
        IObservable<TipResponse> Search(double latitude, double longitude, int page, int limit);
        IObservable<TipResponse> Add(string venueId, string text);
        IObservable<TipResponse> Add(string venueId, string text, string url, Broadcast broadCast);
    }
}
