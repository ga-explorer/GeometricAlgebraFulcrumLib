using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Products.ChangeOfBasis
{
    public static class GaProductCobHipUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Hip<T>(this IGaProcessorChangeOfBasis<T> processor, IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            var s1 = processor.OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = processor.OmTargetToOrthonormal.MapMultivector(mv2);

            var s = processor.Signature.Hip(s1, s2);

            return processor.OmOrthonormalToTarget.MapMultivector(s);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Hip<T>(this IGasMultivector<T> mv1, IGasMultivector<T> mv2, IGaProcessorChangeOfBasis<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.MapMultivector(mv1);
            var s2 = processor.OmTargetToOrthonormal.MapMultivector(mv2);

            var s = processor.Signature.Hip(s1, s2);

            return processor.OmOrthonormalToTarget.MapMultivector(s);
        }
    }
}