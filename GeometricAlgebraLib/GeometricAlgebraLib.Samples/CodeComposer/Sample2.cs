using System;
using GeometricAlgebraLib.CodeComposer.Applications.CSharp.DenseMultivectorsLib;

namespace GeometricAlgebraLib.Samples.CodeComposer
{
    public static class Sample2
    {
        public static void Execute()
        {
            var code = 
                GaLibraryComposer.GenerateCode(4);

            Console.WriteLine(code);
        }
    }
}