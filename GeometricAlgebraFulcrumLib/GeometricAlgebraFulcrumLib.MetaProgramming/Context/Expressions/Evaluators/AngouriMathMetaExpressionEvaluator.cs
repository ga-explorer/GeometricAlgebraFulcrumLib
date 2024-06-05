using System.Runtime.CompilerServices;
using AngouriMath;
using GeometricAlgebraFulcrumLib.Utilities.Structures;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Evaluators;

public sealed class AngouriMathMetaExpressionEvaluator
    : IMetaExpressionEvaluator<Entity>
{
    public MetaContext Context
        => IntoSymbolicExpressionConverter.Context;

    public ISymbolicFromMetaExpressionConverter<Entity> FromSymbolicExpressionConverter
        => AngouriMathFromMetaExpressionConverter.Instance;

    public ISymbolicIntoMetaExpressionConverter<Entity> IntoSymbolicExpressionConverter { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal AngouriMathMetaExpressionEvaluator(MetaContext context)
    {
        IntoSymbolicExpressionConverter =
            new AngouriMathIntoMetaExpressionConverter(context);
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

    public bool IsAffineCombination(IMetaExpression expr)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression Enhance(IMetaExpression expr)
    {
        return Convert(Convert(expr).Simplify());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression Simplify(IMetaExpression expr)
    {
        return Convert(Convert(expr).Simplify());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression Simplify(MetaContext context, string exprText)
    {
        return Convert(MathS.FromString(exprText, false));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double EvaluateToFloat64(IMetaExpression expr)
    {
        var expr1 = Convert(expr);

        return expr1.EvaluableNumerical
            ? (double)expr1.EvalNumerical()
            : throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double EvaluateToFloat64(string exprText)
    {
        var expr1 = MathS.FromString(exprText, false);

        return expr1.EvaluableNumerical
            ? (double)expr1.EvalNumerical()
            : throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpressionEvaluator GetEvaluatorCopy(MetaContext context)
    {
        return ReferenceEquals(Context, context)
            ? this
            : new AngouriMathMetaExpressionEvaluator(context);
    }

}