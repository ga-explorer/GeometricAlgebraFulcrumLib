using System;
using NumericalGeometryLib.UnitTests;

namespace NumericalGeometryLib.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            var k = new ProductBinaryTriesTests();

            k.Setup();

            k.TestProducts();
        }
    }
}
