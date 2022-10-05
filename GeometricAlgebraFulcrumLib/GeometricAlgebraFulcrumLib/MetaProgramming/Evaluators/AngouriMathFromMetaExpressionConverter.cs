using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AngouriMath;
using DataStructuresLib;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Composite;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.HeadSpecs;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Numbers;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Variables;
using Microsoft.CSharp.RuntimeBinder;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Evaluators
{
    public sealed class AngouriMathFromMetaExpressionConverter :
        ISymbolicFromMetaExpressionConverter<Entity>
    {
        public MetaContext Context { get; }

        public bool UseExceptions 
            => true;

        public bool IgnoreNullElements 
            => false;


        internal AngouriMathFromMetaExpressionConverter([NotNull] MetaContext context)
        {
            Context = context;
        }


        public Entity Fallback(IMetaExpression item, RuntimeBinderException excException)
        {
            throw new NotImplementedException();
        }


        public Entity Visit(IMetaExpressionNumber expr)
        {
            return expr.NumberHeadSpecs switch
            {
                MetaExpressionHeadSpecsNumberFloat32 n => 
                    MathS.Numbers.Create(n.NumberFloat32Value),

                MetaExpressionHeadSpecsNumberFloat64 n => 
                    MathS.Numbers.Create(n.NumberFloat64Value),

                MetaExpressionHeadSpecsNumberInt32 n => 
                    MathS.Numbers.Create(n.NumberInt32Value),

                MetaExpressionHeadSpecsNumberUInt32 n => 
                    MathS.Numbers.Create(n.NumberUInt32Value),

                MetaExpressionHeadSpecsNumberInt64 n => 
                    MathS.Numbers.Create(n.NumberInt64Value),

                MetaExpressionHeadSpecsNumberUInt64 n => 
                    MathS.Numbers.Create((long) n.NumberUInt64Value),

                MetaExpressionHeadSpecsNumberRational n => 
                    MathS.Numbers.CreateRational(n.Numerator, n.Denominator),
                    
                MetaExpressionHeadSpecsNumberSymbolic n => 
                    n.NumberText switch
                    {
                        MetaExpressionNumberNames.Pi => MathS.pi,
                        MetaExpressionNumberNames.E => MathS.e,
                        _ => throw new InvalidOperationException()
                    },

                _ => throw new InvalidOperationException()
            };
        }

        public Entity Visit(IMetaExpressionVariable expr)
        {
            return MathS.Var(expr.InternalName);
        }

        public Entity Visit(IMetaExpressionFunction expr)
        {
            var argumentsList =
                expr.Arguments
                    .Select(a => a.AcceptVisitor(this))
                    .ToArray();

            return expr.FunctionHeadSpecs.FunctionName switch
            {
                MetaExpressionFunctionNames.Plus => argumentsList.Aggregate((a, b) => a + b),
                MetaExpressionFunctionNames.Subtract => argumentsList[0] - argumentsList[1],
                MetaExpressionFunctionNames.Times => argumentsList.Aggregate((a, b) => a * b),
                MetaExpressionFunctionNames.Divide => argumentsList[0] / argumentsList[1],
                MetaExpressionFunctionNames.Negative => -argumentsList[0],
                MetaExpressionFunctionNames.Inverse => Entity.Number.Integer.One / argumentsList[0],
                MetaExpressionFunctionNames.Abs => MathS.Abs(argumentsList[0]),
                MetaExpressionFunctionNames.Sqrt => MathS.Sqrt(argumentsList[0]),
                MetaExpressionFunctionNames.Exp => MathS.Pow(MathS.e, argumentsList[0]),
                MetaExpressionFunctionNames.Power => MathS.Pow(argumentsList[0], argumentsList[1]),
                MetaExpressionFunctionNames.Log => MathS.Ln(argumentsList[0]),
                MetaExpressionFunctionNames.Log2 => MathS.Log(2, argumentsList[0]),
                MetaExpressionFunctionNames.Log10 => MathS.Log(argumentsList[0]),
                MetaExpressionFunctionNames.Cos => MathS.Cos(argumentsList[0]),
                MetaExpressionFunctionNames.Sin => MathS.Sin(argumentsList[0]),
                MetaExpressionFunctionNames.Tan => MathS.Tan(argumentsList[0]),
                MetaExpressionFunctionNames.ArcCos => MathS.Arccos(argumentsList[0]),
                MetaExpressionFunctionNames.ArcSin => MathS.Arcsin(argumentsList[0]),
                MetaExpressionFunctionNames.ArcTan => MathS.Arctan(argumentsList[0]),
                MetaExpressionFunctionNames.Cosh => MathS.Hyperbolic.Cosh(argumentsList[0]),
                MetaExpressionFunctionNames.Sinh => MathS.Hyperbolic.Sinh(argumentsList[0]),
                MetaExpressionFunctionNames.Tanh => MathS.Hyperbolic.Tanh(argumentsList[0]),

                _ => throw new InvalidOperationException()
            };
        }
    }
}