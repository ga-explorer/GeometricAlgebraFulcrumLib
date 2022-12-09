using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra
{
    internal static class MultivectorStorageHipUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> Hip<T>(this IGeometricAlgebraProcessor<T> processor, KVectorStorage<T> mv1, KVectorStorage<T> mv2)
        {
            return processor switch
            {
                IGeometricAlgebraEuclideanProcessor<T> =>
                    processor.EHip(mv1, mv2),

                IGeometricAlgebraOrthonormalProcessor<T> ortProcessor =>
                    processor.Hip(ortProcessor.BasisSet, mv1, mv2),

                _ =>
                    ((IGeometricAlgebraChangeOfBasisProcessor<T>)processor).Hip(mv1, mv2)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Hip<T>(this IGeometricAlgebraProcessor<T> processor, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return processor switch
            {
                IGeometricAlgebraEuclideanProcessor<T> =>
                    processor.EHip(mv1, mv2),

                IGeometricAlgebraOrthonormalProcessor<T> ortProcessor =>
                    processor.Hip(ortProcessor.BasisSet, mv1, mv2),

                _ =>
                    ((IGeometricAlgebraChangeOfBasisProcessor<T>)processor).Hip(mv1, mv2)
            };
        }
    }
}