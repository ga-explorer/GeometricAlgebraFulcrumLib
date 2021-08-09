using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Unary
{
    public static class GaProcessorIsZeroUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv)
        {
            return mv.IsEmpty() || mv.GetScalars().All(scalarProcessor.IsZero);
        }

        public static bool IsZero<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv, bool nearZeroFlag)
        {
            return mv.IsEmpty() || 
                nearZeroFlag 
                   ? mv.GetScalars().All(scalarProcessor.IsNearZero)
                   : mv.GetScalars().All(scalarProcessor.IsZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearZero<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv)
        {
            return mv.IsEmpty() || mv.GetScalars().All(scalarProcessor.IsNearZero);
        }

    }
}