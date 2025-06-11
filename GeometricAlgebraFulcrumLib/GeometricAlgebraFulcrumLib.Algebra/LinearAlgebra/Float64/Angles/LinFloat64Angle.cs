using System.Collections.Immutable;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;

public abstract record LinFloat64Angle :
    ILinFloat64PolarVector2D,
    IFloat64Scalar,
    IComparable<LinFloat64Angle>, 
    IComparable
{
    public const double Pi = Math.PI;

    public const double PiTimes2 = Math.Tau;
    
    public const double PiTimes4 = Math.PI * 4;

    public const double PiOver2 = Math.PI / 2;

    public const double DegreeToRadianFactor = Math.PI / 180d;

    public const double RadianToDegreeFactor = 180d / Math.PI;

    public const double Angle0Radians = 0d;

    public const double Angle30Radians = 30 * DegreeToRadianFactor;

    public const double Angle45Radians = 45 * DegreeToRadianFactor;

    public const double Angle60Radians = 60 * DegreeToRadianFactor;

    public const double Angle90Radians = 90 * DegreeToRadianFactor;

    public const double Angle120Radians = 120 * DegreeToRadianFactor;

    public const double Angle135Radians = 135 * DegreeToRadianFactor;

    public const double Angle150Radians = 150 * DegreeToRadianFactor;

    public const double Angle180Radians = 180 * DegreeToRadianFactor;

    public const double Angle210Radians = 210 * DegreeToRadianFactor;

    public const double Angle225Radians = 225 * DegreeToRadianFactor;

    public const double Angle240Radians = 240 * DegreeToRadianFactor;

    public const double Angle270Radians = 270 * DegreeToRadianFactor;

    public const double Angle300Radians = 300 * DegreeToRadianFactor;

    public const double Angle315Radians = 315 * DegreeToRadianFactor;

    public const double Angle330Radians = 330 * DegreeToRadianFactor;

    public const double Angle360Radians = 360 * DegreeToRadianFactor;


    public abstract double SinValue { get; }

    public abstract double CosValue { get; }

    public abstract double TanValue { get; }

    public double SecValue 
        => 1 / CosValue;

    public double CscValue 
        => 1 / SinValue;

    public double CotValue 
        => 1 / TanValue;

    public double CosSquaredValue 
        => CosValue.Square();

    public double SinSquaredValue 
        => SinValue.Square();

    public double TanSquaredValue 
        => TanValue.Square();

    public double SecSquaredValue 
        => SecValue.Square();

    public double CscSquaredValue 
        => CscValue.Square();

    public double CotSquaredValue 
        => CotValue.Square();

    /// <summary>
    /// Rational Trigonometry Spread
    /// </summary>
    public double RtSpreadValue 
        => SinSquaredValue;

    /// <summary>
    /// Rational Trigonometry Cross
    /// </summary>
    public double RtCrossValue 
        => CosSquaredValue;
    
    /// <summary>
    /// Rational Trigonometry Twist
    /// </summary>
    public double RtTwistValue 
        => TanSquaredValue;

    public abstract double RadiansValue { get; }

    public double DegreesValue 
        => RadiansValue * RadianToDegreeFactor;
    
    public double ArcRatioValue 
        => RadiansValue / PiTimes2;
    
    public Float64Scalar Radians 
        => Float64Scalar.Create(RadiansValue);

    public Float64Scalar Degrees 
        => Float64Scalar.Create(DegreesValue);
    
    public Float64Scalar ArcRatio 
        => Float64Scalar.Create(ArcRatioValue);

    public int QuadrantIndex
    {
        get
        {
            var radians = RadiansValue;
            if (radians < 0) radians += PiTimes2;

            return ((int)Math.Floor(2 * radians / Math.PI) % 4 + 4) % 4;
        }
    }

    public int VSpaceDimensions 
        => 2;

    public Float64Scalar Item1 
        => CosValue;

    public Float64Scalar Item2 
        => SinValue;

    public Float64Scalar X 
        => CosValue;

    public Float64Scalar Y 
        => SinValue;

    public Float64Scalar R 
        => Float64Scalar.One;

    public LinFloat64PolarAngle Theta 
        => ToPolarAngle();
    
    public double ScalarValue 
        => RadiansValue;

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out Float64Scalar angleCos, out Float64Scalar angleSin)
    {
        angleCos = Float64Scalar.Create(CosValue);
        angleSin = Float64Scalar.Create(SinValue);
    }

    public abstract bool IsValid();
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        return RadiansValue.IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearZero(double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return RadiansValue.IsNearZero(zeroEpsilon);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsFull()
    {
        var radians = RadiansValue;

        return (radians - PiTimes2).IsZero() ||
               (radians + PiTimes2).IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearFull(double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        var radians = RadiansValue;

        return (radians - PiTimes2).IsNearZero(zeroEpsilon) ||
               (radians + PiTimes2).IsNearZero(zeroEpsilon);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZeroOrFull()
    {
        return CosValue.IsOne();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearZeroOrFull(double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return CosValue.IsNearOne(zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsRight()
    {
        return CosValue.IsZero();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearRight(double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return CosValue.IsNearZero(zeroEpsilon);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsStraight()
    {
        return CosValue.IsMinusOne();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearStraight(double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return CosValue.IsNearMinusOne(zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsAcute()
    {
        return CosValue >= 0 && SinValue >= 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsObtuse()
    {
        return CosValue < 0 && SinValue >= 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsReflex()
    {
        return SinValue < 0;
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsEqual(LinFloat64Angle angle2)
    {
        return (RadiansValue - angle2.RadiansValue).IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearEqual(LinFloat64Angle angle2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return (RadiansValue - angle2.RadiansValue).IsNearZero(zeroEpsilon);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsEqualPolar(LinFloat64Angle angle)
    {
        var radians1 = LinAngleRange.Positive360.GetRadians(RadiansValue);
        var radians2 = LinAngleRange.Positive360.GetRadians(angle.RadiansValue);

        return (radians1 - radians2).IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearEqualPolar(LinFloat64Angle angle, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        var radians1 = LinAngleRange.Positive360.GetRadians(RadiansValue);
        var radians2 = LinAngleRange.Positive360.GetRadians(angle.RadiansValue);

        return (radians1 - radians2).IsNearZero(zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsComplementary(LinFloat64Angle angle)
    {
        return AngleAdd(angle.RadiansValue).IsRight();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearComplementary(LinFloat64Angle angle, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return AngleAdd(angle.RadiansValue).IsNearRight(zeroEpsilon);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsSupplementary(LinFloat64Angle angle)
    {
        return AngleAdd(angle.RadiansValue).IsStraight();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearSupplementary(LinFloat64Angle angle, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return AngleAdd(angle.RadiansValue).IsNearStraight(zeroEpsilon);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsUnitVector()
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearUnitVector(double zeroEpsilon = 1E-12)
    {
        return true;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GetRadians(LinAngleRange range)
    {
        return Float64Scalar.Create(
            range.GetRadians(RadiansValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GetDegrees(LinAngleRange range)
    {
        return Float64Scalar.Create(
            range.GetDegrees(DegreesValue)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Cos()
    {
        return Float64Scalar.Create(CosValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Sin()
    {
        return Float64Scalar.Create(SinValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Tan()
    {
        return Float64Scalar.Create(TanValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Sec()
    {
        return Float64Scalar.Create(SecValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Csc()
    {
        return Float64Scalar.Create(CscValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Cot()
    {
        return Float64Scalar.Create(CotValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar CosSquared()
    {
        return Float64Scalar.Create(CosSquaredValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar SinSquared()
    {
        return Float64Scalar.Create(SinSquaredValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar TanSquared()
    {
        return Float64Scalar.Create(TanSquaredValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar SecSquared()
    {
        return Float64Scalar.Create(SecSquaredValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar CscSquared()
    {
        return Float64Scalar.Create(CscSquaredValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar CotSquared()
    {
        return Float64Scalar.Create(CotSquaredValue);
    }

    /// <summary>
    /// Rational Trigonometry Spread
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar RtSpread()
    {
        return Float64Scalar.Create(RtSpreadValue);
    }
    
    /// <summary>
    /// Rational Trigonometry Cross
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar RtCross()
    {
        return Float64Scalar.Create(RtCrossValue);
    }

    /// <summary>
    /// Rational Trigonometry Twist
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar RtTwist()
    {
        return Float64Scalar.Create(RtTwistValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarAngle HalfPolarAngle()
    {
        return LinFloat64PolarAngle.CreateHalfAngleFromCosSin(CosValue, SinValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarAngle HalfPolarAngle(LinAngleRange range)
    {
        var (cosValue, sinValue) = 
            range.GetPolarAngleFromRadians(RadiansValue);

        return LinFloat64PolarAngle.CreateHalfAngleFromCosSin(cosValue, sinValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64DirectedAngle HalfDirectedAngle()
    {
        return LinFloat64DirectedAngle.CreateFromRadians(RadiansValue / 2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64DirectedAngle HalfDirectedAngle(LinAngleRange range)
    {
        var radiansValue = range.GetRadians(RadiansValue);

        return LinFloat64DirectedAngle.CreateFromRadians(radiansValue / 2);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarAngle DoublePolarAngle()
    {
        return LinFloat64PolarAngle.CreateDoubleAngleFromCosSin(CosValue, SinValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarAngle DoublePolarAngle(LinAngleRange range)
    {
        var (cosValue, sinValue) = range.GetPolarAngleFromRadians(RadiansValue);

        return LinFloat64PolarAngle.CreateDoubleAngleFromCosSin(cosValue, sinValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64DirectedAngle DoubleDirectedAngle()
    {
        return LinFloat64DirectedAngle.CreateFromRadians(RadiansValue * 2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64DirectedAngle DoubleDirectedAngle(LinAngleRange range)
    {
        var radiansValue = range.GetRadians(RadiansValue);

        return LinFloat64DirectedAngle.CreateFromRadians(radiansValue * 2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarAngle TriplePolarAngle()
    {
        return LinFloat64PolarAngle.CreateTripleAngleFromCosSin(CosValue, SinValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarAngle TriplePolarAngle(LinAngleRange range)
    {
        var (cosValue, sinValue) = range.GetPolarAngleFromRadians(RadiansValue);

        return LinFloat64PolarAngle.CreateTripleAngleFromCosSin(cosValue, sinValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64DirectedAngle TripleDirectedAngle()
    {
        return LinFloat64DirectedAngle.CreateFromRadians(RadiansValue * 3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64DirectedAngle TripleDirectedAngle(LinAngleRange range)
    {
        var radiansValue = range.GetRadians(RadiansValue);

        return LinFloat64DirectedAngle.CreateFromRadians(radiansValue * 3);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar ToScalar()
    {
        return RadiansValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64DirectedAngle ToDirectedAngle()
    {
        return this as LinFloat64DirectedAngle 
               ?? LinFloat64DirectedAngle.CreateFromRadians(RadiansValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64DirectedAngle ToDirectedAngle(LinAngleRange range)
    {
        return LinFloat64DirectedAngle.CreateFromRadians(RadiansValue, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarAngle ToPolarAngle()
    {
        return this as LinFloat64PolarAngle 
               ?? LinFloat64PolarAngle.CreateFromRadians(RadiansValue);
    }
    
    public LinFloat64PolarAngle ToPolarAngleInPeriodicRange(double maxRadians)
    {
        Debug.Assert(maxRadians is > 0d and <= PiTimes2);

        var radians = RadiansValue;

        //value < -maxValue
        if (radians < -maxRadians)
            return LinFloat64PolarAngle.CreateFromRadians(radians + Math.Ceiling(-radians / maxRadians) * maxRadians);

        //-maxValue <= value < 0
        if (radians < 0)
            return LinFloat64PolarAngle.CreateFromRadians(radians + maxRadians);

        //value > maxValue
        if (radians > maxRadians)
            return LinFloat64PolarAngle.CreateFromRadians(radians - Math.Truncate(radians / maxRadians) * maxRadians);

        //0 <= value <= maxValue
        return ToPolarAngle();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarVector2D ToPolarVector2D()
    {
        return new LinFloat64PolarVector2D(ToPolarAngle());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarVector2D ToPolarVector2D(double r)
    {
        return new LinFloat64PolarVector2D(r, ToPolarAngle());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Complex ToComplexNumber()
    {
        return new Complex(CosValue, SinValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Complex ToComplexNumber(double modulusValue)
    {
        return new Complex(modulusValue * CosValue, modulusValue * SinValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Complex ToComplexConjugateNumber()
    {
        return new Complex(CosValue, -SinValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Complex ToComplexConjugateNumber(double modulusValue)
    {
        return new Complex(
            modulusValue * CosValue,
            -modulusValue * SinValue
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(LinFloat64Angle? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (other is null) return 1;
        
        return RadiansValue.CompareTo(other.RadiansValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(object? obj)
    {
        if (obj is null) return 1;
        if (ReferenceEquals(this, obj)) return 0;
        
        return obj is LinFloat64Angle other 
            ? CompareTo(other) 
            : throw new ArgumentException($"Object must be of type {nameof(LinFloat64Angle)}");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(LinFloat64Angle? left, LinFloat64Angle? right)
    {
        return Comparer<LinFloat64Angle>.Default.Compare(left, right) < 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(LinFloat64Angle? left, LinFloat64Angle? right)
    {
        return Comparer<LinFloat64Angle>.Default.Compare(left, right) > 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(LinFloat64Angle? left, LinFloat64Angle? right)
    {
        return Comparer<LinFloat64Angle>.Default.Compare(left, right) <= 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(LinFloat64Angle? left, LinFloat64Angle? right)
    {
        return Comparer<LinFloat64Angle>.Default.Compare(left, right) >= 0;
    }


    public abstract LinFloat64Angle NegativeAngle();

    public abstract LinFloat64Angle OppositeAngle();

    public abstract LinFloat64Angle AngleAdd(double angle2);

    public abstract LinFloat64Angle AngleSubtract(double angle2);

    public abstract LinFloat64Angle AngleTimes(double scalingFactor);

    public abstract LinFloat64Angle AngleDivide(double scalingFactor);


    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public LinFloat64Angle HalfAngle(LinAngleRange range)
    //{
    //    return this switch
    //    {
    //        LinFloat64DirectedAngle directedAngle => 
    //            directedAngle.HalfAngle(range),

    //        LinFloat64PolarAngle polarAngle => 
    //            polarAngle.HalfPolarAngle(range),

    //        _ => throw new InvalidOperationException()
    //    };
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public LinFloat64Angle HalfAngle()
    //{
    //    return this switch
    //    {
    //        LinFloat64DirectedAngle directedAngle => 
    //            directedAngle.HalfAngle(),

    //        LinFloat64PolarAngle polarAngle => 
    //            polarAngle.HalfAngle(),

    //        _ => throw new InvalidOperationException()
    //    };
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public LinFloat64Angle DoubleAngle(LinAngleRange range)
    //{
    //    return this switch
    //    {
    //        LinFloat64DirectedAngle directedAngle => 
    //            directedAngle.DoubleAngle(range),

    //        LinFloat64PolarAngle polarAngle => 
    //            polarAngle.DoublePolarAngle(range),

    //        _ => throw new InvalidOperationException()
    //    };
    //}

    //public LinFloat64Angle DoubleAngle()
    //{
    //    return this switch
    //    {
    //        LinFloat64DirectedAngle directedAngle => 
    //            directedAngle.DoubleAngle(),

    //        LinFloat64PolarAngle polarAngle => 
    //            polarAngle.DoublePolarAngle(),

    //        _ => throw new InvalidOperationException()
    //    };
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public LinFloat64Angle TripleAngle(LinAngleRange range)
    //{
    //    return this switch
    //    {
    //        LinFloat64DirectedAngle directedAngle => 
    //            directedAngle.TripleAngle(range),

    //        LinFloat64PolarAngle polarAngle => 
    //            polarAngle.TriplePolarAngle(range),

    //        _ => throw new InvalidOperationException()
    //    };
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public LinFloat64Angle TripleAngle()
    //{
    //    return this switch
    //    {
    //        LinFloat64DirectedAngle directedAngle => 
    //            directedAngle.TripleAngle(),

    //        LinFloat64PolarAngle polarAngle => 
    //            polarAngle.TriplePolarAngle(),

    //        _ => throw new InvalidOperationException()
    //    };
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public LinFloat64Angle ClampNegative()
    //{
    //    const double maxValue = 360d;

    //    var value = this.DegreesValue + 180d;

    //    return value switch
    //    {
    //        //value < -maxValue
    //        < -maxValue => LinFloat64DirectedAngle.CreateFromDegrees(value + Math.Ceiling(-value / maxValue) * maxValue),

    //        //-maxValue <= value < 0
    //        < 0 => LinFloat64DirectedAngle.CreateFromDegrees(value + maxValue),

    //        //value > maxValue
    //        > maxValue => LinFloat64DirectedAngle.CreateFromDegrees(value - Math.Truncate(value / maxValue) * maxValue),

    //        //0 <= value <= maxValue
    //        _ => LinFloat64DirectedAngle.CreateFromDegrees(value)
    //    };
    //}

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<LinFloat64Vector2D> RotateBasisFrame2D()
    {
        return new Pair<LinFloat64Vector2D>(
            Rotate(LinFloat64Vector2D.E1),
            Rotate(LinFloat64Vector2D.E2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D Rotate(double x, double y)
    {
        var cosValue = CosValue;
        var sinValue = SinValue;

        var x1 = x * cosValue - y * sinValue;
        var y1 = x * sinValue + y * cosValue;

        return LinFloat64Vector2D.Create(x1, y1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D Rotate(LinBasisVector axis)
    {
        var cosValue = CosValue;
        var sinValue = SinValue;

        var (x, y) = axis.ToLinVector2D();

        var x1 = x * cosValue - y * sinValue;
        var y1 = x * sinValue + y * cosValue;

        return LinFloat64Vector2D.Create(x1, y1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D Rotate(IPair<Float64Scalar> vector)
    {
        var cosValue = CosValue;
        var sinValue = SinValue;

        var x = vector.Item1;
        var y = vector.Item2;

        var x1 = x * cosValue - y * sinValue;
        var y1 = x * sinValue + y * cosValue;

        return LinFloat64Vector2D.Create(x1, y1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<LinFloat64Vector2D> Rotate(IPair<Float64Scalar> vector1, IPair<Float64Scalar> vector2)
    {
        return new Pair<LinFloat64Vector2D>(
            Rotate(vector1),
            Rotate(vector2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Triplet<LinFloat64Vector2D> Rotate(IPair<Float64Scalar> vector1, IPair<Float64Scalar> vector2, IPair<Float64Scalar> vector3)
    {
        return new Triplet<LinFloat64Vector2D>(
            Rotate(vector1),
            Rotate(vector2),
            Rotate(vector3)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<LinFloat64Vector2D> Rotate(params IPair<Float64Scalar>[] vectorArray)
    {
        return vectorArray
            .Select(Rotate)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<LinFloat64Vector2D> Rotate(IEnumerable<IPair<Float64Scalar>> vectorList)
    {
        return vectorList.Select(Rotate);
    }
    
    /// <summary>
    /// Create a rotation quaternion given an axis and angle of rotation
    /// </summary>
    /// <param name="axis"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Quaternion ToQuaternion(ITriplet<Float64Scalar> axis)
    {
        return LinFloat64Quaternion.CreateFromAxisAngle(axis, this);
    }

    /// <summary>
    /// Create a rotation quaternion given an axis and angle of rotation
    /// </summary>
    /// <param name="axis"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Quaternion ToQuaternion(LinBasisVector axis)
    {
        return LinFloat64Quaternion.CreateFromAxisAngle(axis, this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix2 ToSquareMatrix2()
    {
        return SquareMatrix2.CreateRotationMatrix2D(this);
    }

}