using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Evaluators;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class MetaContextUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AngouriMathMetaExpressionEvaluator CreateAngouriMathEvaluator(this MetaContext context)
        {
            return new AngouriMathMetaExpressionEvaluator(context);
        }
    }
}