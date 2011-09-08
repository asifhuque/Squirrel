using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Squirrel.Services.Abstraction
{
    public interface ICheckInService
    {
        /// <summary>
        /// Allows user to check in to a place. 
        /// </summary>
        IObservable<CheckinResponse> CheckIn(double latitude, double longitude, Broadcast broadcast, string shout);
      
        /// <summary>
        /// Allows user to check in to a place. 
        /// </summary>
        IObservable<CheckinResponse> CheckIn(CheckinRequest request);
    }
}
