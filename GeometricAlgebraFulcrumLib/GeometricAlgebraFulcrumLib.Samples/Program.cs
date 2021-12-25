using System;
using GeometricAlgebraFulcrumLib.Samples.GAPoT;
using GeometricAlgebraFulcrumLib.UnitTests.Processing;

namespace GeometricAlgebraFulcrumLib.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            //var testClass = new MultivectorStoragesTests();
            //testClass.ClassInit();

            //testClass.AssertCorrectInitialization();

            //testClass.AssertBinaryWithSelfOperations();

            //var functionNames = new[] { "add", "subtract", "op", "gp", "lcp", "rcp", "fdp", "hip", "cp", "acp" };
            //foreach (var functionName in functionNames)
            //    testClass.AssertCorrectBinaryOperations(functionName);

            //var functionNames = new[] { "leftTimesScalar", "rightTimesScalar", "divideByScalar", "egpSquared", "egpReverse" };
            //foreach (var functionName in functionNames)
            //    testClass.AssertCorrectUnaryOperations(functionName);

            //var functionNames = new[] { "spSquared", "spReverse" };
            //foreach (var functionName in functionNames)
            //    testClass.AssertCorrectUnaryOperationsWithScalarOutput(functionName);

            //GeometricAlgebraFulcrumLib.Samples.CodeComposer.Sample1.Execute2();
            //EuclideanMultivectorOperations3D.Execute();

            //HilbertTransform.Execute();

            MultiDerivativeSample.Execute();

            //Symbolic.AngouriMath.Sample1.Execute();
            //CodeComposer.Sample1.Execute();

            Console.WriteLine();
            Console.WriteLine("Press any key to exit..");
            Console.ReadKey();
        }
    }
}
