using System;
using GeometricAlgebraFulcrumLib.Samples.GAPoT;
using GeometricAlgebraFulcrumLib.UnitTests.Processing;

namespace GeometricAlgebraFulcrumLib.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            //var testClass = new GaProductsTests();
            //testClass.ClassInit();
            //testClass.AssertCorrectInitialization();

            //var functionNames = new[] {"add", "subtract", "op", "egp", "elcp", "ercp", "efdp", "ehip", "ecp", "eacp"};
            //foreach (var functionName in functionNames)
            //    testClass.AssertCorrectBinaryOperations(functionName);
            
            //Sample1.Execute();

            //HilbertTransform.Execute();

            MultiDerivativeSample.Execute();

            //Sample2.Execute2();

            Console.WriteLine();
            Console.WriteLine("Press any key to exit..");
            Console.ReadKey();
        }
    }
}
