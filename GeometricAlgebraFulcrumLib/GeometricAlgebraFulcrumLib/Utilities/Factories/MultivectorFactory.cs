using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class MultivectorFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivector<T>(this IGeometricAlgebraProcessor<T> processor, IMultivectorStorage<T> storage)
        {
            return new Multivector<T>(processor, storage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivector<T>(this IMultivectorStorage<T> storage, IGeometricAlgebraProcessor<T> processor)
        {
            return new Multivector<T>(processor, storage);
        }
    }
}
