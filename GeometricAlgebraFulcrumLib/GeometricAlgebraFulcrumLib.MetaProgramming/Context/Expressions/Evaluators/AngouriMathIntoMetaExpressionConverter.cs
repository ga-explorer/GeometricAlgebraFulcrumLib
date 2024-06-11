using AngouriMath;
using AngouriMath.Core;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures;
using Microsoft.CSharp.RuntimeBinder;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Numbers;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Evaluators;

public sealed class AngouriMathIntoMetaExpressionConverter :
    ISymbolicIntoMetaExpressionConverter<Entity>
{
    public MetaContext Context { get; }

    public bool UseExceptions
        => true;

    public bool IgnoreNullElements
        => false;


    internal AngouriMathIntoMetaExpressionConverter(MetaContext context)
    {
        Context = context;
    }


    public IMetaExpression Fallback(Entity item, RuntimeBinderException excException)
    {
        throw new NotImplementedException();
    }


    public IMetaExpressionNumber Visit(Entity.Number.Integer expr)
    {
        return Context.GetOrDefineLiteralNumber((int)expr);
    }

    public IMetaExpressionNumber Visit(Entity.Number.Rational expr)
    {
        var (numerator, denominator) = expr;

        return Context.GetOrDefineRationalNumber((int)numerator, (int)denominator);
    }

    public IMetaExpressionNumber Visit(Entity.Number.Real expr)
    {
        return Context.GetOrDefineLiteralNumber((double)expr);
    }

    public IMetaExpressionAtomic Visit(Entity.Variable expr)
    {
        var exprName = expr.Name;

        return exprName switch
        {
            "pi" => Context.PiValue,
            "e" => Context.EValue,
            _ => Context.TryGetVariable(exprName, out var symbolicVariable)
                ? symbolicVariable
                : Context.GetOrDefineParameterVariable(exprName)
        };
    }

    public IMetaExpression Visit(IUnaryNode expr)
    {
        var arg = expr.NodeChild.AcceptVisitor(this);

        return expr switch
        {
            Entity.Absf => Context.MetaExpressionProcessor.Abs(arg).ScalarValue,
            Entity.Cosf => Context.MetaExpressionProcessor.Cos(arg).ScalarValue,
            Entity.Sinf => Context.MetaExpressionProcessor.Sin(arg).ScalarValue,
            Entity.Tanf => Context.MetaExpressionProcessor.Tan(arg).ScalarValue,
            Entity.Arccosf => Context.MetaExpressionProcessor.ArcCos(arg).ScalarValue,
            Entity.Arcsinf => Context.MetaExpressionProcessor.ArcSin(arg).ScalarValue,
            Entity.Arctanf => Context.MetaExpressionProcessor.ArcTan(arg).ScalarValue,

            _ => throw new InvalidOperationException()
        };
    }

    public IMetaExpression Visit(IBinaryNode expr)
    {
        var arg1 = expr.NodeFirstChild.AcceptVisitor(this);
        var arg2 = expr.NodeSecondChild.AcceptVisitor(this);

        return expr switch
        {
            Entity.Sumf => Context.MetaExpressionProcessor.Add(arg1, arg2).ScalarValue,
            Entity.Minusf => Context.MetaExpressionProcessor.Subtract(arg1, arg2).ScalarValue,
            Entity.Mulf => Context.MetaExpressionProcessor.Times(arg1, arg2).ScalarValue,
            Entity.Divf => Context.MetaExpressionProcessor.Divide(arg1, arg2).ScalarValue,
            Entity.Powf => Context.MetaExpressionProcessor.Power(arg1, arg2).ScalarValue,
            Entity.Logf => Context.MetaExpressionProcessor.Log(arg1, arg2).ScalarValue,
            //SymbolicFunctionNames.Negative => -argumentsList[0],

            _ => throw new InvalidOperationException()
        };
    }
}