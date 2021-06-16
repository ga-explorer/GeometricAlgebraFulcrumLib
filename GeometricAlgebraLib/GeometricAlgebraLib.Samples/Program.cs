using System;
using GeometricAlgebraLib.Samples.NamedScalars;

namespace GeometricAlgebraLib.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            //var testClass = new GaProductsTests();
            //testClass.ClassInit();
            //testClass.AssertCorrectBinaryOperations("egp");
            
            Sample1.Execute();

            Console.WriteLine();
            Console.WriteLine("Press any key to exit..");
            Console.ReadKey();
        }
    }
}
