using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Evaluators;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Composite;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Numbers;
using Microsoft.CSharp.RuntimeBinder;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Mathematica.MetaExpressions
{
    public sealed class MathematicaIntoMetaExpressionConverter : 
        ISymbolicIntoMetaExpressionConverter<Expr>
    {
        public MetaContext Context { get; }

        public bool UseExceptions 
            => true;

        public bool IgnoreNullElements 
            => false;


        internal MathematicaIntoMetaExpressionConverter([NotNull] MetaContext context)
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
                    "0" => Context.ScalarZero,
                    "1" => Context.ScalarOne,
                    "-1" => Context.ScalarMinusOne,
                    "2" => Context.ScalarTwo,
                    "-2" => Context.ScalarMinusTwo,
                    "10" => Context.ScalarTen,
                    "-10" => Context.ScalarMinusTen,
                    "Pi" => Context.ScalarPi,
                    "E" => Context.ScalarE,
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
                    Context.MetaExpressionProcessor.Add(argumentsList),

                "Subtract" => 
                    Context.MetaExpressionProcessor.Subtract(argumentsList[0], argumentsList[1]),

                "Times" => 
                    Context.MetaExpressionProcessor.Times(argumentsList),

                "Divide" => 
                    Context.MetaExpressionProcessor.Divide(argumentsList[0], argumentsList[1]),

                "Minus" => 
                    Context.MetaExpressionProcessor.Negative(argumentsList[0]),

                "Inverse" => 
                    Context.MetaExpressionProcessor.Inverse(argumentsList[0]),

                "Abs" => 
                    Context.MetaExpressionProcessor.Abs(argumentsList[0]),

                "Sqrt" => 
                    Context.MetaExpressionProcessor.Sqrt(argumentsList[0]),

                "Exp" => 
                    Context.MetaExpressionProcessor.Exp(argumentsList[0]),

                "Log" => 
                    argumentsList.Length == 1 
                        ? Context.MetaExpressionProcessor.LogE(argumentsList[0])
                        : Context.MetaExpressionProcessor.Log(argumentsList[0], argumentsList[1]),
                
                "Log2" => 
                    Context.MetaExpressionProcessor.Log2(argumentsList[0]),

                "Log10" => 
                    Context.MetaExpressionProcessor.Log10(argumentsList[0]),

                "Cos" => 
                    Context.MetaExpressionProcessor.Cos(argumentsList[0]),

                "Sin" => 
                    Context.MetaExpressionProcessor.Sin(argumentsList[0]),

                "Tan" => 
                    Context.MetaExpressionProcessor.Tan(argumentsList[0]),

                "ArcCos" => 
                    Context.MetaExpressionProcessor.ArcCos(argumentsList[0]),

                "ArcSin" => 
                    Context.MetaExpressionProcessor.ArcSin(argumentsList[0]),

                "ArcTan" => 
                    argumentsList.Length == 1 
                        ? Context.MetaExpressionProcessor.ArcTan(argumentsList[0])
                        : Context.MetaExpressionProcessor.ArcTan2(argumentsList[0], argumentsList[1]),

                "Cosh" => 
                    Context.MetaExpressionProcessor.Cosh(argumentsList[0]),

                "Sinh" => 
                    Context.MetaExpressionProcessor.Sinh(argumentsList[0]),

                "Tanh" => 
                    Context.MetaExpressionProcessor.Tanh(argumentsList[0]),

                _ => 
                    throw new InvalidOperationException(expr.ToString())
            };
        }
    }
}