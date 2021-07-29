using System;
using GeometricAlgebraFulcrumLib.Samples.CodeComposer;

namespace GeometricAlgebraFulcrumLib.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            //var testClass = new GaSimpleEuclideanRotorsTests();
            //testClass.ClassInit();
            //testClass.AssertRotations();

            Sample1.Execute();

            //HilbertTransform.Execute();

            //Sample2.Execute2();

            Console.WriteLine();
            Console.WriteLine("Press any key to exit..");
            Console.ReadKey();
        }
    }
}
