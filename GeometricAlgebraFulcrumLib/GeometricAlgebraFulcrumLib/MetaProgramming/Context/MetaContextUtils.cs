﻿using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MetaProgramming.Evaluators;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context;

public static class MetaContextUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AngouriMathMetaExpressionEvaluator CreateAngouriMathEvaluator(this MetaContext context)
    {
        return new AngouriMathMetaExpressionEvaluator(context);
    }
}