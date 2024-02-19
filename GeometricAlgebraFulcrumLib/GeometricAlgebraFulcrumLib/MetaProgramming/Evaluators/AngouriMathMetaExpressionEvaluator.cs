using System;
using System.Runtime.CompilerServices;
using AngouriMath;
using DataStructuresLib;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Evaluators;

public sealed class AngouriMathMetaExpressionEvaluator 
    : IMetaExpressionEvaluator<Entity>
{
    public MetaContext Context { get; }

    public ISymbolicFromMetaExpressionConverter<Entity> FromSymbolicExpressionConverter { get; }

    public ISymbolicIntoMetaExpressionConverter<Entity> IntoSymbolicExpressionConverter { get; }


    internal AngouriMathMetaExpressionEvaluator(MetaContext context)
    {
        Context = context;
        FromSymbolicExpressionConverter = new AngouriMathFromMetaExpressionConverter(context);
        IntoSymbolicExpressionConverter = new AngouriMathIntoMetaExpressionConverter(context);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression Convert(Entity expr)
    {
        return expr.AcceptVisitor(IntoSymbolicExpressionConverter);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Convert(IMetaExpression expr)
    {
        return expr.AcceptVisitor(FromSymbolicExpressionConverter);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression Simplify(IMetaExpression expr)
    {
        return Convert(Convert(expr).Simplify());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression Simplify(string exprText)
    {
        return Convert(MathS.FromString(exprText, false));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double EvaluateToFloat64(IMetaExpression expr)
    {
        var expr1 = Convert(expr);

        return expr1.EvaluableNumerical
            ? (double) expr1.EvalNumerical()
            : throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double EvaluateToFloat64(string exprText)
    {
        var expr1 = MathS.FromString(exprText, false);

        return expr1.EvaluableNumerical
            ? (double) expr1.EvalNumerical()
            : throw new InvalidOperationException();
    }
}