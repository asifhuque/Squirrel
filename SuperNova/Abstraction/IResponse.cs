using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperNova.Abstraction
{
    public interface IResponse
    { 
        /// <summary>
        ///  Get the id of the reponse object.
        /// </summary>
        int Id { get; }
    }
}
