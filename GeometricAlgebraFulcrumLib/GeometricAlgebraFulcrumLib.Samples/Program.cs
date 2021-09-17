using System;
using GeometricAlgebraFulcrumLib.Samples.CodeComposer;

namespace GeometricAlgebraFulcrumLib.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            //var testClass = new MultivectorStoragesTests();
            //testClass.ClassInit();
            //testClass.AssertCorrectInitialization();

            //var functionNames = new[] { "add", "subtract", "op", "egp", "elcp", "ercp", "efdp", "ehip", "ecp", "eacp" };
            //foreach (var functionName in functionNames)
            //    testClass.AssertCorrectBinaryOperations(functionName);

            Sample1.Execute();

            //HilbertTransform.Execute();

            //MultiDerivativeSample.Execute();

            //Symbolic.AngouriMath.Sample1.Execute();
            //CodeComposer.Sample1.Execute();

            Console.WriteLine();
            Console.WriteLine("Press any key to exit..");
            Console.ReadKey();
        }
    }
}
