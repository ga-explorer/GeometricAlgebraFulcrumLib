using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.CSharp.DenseKVectorsLib;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.CSharp;

public static class DenseKVectorsLibSample
{
    public static void Execute()
    {
        //var code = 
        //    GeoLibraryComposer.GenerateCode(4);

        var processor = 
            new MetaContext().CreateEuclideanXGaProcessor();

        var codeComposer = 
            GaFuLLibraryComposer.Generate(
                "Euclidean3D", 
                processor, 
                4, 
                false
            );

        codeComposer.CodeFilesComposer.SaveToFolder(@"D:\CodeGenOutput\");

        Console.WriteLine("Done");
    }
}