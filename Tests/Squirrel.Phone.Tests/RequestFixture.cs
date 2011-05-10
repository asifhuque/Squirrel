
#if SILVERLIGHT
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestFixture = Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute;
using Test = Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute;
using Microsoft.Silverlight.Testing;
#else
using NUnit.Framework;
using Squirrel.Domain;
using System;

#endif

using Squirrel.Proxy;
using Squirrel.Domain;
using Microsoft.Phone.Reactive;
namespace Squirrel.Tests
{
    [TestFixture]
    public class TipFixture
    {

    }
}
