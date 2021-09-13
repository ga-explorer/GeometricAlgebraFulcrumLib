using System;
using AngouriMath;
using AngouriMath.Core;
using System.Diagnostics.CodeAnalysis;
using DataStructuresLib;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.Numbers;
using GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Context;
using Microsoft.CSharp.RuntimeBinder;

namespace GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Evaluators
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
                Entity.Absf => Context.SymbolicScalarProcessor.Abs(arg),
                Entity.Cosf => Context.SymbolicScalarProcessor.Cos(arg),
                Entity.Sinf => Context.SymbolicScalarProcessor.Sin(arg),
                Entity.Tanf => Context.SymbolicScalarProcessor.Tan(arg),
                Entity.Arccosf => Context.SymbolicScalarProcessor.ArcCos(arg),
                Entity.Arcsinf => Context.SymbolicScalarProcessor.ArcSin(arg),
                Entity.Arctanf => Context.SymbolicScalarProcessor.ArcTan(arg),
                
                _ => throw new InvalidOperationException()
            };
        }

        public ISymbolicExpression Visit(IBinaryNode expr)
        {
            var arg1 = expr.NodeFirstChild.AcceptVisitor(this);
            var arg2 = expr.NodeSecondChild.AcceptVisitor(this);

            return expr switch
            {
                Entity.Sumf => Context.SymbolicScalarProcessor.Add(arg1, arg2),
                Entity.Minusf => Context.SymbolicScalarProcessor.Subtract(arg1, arg2),
                Entity.Mulf => Context.SymbolicScalarProcessor.Times(arg1, arg2),
                Entity.Divf => Context.SymbolicScalarProcessor.Divide(arg1, arg2),
                Entity.Powf => Context.SymbolicScalarProcessor.Power(arg1, arg2),
                Entity.Logf => Context.SymbolicScalarProcessor.Log(arg1, arg2),
                //SymbolicFunctionNames.Negative => -argumentsList[0],

                _ => throw new InvalidOperationException()
            };
        }
    }
}