using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using NumpyDotNet;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars
{
    public sealed class ScalarProcessorFloat64NdArray :
        INumericScalarProcessor<ndarray>
    {
        public static shape ScalarShape { get; } = new shape(1);


        public double ZeroEpsilon { get; set; }
            = 1e-13d;

        public bool IsNumeric
            => true;

        public bool IsSymbolic
            => false;

        public ndarray ScalarZero { get; }
            = np.zeros(ScalarShape, np.Float64);

        public ndarray ScalarOne { get; }
            = np.ones(ScalarShape, np.Float64);

        public ndarray ScalarMinusOne { get; }
            = np.full(ScalarShape, -1d, np.Float64);

        public ndarray ScalarTwo { get; }
            = np.full(ScalarShape, 2d, np.Float64);

        public ndarray ScalarMinusTwo { get; }
            = np.full(ScalarShape, -2d, np.Float64);

        public ndarray ScalarTen { get; }
            = np.full(ScalarShape, 10d, np.Float64);

        public ndarray ScalarMinusTen { get; }
            = np.full(ScalarShape, -10d, np.Float64);

        public ndarray ScalarPi { get; }
            = np.full(ScalarShape, Math.PI, np.Float64);

        public ndarray ScalarTwoPi { get; }
            = np.full(ScalarShape, Math.PI * 2, np.Float64);

        public ndarray ScalarPiOver2 { get; }
            = np.full(ScalarShape, Math.PI / 2, np.Float64);

        public ndarray ScalarE { get; }
            = np.full(ScalarShape, Math.E, np.Float64);

        public ndarray ScalarDegreeToRadian { get; }
            = np.full(ScalarShape, Math.PI / 180, np.Float64);

        public ndarray ScalarRadianToDegree { get; }
            = np.full(ScalarShape, 180 / Math.PI, np.Float64);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray GetScalarFromNumber(int value)
        {
            return np.full(ScalarShape, (double)value, np.Float64);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray GetScalarFromNumber(uint value)
        {
            return np.full(ScalarShape, (double)value, np.Float64);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray GetScalarFromNumber(long value)
        {
            return np.full(ScalarShape, (double)value, np.Float64);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray GetScalarFromNumber(ulong value)
        {
            return np.full(ScalarShape, (double)value, np.Float64);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray GetScalarFromNumber(float value)
        {
            return np.full(ScalarShape, (double)value, np.Float64);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray GetScalarFromNumber(double value)
        {
            return np.full(ScalarShape, value, np.Float64);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray GetScalarFromRational(long numerator, long denominator)
        {
            return np.full(ScalarShape, (double)numerator / denominator, np.Float64);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray GetScalarFromRandom(System.Random randomGenerator, double minValue, double maxValue)
        {
            var scalar =
                minValue + (maxValue - minValue) * randomGenerator.NextDouble();

            return np.full(ScalarShape, scalar, np.Float64);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray GetScalarFromText(string text)
        {
            var value = double.Parse(text);

            return np.full(ScalarShape, value, np.Float64);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray Add(ndarray scalar1, ndarray scalar2)
        {
            return np.add(scalar1, scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray Subtract(ndarray scalar1, ndarray scalar2)
        {
            return np.subtract(scalar1, scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray Times(ndarray scalar1, ndarray scalar2)
        {
            return np.multiply(scalar1, scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray Times(IntegerSign sign, ndarray scalar)
        {
            if (sign.IsZero) return ScalarZero;

            return sign.IsPositive
                ? scalar
                : Negative(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray NegativeTimes(ndarray scalar1, ndarray scalar2)
        {
            return np.negative(np.multiply(scalar1, scalar2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray Divide(ndarray scalar1, ndarray scalar2)
        {
            return np.divide(scalar1, scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray NegativeDivide(ndarray scalar1, ndarray scalar2)
        {
            return np.negative(np.divide(scalar1, scalar2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray Positive(ndarray scalar)
        {
            return scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray Negative(ndarray scalar)
        {
            return np.negative(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray Inverse(ndarray scalar)
        {
            return np.reciprocal(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray Sign(ndarray scalar)
        {
            return np.sign(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray UnitStep(ndarray scalar)
        {
            return np.heaviside(scalar, ScalarOne);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray Abs(ndarray scalar)
        {
            return np.absolute(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray Power(ndarray baseScalar, ndarray scalar)
        {
            return np.power(baseScalar, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray Sqrt(ndarray scalar)
        {
            return np.sqrt(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray SqrtOfAbs(ndarray scalar)
        {
            return np.absolute(np.sqrt(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray Exp(ndarray scalar)
        {
            return np.exp(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray LogE(ndarray scalar)
        {
            return np.log(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray Log2(ndarray scalar)
        {
            return np.log2(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray Log10(ndarray scalar)
        {
            return np.log10(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray Log(ndarray baseScalar, ndarray scalar)
        {
            return np.divide(np.log2(scalar), np.log2(baseScalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray Cos(ndarray scalar)
        {
            return np.cos(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray Sin(ndarray scalar)
        {
            return np.sin(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray Tan(ndarray scalar)
        {
            return np.tan(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray ArcCos(ndarray scalar)
        {
            return np.arccos(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray ArcSin(ndarray scalar)
        {
            return np.arcsin(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray ArcTan(ndarray scalar)
        {
            return np.arctan(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray ArcTan2(ndarray scalarX, ndarray scalarY)
        {
            return np.arctan2(scalarY, scalarX);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray Cosh(ndarray scalar)
        {
            return np.cosh(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray Sinh(ndarray scalar)
        {
            return np.sinh(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray Tanh(ndarray scalar)
        {
            return np.tanh(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ndarray Sinc(ndarray scalar)
        {
            return np.sinc(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid(ndarray scalar)
        {
            return scalar is not null && scalar.Dtype == np.Float64;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsFiniteNumber(ndarray scalar)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(ndarray scalar)
        {
            return np.array_equiv(scalar, ScalarZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(ndarray scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? IsNearZero(scalar)
                : IsZero(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero(ndarray scalar)
        {
            return np.allclose(scalar, ScalarZero, 0d, ZeroEpsilon, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotZero(ndarray scalar)
        {
            return !IsZero(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotZero(ndarray scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? !IsNearZero(scalar)
                : !IsZero(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearZero(ndarray scalar)
        {
            return !IsNearZero(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsPositive(ndarray scalar)
        {
            return np.greater(scalar, ScalarZero).AsBoolArray().All(b => b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNegative(ndarray scalar)
        {
            return np.less(scalar, ScalarZero).AsBoolArray().All(b => b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotPositive(ndarray scalar)
        {
            return np.less_equal(scalar, ScalarZero).AsBoolArray().All(b => b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNegative(ndarray scalar)
        {
            return np.greater_equal(scalar, ScalarZero).AsBoolArray().All(b => b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearPositive(ndarray scalar)
        {
            return np.less(scalar, -ZeroEpsilon).AsBoolArray().All(b => b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearNegative(ndarray scalar)
        {
            return np.greater(scalar, ZeroEpsilon).AsBoolArray().All(b => b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToText(ndarray scalar)
        {
            throw new NotImplementedException();
        }
    }
}