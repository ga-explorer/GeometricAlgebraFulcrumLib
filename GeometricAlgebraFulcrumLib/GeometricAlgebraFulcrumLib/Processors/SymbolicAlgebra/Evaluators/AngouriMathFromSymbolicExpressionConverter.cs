using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AngouriMath;
using DataStructuresLib;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.Composite;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.HeadSpecs;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.Numbers;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.Variables;
using GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Context;
using Microsoft.CSharp.RuntimeBinder;

namespace GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Evaluators
{
    public sealed class AngouriMathFromSymbolicExpressionConverter :
        IFromSymbolicExpressionConverter<Entity>
    {
        public SymbolicContext Context { get; }

        public bool UseExceptions 
            => true;

        public bool IgnoreNullElements 
            => false;


        internal AngouriMathFromSymbolicExpressionConverter([NotNull] SymbolicContext context)
        {
            Context = context;
        }


        public Entity Fallback(ISymbolicExpression item, RuntimeBinderException excException)
        {
            throw new NotImplementedException();
        }


        public Entity Visit(ISymbolicNumber expr)
        {
            return expr.NumberHeadSpecs switch
            {
                SymbolicHeadSpecsNumberFloat32 n => 
                    MathS.Numbers.Create(n.NumberFloat32Value),

                SymbolicHeadSpecsNumberFloat64 n => 
                    MathS.Numbers.Create(n.NumberFloat64Value),

                SymbolicHeadSpecsNumberInt32 n => 
                    MathS.Numbers.Create(n.NumberInt32Value),

                SymbolicHeadSpecsNumberUInt32 n => 
                    MathS.Numbers.Create(n.NumberUInt32Value),

                SymbolicHeadSpecsNumberInt64 n => 
                    MathS.Numbers.Create(n.NumberInt64Value),

                SymbolicHeadSpecsNumberUInt64 n => 
                    MathS.Numbers.Create((long) n.NumberUInt64Value),

                SymbolicHeadSpecsNumberRational n => 
                    MathS.Numbers.CreateRational(n.Numerator, n.Denominator),
                    
                SymbolicHeadSpecsNumberSymbolic n => 
                    n.NumberText switch
                    {
                        SymbolicNumberNames.Pi => MathS.pi,
                        SymbolicNumberNames.E => MathS.e,
                        _ => throw new InvalidOperationException()
                    },

                _ => throw new InvalidOperationException()
            };
        }

        public Entity Visit(ISymbolicVariable expr)
        {
            return MathS.Var(expr.InternalName);
        }

        public Entity Visit(ISymbolicFunction expr)
        {
            var argumentsList =
                expr.Arguments
                    .Select(a => a.AcceptVisitor(this))
                    .ToArray();

            return expr.FunctionHeadSpecs.FunctionName switch
            {
                SymbolicFunctionNames.Plus => argumentsList.Aggregate((a, b) => a + b),
                SymbolicFunctionNames.Subtract => argumentsList[0] - argumentsList[1],
                SymbolicFunctionNames.Times => argumentsList.Aggregate((a, b) => a * b),
                SymbolicFunctionNames.Divide => argumentsList[0] / argumentsList[1],
                SymbolicFunctionNames.Negative => -argumentsList[0],
                SymbolicFunctionNames.Inverse => Entity.Number.Integer.One / argumentsList[0],
                SymbolicFunctionNames.Abs => MathS.Abs(argumentsList[0]),
                SymbolicFunctionNames.Sqrt => MathS.Sqrt(argumentsList[0]),
                SymbolicFunctionNames.Exp => MathS.Pow(MathS.e, argumentsList[0]),
                SymbolicFunctionNames.Power => MathS.Pow(argumentsList[0], argumentsList[1]),
                SymbolicFunctionNames.Log => MathS.Ln(argumentsList[0]),
                SymbolicFunctionNames.Log2 => MathS.Log(2, argumentsList[0]),
                SymbolicFunctionNames.Log10 => MathS.Log(argumentsList[0]),
                SymbolicFunctionNames.Cos => MathS.Cos(argumentsList[0]),
                SymbolicFunctionNames.Sin => MathS.Sin(argumentsList[0]),
                SymbolicFunctionNames.Tan => MathS.Tan(argumentsList[0]),
                SymbolicFunctionNames.ArcCos => MathS.Arccos(argumentsList[0]),
                SymbolicFunctionNames.ArcSin => MathS.Arcsin(argumentsList[0]),
                SymbolicFunctionNames.ArcTan => MathS.Arctan(argumentsList[0]),
                SymbolicFunctionNames.Cosh => MathS.Hyperbolic.Cosh(argumentsList[0]),
                SymbolicFunctionNames.Sinh => MathS.Hyperbolic.Sinh(argumentsList[0]),
                SymbolicFunctionNames.Tanh => MathS.Hyperbolic.Tanh(argumentsList[0]),

                _ => throw new InvalidOperationException()
            };
        }
    }
}