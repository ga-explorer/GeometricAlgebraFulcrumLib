using System;
using GeometricAlgebraLib.Samples.GAPoT;
using GeometricAlgebraLib.Samples.UnitTests;

namespace GeometricAlgebraLib.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            //var testClass = new GaProductsTests();
            //testClass.ClassInit();
            //testClass.AssertCorrectBinaryOperations("egp");
            
            BasicOperationsSymbolicSample.Execute();

            Console.WriteLine();
            Console.WriteLine("Press any key to exit..");
            Console.ReadKey();
        }
    }
}
