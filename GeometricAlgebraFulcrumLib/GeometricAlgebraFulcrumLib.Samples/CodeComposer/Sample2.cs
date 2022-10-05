using System;
using GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.DenseKVectorsLib;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Samples.CodeComposer
{
    public static class Sample2
    {
        public static void Execute()
        {
            //var code = 
            //    GeoLibraryComposer.GenerateCode(4);

            var processor = 
                new MetaContext().CreateGeometricAlgebraEuclideanProcessor(3);

            var codeComposer = 
                GaFuLLibraryComposer.Generate("Euclidean3D", processor, false);

            codeComposer.CodeFilesComposer.SaveToFolder(@"D:\CodeGenOutput\");

            Console.WriteLine("Done");
        }
    }
}