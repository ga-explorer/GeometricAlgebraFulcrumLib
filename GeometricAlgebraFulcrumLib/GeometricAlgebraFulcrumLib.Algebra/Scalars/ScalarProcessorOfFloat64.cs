using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.Scalars;

public class ScalarProcessorOfFloat64
    : INumericScalarProcessor<double>
{
    public static ScalarProcessorOfFloat64 Instance { get; }
        = new ScalarProcessorOfFloat64();

    
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

    public bool IsNumeric
        => true;

    public bool IsSymbolic
        => false;

    public Scalar<double> Zero { get; }

    public Scalar<double> PositiveInfinity { get; }
    
    public Scalar<double> NegativeInfinity { get; }

    public Scalar<double> One { get; }
    
    public Scalar<double> MinusOne { get; }
    
    public Scalar<double> Two { get; }
    
    public Scalar<double> MinusTwo { get; }
    
    public Scalar<double> Ten { get; }
    
    public Scalar<double> MinusTen { get; }
    
    public Scalar<double> Pi { get; }
    
    public Scalar<double> PiTimes2 { get; }
    
    public Scalar<double> PiTimes4 { get; }

    public Scalar<double> PiOver2 { get; }
    
    public Scalar<double> E { get; }
    
    public Scalar<double> DegreeToRadianFactor { get; }
    
    public Scalar<double> RadianToDegreeFactor { get; }

    public double ZeroValue
        => 0d;

    public double PositiveInfinityValue 
        => double.PositiveInfinity;

    public double NegativeInfinityValue 
        => double.NegativeInfinity;

    public double OneValue
        => 1d;

    public double MinusOneValue
        => -1d;

    public double TwoValue
        => 2d;

    public double MinusTwoValue
        => -2d;

    public double TenValue
        => 10d;

    public double MinusTenValue
        => -10d;

    public double PiValue
        => Math.PI;

    public double PiTimes2Value
        => 2d * Math.PI;
    
    public double PiTimes4Value
        => 4d * Math.PI;

    public double PiOver2Value
        => 0.5d * Math.PI;

    public double EValue
        => Math.E;

    public double DegreeToRadianFactorValue
        => Math.PI / 180;

    public double RadianToDegreeFactorValue
        => 180 / Math.PI;


    protected ScalarProcessorOfFloat64()
    {
        Zero = ScalarFromNumber(ZeroValue);
        One = ScalarFromNumber(OneValue);
        MinusOne = ScalarFromNumber(MinusOneValue);
        Two = ScalarFromNumber(TwoValue);
        MinusTwo = ScalarFromNumber(MinusTwoValue);
        Ten = ScalarFromNumber(TenValue);
        MinusTen = ScalarFromNumber(MinusTenValue);
        Pi = ScalarFromNumber(PiValue);
        E = ScalarFromNumber(EValue);
        PiTimes2 = ScalarFromNumber(PiTimes2Value);
        PiTimes4 = ScalarFromNumber(PiTimes4Value);
        PiOver2 = ScalarFromNumber(PiOver2Value);
        DegreeToRadianFactor = ScalarFromNumber(DegreeToRadianFactorValue);
        RadianToDegreeFactor = ScalarFromNumber(RadianToDegreeFactorValue);
        PositiveInfinity = ScalarFromNumber(PositiveInfinityValue);
        NegativeInfinity = ScalarFromNumber(NegativeInfinityValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<double> Add(double scalar1, double scalar2)
    {
        return this.ScalarFromValue(scalar1 + scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<double> Subtract(double scalar1, double scalar2)
    {
        return this.ScalarFromValue(scalar1 - scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<double> Times(double scalar1, double scalar2)
    {
        return this.ScalarFromValue(scalar1 * scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<double> Divide(double scalar1, double scalar2)
    {
        return this.ScalarFromValue(scalar1 / scalar2);

        ////TODO: Is this acceptable?
        //var scalar = scalar1 / scalar2;

        //return double.IsNaN(scalar) ? Zero : this.ScalarFromValue(scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<double> Remainder(double scalar1, double scalar2)
    {
        var remainder = double.Ieee754Remainder(scalar1, scalar2);
        //Math.IEEERemainder := dividend - (divisor * Math.Round(dividend / divisor))
        //
        //Remainder operator (%) := (Math.Abs(dividend) - (Math.Abs(divisor) *
        // (Math.Floor(Math.Abs(dividend) / Math.Abs(divisor))))) *
        // Math.Sign(dividend)

        //var ratio = scalar1 / scalar2;
        //var fractionalPart = ratio - Math.Truncate(ratio);
        //var remainder = fractionalPart * scalar2;
        
        return this.ScalarFromValue(remainder);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<double> Positive(double scalar)
    {
        return this.ScalarFromValue(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<double> Negative(double scalar)
    {
        return this.ScalarFromValue(-scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<double> Inverse(double scalar)
    {
        return this.ScalarFromValue(1d / scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<double> Sign(double scalar)
    {
        return this.ScalarFromValue(Math.Sign(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<double> UnitStep(double scalar)
    {
        return scalar < 0d ? Zero : One;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<double> IntegerPart(double scalar)
    {
        return this.ScalarFromValue(Math.Truncate(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<double> FractionalPart(double scalar)
    {
        return this.ScalarFromValue(scalar - Math.Truncate(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<double> Abs(double scalar)
    {
        return this.ScalarFromValue(Math.Abs(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<double> Sqrt(double scalar)
    {
        return this.ScalarFromValue(Math.Sqrt(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<double> SqrtOfAbs(double scalar)
    {
        return this.ScalarFromValue( Math.Sqrt(Math.Abs(scalar)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<double> Exp(double scalar)
    {
        return this.ScalarFromValue(Math.Exp(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<double> LogE(double scalar)
    {
        return this.ScalarFromValue(Math.Log(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<double> Log2(double scalar)
    {
        return this.ScalarFromValue(Math.Log2(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<double> Log10(double scalar)
    {
        return this.ScalarFromValue(Math.Log10(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<double> Power(double baseScalar, double scalar)
    {
        return this.ScalarFromValue(Math.Pow(baseScalar, scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<double> Log(double baseScalar, double scalar)
    {
        return this.ScalarFromValue(Math.Log(scalar, baseScalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<double> Cos(double scalar)
    {
        return this.ScalarFromValue(Math.Cos(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<double> Sin(double scalar)
    {
        return this.ScalarFromValue(Math.Sin(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<double> Tan(double scalar)
    {
        return this.ScalarFromValue(Math.Tan(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<double> Cosh(double scalar)
    {
        return this.ScalarFromValue( Math.Cosh(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<double> Sinh(double scalar)
    {
        return this.ScalarFromValue( Math.Sinh(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<double> Tanh(double scalar)
    {
        return this.ScalarFromValue(Math.Tanh(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid(double scalar)
    {
        return !double.IsNaN(scalar);
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsFiniteNumber(double scalar)
    //{
    //    return double.IsFinite(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsZero(double scalar)
    //{
    //    return scalar == 0d;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsZero(double scalar, bool nearZeroFlag)
    //{
    //    return nearZeroFlag
    //        ? scalar > -ZeroEpsilon && scalar < ZeroEpsilon
    //        : scalar == 0d;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNearZero(double scalar)
    //{
    //    return scalar > -ZeroEpsilon && scalar < ZeroEpsilon;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotZero(double scalar)
    //{
    //    return scalar != 0d;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotZero(double scalar, bool nearZeroFlag)
    //{
    //    return nearZeroFlag
    //        ? scalar <= -ZeroEpsilon || scalar >= ZeroEpsilon
    //        : scalar != 0d;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearZero(double scalar)
    //{
    //    return scalar <= -ZeroEpsilon || scalar >= ZeroEpsilon;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsPositive(double scalar)
    //{
    //    return scalar > 0;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNegative(double scalar)
    //{
    //    return scalar < 0;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotPositive(double scalar)
    //{
    //    return scalar <= 0;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNegative(double scalar)
    //{
    //    return scalar >= 0;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearPositive(double scalar)
    //{
    //    return scalar < -ZeroEpsilon;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearNegative(double scalar)
    //{
    //    return scalar > ZeroEpsilon;
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<double> ScalarFromText(string text)
    {
        return this.ScalarFromValue(
            double.TryParse(text, out var value) ? value : double.NaN
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<double> ScalarFromNumber(int value)
    {
        return this.ScalarFromValue(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<double> ScalarFromNumber(uint value)
    {
        return this.ScalarFromValue(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<double> ScalarFromNumber(long value)
    {
        return this.ScalarFromValue(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<double> ScalarFromNumber(ulong value)
    {
        return this.ScalarFromValue(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<double> ScalarFromNumber(float value)
    {
        return this.ScalarFromValue(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<double> ScalarFromNumber(double value)
    {
        return this.ScalarFromValue(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<double> ScalarFromRational(long numerator, long denominator)
    {
        return this.ScalarFromValue(numerator / (double)denominator);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<double> ScalarFromRandom(Random randomGenerator, double minValue, double maxValue)
    {
        return this.ScalarFromValue(minValue + (maxValue - minValue) * randomGenerator.NextDouble());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double ToFloat64(double scalar)
    {
        return scalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToText(double scalar)
    {
        return scalar.ToString("G");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<double> VectorToRadians(double scalarX, double scalarY)
    {
        var value = Math.Atan2(scalarY, scalarX);

        if (value < 0) value += 2 * Math.PI;

        return value.ScalarFromValue(this);
    }


}