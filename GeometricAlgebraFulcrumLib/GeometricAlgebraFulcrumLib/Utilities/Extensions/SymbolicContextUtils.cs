using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Evaluators;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class SymbolicContextUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AngouriMathSymbolicExpressionEvaluator CreateAngouriMathEvaluator(this SymbolicContext context)
        {
            return new AngouriMathSymbolicExpressionEvaluator(context);
        }
    }
}