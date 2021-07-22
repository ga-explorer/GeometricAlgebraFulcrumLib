using System;
using GeometricAlgebraFulcrumLib.Samples.CodeComposer;
using GeometricAlgebraFulcrumLib.Samples.UnitTests;

namespace GeometricAlgebraFulcrumLib.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            //var testClass = new GaProductsTests();
            //testClass.ClassInit();
            //testClass.AssertCorrectBinaryOperations("elcp");

            //HilbertTransform.Execute();

            Sample1.Execute();

            Console.WriteLine();
            Console.WriteLine("Press any key to exit..");
            Console.ReadKey();
        }
    }
}
