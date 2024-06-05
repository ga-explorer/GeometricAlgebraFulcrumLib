using System;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Composite;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Evaluators;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Numbers;
using GeometricAlgebraFulcrumLib.Utilities.Structures;
using Microsoft.CSharp.RuntimeBinder;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Mathematica.MetaProgramming;

public sealed class MathematicaIntoMetaExpressionConverter : 
    ISymbolicIntoMetaExpressionConverter<Expr>
{
    public MetaContext Context { get; }

    public IScalarProcessor<IMetaExpressionAtomic> ScalarProcessor 
        => Context.ScalarProcessor;

    public bool UseExceptions 
        => true;

    public bool IgnoreNullElements 
        => false;


    internal MathematicaIntoMetaExpressionConverter(MetaContext context)
    {
        Context = context;
    }


    public IMetaExpression Fallback(Expr item, RuntimeBinderException excException)
    {
        throw new NotImplementedException();
    }


    public IMetaExpression Visit(Expr expr)
    {
        var isNumber = expr.NumberQ();
        var isSymbol = expr.SymbolQ();
        var isRational = expr.RationalQ();

        if (isRational)
            return Context.GetOrDefineRationalNumber(
                expr.Args[0].AsInt64(),
                expr.Args[1].AsInt64()
            );

        if (isNumber)
        {
            return expr.ToString() switch
            {
                "0" => Context.ZeroValue,
                "1" => Context.OneValue,
                "-1" => Context.MinusOneValue,
                "2" => Context.TwoValue,
                "-2" => Context.MinusTwoValue,
                "10" => Context.TenValue,
                "-10" => Context.MinusTenValue,
                "Pi" => Context.PiValue,
                "E" => Context.EValue,
                _ => MetaExpressionNumber.Create(Context, expr.ToNumber())
            };
        }

        if (isSymbol)
        {
            var variableName = expr.ToString();
            return Context.TryGetVariable(variableName, out var symbolicVariable)
                ? symbolicVariable
                : Context.GetOrDefineParameterVariable(variableName);
        }

        var functionName = expr.Head.ToString();

        if (expr.Args.Length == 0)
            return MetaExpressionFunction.CreateNonAssociative(Context, functionName);

        var argumentsList = 
            expr.Args.Select(subExpr => subExpr.AcceptVisitor(this)).ToArray();

        return functionName switch
        {
            "Plus" => 
                Context.MetaExpressionProcessor.Add(argumentsList).ScalarValue,

            "Subtract" => 
                Context.MetaExpressionProcessor.Subtract(argumentsList[0], argumentsList[1]).ScalarValue,

            "Times" => 
                Context.MetaExpressionProcessor.Times(argumentsList).ScalarValue,

            "Divide" => 
                Context.MetaExpressionProcessor.Divide(argumentsList[0], argumentsList[1]).ScalarValue,

            "Minus" => 
                Context.MetaExpressionProcessor.Negative(argumentsList[0]).ScalarValue,

            "Inverse" => 
                Context.MetaExpressionProcessor.Inverse(argumentsList[0]).ScalarValue,

            "Abs" => 
                Context.MetaExpressionProcessor.Abs(argumentsList[0]).ScalarValue,

            "Sqrt" => 
                Context.MetaExpressionProcessor.Sqrt(argumentsList[0]).ScalarValue,

            "Exp" => 
                Context.MetaExpressionProcessor.Exp(argumentsList[0]).ScalarValue,

            "Log" => 
                argumentsList.Length == 1 
                    ? Context.MetaExpressionProcessor.LogE(argumentsList[0]).ScalarValue
                    : Context.MetaExpressionProcessor.Log(argumentsList[0], argumentsList[1]).ScalarValue,
                
            "Log2" => 
                Context.MetaExpressionProcessor.Log2(argumentsList[0]).ScalarValue,

            "Log10" => 
                Context.MetaExpressionProcessor.Log10(argumentsList[0]).ScalarValue,

            "Cos" => 
                Context.MetaExpressionProcessor.Cos(argumentsList[0]).ScalarValue,

            "Sin" => 
                Context.MetaExpressionProcessor.Sin(argumentsList[0]).ScalarValue,

            "Tan" => 
                Context.MetaExpressionProcessor.Tan(argumentsList[0]).ScalarValue,

            "ArcCos" => 
                Context.MetaExpressionProcessor.ArcCos(argumentsList[0]).ScalarValue,

            "ArcSin" => 
                Context.MetaExpressionProcessor.ArcSin(argumentsList[0]).ScalarValue,

            "ArcTan" => 
                argumentsList.Length == 1 
                    ? Context.MetaExpressionProcessor.ArcTan(argumentsList[0]).ScalarValue
                    : Context.MetaExpressionProcessor.ArcTan2(argumentsList[0], argumentsList[1]).ScalarValue,

            "Cosh" => 
                Context.MetaExpressionProcessor.Cosh(argumentsList[0]).ScalarValue,

            "Sinh" => 
                Context.MetaExpressionProcessor.Sinh(argumentsList[0]).ScalarValue,

            "Tanh" => 
                Context.MetaExpressionProcessor.Tanh(argumentsList[0]).ScalarValue,

            _ => 
                throw new InvalidOperationException(expr.ToString())
        };
    }
}