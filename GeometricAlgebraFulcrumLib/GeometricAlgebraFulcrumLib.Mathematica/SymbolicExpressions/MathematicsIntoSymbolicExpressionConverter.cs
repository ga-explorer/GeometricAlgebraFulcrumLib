using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.Composite;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.Numbers;
using GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Context;
using GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Evaluators;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using Microsoft.CSharp.RuntimeBinder;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Mathematica.SymbolicExpressions
{
    public sealed class MathematicsIntoSymbolicExpressionConverter : 
        IIntoSymbolicExpressionConverter<Expr>
    {
        public SymbolicContext Context { get; }

        public bool UseExceptions 
            => true;

        public bool IgnoreNullElements 
            => false;


        internal MathematicsIntoSymbolicExpressionConverter([NotNull] SymbolicContext context)
        {
            Context = context;
        }


        public ISymbolicExpression Fallback(Expr item, RuntimeBinderException excException)
        {
            throw new NotImplementedException();
        }


        public ISymbolicExpression Visit(Expr expr)
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
                    "0" => Context.ScalarZero,
                    "1" => Context.ScalarOne,
                    "-1" => Context.ScalarMinusOne,
                    "2" => Context.ScalarTwo,
                    "-2" => Context.ScalarMinusTwo,
                    "10" => Context.ScalarTen,
                    "-10" => Context.ScalarMinusTen,
                    "Pi" => Context.ScalarPi,
                    "E" => Context.ScalarE,
                    _ => SymbolicNumber.Create(Context, expr.ToNumber())
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
                return SymbolicFunction.CreateNonAssociative(Context, functionName);

            var argumentsList = 
                expr.Args.Select(subExpr => subExpr.AcceptVisitor(this)).ToArray();

            return functionName switch
            {
                "Plus" => 
                    Context.SymbolicScalarProcessor.Add(argumentsList),

                "Subtract" => 
                    Context.SymbolicScalarProcessor.Subtract(argumentsList[0], argumentsList[1]),

                "Times" => 
                    Context.SymbolicScalarProcessor.Times(argumentsList),

                "Divide" => 
                    Context.SymbolicScalarProcessor.Divide(argumentsList[0], argumentsList[1]),

                "Minus" => 
                    Context.SymbolicScalarProcessor.Negative(argumentsList[0]),

                "Inverse" => 
                    Context.SymbolicScalarProcessor.Inverse(argumentsList[0]),

                "Abs" => 
                    Context.SymbolicScalarProcessor.Abs(argumentsList[0]),

                "Sqrt" => 
                    Context.SymbolicScalarProcessor.Sqrt(argumentsList[0]),

                "Exp" => 
                    Context.SymbolicScalarProcessor.Exp(argumentsList[0]),

                "Log" => 
                    argumentsList.Length == 1 
                        ? Context.SymbolicScalarProcessor.LogE(argumentsList[0])
                        : Context.SymbolicScalarProcessor.Log(argumentsList[0], argumentsList[1]),
                
                "Log2" => 
                    Context.SymbolicScalarProcessor.Log2(argumentsList[0]),

                "Log10" => 
                    Context.SymbolicScalarProcessor.Log10(argumentsList[0]),

                "Cos" => 
                    Context.SymbolicScalarProcessor.Cos(argumentsList[0]),

                "Sin" => 
                    Context.SymbolicScalarProcessor.Sin(argumentsList[0]),

                "Tan" => 
                    Context.SymbolicScalarProcessor.Tan(argumentsList[0]),

                "ArcCos" => 
                    Context.SymbolicScalarProcessor.ArcCos(argumentsList[0]),

                "ArcSin" => 
                    Context.SymbolicScalarProcessor.ArcSin(argumentsList[0]),

                "ArcTan" => 
                    argumentsList.Length == 1 
                        ? Context.SymbolicScalarProcessor.ArcTan(argumentsList[0])
                        : Context.SymbolicScalarProcessor.ArcTan2(argumentsList[0], argumentsList[1]),

                "Cosh" => 
                    Context.SymbolicScalarProcessor.Cosh(argumentsList[0]),

                "Sinh" => 
                    Context.SymbolicScalarProcessor.Sinh(argumentsList[0]),

                "Tanh" => 
                    Context.SymbolicScalarProcessor.Tanh(argumentsList[0]),

                _ => 
                    throw new InvalidOperationException(expr.ToString())
            };
        }
    }
}