using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean
{
    public static class GaProductEucUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> EDual<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1, uint vSpaceDimension)
        {
            var pseudoScalarInverse =
                scalarProcessor.CreateEuclideanPseudoScalarInverseStorage(vSpaceDimension);

            return scalarProcessor.ELcp(mv1, pseudoScalarInverse);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> EUnDual<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1, uint vSpaceDimension)
        {
            var pseudoScalarReverse =
                scalarProcessor.CreatePseudoScalarReverseStorage(vSpaceDimension);

            return scalarProcessor.ELcp(mv1, pseudoScalarReverse);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> EBladeInverse<T>(this IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> kVector)
        {
            var bladeSpSquared = scalarProcessor.ESp(kVector);

            return scalarProcessor.Divide(kVector, bladeSpSquared);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> EBladeInverse<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1)
        {
            var bladeSpSquared = scalarProcessor.ESp(mv1);

            return scalarProcessor.Divide(mv1, bladeSpSquared);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> EVersorInverse<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1)
        {
            var versorSpReverse = scalarProcessor.ENormSquared(mv1);

            return scalarProcessor.Divide(scalarProcessor.Reverse(mv1), versorSpReverse);
        }
    }
}