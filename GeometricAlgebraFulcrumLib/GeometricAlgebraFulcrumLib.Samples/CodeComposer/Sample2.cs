using System;
using GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib;
using GeometricAlgebraFulcrumLib.Processing;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;

namespace GeometricAlgebraFulcrumLib.Samples.CodeComposer
{
    public static class Sample2
    {
        public static void Execute()
        {
            //var code = 
            //    GaLibraryComposer.GenerateCode(4);

            var processor = 
                new SymbolicContext().CreateEuclideanProcessor(3);

            var codeComposer = 
                GaLibraryComposer.Generate("Euclidean3D", processor, false);

            codeComposer.CodeFilesComposer.SaveToFolder(@"D:\CodeGenOutput\");

            Console.WriteLine("Done");
        }
    }
}