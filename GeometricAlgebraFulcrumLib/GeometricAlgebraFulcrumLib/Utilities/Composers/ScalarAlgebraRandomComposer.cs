using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Composers
{
    public class ScalarAlgebraRandomComposer<T>
    {
        public IScalarAlgebraProcessor<T> ScalarProcessor { get; }

        public Random RandomGenerator { get; }


        internal ScalarAlgebraRandomComposer([NotNull] IScalarAlgebraProcessor<T> scalarProcessor)
        {
            ScalarProcessor = scalarProcessor;
            RandomGenerator = new Random();
        }

        internal ScalarAlgebraRandomComposer([NotNull] IScalarAlgebraProcessor<T> scalarProcessor, int seed)
        {
            ScalarProcessor = scalarProcessor;
            RandomGenerator = new Random(seed);
        }
        
        internal ScalarAlgebraRandomComposer([NotNull] IScalarAlgebraProcessor<T> scalarProcessor, [NotNull] Random randomGenerator)
        {
            ScalarProcessor = scalarProcessor;
            RandomGenerator = randomGenerator;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar()
        {
            return ScalarProcessor.GetScalarFromRandom(
                RandomGenerator, 
                -1d, 
                1d
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(double minValue, double maxValue)
        {
            return ScalarProcessor.GetScalarFromRandom(
                RandomGenerator, 
                minValue, 
                maxValue
            );
        }
    }
}