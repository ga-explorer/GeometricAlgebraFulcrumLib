using System;
//using GeometricAlgebraLib.CodeComposer.Applications.CSharp.DenseMultivectorsLib;
using GeometricAlgebraLib.CodeComposer.Applications.CSharp.DenseKVectorsLib;
using GeometricAlgebraLib.Processing.Multivectors;
using GeometricAlgebraLib.Processing.SymbolicExpressions;
using GeometricAlgebraLib.Processing.SymbolicExpressions.Context;

namespace GeometricAlgebraLib.Samples.CodeComposer
{
    public static class Sample2
    {
        public static void Execute()
        {
            //var code = 
            //    GaLibraryComposer.GenerateCode(4);

            var processor = 
                GaMultivectorsProcessor<ISymbolicExpressionAtomic>.CreateEuclidean(
                    new SymbolicContext(), 
                    3
                );

            var codeComposer = 
                GaLibraryComposer.Generate("Euclidean3D", processor, false);

            codeComposer.CodeFilesComposer.SaveToFolder(@"D:\CodeGenOutput\");

            Console.WriteLine("Done");
        }
    }
}