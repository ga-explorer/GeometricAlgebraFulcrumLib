using System.Runtime.CompilerServices;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.Numbers;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Context;

namespace GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra
{
    /// <summary>
    /// This processor performs basic operations on symbolic expressions
    /// of all kinds. This processor only constructs new expressions without
    /// adding or querying data of the associated Context object
    /// </summary>
    public class ScalarAlgebraSymbolicExpressionProcessor :
        IScalarAlgebraProcessor<ISymbolicExpression>
    {
        public SymbolicContext Context { get; }

        public bool IsNumeric 
            => false;

        public bool IsSymbolic 
            => true;

        public ISymbolicExpression ScalarZero
            => Context.ScalarZero;

        public ISymbolicExpression ScalarOne
            => Context.ScalarOne;

        public ISymbolicExpression ScalarMinusOne
            => Context.ScalarMinusOne;

        public ISymbolicExpression ScalarTwo
            => Context.ScalarTwo;

        public ISymbolicExpression ScalarMinusTwo
            => Context.ScalarMinusOne;

        public ISymbolicExpression ScalarTen
            => Context.ScalarTen;

        public ISymbolicExpression ScalarMinusTen
            => Context.ScalarMinusTen;

        public ISymbolicExpression ScalarPi
            => Context.ScalarPi;

        public ISymbolicExpression ScalarPiOver2 
            => Context.ScalarPiOver2;

        public ISymbolicExpression ScalarE
            => Context.ScalarE;


        internal ScalarAlgebraSymbolicExpressionProcessor([NotNull] SymbolicContext context)
        {
            Context = context;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression Add(ISymbolicExpression scalar1, ISymbolicExpression scalar2)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Plus
                .CreateFunction(scalar1, scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression Subtract(ISymbolicExpression scalar1, ISymbolicExpression scalar2)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Subtract
                .CreateFunction(scalar1, scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression Times(ISymbolicExpression scalar1, ISymbolicExpression scalar2)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Times
                .CreateFunction(scalar1, scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression NegativeTimes(ISymbolicExpression scalar1, ISymbolicExpression scalar2)
        {
            return Negative(Times(scalar1, scalar2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression Divide(ISymbolicExpression scalar1, ISymbolicExpression scalar2)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Divide
                .CreateFunction(scalar1, scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression NegativeDivide(ISymbolicExpression scalar1, ISymbolicExpression scalar2)
        {
            return Negative(Divide(scalar1, scalar2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression Positive(ISymbolicExpression scalar)
        {
            return scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression Negative(ISymbolicExpression scalar)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Negative
                .CreateFunction(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression Inverse(ISymbolicExpression scalar)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Inverse
                .CreateFunction(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression Sign(ISymbolicExpression scalar)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Sign
                .CreateFunction(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression UnitStep(ISymbolicExpression scalar)
        {
            return Context
                .FunctionHeadSpecsFactory
                .UnitStep
                .CreateFunction(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression Abs(ISymbolicExpression scalar)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Abs
                .CreateFunction(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression Sqrt(ISymbolicExpression scalar)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Sqrt
                .CreateFunction(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression SqrtOfAbs(ISymbolicExpression scalar)
        {
            return Sqrt(Abs(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression Exp(ISymbolicExpression scalar)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Exp
                .CreateFunction(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression LogE(ISymbolicExpression scalar)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Log
                .CreateFunction(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression Log2(ISymbolicExpression scalar)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Log2
                .CreateFunction(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression Log10(ISymbolicExpression scalar)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Log10
                .CreateFunction(scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression Power(ISymbolicExpression baseScalar, ISymbolicExpression scalar)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Power
                .CreateFunction(baseScalar, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression Log(ISymbolicExpression baseScalar, ISymbolicExpression scalar)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Log
                .CreateFunction(baseScalar, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression Cos(ISymbolicExpression scalar)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Cos
                .CreateFunction(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression Sin(ISymbolicExpression scalar)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Sin
                .CreateFunction(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression Tan(ISymbolicExpression scalar)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Tan
                .CreateFunction(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression ArcCos(ISymbolicExpression scalar)
        {
            return Context
                .FunctionHeadSpecsFactory
                .ArcCos
                .CreateFunction(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression ArcSin(ISymbolicExpression scalar)
        {
            return Context
                .FunctionHeadSpecsFactory
                .ArcSin
                .CreateFunction(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression ArcTan(ISymbolicExpression scalar)
        {
            return Context
                .FunctionHeadSpecsFactory
                .ArcTan
                .CreateFunction(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression ArcTan2(ISymbolicExpression scalarX, ISymbolicExpression scalarY)
        {
            return Context
                .FunctionHeadSpecsFactory
                .ArcTan2
                .CreateFunction(scalarX, scalarY);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression Cosh(ISymbolicExpression scalar)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Cosh
                .CreateFunction(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression Sinh(ISymbolicExpression scalar)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Sinh
                .CreateFunction(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression Tanh(ISymbolicExpression scalar)
        {
            return Context
                .FunctionHeadSpecsFactory
                .Tanh
                .CreateFunction(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid(ISymbolicExpression scalar)
        {
            return scalar is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(ISymbolicExpression scalar)
        {
            return scalar is ISymbolicNumber {IsZero: true};
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(ISymbolicExpression scalar, bool nearZeroFlag)
        {
            if (scalar is not ISymbolicNumber number)
                return false;

            return nearZeroFlag 
                ? number.IsNearZero 
                : number.IsZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero(ISymbolicExpression scalar)
        {
            return scalar is ISymbolicNumber {IsNearZero: true};
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotZero(ISymbolicExpression scalar)
        {
            return scalar is ISymbolicNumber {IsZero: false};
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotZero(ISymbolicExpression scalar, bool nearZeroFlag)
        {
            if (scalar is not ISymbolicNumber number)
                return false;

            return nearZeroFlag 
                ? !number.IsNearZero 
                : !number.IsZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearZero(ISymbolicExpression scalar)
        {
            if (scalar is not ISymbolicNumber number)
                return false;

            return !number.IsNearZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsPositive(ISymbolicExpression scalar)
        {
            return scalar is ISymbolicNumber {IsPositive: true};
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNegative(ISymbolicExpression scalar)
        {
            return scalar is ISymbolicNumber {IsNegative: true};
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotPositive(ISymbolicExpression scalar)
        {
            return scalar is ISymbolicNumber {IsPositive: false};
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNegative(ISymbolicExpression scalar)
        {
            return scalar is ISymbolicNumber {IsNegative: false};
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearPositive(ISymbolicExpression scalar)
        {
            return scalar is ISymbolicNumber {IsNotNearPositive: true};
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearNegative(ISymbolicExpression scalar)
        {
            return scalar is ISymbolicNumber {IsNotNearNegative: true};
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression GetScalarFromText(string text)
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression GetScalarFromNumber(int value)
        {
            return SymbolicNumber.Create(Context, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression GetScalarFromNumber(uint value)
        {
            return SymbolicNumber.Create(Context, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression GetScalarFromNumber(long value)
        {
            return SymbolicNumber.Create(Context, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression GetScalarFromNumber(ulong value)
        {
            return SymbolicNumber.Create(Context, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression GetScalarFromNumber(float value)
        {
            return SymbolicNumber.Create(Context, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression GetScalarFromNumber(double value)
        {
            return SymbolicNumber.Create(Context, value);
        }

        public ISymbolicExpression GetScalarFromRational(long numerator, long denominator)
        {
            return SymbolicNumber.CreateRational(Context, numerator, denominator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpression GetScalarFromRandom(System.Random randomGenerator, double minValue, double maxValue)
        {
            var value = minValue + (maxValue - minValue) * randomGenerator.NextDouble();

            return GetScalarFromNumber(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToText(ISymbolicExpression scalar)
        {
            return scalar.ToString();
        }
    }
}