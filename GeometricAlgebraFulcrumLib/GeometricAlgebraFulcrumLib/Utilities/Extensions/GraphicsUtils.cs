using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GraphicsUtils
    {
        public static IGeometricAlgebraEuclideanProcessor<double> GeometricProcessor { get; }
            = ScalarAlgebraFloat64Processor.DefaultProcessor.CreateGeometricAlgebraEuclideanProcessor(3);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetTermScalarByIndex(this VectorStorage<double> vectorStorage, ulong index)
        {
            return vectorStorage.TryGetTermScalarByIndex(index, out var x) ? x : 0d;
        }

    }
}