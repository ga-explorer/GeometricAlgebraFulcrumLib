using System;
using AngouriMath;
using AngouriMath.Core;
using Antlr4.Runtime.Misc;
using DataStructuresLib;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Numbers;
using Microsoft.CSharp.RuntimeBinder;

namespace GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Evaluators
{
    public sealed class AngouriMathIntoSymbolicExpressionConverter : 
        IIntoSymbolicExpressionConverter<Entity>
    {
        public SymbolicContext Context { get; }

        public bool UseExceptions 
            => true;

        public bool IgnoreNullElements 
            => false;


        internal AngouriMathIntoSymbolicExpressionConverter([NotNull] SymbolicContext context)
        {
            Context = context;
        }


        public ISymbolicExpression Fallback(Entity item, RuntimeBinderException excException)
        {
            throw new NotImplementedException();
        }


        public ISymbolicNumber Visit(Entity.Number.Integer expr)
        {
            return Context.GetOrDefineLiteralNumber((int) expr);
        }

        public ISymbolicNumber Visit(Entity.Number.Rational expr)
        {
            var (numerator, denominator) = expr;

            return Context.GetOrDefineRationalNumber((int) numerator, (int) denominator);
        }

        public ISymbolicNumber Visit(Entity.Number.Real expr)
        {
            return Context.GetOrDefineLiteralNumber((double) expr);
        }
        
        public ISymbolicExpressionAtomic Visit(Entity.Variable expr)
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

        public ISymbolicExpression Visit(IUnaryNode expr)
        {
            var arg = expr.NodeChild.AcceptVisitor(this);

            return expr switch
            {
                Entity.Absf => Context.SymbolicExpressionProcessor.Abs(arg),
                Entity.Cosf => Context.SymbolicExpressionProcessor.Cos(arg),
                Entity.Sinf => Context.SymbolicExpressionProcessor.Sin(arg),
                Entity.Tanf => Context.SymbolicExpressionProcessor.Tan(arg),
                Entity.Arccosf => Context.SymbolicExpressionProcessor.ArcCos(arg),
                Entity.Arcsinf => Context.SymbolicExpressionProcessor.ArcSin(arg),
                Entity.Arctanf => Context.SymbolicExpressionProcessor.ArcTan(arg),
                
                _ => throw new InvalidOperationException()
            };
        }

        public ISymbolicExpression Visit(IBinaryNode expr)
        {
            var arg1 = expr.NodeFirstChild.AcceptVisitor(this);
            var arg2 = expr.NodeSecondChild.AcceptVisitor(this);

            return expr switch
            {
                Entity.Sumf => Context.SymbolicExpressionProcessor.Add(arg1, arg2),
                Entity.Minusf => Context.SymbolicExpressionProcessor.Subtract(arg1, arg2),
                Entity.Mulf => Context.SymbolicExpressionProcessor.Times(arg1, arg2),
                Entity.Divf => Context.SymbolicExpressionProcessor.Divide(arg1, arg2),
                Entity.Powf => Context.SymbolicExpressionProcessor.Power(arg1, arg2),
                Entity.Logf => Context.SymbolicExpressionProcessor.Log(arg1, arg2),
                //SymbolicFunctionNames.Negative => -argumentsList[0],

                _ => throw new InvalidOperationException()
            };
        }
    }
}