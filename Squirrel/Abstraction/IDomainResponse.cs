using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Squirrel.Abstraction
{
    public interface IDomainResponse
    { 
        /// <summary>
        ///  Get the id of the reponse object.
        /// </summary>
        int Id { get; }
    }
}
