using System.Runtime.CompilerServices;
using AngouriMath;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Composite;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.HeadSpecs;
using GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree.Expressions;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Processors;

public static class SymbolicScalarProcessorUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Entity ArrayToMatrixExpr(this Entity[,] exprArray)
    {
        return MathS.Matrix(exprArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Entity MatrixDeterminant(this Entity[,] exprArray)
    {
        return MathS.Matrix(exprArray).Determinant ?? Entity.Number.Integer.Zero;
    }

    /// <summary>
    /// Convert the given Mathematica Entity object into a SteExpression object
    /// </summary>
    /// <param name="expr"></param>
    /// <returns></returns>
    public static SteExpression ToSimpleTextExpression(this Entity expr)
    {
        var isNumber = expr.EvaluableNumerical;
        var isSymbol = expr.IsSymbolic;

        if (isNumber)
        {
            return isSymbol
                ? SteExpression.CreateSymbolicNumber(expr.ToString())
                : SteExpression.CreateLiteralNumber(expr.ToString());
        }

        if (isSymbol)
            return SteExpression.CreateVariable(expr.ToString());

        if (expr.DirectChildren.Count == 0)
            return SteExpression.CreateFunction(expr.ToString());

        var args = new SteExpression[expr.DirectChildren.Count];

        for (var i = 0; i < expr.DirectChildren.Count; i++)
            args[i] = expr.DirectChildren[i].ToSimpleTextExpression();

        return SteExpression.CreateFunction(expr.GetType().Name, args);
    }

    public static IMetaExpression ToSymbolicExpression(this Entity expr, MetaContext context)
    {
        var isNumber = expr.EvaluableNumerical;
        var isSymbol = expr.IsSymbolic;

        if (isNumber)
            return context.GetOrDefineSymbolicNumber(
                expr.ToString(),
                (double)expr.EvalNumerical()
            );

        if (isSymbol)
            return context.GetVariable(expr.ToString());

        if (expr.DirectChildren.Count == 0)
            return MetaExpressionFunction.CreateNonAssociative(context, expr.GetType().Name);

        var args = expr.DirectChildren.Select(
            argExpr => argExpr.ToSymbolicExpression(context)
        );

        var functionName = expr.GetType().Name;
        return functionName switch
        {
            "Minus" => context.FunctionHeadSpecsFactory.Negative.CreateFunction(context, args),

            "Plus" => context.FunctionHeadSpecsFactory.Plus.CreateFunction(context, args),
            "Subtract" => context.FunctionHeadSpecsFactory.Subtract.CreateFunction(context, args),
            "Times" => context.FunctionHeadSpecsFactory.Times.CreateFunction(context, args),
            "Divide" => context.FunctionHeadSpecsFactory.Divide.CreateFunction(context, args),

            _ => MetaExpressionFunction.CreateNonAssociative(context, functionName, args)
        };
    }

    public static IMetaExpression ToSymbolicExpression(this MetaContext context, Entity expr)
    {
        return expr.ToSymbolicExpression(context);
    }

    ///// <summary>
    ///// Convert this symbolic expression into a Mathematica expression object
    ///// </summary>
    ///// <param name="symbolicExpr"></param>
    ///// <returns></returns>
    //public static Entity ToExpr(this SteExpression symbolicExpr)
    //{
    //    return MathematicaInterface.DefaultCas[symbolicExpr.ToString()];
    //}
}