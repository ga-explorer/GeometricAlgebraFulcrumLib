using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;
using NumpyDotNet;

namespace GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

public sealed class ScalarProcessorOfFloat64NdArray :
    INumericScalarProcessor<ndarray>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarProcessorOfFloat64NdArray Create(int dim1Size)
    {
        return new ScalarProcessorOfFloat64NdArray(
            new shape(dim1Size)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarProcessorOfFloat64NdArray Create(int dim1Size, int dim2Size)
    {
        return new ScalarProcessorOfFloat64NdArray(
            new shape(dim1Size, dim2Size)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarProcessorOfFloat64NdArray Create(int dim1Size, int dim2Size, int dim3Size)
    {
        return new ScalarProcessorOfFloat64NdArray(
            new shape(dim1Size, dim2Size, dim3Size)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarProcessorOfFloat64NdArray Create(int dim1Size, int dim2Size, int dim3Size, int dim4Size)
    {
        return new ScalarProcessorOfFloat64NdArray(
            new shape(dim1Size, dim2Size, dim3Size, dim4Size)
        );
    }

    
    public shape ScalarShape { get; }
    
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

    public Scalar<ndarray> Zero { get; }
    
    public Scalar<ndarray> PositiveInfinity { get; }
    
    public Scalar<ndarray> NegativeInfinity { get; }

    public Scalar<ndarray> One { get; }

    public Scalar<ndarray> MinusOne { get; }
    
    public Scalar<ndarray> Two { get; }
    
    public Scalar<ndarray> MinusTwo { get; }
    
    public Scalar<ndarray> Ten { get; }
    
    public Scalar<ndarray> MinusTen { get; }
    
    public Scalar<ndarray> Pi { get; }
    
    public Scalar<ndarray> PiTimes2 { get; }
    
    public Scalar<ndarray> PiTimes4 { get; }

    public Scalar<ndarray> PiOver2 { get; }
    
    public Scalar<ndarray> E { get; }
    
    public Scalar<ndarray> DegreeToRadianFactor { get; }
    
    public Scalar<ndarray> RadianToDegreeFactor { get; }

    public ndarray ZeroValue { get; }

    public ndarray PositiveInfinityValue { get; }

    public ndarray NegativeInfinityValue { get; }

    public ndarray OneValue { get; }

    public ndarray MinusOneValue { get; }

    public ndarray TwoValue { get; }

    public ndarray MinusTwoValue { get; }

    public ndarray TenValue { get; }

    public ndarray MinusTenValue { get; }

    public ndarray PiValue { get; }

    public ndarray PiTimes2Value { get; }
    
    public ndarray PiTimes4Value { get; }

    public ndarray PiOver2Value { get; }

    public ndarray EValue { get; }

    public ndarray DegreeToRadianFactorValue { get; }

    public ndarray RadianToDegreeFactorValue { get; }


    private ScalarProcessorOfFloat64NdArray(shape scalarShape)
    {
        ScalarShape = scalarShape;

        ZeroValue = np.zeros(ScalarShape, np.Float64);
        OneValue = np.ones(ScalarShape, np.Float64);
        TwoValue = np.full(ScalarShape, 2d, np.Float64);
        TenValue = np.full(ScalarShape, 10d, np.Float64);
        MinusOneValue = np.full(ScalarShape, -1d, np.Float64);
        MinusTenValue = np.full(ScalarShape, -10d, np.Float64);
        MinusTwoValue = np.full(ScalarShape, -2d, np.Float64);
        EValue = np.full(ScalarShape, Math.E, np.Float64);
        PiValue = np.full(ScalarShape, Math.PI, np.Float64);
        PiTimes2Value = np.full(ScalarShape, Math.Tau, np.Float64);
        PiTimes4Value = np.full(ScalarShape, Math.PI * 4, np.Float64);
        PiOver2Value = np.full(ScalarShape, Math.PI / 2, np.Float64);
        NegativeInfinityValue = np.full(ScalarShape, double.NegativeInfinity, np.Float64);
        PositiveInfinityValue = np.full(ScalarShape, double.PositiveInfinity, np.Float64);
        DegreeToRadianFactorValue = np.full(ScalarShape, Math.PI / 180, np.Float64);
        RadianToDegreeFactorValue = np.full(ScalarShape, 180 / Math.PI, np.Float64);
        
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
    public Scalar<ndarray> ScalarFromNumber(int value)
    {
        return this.ScalarFromValue(np.full(ScalarShape, (double)value, np.Float64));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ndarray> ScalarFromNumber(uint value)
    {
        return this.ScalarFromValue(np.full(ScalarShape, (double)value, np.Float64));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ndarray> ScalarFromNumber(long value)
    {
        return this.ScalarFromValue(np.full(ScalarShape, (double)value, np.Float64));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ndarray> ScalarFromNumber(ulong value)
    {
        return this.ScalarFromValue(np.full(ScalarShape, (double)value, np.Float64));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ndarray> ScalarFromNumber(float value)
    {
        return this.ScalarFromValue(np.full(ScalarShape, (double)value, np.Float64));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ndarray> ScalarFromNumber(double value)
    {
        return this.ScalarFromValue(np.full(ScalarShape, value, np.Float64));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ndarray> ScalarFromRational(long numerator, long denominator)
    {
        return this.ScalarFromValue(np.full(ScalarShape, (double)numerator / denominator, np.Float64));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ndarray> ScalarFromRandom(Random randomGenerator, double minValue, double maxValue)
    {
        var scalar =
            minValue + (maxValue - minValue) * randomGenerator.NextDouble();

        return this.ScalarFromValue(np.full(ScalarShape, scalar, np.Float64));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ndarray> ScalarFromText(string text)
    {
        var value = double.Parse(text);

        return this.ScalarFromValue(np.full(ScalarShape, value, np.Float64));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ndarray> Add(ndarray scalar1, ndarray scalar2)
    {
        return this.ScalarFromValue(np.add(scalar1, scalar2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ndarray> Subtract(ndarray scalar1, ndarray scalar2)
    {
        return this.ScalarFromValue(np.subtract(scalar1, scalar2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ndarray> Times(ndarray scalar1, ndarray scalar2)
    {
        return this.ScalarFromValue(np.multiply(scalar1, scalar2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ndarray> Divide(ndarray scalar1, ndarray scalar2)
    {
        return this.ScalarFromValue(np.divide(scalar1, scalar2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ndarray> VectorToRadians(ndarray scalarX, ndarray scalarY)
    {
        var value = np.arctan2(scalarY, scalarX);
        
        var radians = 
            np.add(
                value, 
                np.multiply(
                    np.less(value, ZeroValue), 
                    PiTimes2Value
                )
            );

        return this.ScalarFromValue(radians);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ndarray> Positive(ndarray scalar)
    {
        return this.ScalarFromValue(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ndarray> Negative(ndarray scalar)
    {
        return this.ScalarFromValue(np.negative(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ndarray> Inverse(ndarray scalar)
    {
        return this.ScalarFromValue(np.reciprocal(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ndarray> Sign(ndarray scalar)
    {
        return this.ScalarFromValue(np.sign(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ndarray> UnitStep(ndarray scalar)
    {
        return this.ScalarFromValue(np.heaviside(scalar, OneValue));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ndarray> Abs(ndarray scalar)
    {
        return this.ScalarFromValue(np.absolute(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ndarray> Power(ndarray baseScalar, ndarray scalar)
    {
        return this.ScalarFromValue(np.power(baseScalar, scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ndarray> Sqrt(ndarray scalar)
    {
        return this.ScalarFromValue(np.sqrt(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ndarray> SqrtOfAbs(ndarray scalar)
    {
        return this.ScalarFromValue(np.sqrt(np.absolute(scalar)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ndarray> Exp(ndarray scalar)
    {
        return this.ScalarFromValue(np.exp(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ndarray> LogE(ndarray scalar)
    {
        return this.ScalarFromValue(np.log(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ndarray> Log2(ndarray scalar)
    {
        return this.ScalarFromValue(np.log2(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ndarray> Log10(ndarray scalar)
    {
        return this.ScalarFromValue(np.log10(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ndarray> Log(ndarray baseScalar, ndarray scalar)
    {
        return this.ScalarFromValue(np.divide(np.log2(scalar), np.log2(baseScalar)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ndarray> Cos(ndarray scalar)
    {
        return this.ScalarFromValue(np.cos(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ndarray> Sin(ndarray scalar)
    {
        return this.ScalarFromValue(np.sin(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ndarray> Tan(ndarray scalar)
    {
        return this.ScalarFromValue(np.tan(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ndarray> Cosh(ndarray scalar)
    {
        return this.ScalarFromValue(np.cosh(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ndarray> Sinh(ndarray scalar)
    {
        return this.ScalarFromValue(np.sinh(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ndarray> Tanh(ndarray scalar)
    {
        return this.ScalarFromValue(np.tanh(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid(ndarray scalar)
    {
        return scalar.Dtype == np.Float64;
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsFiniteNumber(ndarray scalar)
    //{
    //    throw new NotImplementedException();
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsZero(ndarray scalar)
    //{
    //    return np.array_equiv(scalar, ZeroValue);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsZero(ndarray scalar, bool nearZeroFlag)
    //{
    //    return nearZeroFlag
    //        ? IsNearZero(scalar)
    //        : IsZero(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNearZero(ndarray scalar)
    //{
    //    return np.allclose(scalar, ZeroValue, 0d, ZeroEpsilon, false);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotZero(ndarray scalar)
    //{
    //    return !IsZero(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotZero(ndarray scalar, bool nearZeroFlag)
    //{
    //    return nearZeroFlag
    //        ? !IsNearZero(scalar)
    //        : !IsZero(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearZero(ndarray scalar)
    //{
    //    return !IsNearZero(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsPositive(ndarray scalar)
    //{
    //    return np.greater(scalar, ZeroValue).AsBoolArray().All(b => b);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNegative(ndarray scalar)
    //{
    //    return np.less(scalar, ZeroValue).AsBoolArray().All(b => b);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotPositive(ndarray scalar)
    //{
    //    return np.less_equal(scalar, ZeroValue).AsBoolArray().All(b => b);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNegative(ndarray scalar)
    //{
    //    return np.greater_equal(scalar, ZeroValue).AsBoolArray().All(b => b);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearPositive(ndarray scalar)
    //{
    //    return np.less(scalar, -ZeroEpsilon).AsBoolArray().All(b => b);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearNegative(ndarray scalar)
    //{
    //    return np.greater(scalar, ZeroEpsilon).AsBoolArray().All(b => b);
    //}
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double ToFloat64(ndarray scalar)
    {
        return double.NaN;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToText(ndarray scalar)
    {
        return np.ToString(scalar);
        //return scalar
        //    .Select(s => s.ToString())
        //    .Concatenate(", ", "{", "}");
    }
}