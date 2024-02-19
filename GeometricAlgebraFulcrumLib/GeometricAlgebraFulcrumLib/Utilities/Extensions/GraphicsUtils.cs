using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions;

public static class GraphicsUtils
{
    public static RGaFloat64Processor GeometricProcessor { get; }
        = RGaFloat64Processor.Euclidean;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetTermScalarByIndex(this VectorStorage<double> vectorStorage, ulong index)
    {
        return vectorStorage.TryGetTermScalarByIndex(index, out var x) ? x : 0d;
    }

}