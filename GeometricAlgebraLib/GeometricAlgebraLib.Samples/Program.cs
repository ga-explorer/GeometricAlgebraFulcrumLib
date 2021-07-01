using System;
using GeometricAlgebraLib.Samples.CodeComposer;
using GeometricAlgebraLib.Samples.GAPoT;

namespace GeometricAlgebraLib.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            //var testClass = new GaProductsTests();
            //testClass.ClassInit();
            //testClass.AssertCorrectBinaryOperations("egp");
            
            HilbertTransform.Execute();

            Console.WriteLine();
            Console.WriteLine("Press any key to exit..");
            Console.ReadKey();
        }
    }
}
