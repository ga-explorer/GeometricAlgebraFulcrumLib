using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Context;
using GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Evaluators;

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