using System;
using GeometricAlgebraLib.Samples.CodeComposer;

namespace GeometricAlgebraLib.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            //var testClass = new GaProductsTests();
            //testClass.ClassInit();
            //testClass.AssertCorrectBinaryOperations("egp");
            
            Sample2.Execute();

            Console.WriteLine();
            Console.WriteLine("Press any key to exit..");
            Console.ReadKey();
        }
    }
}
