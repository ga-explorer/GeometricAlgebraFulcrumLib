using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GaProcessorIsZeroUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv)
        {
            return mv.IsEmpty() || mv.GetScalars().All(scalarProcessor.IsZero);
        }

        public static bool IsZero<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv, bool nearZeroFlag)
        {
            return mv.IsEmpty() || 
                nearZeroFlag 
                   ? mv.GetScalars().All(scalarProcessor.IsNearZero)
                   : mv.GetScalars().All(scalarProcessor.IsZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearZero<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv)
        {
            return mv.IsEmpty() || mv.GetScalars().All(scalarProcessor.IsNearZero);
        }

    }
}