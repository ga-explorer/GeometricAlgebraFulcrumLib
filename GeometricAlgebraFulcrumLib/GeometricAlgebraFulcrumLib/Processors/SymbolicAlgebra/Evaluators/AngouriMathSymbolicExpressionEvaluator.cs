using System;
using System.Runtime.CompilerServices;
using AngouriMath;
using System.Diagnostics.CodeAnalysis;
using DataStructuresLib;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra;
using GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Context;

namespace GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Evaluators
{
    public sealed class AngouriMathSymbolicExpressionEvaluator 
        : ISymbolicExpressionEvaluator<Entity>
    {
        public SymbolicContext Context { get; }

        public IFromSymbolicExpressionConverter<Entity> FromSymbolicExpressionConverter { get; }

        public IIntoSymbolicExpressionConverter<Entity> IntoSymbolicExpressionConverter { get; }


        internal AngouriMathSymbolicExpressionEvaluator([NotNull] SymbolicContext context)
        {
            Context = context;
            FromSymbolicExpressionConverter = new AngouriMathFromSymbolicExpressionConverter(context);
            IntoSymbolicExpressionConverter = new AngouriMathIntoSymbolicExpressionConverter(context);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression Convert(Entity expr)
        {
            return expr.AcceptVisitor(IntoSymbolicExpressionConverter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Entity Convert(ISymbolicExpression expr)
        {
            return expr.AcceptVisitor(FromSymbolicExpressionConverter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression Simplify(ISymbolicExpression expr)
        {
            return Convert(Convert(expr).Simplify());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression Simplify(string exprText)
        {
            return Convert(MathS.FromString(exprText, false));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double EvaluateToFloat64(ISymbolicExpression expr)
        {
            var expr1 = Convert(expr);

            return expr1.EvaluableNumerical
                ? (double) expr1.EvalNumerical()
                : throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double EvaluateToFloat64(string exprText)
        {
            var expr1 = MathS.FromString(exprText, false);

            return expr1.EvaluableNumerical
                ? (double) expr1.EvalNumerical()
                : throw new InvalidOperationException();
        }
    }
}