using System;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.DenseKVectorsLib;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;

namespace GeometricAlgebraFulcrumLib.Samples.MetaProgramming;

public static class Sample2
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