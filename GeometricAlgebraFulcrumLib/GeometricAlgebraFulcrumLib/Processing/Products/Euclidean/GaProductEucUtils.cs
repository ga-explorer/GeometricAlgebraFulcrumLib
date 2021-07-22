using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Products.Euclidean
{
    public static class GaProductEucUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> EDual<T>(this IGasMultivector<T> mv1, uint vSpaceDimension)
        {
            var pseudoScalarInverse =
                mv1.ScalarProcessor.CreateEuclideanPseudoScalarInverse(vSpaceDimension);

            return mv1.ELcp(pseudoScalarInverse);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> EUnDual<T>(this IGasMultivector<T> mv1, uint vSpaceDimension)
        {
            var pseudoScalarReverse =
                mv1.ScalarProcessor.CreatePseudoScalarReverse(vSpaceDimension);

            return mv1.ELcp(pseudoScalarReverse);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasKVector<T> EBladeInverse<T>(this IGasKVector<T> kVector)
        {
            var bladeSpSquared = kVector.ESp();

            return kVector.Divide(bladeSpSquared);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> EBladeInverse<T>(this IGasMultivector<T> mv1)
        {
            var bladeSpSquared = mv1.ESp();

            return mv1.Divide(bladeSpSquared);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> EVersorInverse<T>(this IGasMultivector<T> mv1)
        {
            var versorSpReverse = mv1.ENormSquared();

            return mv1.GetReverse().Divide(versorSpReverse);
        }
    }
}