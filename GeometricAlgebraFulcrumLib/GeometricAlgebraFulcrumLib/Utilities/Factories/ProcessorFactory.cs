using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.Signatures;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class ProcessorFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinearAlgebraProcessor<T> CreateLinearAlgebraProcessor<T>(this IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return new LinearAlgebraProcessor<T>(scalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeometricAlgebraEuclideanProcessor<T> CreateGeometricAlgebraEuclideanProcessor<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            return new GeometricAlgebraEuclideanProcessor<T>(
                scalarProcessor,
                vSpaceDimension
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeometricAlgebraConformalProcessor<T> CreateGeometricAlgebraConformalProcessor<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            return new GeometricAlgebraConformalProcessor<T>(scalarProcessor, vSpaceDimension);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeometricAlgebraProjectiveProcessor<T> CreateGeometricAlgebraProjectiveProcessor<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            return new GeometricAlgebraProjectiveProcessor<T>(scalarProcessor, vSpaceDimension);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeometricAlgebraOrthonormalProcessor<T> CreateGeometricAlgebraOrthonormalProcessor<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint positiveCount, uint negativeCount)
        {
            return new GeometricAlgebraOrthonormalProcessor<T>(
                scalarProcessor, 
                GeometricAlgebraSignatureFactory.Create(positiveCount, negativeCount)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeometricAlgebraOrthonormalProcessor<T> CreateGeometricAlgebraOrthonormalProcessor<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint positiveCount, uint negativeCount, uint zeroCount)
        {
            return new GeometricAlgebraOrthonormalProcessor<T>(
                scalarProcessor, 
                GeometricAlgebraSignatureFactory.Create(positiveCount, negativeCount, zeroCount)
            );
        }
    }
}