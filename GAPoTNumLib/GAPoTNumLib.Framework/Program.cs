using System;
using GAPoTNumLib.Framework.Samples.Validations;

namespace GAPoTNumLib.Framework
{
    class Program
    {
        static void Main(string[] args)
        {
            ValidationSample1.Execute3();

            Console.WriteLine();
            Console.WriteLine(@"Press any key to exit...");
            Console.ReadKey();
        }
    }
}
