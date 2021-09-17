using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Composers
{
    public class LinearAlgebraRandomComposer<T> :
        ScalarAlgebraRandomComposer<T>
    {
        public ILinearAlgebraProcessor<T> LinearProcessor { get; }


        internal LinearAlgebraRandomComposer([NotNull] ILinearAlgebraProcessor<T> linearProcessor)
            : base(linearProcessor)
        {
            LinearProcessor = linearProcessor;
        }

        internal LinearAlgebraRandomComposer([NotNull] ILinearAlgebraProcessor<T> linearProcessor, int seed)
            : base(linearProcessor, seed)
        {
            LinearProcessor = linearProcessor;
        }

        internal LinearAlgebraRandomComposer([NotNull] ILinearAlgebraProcessor<T> linearProcessor, Random randomGenerator)
            : base(linearProcessor, randomGenerator)
        {
            LinearProcessor = linearProcessor;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetIndex(ulong maxIndex)
        {
            return (ulong) (RandomGenerator.NextDouble() * maxIndex);
        }
    }
}