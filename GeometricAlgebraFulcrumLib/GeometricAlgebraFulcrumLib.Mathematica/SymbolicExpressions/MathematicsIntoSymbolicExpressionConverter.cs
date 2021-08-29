using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Composite;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Evaluators;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Numbers;
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
                    Context.SymbolicExpressionProcessor.Add(argumentsList),

                "Subtract" => 
                    Context.SymbolicExpressionProcessor.Subtract(argumentsList[0], argumentsList[1]),

                "Times" => 
                    Context.SymbolicExpressionProcessor.Times(argumentsList),

                "Divide" => 
                    Context.SymbolicExpressionProcessor.Divide(argumentsList[0], argumentsList[1]),

                "Minus" => 
                    Context.SymbolicExpressionProcessor.Negative(argumentsList[0]),

                "Inverse" => 
                    Context.SymbolicExpressionProcessor.Inverse(argumentsList[0]),

                "Abs" => 
                    Context.SymbolicExpressionProcessor.Abs(argumentsList[0]),

                "Sqrt" => 
                    Context.SymbolicExpressionProcessor.Sqrt(argumentsList[0]),

                "Exp" => 
                    Context.SymbolicExpressionProcessor.Exp(argumentsList[0]),

                "Log" => 
                    argumentsList.Length == 1 
                        ? Context.SymbolicExpressionProcessor.Log(argumentsList[0])
                        : Context.SymbolicExpressionProcessor.Log(argumentsList[0], argumentsList[1]),
                
                "Log2" => 
                    Context.SymbolicExpressionProcessor.Log2(argumentsList[0]),

                "Log10" => 
                    Context.SymbolicExpressionProcessor.Log10(argumentsList[0]),

                "Cos" => 
                    Context.SymbolicExpressionProcessor.Cos(argumentsList[0]),

                "Sin" => 
                    Context.SymbolicExpressionProcessor.Sin(argumentsList[0]),

                "Tan" => 
                    Context.SymbolicExpressionProcessor.Tan(argumentsList[0]),

                "ArcCos" => 
                    Context.SymbolicExpressionProcessor.ArcCos(argumentsList[0]),

                "ArcSin" => 
                    Context.SymbolicExpressionProcessor.ArcSin(argumentsList[0]),

                "ArcTan" => 
                    argumentsList.Length == 1 
                        ? Context.SymbolicExpressionProcessor.ArcTan(argumentsList[0])
                        : Context.SymbolicExpressionProcessor.ArcTan2(argumentsList[0], argumentsList[1]),

                "Cosh" => 
                    Context.SymbolicExpressionProcessor.Cosh(argumentsList[0]),

                "Sinh" => 
                    Context.SymbolicExpressionProcessor.Sinh(argumentsList[0]),

                "Tanh" => 
                    Context.SymbolicExpressionProcessor.Tanh(argumentsList[0]),

                _ => 
                    throw new InvalidOperationException(expr.ToString())
            };
        }
    }
}