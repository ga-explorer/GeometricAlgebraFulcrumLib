using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Binary;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Unary;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Factories;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean
{
    public static class GaProductEucUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> EDual<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1, uint vSpaceDimension)
        {
            var pseudoScalarInverse =
                scalarProcessor.CreateStorageEuclideanPseudoScalarInverse(vSpaceDimension);

            return scalarProcessor.ELcp(mv1, pseudoScalarInverse);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> EUnDual<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1, uint vSpaceDimension)
        {
            var pseudoScalarReverse =
                scalarProcessor.CreateStoragePseudoScalarReverse(vSpaceDimension);

            return scalarProcessor.ELcp(mv1, pseudoScalarReverse);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> EBladeInverse<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> kVector)
        {
            var bladeSpSquared = scalarProcessor.ESp(kVector);

            return scalarProcessor.Divide(kVector, bladeSpSquared);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> EBladeInverse<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1)
        {
            var bladeSpSquared = scalarProcessor.ESp(mv1);

            return scalarProcessor.Divide(mv1, bladeSpSquared);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> EVersorInverse<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1)
        {
            var versorSpReverse = scalarProcessor.ENormSquared(mv1);

            return scalarProcessor.Divide(scalarProcessor.Reverse(mv1), versorSpReverse);
        }
    }
}