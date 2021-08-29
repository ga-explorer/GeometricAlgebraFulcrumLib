using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica.ExprFactory;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Composite;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Evaluators;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.HeadSpecs;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Numbers;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Variables;
using Microsoft.CSharp.RuntimeBinder;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Mathematica.SymbolicExpressions
{
    public sealed class MathematicsFromSymbolicExpressionConverter :
        IFromSymbolicExpressionConverter<Expr>
    {
        public SymbolicContext Context { get; }

        public bool UseExceptions 
            => true;

        public bool IgnoreNullElements 
            => false;


        internal MathematicsFromSymbolicExpressionConverter([NotNull] SymbolicContext context)
        {
            Context = context;
        }


        public Expr Fallback(ISymbolicExpression item, RuntimeBinderException excException)
        {
            throw new NotImplementedException();
        }


        public Expr Visit(ISymbolicNumber expr)
        {
            return expr.NumberHeadSpecs switch
            {
                SymbolicHeadSpecsNumberFloat64 n => 
                    n.NumberValue.ToExpr(),

                SymbolicHeadSpecsNumberInt32 n => 
                    n.NumberValueInt32.ToExpr(),

                SymbolicHeadSpecsNumberRational n => 
                    Mfs.Rational[n.Numerator.ToExpr(), n.Denominator.ToExpr()],
                    
                SymbolicHeadSpecsNumberSymbolic n => 
                    n.NumberText switch
                    {
                        SymbolicNumberNames.Pi => MathematicaInterface.DefaultCasConstants.ExprPi,
                        SymbolicNumberNames.E => MathematicaInterface.DefaultCasConstants.ExprE,
                        _ => throw new InvalidOperationException()
                    },

                _ => throw new InvalidOperationException()
            };
        }

        public Expr Visit(ISymbolicVariable expr)
        {
            return expr.InternalName.ToSymbolExpr();
        }

        public Expr Visit(ISymbolicFunction expr)
        {
            var argumentsList =
                expr.Arguments
                    .Select(a => a.AcceptVisitor(this))
                    .Cast<object>()
                    .ToArray();

            return expr.FunctionHeadSpecs.FunctionName switch
            {
                SymbolicFunctionNames.Plus => Mfs.Plus[argumentsList],
                SymbolicFunctionNames.Subtract => Mfs.Subtract[argumentsList],
                SymbolicFunctionNames.Times => Mfs.Times[argumentsList],
                SymbolicFunctionNames.Divide => Mfs.Divide[argumentsList],
                SymbolicFunctionNames.Negative => Mfs.Minus[argumentsList],
                SymbolicFunctionNames.Inverse => Mfs.Inverse[argumentsList],
                SymbolicFunctionNames.Abs => Mfs.Minus[argumentsList],
                SymbolicFunctionNames.Sqrt => Mfs.Sqrt[argumentsList],
                SymbolicFunctionNames.Exp => Mfs.Exp[argumentsList],
                SymbolicFunctionNames.Log => Mfs.Log[argumentsList],
                SymbolicFunctionNames.Log2 => Mfs.Log2[argumentsList],
                SymbolicFunctionNames.Log10 => Mfs.Log10[argumentsList],
                SymbolicFunctionNames.Cos => Mfs.Cos[argumentsList],
                SymbolicFunctionNames.Sin => Mfs.Sin[argumentsList],
                SymbolicFunctionNames.Tan => Mfs.Tan[argumentsList],
                SymbolicFunctionNames.ArcCos => Mfs.ArcCos[argumentsList],
                SymbolicFunctionNames.ArcSin => Mfs.ArcSin[argumentsList],
                SymbolicFunctionNames.ArcTan => Mfs.ArcTan[argumentsList],
                SymbolicFunctionNames.Cosh => Mfs.Cosh[argumentsList],
                SymbolicFunctionNames.Sinh => Mfs.Sinh[argumentsList],
                SymbolicFunctionNames.Tanh => Mfs.Tanh[argumentsList],

                _ => throw new InvalidOperationException()
            };
        }
    }
}