using System;
using GeometricAlgebraFulcrumLib.Samples.Numeric;

namespace GeometricAlgebraFulcrumLib.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            //var testClass = new BasisBladeTests();
            //testClass.TestGpReverse();

            //var testClass = new MultivectorStoragesTests();
            //testClass.ClassInit();

            //testClass.AssertCorrectInitialization();

            //testClass.AssertBinaryWithSelfOperations();

            //var functionNames = new[] { "add", "subtract", "op", "gp", "lcp", "rcp", "fdp", "hip", "cp", "acp" };
            //foreach (var functionName in functionNames)
            //    testClass.AssertCorrectBinaryOperations(functionName);

            //var functionNames = new[] { "leftTimesScalar", "rightTimesScalar", "divideByScalar", "gpSquared", "gpReverse" };
            //foreach (var functionName in functionNames)
            //    testClass.AssertCorrectUnaryOperations(functionName);

            //var functionNames = new[] { "spSquared", "spReverse" };
            //foreach (var functionName in functionNames)
            //    testClass.AssertCorrectUnaryOperationsWithScalarOutput(functionName);

            //GeometricAlgebraFulcrumLib.Samples.CodeComposer.Sample1.Execute2();
            //EuclideanMultivectorOperations3D.Execute();

            //HilbertTransform.Execute();

            //MultiDerivativeSample.Execute();

            //Symbolic.AngouriMath.Sample1.Execute();
            //CodeComposer.Sample1.Execute();

            GeometricFrequency.Sample1.Example2();
            //SymbolicBSplineSample.Example2();
            //NumericPhCurveSamples.Example3();
            //SymbolicBernsteinPolynomialsSample.Example5();
            //SymbolicPhConstruction3DSample.Example2();
            //SymbolicPhConstruction2DSample.Example2();
            //SymbolicRotorsSample.Example9();
            //NumericRotorsSample.Example12();
            //SymbolicScaledRotors3DSample.Example3();
            //SymbolicScaledRotors2DSample.Example3();
            //NumericPhSplineCurveSamples.Example1();

            Console.WriteLine();
            Console.WriteLine("Press any key to exit..");
            Console.ReadKey();
        }
    }
}
