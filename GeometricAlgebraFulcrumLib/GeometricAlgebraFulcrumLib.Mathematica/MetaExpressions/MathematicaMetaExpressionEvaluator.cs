using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica.ExprFactory;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Evaluators;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Mathematica.MetaExpressions;

public sealed class MathematicaMetaExpressionEvaluator :
    IMetaExpressionEvaluator<Expr>
{
    public MetaContext Context { get; }
        
    public ISymbolicFromMetaExpressionConverter<Expr> FromSymbolicExpressionConverter { get; }
        
    public ISymbolicIntoMetaExpressionConverter<Expr> IntoSymbolicExpressionConverter { get; }


    internal MathematicaMetaExpressionEvaluator([NotNull] MetaContext context)
    {
        Context = context;
        FromSymbolicExpressionConverter = new MathematicaFromMetaExpressionConverter(context);
        IntoSymbolicExpressionConverter = new MathematicaIntoMetaExpressionConverter(context);
    }


    private static Expr Enhance(Expr symExpr)
    {
        // Convert Times[-1, x] to Minus[x]
        if (symExpr.Head.ToString() == "Times" && symExpr.Args.Length == 2 && symExpr.Args[0].ToString() == "-1")
            return Mfs.Minus[symExpr.Args[1]];

        // Convert Times[x, -1] to Minus[x]
        if (symExpr.Head.ToString() == "Times" && symExpr.Args.Length == 2 && symExpr.Args[1].ToString() == "-1")
            return Mfs.Minus[symExpr.Args[0]];

        // Convert Power[x, -1] to Divide[1, x]
        if (symExpr.Head.ToString() == "Power" && symExpr.Args.Length == 2 && symExpr.Args[1].ToString() == "-1")
            return Mfs.Divide[Expr.INT_ONE, symExpr.Args[0]];

        // Convert Power[x, Rational[1, 2]] to Sqrt[x]
        if (symExpr.Head.ToString() == "Power" && symExpr.Args.Length == 2 && symExpr.Args[1].ToString() == "Rational[1, 2]")
            return Mfs.Sqrt[symExpr.Args[0]];

        // Convert Power[x, Rational[-1, 2]] to 1/Sqrt[x]
        if (symExpr.Head.ToString() == "Power" && symExpr.Args.Length == 2 && symExpr.Args[1].ToString() == "Rational[1, 2]")
            return Mfs.Divide[Expr.INT_ONE, Mfs.Sqrt[symExpr.Args[0]]];

        return symExpr;
    }

    public IMetaExpression Simplify(IMetaExpression expr)
    {
        var symExpr = MathematicaInterface
            .DefaultCas
            .Connection
            .EvaluateToExpr($"Simplify[{expr}]");

        symExpr = Enhance(symExpr);

        return symExpr.ToSymbolicExpression(Context);
    }

    public IMetaExpression Simplify(string exprText)
    {
        return MathematicaInterface
            .DefaultCas
            .Connection
            .EvaluateToExpr($"Simplify[{exprText}]")
            .ToSymbolicExpression(Context);
    }

    public double EvaluateToFloat64(IMetaExpression expr)
    {
        return MathematicaInterface
            .DefaultCas
            .Connection
            .EvaluateToExpr($"{expr}")
            .ToNumber();
    }

    public double EvaluateToFloat64(string exprText)
    {
        return MathematicaInterface
            .DefaultCas
            .Connection
            .EvaluateToExpr(exprText)
            .ToNumber();
    }
}