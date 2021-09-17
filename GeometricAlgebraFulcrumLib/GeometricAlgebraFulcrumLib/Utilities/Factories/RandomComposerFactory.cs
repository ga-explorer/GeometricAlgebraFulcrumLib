using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Composers;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class RandomComposerFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarAlgebraRandomComposer<T> CreateScalarRandomComposer<T>(this IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return new ScalarAlgebraRandomComposer<T>(scalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarAlgebraRandomComposer<T> CreateScalarRandomComposer<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int seed)
        {
            return new ScalarAlgebraRandomComposer<T>(scalarProcessor, seed);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarAlgebraRandomComposer<T> CreateScalarRandomComposer<T>(this IScalarAlgebraProcessor<T> scalarProcessor, Random randomGenerator)
        {
            return new ScalarAlgebraRandomComposer<T>(scalarProcessor, randomGenerator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarAlgebraRandomComposer<T> CreateScalarRandomComposer<T>(this Random randomGenerator, IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return new ScalarAlgebraRandomComposer<T>(scalarProcessor, randomGenerator);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinearAlgebraRandomComposer<T> CreateLinearRandomComposer<T>(this ILinearAlgebraProcessor<T> linearProcessor)
        {
            return new LinearAlgebraRandomComposer<T>(linearProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinearAlgebraRandomComposer<T> CreateLinearRandomComposer<T>(this ILinearAlgebraProcessor<T> linearProcessor, int seed)
        {
            return new LinearAlgebraRandomComposer<T>(linearProcessor, seed);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinearAlgebraRandomComposer<T> CreateLinearRandomComposer<T>(this ILinearAlgebraProcessor<T> linearProcessor, Random randomGenerator)
        {
            return new LinearAlgebraRandomComposer<T>(linearProcessor, randomGenerator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinearAlgebraRandomComposer<T> CreateLinearRandomComposer<T>(this Random randomGenerator, ILinearAlgebraProcessor<T> linearProcessor)
        {
            return new LinearAlgebraRandomComposer<T>(linearProcessor, randomGenerator);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeometricAlgebraRandomComposer<T> CreateGeometricRandomComposer<T>(this IGeometricAlgebraProcessor<T> geometricProcessor)
        {
            return new GeometricAlgebraRandomComposer<T>(geometricProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeometricAlgebraRandomComposer<T> CreateGeometricRandomComposer<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int seed)
        {
            return new GeometricAlgebraRandomComposer<T>(geometricProcessor, seed);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeometricAlgebraRandomComposer<T> CreateGeometricRandomComposer<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, Random randomGenerator)
        {
            return new GeometricAlgebraRandomComposer<T>(geometricProcessor, randomGenerator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeometricAlgebraRandomComposer<T> CreateGeometricRandomComposer<T>(this Random randomGenerator, IGeometricAlgebraProcessor<T> geometricProcessor)
        {
            return new GeometricAlgebraRandomComposer<T>(geometricProcessor, randomGenerator);
        }
    }
}
