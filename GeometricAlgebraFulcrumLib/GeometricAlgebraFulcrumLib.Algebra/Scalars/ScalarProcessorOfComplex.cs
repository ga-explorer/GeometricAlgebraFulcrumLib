using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Algebra.Scalars;

public sealed class ScalarProcessorOfComplex
    : INumericScalarProcessor<Complex>
{
    public static ScalarProcessorOfComplex Instance { get; }
        = new ScalarProcessorOfComplex();

    
    private double _zeroEpsilon = 1e-12;
    public double ZeroEpsilon
    {
        get => _zeroEpsilon;
        set
        {
            if (value.IsNaN() || value.Abs() > 1)
                throw new ArgumentException(nameof(value));

            _zeroEpsilon = value.Abs();
        }
    }

    public Complex ZeroValue
        => Complex.Zero;

    public Complex PositiveInfinityValue { get; }
        = new Complex(double.PositiveInfinity, double.PositiveInfinity);

    public Complex NegativeInfinityValue { get; }
        = new Complex(double.NegativeInfinity, double.NegativeInfinity);

    public Complex OneValue { get; }
        = Complex.One;

    public Complex MinusOneValue { get; }
        = -Complex.One;

    public Complex TwoValue { get; }
        = 2d;

    public Complex MinusTwoValue { get; }
        = -2d;

    public Complex TenValue { get; }
        = 10d;

    public Complex MinusTenValue { get; }
        = -10d;

    public Complex PiValue { get; }
        = Math.PI;

    public Complex PiTimes2Value { get; }
        = 2d * Math.PI;
    
    public Complex PiTimes4Value { get; }
        = 4d * Math.PI;

    public Complex PiOver2Value { get; }
        = 0.5d * Math.PI;

    public Complex EValue { get; }
        = Math.E;

    public Complex DegreeToRadianFactorValue { get; }
        = Math.PI / 180d;

    public Complex RadianToDegreeFactorValue { get; }
        = 180d / Math.PI;

    public bool IsNumeric
        => true;

    public bool IsSymbolic
        => false;

    public Scalar<Complex> Zero { get; }

    public Scalar<Complex> PositiveInfinity { get; }

    public Scalar<Complex> NegativeInfinity { get; }

    public Scalar<Complex> One { get; }
    
    public Scalar<Complex> MinusOne { get; }
    
    public Scalar<Complex> Two { get; }
    
    public Scalar<Complex> MinusTwo { get; }
    
    public Scalar<Complex> Ten { get; }
    
    public Scalar<Complex> MinusTen { get; }
    
    public Scalar<Complex> Pi { get; }
    
    public Scalar<Complex> PiTimes2 { get; }
    
    public Scalar<Complex> PiTimes4 { get; }

    public Scalar<Complex> PiOver2 { get; }
    
    public Scalar<Complex> E { get; }
    
    public Scalar<Complex> DegreeToRadianFactor { get; }

    public Scalar<Complex> RadianToDegreeFactor { get; }


    private ScalarProcessorOfComplex()
    {
        Zero = this.ScalarFromValue(ZeroValue);
        One = this.ScalarFromValue(OneValue);
        MinusOne = this.ScalarFromValue(MinusOneValue);
        Two = this.ScalarFromValue(TwoValue);
        MinusTwo = this.ScalarFromValue(MinusTwoValue);
        Ten = this.ScalarFromValue(TenValue);
        MinusTen = this.ScalarFromValue(MinusTenValue);
        Pi = this.ScalarFromValue(PiValue);
        E = this.ScalarFromValue(EValue);
        PiTimes2 = this.ScalarFromValue(PiTimes2Value);
        PiTimes4 = this.ScalarFromValue(PiTimes4Value);
        PiOver2 = this.ScalarFromValue(PiOver2Value);
        DegreeToRadianFactor = this.ScalarFromValue(DegreeToRadianFactorValue);
        RadianToDegreeFactor = this.ScalarFromValue(RadianToDegreeFactorValue);
        PositiveInfinity = this.ScalarFromValue(PositiveInfinityValue);
        NegativeInfinity = this.ScalarFromValue(NegativeInfinityValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Complex> Add(Complex scalar1, Complex scalar2)
    {
        return this.ScalarFromValue(scalar1 + scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Complex> Subtract(Complex scalar1, Complex scalar2)
    {
        return this.ScalarFromValue(scalar1 - scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Complex> Times(Complex scalar1, Complex scalar2)
    {
        return this.ScalarFromValue(scalar1 * scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Complex> Divide(Complex scalar1, Complex scalar2)
    {
        return this.ScalarFromValue(scalar1 / scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Complex> Positive(Complex scalar)
    {
        return this.ScalarFromValue(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Complex> Negative(Complex scalar)
    {
        return this.ScalarFromValue(-scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Complex> Inverse(Complex scalar)
    {
        return this.ScalarFromValue(1d / scalar);
    }

    public Scalar<Complex> Sign(Complex scalar)
    {
        throw new NotImplementedException();
    }

    public Scalar<Complex> UnitStep(Complex scalar)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Complex> Abs(Complex scalar)
    {
        return this.ScalarFromValue(Complex.Abs(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Complex> Power(Complex baseScalar, Complex scalar)
    {
        return this.ScalarFromValue(Complex.Pow(baseScalar, scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Complex> Sqrt(Complex scalar)
    {
        return this.ScalarFromValue(Complex.Sqrt(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Complex> SqrtOfAbs(Complex scalar)
    {
        return this.ScalarFromValue(Math.Sqrt(Complex.Abs(scalar)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Complex> Exp(Complex scalar)
    {
        return this.ScalarFromValue(Complex.Exp(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Complex> LogE(Complex scalar)
    {
        return this.ScalarFromValue(Complex.Log(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Complex> Log2(Complex scalar)
    {
        return this.ScalarFromValue(Complex.Log(scalar, 2d));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Complex> Log10(Complex scalar)
    {
        return this.ScalarFromValue(Complex.Log10(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Complex> Log(Complex baseScalar, Complex scalar)
    {
        return this.ScalarFromValue(Complex.Log(scalar, baseScalar.Real));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Complex> Cos(Complex scalar)
    {
        return this.ScalarFromValue(Complex.Cos(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Complex> Sin(Complex scalar)
    {
        return this.ScalarFromValue(Complex.Sin(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Complex> Tan(Complex scalar)
    {
        return this.ScalarFromValue(Complex.Tan(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Complex> Cosh(Complex scalar)
    {
        return this.ScalarFromValue(Complex.Cosh(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Complex> Sinh(Complex scalar)
    {
        return this.ScalarFromValue(Complex.Sinh(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Complex> Tanh(Complex scalar)
    {
        return this.ScalarFromValue(Complex.Tanh(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid(Complex scalar)
    {
        return !scalar.IsNaN();
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsFiniteNumber(Complex scalar)
    //{
    //    return double.IsFinite(scalar.Real) &&
    //           double.IsFinite(scalar.Imaginary);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsZero(Complex scalar)
    //{
    //    return scalar is { Real: 0d, Imaginary: 0d };
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsZero(Complex scalar, bool nearZeroFlag)
    //{
    //    return nearZeroFlag
    //        ? scalar.Magnitude < ZeroEpsilon
    //        : scalar is { Real: 0d, Imaginary: 0d };
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNearZero(Complex scalar)
    //{
    //    return scalar.Magnitude < ZeroEpsilon;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotZero(Complex scalar)
    //{
    //    return scalar.Real != 0d || scalar.Imaginary != 0d;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotZero(Complex scalar, bool nearZeroFlag)
    //{
    //    return nearZeroFlag
    //        ? scalar.Magnitude >= ZeroEpsilon
    //        : scalar.Real != 0d || scalar.Imaginary != 0d;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearZero(Complex scalar)
    //{
    //    return scalar.Magnitude >= ZeroEpsilon;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsPositive(Complex scalar)
    //{
    //    return scalar.Real > 0;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNegative(Complex scalar)
    //{
    //    return scalar.Real < 0;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotPositive(Complex scalar)
    //{
    //    return scalar.Real <= 0;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNegative(Complex scalar)
    //{
    //    return scalar.Real >= 0;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearPositive(Complex scalar)
    //{
    //    return scalar.Real >= ZeroEpsilon;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearNegative(Complex scalar)
    //{
    //    return scalar.Real <= -ZeroEpsilon;
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Complex> ScalarFromText(string text)
    {
        return Complex.Parse(text, NumberStyles.Any, null).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Complex> ScalarFromNumber(int value)
    {
        return this.ScalarFromValue(new Complex(value, 0));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Complex> ScalarFromNumber(uint value)
    {
        return this.ScalarFromValue(new Complex(value, 0));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Complex> ScalarFromNumber(long value)
    {
        return this.ScalarFromValue(new Complex(value, 0));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Complex> ScalarFromNumber(ulong value)
    {
        return this.ScalarFromValue(new Complex(value, 0));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Complex> ScalarFromNumber(float value)
    {
        return this.ScalarFromValue(new Complex(value, 0));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Complex> ScalarFromNumber(double value)
    {
        return this.ScalarFromValue(new Complex(value, 0d));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Complex> ScalarFromRational(long numerator, long denominator)
    {
        return this.ScalarFromValue(new Complex(numerator / (double)denominator, 0d));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Complex> ScalarFromRandom(Random randomGenerator, double minValue, double maxValue)
    {
        var magnitude =
            minValue + (maxValue - minValue) * randomGenerator.NextDouble();

        var angle =
            2d * Math.PI * randomGenerator.NextDouble();

        return this.ScalarFromValue(Complex.FromPolarCoordinates(magnitude, angle));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double ToFloat64(Complex scalar)
    {
        return scalar.Imaginary.IsNearZero(ZeroEpsilon)
            ? scalar.Real 
            : double.NaN;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToText(Complex scalar)
    {
        return scalar.ToString("G");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Complex> VectorToRadians(Complex scalarX, Complex scalarY)
    {
        var value = Complex.Atan(scalarY / scalarX);

        return this.ScalarFromValue(value);
    }
}