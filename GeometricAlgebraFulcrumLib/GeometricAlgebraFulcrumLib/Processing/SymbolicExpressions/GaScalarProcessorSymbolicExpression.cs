using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Numbers;

namespace GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions
{
    /// <summary>
    /// This processor performs basic operations on symbolic expressions
    /// of all kinds. This processor only constructs new expressions without
    /// adding or querying data of the associated Context object
    /// </summary>
    public class GaScalarProcessorSymbolicExpression :
        IGaScalarProcessor<ISymbolicExpression>
    {
        public SymbolicContext Context { get; }

        public bool IsNumeric => false;

        public bool IsSymbolic => true;

        public ISymbolicExpression ZeroScalar { get; }
        
        public ISymbolicExpression OneScalar { get; }
        
        public ISymbolicExpression MinusOneScalar { get; }
        
        public ISymbolicExpression PiScalar { get; }


        internal GaScalarProcessorSymbolicExpression(SymbolicContext context)
        {
            Context = context;
            ZeroScalar = context.ZeroScalar;
            OneScalar = context.OneScalar;
            MinusOneScalar = context.MinusOneScalar;
            PiScalar = context.PiScalar;
        }


        public ISymbolicExpression Add(ISymbolicExpression scalar1, ISymbolicExpression scalar2)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Plus
                .CreateFunction(scalar1, scalar2);
        }

        public ISymbolicExpression Add(params ISymbolicExpression[] scalarsList)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Plus
                .CreateFunction(scalarsList);
        }

        public ISymbolicExpression Add(IEnumerable<ISymbolicExpression> scalarsList)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Plus
                .CreateFunction(scalarsList);
        }

        public ISymbolicExpression Subtract(ISymbolicExpression scalar1, ISymbolicExpression scalar2)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Subtract
                .CreateFunction(scalar1, scalar2);
        }

        public ISymbolicExpression Times(ISymbolicExpression scalar1, ISymbolicExpression scalar2)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Times
                .CreateFunction(scalar1, scalar2);
        }

        public ISymbolicExpression Times(params ISymbolicExpression[] scalarsList)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Times
                .CreateFunction(scalarsList);
        }

        public ISymbolicExpression Times(IEnumerable<ISymbolicExpression> scalarsList)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Times
                .CreateFunction(scalarsList);
        }

        public ISymbolicExpression NegativeTimes(ISymbolicExpression scalar1, ISymbolicExpression scalar2)
        {
            return Negative(Times(scalar1, scalar2));
        }

        public ISymbolicExpression NegativeTimes(params ISymbolicExpression[] scalarsList)
        {
            return Negative(Times(scalarsList));
        }

        public ISymbolicExpression NegativeTimes(IEnumerable<ISymbolicExpression> scalarsList)
        {
            return Negative(Times(scalarsList));
        }

        public ISymbolicExpression Divide(ISymbolicExpression scalar1, ISymbolicExpression scalar2)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Divide
                .CreateFunction(scalar1, scalar2);
        }

        public ISymbolicExpression NegativeDivide(ISymbolicExpression scalar1, ISymbolicExpression scalar2)
        {
            return Negative(Divide(scalar1, scalar2));
        }

        public ISymbolicExpression Positive(ISymbolicExpression scalar)
        {
            return scalar;
        }

        public ISymbolicExpression Negative(ISymbolicExpression scalar)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Negative
                .CreateFunction(scalar);
        }

        public ISymbolicExpression Inverse(ISymbolicExpression scalar)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Inverse
                .CreateFunction(scalar);
        }

        public ISymbolicExpression Abs(ISymbolicExpression scalar)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Abs
                .CreateFunction(scalar);
        }

        public ISymbolicExpression Sqrt(ISymbolicExpression scalar)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Sqrt
                .CreateFunction(scalar);
        }

        public ISymbolicExpression SqrtOfAbs(ISymbolicExpression scalar)
        {
            return Sqrt(Abs(scalar));
        }

        public ISymbolicExpression Exp(ISymbolicExpression scalar)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Exp
                .CreateFunction(scalar);
        }

        public ISymbolicExpression Log(ISymbolicExpression scalar)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Log
                .CreateFunction(scalar);
        }

        public ISymbolicExpression Log2(ISymbolicExpression scalar)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Log2
                .CreateFunction(scalar);
        }

        public ISymbolicExpression Log10(ISymbolicExpression scalar)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Log10
                .CreateFunction(scalar);
        }

        public ISymbolicExpression Log(ISymbolicExpression scalar, ISymbolicExpression baseScalar)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Log
                .CreateFunction(scalar, baseScalar);
        }

        public ISymbolicExpression Cos(ISymbolicExpression scalar)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Cos
                .CreateFunction(scalar);
        }

        public ISymbolicExpression Sin(ISymbolicExpression scalar)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Sin
                .CreateFunction(scalar);
        }

        public ISymbolicExpression Tan(ISymbolicExpression scalar)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Tan
                .CreateFunction(scalar);
        }

        public ISymbolicExpression ArcCos(ISymbolicExpression scalar)
        {
            return Context
                .FunctionHeadSpecsFactory
                .ArcCos
                .CreateFunction(scalar);
        }

        public ISymbolicExpression ArcSin(ISymbolicExpression scalar)
        {
            return Context
                .FunctionHeadSpecsFactory
                .ArcSin
                .CreateFunction(scalar);
        }

        public ISymbolicExpression ArcTan(ISymbolicExpression scalar)
        {
            return Context
                .FunctionHeadSpecsFactory
                .ArcTan
                .CreateFunction(scalar);
        }

        public ISymbolicExpression ArcTan2(ISymbolicExpression scalarX, ISymbolicExpression scalarY)
        {
            return Context
                .FunctionHeadSpecsFactory
                .ArcTan2
                .CreateFunction(scalarX, scalarY);
        }

        public ISymbolicExpression Cosh(ISymbolicExpression scalar)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Cosh
                .CreateFunction(scalar);
        }

        public ISymbolicExpression Sinh(ISymbolicExpression scalar)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Sinh
                .CreateFunction(scalar);
        }

        public ISymbolicExpression Tanh(ISymbolicExpression scalar)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Tanh
                .CreateFunction(scalar);
        }

        public bool IsValid(ISymbolicExpression scalar)
        {
            return scalar is not null;
        }

        public bool IsZero(ISymbolicExpression scalar)
        {
            return scalar is ISymbolicNumber {IsZero: true};
        }

        public bool IsZero(ISymbolicExpression scalar, bool nearZeroFlag)
        {
            if (scalar is not ISymbolicNumber number)
                return false;

            return nearZeroFlag 
                ? number.IsNearZero 
                : number.IsZero;
        }

        public bool IsNearZero(ISymbolicExpression scalar)
        {
            return scalar is ISymbolicNumber {IsNearZero: true};
        }

        public bool IsPositive(ISymbolicExpression scalar)
        {
            return scalar is ISymbolicNumber {IsPositive: true};
        }

        public bool IsNegative(ISymbolicExpression scalar)
        {
            return scalar is ISymbolicNumber {IsNegative: true};
        }

        public bool IsNotNearPositive(ISymbolicExpression scalar)
        {
            return scalar is ISymbolicNumber {IsNotNearPositive: true};
        }

        public bool IsNotNearNegative(ISymbolicExpression scalar)
        {
            return scalar is ISymbolicNumber {IsNotNearNegative: true};
        }

        public ISymbolicExpression TextToScalar(string text)
        {
            throw new System.NotImplementedException();
        }

        public ISymbolicExpression IntegerToScalar(int value)
        {
            return SymbolicNumber.Create(
                Context, 
                value
            );
        }

        public ISymbolicExpression Float64ToScalar(double value)
        {
            return SymbolicNumber.Create(
                Context, 
                value
            );
        }

        public ISymbolicExpression GetRandomScalar(System.Random randomGenerator, double minValue, double maxValue)
        {
            var value = minValue + (maxValue - minValue) * randomGenerator.NextDouble();

            return Float64ToScalar(value);
        }

        public string ToText(ISymbolicExpression scalar)
        {
            return scalar.ToString();
        }
    }
}