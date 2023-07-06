using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Composers;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class RandomComposerFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarRandomComposer<T> CreateScalarRandomComposer<T>(this IScalarProcessor<T> scalarProcessor)
        {
            return new ScalarRandomComposer<T>(scalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarRandomComposer<T> CreateScalarRandomComposer<T>(this IScalarProcessor<T> scalarProcessor, int seed)
        {
            return new ScalarRandomComposer<T>(scalarProcessor, seed);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarRandomComposer<T> CreateScalarRandomComposer<T>(this IScalarProcessor<T> scalarProcessor, Random randomGenerator)
        {
            return new ScalarRandomComposer<T>(scalarProcessor, randomGenerator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarRandomComposer<T> CreateScalarRandomComposer<T>(this Random randomGenerator, IScalarProcessor<T> scalarProcessor)
        {
            return new ScalarRandomComposer<T>(scalarProcessor, randomGenerator);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinearAlgebraRandomComposer<T> CreateLinearRandomComposer<T>(this ILinearProcessor<T> linearProcessor)
        {
            return new LinearAlgebraRandomComposer<T>(linearProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinearAlgebraRandomComposer<T> CreateLinearRandomComposer<T>(this ILinearProcessor<T> linearProcessor, int seed)
        {
            return new LinearAlgebraRandomComposer<T>(linearProcessor, seed);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinearAlgebraRandomComposer<T> CreateLinearRandomComposer<T>(this ILinearProcessor<T> linearProcessor, Random randomGenerator)
        {
            return new LinearAlgebraRandomComposer<T>(linearProcessor, randomGenerator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinearAlgebraRandomComposer<T> CreateLinearRandomComposer<T>(this Random randomGenerator, ILinearProcessor<T> linearProcessor)
        {
            return new LinearAlgebraRandomComposer<T>(linearProcessor, randomGenerator);
        }

        
    }
}
