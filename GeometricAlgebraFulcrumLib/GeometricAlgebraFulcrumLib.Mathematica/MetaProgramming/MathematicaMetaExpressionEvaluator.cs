using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures.ExprFactory;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Evaluators;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Mathematica.MetaProgramming;

public sealed class MathematicaMetaExpressionEvaluator :
    IMetaExpressionEvaluator<Expr>
{
    public MetaContext Context 
        => IntoSymbolicExpressionConverter.Context;

    public ISymbolicFromMetaExpressionConverter<Expr> FromSymbolicExpressionConverter 
        => MathematicaFromMetaExpressionConverter.Instance;
    
    public ISymbolicIntoMetaExpressionConverter<Expr> IntoSymbolicExpressionConverter { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal MathematicaMetaExpressionEvaluator(MetaContext context)
    {
        IntoSymbolicExpressionConverter = 
            new MathematicaIntoMetaExpressionConverter(context);
    }


    private static Expr Enhance(Expr symExpr)
    {
        if (
            symExpr.AtomQ() || 
            symExpr.NumberQ() || 
            symExpr.RationalQ() ||
            symExpr.SymbolQ()
            ) return symExpr;

        symExpr = symExpr.MapArgs(Enhance);

        Expr powerArg1, powerArg2;

        if (symExpr.IsPlus() && symExpr.Args.All(a => a.IsUnaryMinus()))
        {
            // Convert Plus[Minus[x], Minus[y], ...] to Minus[Plus[x, y, ...]]
            return Mfs.Minus[
                symExpr.MapArgs(a => a.Args[0])
            ];
        }

        if (symExpr.IsUnaryInverse(out var unaryArg))
        {
            // Convert Inverse[x] to Divide[1, x]
            return Mfs.Divide[Expr.INT_ONE, unaryArg];
        }

        if (symExpr.IsUnarySec(out unaryArg))
        {
            // Convert Sec[x] to Divide[1, Cos[x]]
            return Mfs.Divide[Expr.INT_ONE, Mfs.Cos[unaryArg]];
        }

        if (symExpr.IsUnarySec(out unaryArg))
        {
            // Convert Sec[x] to Divide[1, Cos[x]]
            return Mfs.Divide[Expr.INT_ONE, Mfs.Cos[unaryArg]];
        }
        
        if (symExpr.IsUnaryCsc(out unaryArg))
        {
            // Convert Csc[x] to Divide[1, Sin[x]]
            return Mfs.Divide[Expr.INT_ONE, Mfs.Sin[unaryArg]];
        }

        if (symExpr.IsUnaryCot(out unaryArg))
        {
            // Convert Cot[x] to Divide[1, Tan[x]]
            return Mfs.Divide[Expr.INT_ONE, Mfs.Tan[unaryArg]];
        }

        if (symExpr.IsBinaryPlus(out var plusArg1, out var plusArg2))
        {
            // Convert Plus[x, Minus[y]] to Subtract[x, y]
            if (plusArg2.IsUnaryMinus(out unaryArg))
                return Mfs.Subtract[plusArg1, unaryArg];

            // Convert Plus[Minus[x], y] to Subtract[y, x]
            if (plusArg1.IsUnaryMinus(out unaryArg))
                return Mfs.Subtract[plusArg2, unaryArg];

            return symExpr;
        }
        
        if (symExpr.IsBinaryTimes(out var timesArg1, out var timesArg2))
        {
            // Convert Times[-1, x] to Minus[x]
            if (timesArg1.IsMinusOne())
                return Mfs.Minus[timesArg2];
            
            if (timesArg2.IsUnarySec(out unaryArg))
            {
                // Convert Times[x, Sec[y]] to Divide[x, Cos[y]]
                return Mfs.Divide[timesArg1, Mfs.Cos[unaryArg]];
            }
            
            if (timesArg2.IsUnaryCsc(out unaryArg))
            {
                // Convert Times[x, Csc[y]] to Divide[x, Csc[y]]
                return Mfs.Divide[timesArg1, Mfs.Sin[unaryArg]];
            }

            if (timesArg2.IsUnaryCot(out unaryArg))
            {
                // Convert Times[x, Cot[y]] to Divide[x, Tan[y]]
                return Mfs.Divide[timesArg1, Mfs.Tan[unaryArg]];
            }

            if (timesArg2.IsBinaryPower(out powerArg1, out powerArg2))
            {
                // Convert Times[x, Power[y, -1]] to Divide[x, y]
                if (powerArg2.IsMinusOne())
                    return Mfs.Divide[timesArg1, powerArg1];
            }
            
            if (timesArg1.IsBinaryPower(out powerArg1, out powerArg2))
            {
                // Convert Times[Power[x, -1], y] to Divide[y, x]
                if (powerArg2.IsMinusOne())
                    return Mfs.Divide[timesArg2, powerArg1];
            }

            return symExpr;
        }

        if (symExpr.IsTernaryTimes(out timesArg1, out timesArg2, out var timesArg3))
        {
            // Convert Times[-1, x, y] to Times[Minus[x], y]
            if (timesArg1.IsMinusOne())
                return Mfs.Times[Mfs.Minus[timesArg2], timesArg3];

            return symExpr;
        }

        if (symExpr.IsBinaryPower(out powerArg1, out powerArg2))
        {
            // Convert Power[x, 2] to Times[x, x]
            if (powerArg2.IsTwo())
                return Mfs.Times[powerArg1, powerArg1];
            
            // Convert Power[x, 3] to Times[x, x]
            if (powerArg2.IsThree())
                return Mfs.Times[powerArg1, powerArg1, powerArg1];

            // Convert Power[x, -1] to Divide[1, x]
            if (powerArg2.IsMinusOne())
                return Mfs.Divide[Expr.INT_ONE, powerArg1];
            
            // Convert Power[x, -2] to Divide[1, Times[x, x]]
            if (powerArg2.IsMinusTwo())
                return Mfs.Divide[Expr.INT_ONE, Mfs.Times[powerArg1, powerArg1]];
            
            // Convert Power[x, -3] to Divide[1, Times[x, x, x]]
            if (powerArg2.IsMinusThree())
                return Mfs.Divide[Expr.INT_ONE, Mfs.Times[powerArg1, powerArg1, powerArg1]];

            // Convert Power[x, Rational[1, 2]] to Sqrt[x]
            if (powerArg2.IsHalf())
                return Mfs.Sqrt[powerArg1];
            
            // Convert Power[x, Rational[1, 3]] to CubeRoot[x]
            if (powerArg2.IsRationalOneThird())
                return Mfs.CubeRoot[powerArg1];

            // Convert Power[x, Rational[-1, 2]] to 1/Sqrt[x]
            if (powerArg2.IsMinusHalf())
                return Mfs.Divide[Expr.INT_ONE, Mfs.Sqrt[powerArg1]];
            
            // Convert Power[x, Rational[-1, 3]] to 1/CubeRoot[x]
            if (powerArg2.IsMinusHalf())
                return Mfs.Divide[Expr.INT_ONE, Mfs.CubeRoot[powerArg1]];

            return symExpr;
        }

        return symExpr.MapArgs(Enhance);
    }

    private bool IsAffineCombinationTerm(Expr newExpr)
    {
        if (newExpr.AtomQ())
            return true;

        if (newExpr.IsUnaryMinus(out var arg) && arg.AtomQ())
            return true;

        if (newExpr.IsBinaryTimes(out var arg1, out var arg2) && arg1.NumberQ() && arg2.SymbolQ())
            return true;

        return false;
    }

    public bool IsAffineCombination(IMetaExpression expr)
    {
        var symExpr = 
            MathematicaInterface
                .DefaultCas
                .Connection
                .EvaluateToExpr($"Expand[Simplify[{expr}]]");

        var newExpr = Enhance(symExpr);

        if (IsAffineCombinationTerm(newExpr))
            return true;

        if (newExpr.IsPlus() || newExpr.IsSubtract())
            return newExpr.Args.All(IsAffineCombinationTerm);

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression Enhance(IMetaExpression expr)
    {
        var symExpr = 
            MathematicaInterface
                .DefaultCas
                .Connection
                .EvaluateToExpr($"Expand[Simplify[{expr}]]");

        var newExpr = Enhance(symExpr).ToSymbolicExpression(expr.Context);
        
        return newExpr;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression Simplify(IMetaExpression expr)
    {
        return MathematicaInterface
            .DefaultCas
            .Connection
            .EvaluateToExpr($"Simplify[{expr}]")
            .ToSymbolicExpression(expr.Context);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression Simplify(MetaContext context, string exprText)
    {
        return MathematicaInterface
            .DefaultCas
            .Connection
            .EvaluateToExpr($"Simplify[{exprText}]")
            .ToSymbolicExpression(context);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double EvaluateToFloat64(IMetaExpression expr)
    {
        return MathematicaInterface
            .DefaultCas
            .Connection
            .EvaluateToExpr($"{expr}")
            .ToNumber();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double EvaluateToFloat64(string exprText)
    {
        return MathematicaInterface
            .DefaultCas
            .Connection
            .EvaluateToExpr(exprText)
            .ToNumber();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpressionEvaluator GetEvaluatorCopy(MetaContext context)
    {
        return ReferenceEquals(Context, context) 
            ? this 
            : new MathematicaMetaExpressionEvaluator(context);
    }
}