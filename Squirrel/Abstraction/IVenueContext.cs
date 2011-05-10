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

namespace Squirrel.Abstraction
{
    public interface IVenueContext
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
