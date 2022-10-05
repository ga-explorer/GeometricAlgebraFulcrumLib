using System;
using System.Diagnostics.CodeAnalysis;
using AngouriMath;
using AngouriMath.Core;
using DataStructuresLib;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Numbers;
using Microsoft.CSharp.RuntimeBinder;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Evaluators
{
    public sealed class AngouriMathIntoMetaExpressionConverter : 
        ISymbolicIntoMetaExpressionConverter<Entity>
    {
        public MetaContext Context { get; }

        public bool UseExceptions 
            => true;

        public bool IgnoreNullElements 
            => false;


        internal AngouriMathIntoMetaExpressionConverter([NotNull] MetaContext context)
        {
            Context = context;
        }


        public IMetaExpression Fallback(Entity item, RuntimeBinderException excException)
        {
            throw new NotImplementedException();
        }


        public IMetaExpressionNumber Visit(Entity.Number.Integer expr)
        {
            return Context.GetOrDefineLiteralNumber((int) expr);
        }

        public IMetaExpressionNumber Visit(Entity.Number.Rational expr)
        {
            var (numerator, denominator) = expr;

            return Context.GetOrDefineRationalNumber((int) numerator, (int) denominator);
        }

        public IMetaExpressionNumber Visit(Entity.Number.Real expr)
        {
            return Context.GetOrDefineLiteralNumber((double) expr);
        }
        
        public IMetaExpressionAtomic Visit(Entity.Variable expr)
        {
            var exprName = expr.Name;

            return exprName switch
            {
                "pi" => Context.ScalarPi,
                "e" => Context.ScalarE,
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
                Entity.Absf => Context.MetaExpressionProcessor.Abs(arg),
                Entity.Cosf => Context.MetaExpressionProcessor.Cos(arg),
                Entity.Sinf => Context.MetaExpressionProcessor.Sin(arg),
                Entity.Tanf => Context.MetaExpressionProcessor.Tan(arg),
                Entity.Arccosf => Context.MetaExpressionProcessor.ArcCos(arg),
                Entity.Arcsinf => Context.MetaExpressionProcessor.ArcSin(arg),
                Entity.Arctanf => Context.MetaExpressionProcessor.ArcTan(arg),
                
                _ => throw new InvalidOperationException()
            };
        }

        public IMetaExpression Visit(IBinaryNode expr)
        {
            var arg1 = expr.NodeFirstChild.AcceptVisitor(this);
            var arg2 = expr.NodeSecondChild.AcceptVisitor(this);

            return expr switch
            {
                Entity.Sumf => Context.MetaExpressionProcessor.Add(arg1, arg2),
                Entity.Minusf => Context.MetaExpressionProcessor.Subtract(arg1, arg2),
                Entity.Mulf => Context.MetaExpressionProcessor.Times(arg1, arg2),
                Entity.Divf => Context.MetaExpressionProcessor.Divide(arg1, arg2),
                Entity.Powf => Context.MetaExpressionProcessor.Power(arg1, arg2),
                Entity.Logf => Context.MetaExpressionProcessor.Log(arg1, arg2),
                //SymbolicFunctionNames.Negative => -argumentsList[0],

                _ => throw new InvalidOperationException()
            };
        }
    }
}