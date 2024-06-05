using System;
using System.Linq;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures.ExprFactory;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Composite;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Evaluators;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.HeadSpecs;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Numbers;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Variables;
using GeometricAlgebraFulcrumLib.Utilities.Structures;
using Microsoft.CSharp.RuntimeBinder;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Mathematica.MetaProgramming;

public sealed class MathematicaFromMetaExpressionConverter :
    ISymbolicFromMetaExpressionConverter<Expr>
{
    public static MathematicaFromMetaExpressionConverter Instance { get; }
        = new MathematicaFromMetaExpressionConverter();


    public bool UseExceptions 
        => true;

    public bool IgnoreNullElements 
        => false;


    private MathematicaFromMetaExpressionConverter()
    {
    }


    public Expr Fallback(IMetaExpression item, RuntimeBinderException excException)
    {
        throw new NotImplementedException();
    }


    public Expr Visit(IMetaExpressionNumber expr)
    {
        return expr.NumberHeadSpecs switch
        {
            MetaExpressionHeadSpecsNumberFloat64 n => 
                n.NumberFloat64Value.ToExpr(),

            MetaExpressionHeadSpecsNumberFloat32 n => 
                n.NumberFloat64Value.ToExpr(),

            MetaExpressionHeadSpecsNumberInt32 n => 
                n.NumberInt32Value.ToExpr(),

            MetaExpressionHeadSpecsNumberUInt32 n => 
                n.NumberUInt32Value.ToExpr(),

            MetaExpressionHeadSpecsNumberInt64 n => 
                n.NumberInt64Value.ToExpr(),

            MetaExpressionHeadSpecsNumberUInt64 n => 
                n.NumberUInt64Value.ToExpr(),

            MetaExpressionHeadSpecsNumberRational n => 
                Mfs.Rational[n.Numerator.ToExpr(), n.Denominator.ToExpr()],
                    
            MetaExpressionHeadSpecsNumberSymbolic n => 
                n.NumberText switch
                {
                    MetaExpressionNumberNames.Pi => MathematicaInterface.DefaultCasConstants.ExprPi,
                    MetaExpressionNumberNames.E => MathematicaInterface.DefaultCasConstants.ExprE,
                    _ => throw new InvalidOperationException()
                },

            _ => throw new InvalidOperationException()
        };
    }

    public Expr Visit(IMetaExpressionVariable expr)
    {
        return expr.InternalName.ToSymbolExpr();
    }

    public Expr Visit(IMetaExpressionFunction expr)
    {
        var argumentsList =
            expr.Arguments
                .Select(a => a.AcceptVisitor(this))
                .Cast<object>()
                .ToArray();

        return expr.FunctionHeadSpecs.FunctionName switch
        {
            MetaExpressionFunctionNames.Plus => Mfs.Plus[argumentsList],
            MetaExpressionFunctionNames.Subtract => Mfs.Subtract[argumentsList],
            MetaExpressionFunctionNames.Times => Mfs.Times[argumentsList],
            MetaExpressionFunctionNames.Divide => Mfs.Divide[argumentsList],
            MetaExpressionFunctionNames.Negative => Mfs.Minus[argumentsList],
            MetaExpressionFunctionNames.Inverse => Mfs.Inverse[argumentsList],
            MetaExpressionFunctionNames.Abs => Mfs.Minus[argumentsList],
            MetaExpressionFunctionNames.Sqrt => Mfs.Sqrt[argumentsList],
            MetaExpressionFunctionNames.Exp => Mfs.Exp[argumentsList],
            MetaExpressionFunctionNames.Log => Mfs.Log[argumentsList],
            MetaExpressionFunctionNames.Log2 => Mfs.Log2[argumentsList],
            MetaExpressionFunctionNames.Log10 => Mfs.Log10[argumentsList],
            MetaExpressionFunctionNames.Cos => Mfs.Cos[argumentsList],
            MetaExpressionFunctionNames.Sin => Mfs.Sin[argumentsList],
            MetaExpressionFunctionNames.Tan => Mfs.Tan[argumentsList],
            MetaExpressionFunctionNames.ArcCos => Mfs.ArcCos[argumentsList],
            MetaExpressionFunctionNames.ArcSin => Mfs.ArcSin[argumentsList],
            MetaExpressionFunctionNames.ArcTan => Mfs.ArcTan[argumentsList],
            MetaExpressionFunctionNames.Cosh => Mfs.Cosh[argumentsList],
            MetaExpressionFunctionNames.Sinh => Mfs.Sinh[argumentsList],
            MetaExpressionFunctionNames.Tanh => Mfs.Tanh[argumentsList],

            _ => throw new InvalidOperationException()
        };
    }
}