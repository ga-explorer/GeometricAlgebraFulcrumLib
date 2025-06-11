using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Angles;

public abstract record LinFloat64Angle :
    ILinFloat64PolarVector2D,
    IFloat64Scalar,
    IComparable<LinFloat64Angle>, 
    IComparable
{
    public const double Pi = Math.PI;

    public const double PiTimes2 = 2 * Math.PI;
    
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
    
    public double Radians 
        => RadiansValue;

    public double Degrees 
        => DegreesValue;
    
    public double ArcRatio 
        => ArcRatioValue;

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

    public double Item1 
        => CosValue;

    public double Item2 
        => SinValue;

    public double X 
        => CosValue;

    public double Y 
        => SinValue;

    public double R 
        => 1d;

    public LinFloat64PolarAngle Theta 
        => ToPolarAngle();
    
    public double ScalarValue 
        => RadiansValue;

    
    
    public void Deconstruct(out double angleCos, out double angleSin)
    {
        angleCos = CosValue;
        angleSin = SinValue;
    }

    public abstract bool IsValid();
    
    
    
    public bool IsZero()
    {
        return RadiansValue.IsZero();
    }

    
    public bool IsNearZero(double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return RadiansValue.IsNearZero(zeroEpsilon);
    }
    
    
    public bool IsFull()
    {
        var radians = RadiansValue;

        return (radians - PiTimes2).IsZero() ||
               (radians + PiTimes2).IsZero();
    }

    
    public bool IsNearFull(double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        var radians = RadiansValue;

        return (radians - PiTimes2).IsNearZero(zeroEpsilon) ||
               (radians + PiTimes2).IsNearZero(zeroEpsilon);
    }
    
    
    public bool IsZeroOrFull()
    {
        return CosValue.IsOne();
    }
    
    
    public bool IsNearZeroOrFull(double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return CosValue.IsNearOne(zeroEpsilon);
    }

    
    public bool IsRight()
    {
        return CosValue.IsZero();
    }
    
    
    public bool IsNearRight(double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return CosValue.IsNearZero(zeroEpsilon);
    }
    
    
    public bool IsStraight()
    {
        return CosValue.IsMinusOne();
    }

    
    public bool IsNearStraight(double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return CosValue.IsNearMinusOne(zeroEpsilon);
    }

    
    public bool IsAcute()
    {
        return CosValue >= 0 && SinValue >= 0;
    }

    
    public bool IsObtuse()
    {
        return CosValue < 0 && SinValue >= 0;
    }

    
    public bool IsReflex()
    {
        return SinValue < 0;
    }
    

    
    public bool IsEqual(LinFloat64Angle angle2)
    {
        return (RadiansValue - angle2.RadiansValue).IsZero();
    }

    
    public bool IsNearEqual(LinFloat64Angle angle2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return (RadiansValue - angle2.RadiansValue).IsNearZero(zeroEpsilon);
    }
    
    
    public bool IsEqualPolar(LinFloat64Angle angle)
    {
        var radians1 = LinAngleRange.Positive360.GetRadians(RadiansValue);
        var radians2 = LinAngleRange.Positive360.GetRadians(angle.RadiansValue);

        return (radians1 - radians2).IsZero();
    }

    
    public bool IsNearEqualPolar(LinFloat64Angle angle, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        var radians1 = LinAngleRange.Positive360.GetRadians(RadiansValue);
        var radians2 = LinAngleRange.Positive360.GetRadians(angle.RadiansValue);

        return (radians1 - radians2).IsNearZero(zeroEpsilon);
    }

    
    public bool IsComplementary(LinFloat64Angle angle)
    {
        return AngleAdd(angle.RadiansValue).IsRight();
    }

    
    public bool IsNearComplementary(LinFloat64Angle angle, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return AngleAdd(angle.RadiansValue).IsNearRight(zeroEpsilon);
    }
    
    
    public bool IsSupplementary(LinFloat64Angle angle)
    {
        return AngleAdd(angle.RadiansValue).IsStraight();
    }

    
    public bool IsNearSupplementary(LinFloat64Angle angle, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return AngleAdd(angle.RadiansValue).IsNearStraight(zeroEpsilon);
    }
    
    
    public bool IsUnitVector()
    {
        return true;
    }

    
    public bool IsNearUnitVector(double zeroEpsilon = 1E-12)
    {
        return true;
    }


    
    public double GetRadians(LinAngleRange range)
    {
        return range.GetRadians(RadiansValue);
    }

    
    public double GetDegrees(LinAngleRange range)
    {
        return range.GetDegrees(DegreesValue);
    }


    
    public double Cos()
    {
        return CosValue;
    }

    
    public double Sin()
    {
        return SinValue;
    }

    
    public double Tan()
    {
        return TanValue;
    }

    
    public double Sec()
    {
        return SecValue;
    }

    
    public double Csc()
    {
        return CscValue;
    }

    
    public double Cot()
    {
        return CotValue;
    }

    
    public double CosSquared()
    {
        return CosSquaredValue;
    }

    
    public double SinSquared()
    {
        return SinSquaredValue;
    }

    
    public double TanSquared()
    {
        return TanSquaredValue;
    }

    
    public double SecSquared()
    {
        return SecSquaredValue;
    }

    
    public double CscSquared()
    {
        return CscSquaredValue;
    }

    
    public double CotSquared()
    {
        return CotSquaredValue;
    }

    /// <summary>
    /// Rational Trigonometry Spread
    /// </summary>
    /// <returns></returns>
    
    public double RtSpread()
    {
        return RtSpreadValue;
    }
    
    /// <summary>
    /// Rational Trigonometry Cross
    /// </summary>
    /// <returns></returns>
    
    public double RtCross()
    {
        return RtCrossValue;
    }

    /// <summary>
    /// Rational Trigonometry Twist
    /// </summary>
    /// <returns></returns>
    
    public double RtTwist()
    {
        return RtTwistValue;
    }

    
    
    public LinFloat64PolarAngle HalfPolarAngle()
    {
        return LinFloat64PolarAngle.CreateHalfAngleFromCosSin(CosValue, SinValue);
    }
    
    
    public LinFloat64PolarAngle HalfPolarAngle(LinAngleRange range)
    {
        var (cosValue, sinValue) = 
            range.GetPolarAngleFromRadians(RadiansValue);

        return LinFloat64PolarAngle.CreateHalfAngleFromCosSin(cosValue, sinValue);
    }

    
    public LinFloat64DirectedAngle HalfDirectedAngle()
    {
        return LinFloat64DirectedAngle.CreateFromRadians(RadiansValue / 2);
    }

    
    public LinFloat64DirectedAngle HalfDirectedAngle(LinAngleRange range)
    {
        var radiansValue = range.GetRadians(RadiansValue);

        return LinFloat64DirectedAngle.CreateFromRadians(radiansValue / 2);
    }

    
    
    public LinFloat64PolarAngle DoublePolarAngle()
    {
        return LinFloat64PolarAngle.CreateDoubleAngleFromCosSin(CosValue, SinValue);
    }

    
    public LinFloat64PolarAngle DoublePolarAngle(LinAngleRange range)
    {
        var (cosValue, sinValue) = range.GetPolarAngleFromRadians(RadiansValue);

        return LinFloat64PolarAngle.CreateDoubleAngleFromCosSin(cosValue, sinValue);
    }
    
    
    public LinFloat64DirectedAngle DoubleDirectedAngle()
    {
        return LinFloat64DirectedAngle.CreateFromRadians(RadiansValue * 2);
    }

    
    public LinFloat64DirectedAngle DoubleDirectedAngle(LinAngleRange range)
    {
        var radiansValue = range.GetRadians(RadiansValue);

        return LinFloat64DirectedAngle.CreateFromRadians(radiansValue * 2);
    }


    
    public LinFloat64PolarAngle TriplePolarAngle()
    {
        return LinFloat64PolarAngle.CreateTripleAngleFromCosSin(CosValue, SinValue);
    }

    
    public LinFloat64PolarAngle TriplePolarAngle(LinAngleRange range)
    {
        var (cosValue, sinValue) = range.GetPolarAngleFromRadians(RadiansValue);

        return LinFloat64PolarAngle.CreateTripleAngleFromCosSin(cosValue, sinValue);
    }
    
    
    public LinFloat64DirectedAngle TripleDirectedAngle()
    {
        return LinFloat64DirectedAngle.CreateFromRadians(RadiansValue * 3);
    }

    
    public LinFloat64DirectedAngle TripleDirectedAngle(LinAngleRange range)
    {
        var radiansValue = range.GetRadians(RadiansValue);

        return LinFloat64DirectedAngle.CreateFromRadians(radiansValue * 3);
    }


    
    public double ToScalar()
    {
        return RadiansValue;
    }

    
    public LinFloat64DirectedAngle ToDirectedAngle()
    {
        return this as LinFloat64DirectedAngle 
               ?? LinFloat64DirectedAngle.CreateFromRadians(RadiansValue);
    }

    
    public LinFloat64DirectedAngle ToDirectedAngle(LinAngleRange range)
    {
        return LinFloat64DirectedAngle.CreateFromRadians(RadiansValue, range);
    }

    
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
    
    
    public LinFloat64PolarVector2D ToPolarVector2D()
    {
        return new LinFloat64PolarVector2D(ToPolarAngle());
    }

    
    public LinFloat64PolarVector2D ToPolarVector2D(double r)
    {
        return new LinFloat64PolarVector2D(r, ToPolarAngle());
    }
    
    
    public Complex ToComplexNumber()
    {
        return new Complex(CosValue, SinValue);
    }

    
    public Complex ToComplexNumber(double modulusValue)
    {
        return new Complex(modulusValue * CosValue, modulusValue * SinValue);
    }

    
    public Complex ToComplexConjugateNumber()
    {
        return new Complex(CosValue, -SinValue);
    }

    
    public Complex ToComplexConjugateNumber(double modulusValue)
    {
        return new Complex(
            modulusValue * CosValue,
            -modulusValue * SinValue
        );
    }


    
    public int CompareTo(LinFloat64Angle other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (other is null) return 1;
        
        return RadiansValue.CompareTo(other.RadiansValue);
    }

    
    public int CompareTo(object obj)
    {
        if (obj is null) return 1;
        if (ReferenceEquals(this, obj)) return 0;
        
        return obj is LinFloat64Angle other 
            ? CompareTo(other) 
            : throw new ArgumentException($"Object must be of type {nameof(LinFloat64Angle)}");
    }

    
    public static bool operator <(LinFloat64Angle left, LinFloat64Angle right)
    {
        return Comparer<LinFloat64Angle>.Default.Compare(left, right) < 0;
    }

    
    public static bool operator >(LinFloat64Angle left, LinFloat64Angle right)
    {
        return Comparer<LinFloat64Angle>.Default.Compare(left, right) > 0;
    }

    
    public static bool operator <=(LinFloat64Angle left, LinFloat64Angle right)
    {
        return Comparer<LinFloat64Angle>.Default.Compare(left, right) <= 0;
    }

    
    public static bool operator >=(LinFloat64Angle left, LinFloat64Angle right)
    {
        return Comparer<LinFloat64Angle>.Default.Compare(left, right) >= 0;
    }


    
    public LinFloat64Angle NegativeAngle()
    {
        return this switch
        {
            LinFloat64PolarAngle a => a.NegativeAngle(),
            LinFloat64DirectedAngle a => a.NegativeAngle(),
            _ => throw new InvalidOperationException()
        };
    }

    
    public LinFloat64Angle OppositeAngle()
    {
        return this switch
        {
            LinFloat64PolarAngle a => a.OppositeAngle(),
            LinFloat64DirectedAngle a => a.OppositeAngle(),
            _ => throw new InvalidOperationException()
        };
    }

    
    public LinFloat64Angle AngleAdd(double angle2)
    {
        return this switch
        {
            LinFloat64PolarAngle a => a.AngleAdd(angle2),
            LinFloat64DirectedAngle a => a.AngleAdd(angle2),
            _ => throw new InvalidOperationException()
        };
    }

    
    public LinFloat64Angle AngleSubtract(double angle2)
    {
        return this switch
        {
            LinFloat64PolarAngle a => a.AngleSubtract(angle2),
            LinFloat64DirectedAngle a => a.AngleSubtract(angle2),
            _ => throw new InvalidOperationException()
        };
    }

    
    public LinFloat64Angle AngleTimes(double scalingFactor)
    {
        return this switch
        {
            LinFloat64PolarAngle a => a.AngleTimes(scalingFactor),
            LinFloat64DirectedAngle a => a.AngleTimes(scalingFactor),
            _ => throw new InvalidOperationException()
        };
    }

    
    public LinFloat64Angle AngleDivide(double scalingFactor)
    {
        return this switch
        {
            LinFloat64PolarAngle a => a.AngleDivide(scalingFactor),
            LinFloat64DirectedAngle a => a.AngleDivide(scalingFactor),
            _ => throw new InvalidOperationException()
        };
    }
    
    
    //
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

    //
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
    
    //
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
    
    //
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

    //
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

    //
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

    
    
    public Pair<LinFloat64Vector2D> RotateBasisFrame2D()
    {
        return new Pair<LinFloat64Vector2D>(
            Rotate(LinFloat64Vector2D.E1),
            Rotate(LinFloat64Vector2D.E2)
        );
    }

    
    public LinFloat64Vector2D Rotate(double x, double y)
    {
        var cosValue = CosValue;
        var sinValue = SinValue;

        var x1 = x * cosValue - y * sinValue;
        var y1 = x * sinValue + y * cosValue;

        return LinFloat64Vector2D.Create(x1, y1);
    }

    
    public LinFloat64Vector2D Rotate(LinBasisVector axis)
    {
        var cosValue = CosValue;
        var sinValue = SinValue;

        var (x, y) = axis.ToLinVector2D();

        var x1 = x * cosValue - y * sinValue;
        var y1 = x * sinValue + y * cosValue;

        return LinFloat64Vector2D.Create(x1, y1);
    }

    
    public LinFloat64Vector2D Rotate(IPair<double> vector)
    {
        var cosValue = CosValue;
        var sinValue = SinValue;

        var x = vector.Item1;
        var y = vector.Item2;

        var x1 = x * cosValue - y * sinValue;
        var y1 = x * sinValue + y * cosValue;

        return LinFloat64Vector2D.Create(x1, y1);
    }

    
    public Pair<LinFloat64Vector2D> Rotate(IPair<double> vector1, IPair<double> vector2)
    {
        return new Pair<LinFloat64Vector2D>(
            Rotate(vector1),
            Rotate(vector2)
        );
    }

    
    public Triplet<LinFloat64Vector2D> Rotate(IPair<double> vector1, IPair<double> vector2, IPair<double> vector3)
    {
        return new Triplet<LinFloat64Vector2D>(
            Rotate(vector1),
            Rotate(vector2),
            Rotate(vector3)
        );
    }

    
    public IReadOnlyList<LinFloat64Vector2D> Rotate(params IPair<double>[] vectorArray)
    {
        return vectorArray
            .Select(Rotate)
            .ToArray();
    }

    
    public IEnumerable<LinFloat64Vector2D> Rotate(IEnumerable<IPair<double>> vectorList)
    {
        return vectorList.Select(Rotate);
    }
    
    /// <summary>
    /// Create a rotation quaternion given an axis and angle of rotation
    /// </summary>
    /// <param name="axis"></param>
    /// <returns></returns>
    
    public LinFloat64Quaternion ToQuaternion(ITriplet<double> axis)
    {
        return LinFloat64Quaternion.CreateFromAxisAngle(axis, this);
    }

    /// <summary>
    /// Create a rotation quaternion given an axis and angle of rotation
    /// </summary>
    /// <param name="axis"></param>
    /// <returns></returns>
    
    public LinFloat64Quaternion ToQuaternion(LinBasisVector axis)
    {
        return LinFloat64Quaternion.CreateFromAxisAngle(axis, this);
    }

    
    public SquareMatrix2 ToSquareMatrix2()
    {
        return SquareMatrix2.CreateRotationMatrix2D(this);
    }

}