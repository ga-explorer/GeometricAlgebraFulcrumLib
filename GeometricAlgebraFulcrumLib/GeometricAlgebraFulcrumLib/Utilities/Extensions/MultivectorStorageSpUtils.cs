using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class MultivectorStorageSpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sp<T>(this IGeometricAlgebraProcessor<T> processor, IMultivectorStorage<T> mv1)
        {
            return processor switch
            {
                IGeometricAlgebraEuclideanProcessor<T> =>
                    processor.ESp(mv1),
                
                IGeometricAlgebraOrthonormalProcessor<T> ortProcessor =>
                    processor.Sp(ortProcessor, mv1),

                _ =>
                    ((IGeometricAlgebraChangeOfBasisProcessor<T>) processor).Sp(mv1)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sp<T>(this IGeometricAlgebraProcessor<T> processor, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return processor switch
            {
                IGeometricAlgebraEuclideanProcessor<T> =>
                    processor.ESp(mv1, mv2),
                
                IGeometricAlgebraOrthonormalProcessor<T> ortProcessor =>
                    processor.Sp(ortProcessor, mv1, mv2),

                _ =>
                    ((IGeometricAlgebraChangeOfBasisProcessor<T>) processor).Sp(mv1, mv2)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> BladeInverse<T>(this IGeometricAlgebraProcessor<T> processor, VectorStorage<T> vector)
        {
            return processor.Divide(vector, processor.Sp(vector));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> BladeInverse<T>(this IGeometricAlgebraProcessor<T> processor, BivectorStorage<T> vector)
        {
            return processor.Divide(vector, processor.Sp(vector));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> BladeInverse<T>(this IGeometricAlgebraProcessor<T> processor, KVectorStorage<T> kVector)
        {
            return processor.Divide(kVector, processor.Sp(kVector));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> BladeInverse<T>(this IGeometricAlgebraProcessor<T> processor, IMultivectorStorage<T> mv)
        {
            return processor.Divide(mv, processor.Sp(mv));
        }
    }
}