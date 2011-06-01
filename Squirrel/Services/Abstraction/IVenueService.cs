using System;

namespace Squirrel.Services.Abstraction
{
    public interface IVenueService
    {
        /// <summary>
        /// Searches venue based on location and keyword.
        /// </summary>
        /// <param name="text">Keyword</param>
        /// <param name="latitude">Latitude</param>
        /// <param name="longitude">Longitude</param>
        /// <returns></returns>
        IObservable<VenueResponse> Search(string text, double latitude, double longitude);
    }
}
