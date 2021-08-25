using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;
using GeometricAlgebraFulcrumLib.Structures.Utils;

namespace GeometricAlgebraFulcrumLib.Processing.ScalarsGrids.Unary
{
    public static class GaScalarsGridProcessorUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListEven<T> vector, int index)
        {
            return vector.GetValue(index, scalarProcessor.GetZeroScalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListEven<T> vector, ulong index)
        {
            return vector.GetValue(index, scalarProcessor.GetZeroScalar);
        }

    }
}