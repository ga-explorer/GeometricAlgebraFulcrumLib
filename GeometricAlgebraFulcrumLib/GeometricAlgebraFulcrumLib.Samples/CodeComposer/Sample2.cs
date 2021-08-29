using System;
using GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Samples.CodeComposer
{
    public static class Sample2
    {
        public static void Execute()
        {
            //var code = 
            //    GaLibraryComposer.GenerateCode(4);

            var processor = 
                new SymbolicContext().CreateGaEuclideanProcessor(3);

            var codeComposer = 
                GaFuLLibraryComposer.Generate("Euclidean3D", processor, false);

            codeComposer.CodeFilesComposer.SaveToFolder(@"D:\CodeGenOutput\");

            Console.WriteLine("Done");
        }
    }
}