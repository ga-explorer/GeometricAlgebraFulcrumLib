using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.ComplexAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;

public abstract record LinAngle<T> :
    ILinPolarVector2D<T>,
    IScalar<T>,
    IComparable<LinAngle<T>>, 
    IComparable
{
    public abstract IScalarProcessor<T> ScalarProcessor { get; }

    public abstract T SinValue { get; }

    public abstract T CosValue { get; }

    public abstract T TanValue { get; }

    public T SecValue 
        => ScalarProcessor.Divide(ScalarProcessor.OneValue, CosValue).ScalarValue;

    public T CscValue 
        => ScalarProcessor.Divide(ScalarProcessor.OneValue,  SinValue).ScalarValue;

    public T CotValue 
        => ScalarProcessor.Divide(ScalarProcessor.OneValue,  TanValue).ScalarValue;

    public T CosSquaredValue 
        => ScalarProcessor.Square(CosValue).ScalarValue;

    public T SinSquaredValue 
        => ScalarProcessor.Square(SinValue).ScalarValue;

    public T TanSquaredValue 
        => ScalarProcessor.Square(TanValue).ScalarValue;

    public T SecSquaredValue 
        => ScalarProcessor.Square(SecValue).ScalarValue;

    public T CscSquaredValue 
        => ScalarProcessor.Square(CscValue).ScalarValue;

    public T CotSquaredValue 
        => ScalarProcessor.Square(CotValue).ScalarValue;
    
    /// <summary>
    /// Rational Trigonometry Spread
    /// </summary>
    public T RtSpreadValue 
        => SinSquaredValue;

    /// <summary>
    /// Rational Trigonometry Cross
    /// </summary>
    public T RtCrossValue 
        => CosSquaredValue;
    
    /// <summary>
    /// Rational Trigonometry Twist
    /// </summary>
    public T RtTwistValue 
        => TanSquaredValue;

    public abstract T RadiansValue { get; }

    public T DegreesValue 
        => ScalarProcessor.RadiansToDegrees(RadiansValue).ScalarValue;
    
    public T ArcRatioValue 
        => ScalarProcessor.Divide(RadiansValue, ScalarProcessor.PiTimes2Value).ScalarValue;
    
    public Scalar<T> Radians 
        => ScalarProcessor.ScalarFromValue(RadiansValue);

    public Scalar<T> Degrees 
        => ScalarProcessor.ScalarFromValue(DegreesValue);
    
    public Scalar<T> ArcRatio 
        => ScalarProcessor.ScalarFromValue(ArcRatioValue);

    public int QuadrantIndex
    {
        get
        {
            var radians = Radians.ToFloat64();

            if (radians.IsNaNOrInfinite()) return -1;

            if (radians < 0) radians += ScalarProcessor.PiTimes2.ToFloat64();

            return ((int)Math.Floor(2 * radians / ScalarProcessor.Pi.ToFloat64()) % 4 + 4) % 4;
        }
    }

    public int VSpaceDimensions 
        => 2;

    public Scalar<T> Item1 
        => ScalarProcessor.ScalarFromValue(CosValue);

    public Scalar<T> Item2 
        => ScalarProcessor.ScalarFromValue(SinValue);

    public Scalar<T> X 
        => ScalarProcessor.ScalarFromValue(CosValue);

    public Scalar<T> Y 
        => ScalarProcessor.ScalarFromValue(SinValue);

    public Scalar<T> R 
        => ScalarProcessor.One;

    public LinPolarAngle<T> Theta 
        => ToPolarAngle();
    
    public T ScalarValue 
        => RadiansValue;

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out Scalar<T> cosValue, out Scalar<T> sinValue)
    {
        cosValue = ScalarProcessor.ScalarFromValue(CosValue);
        sinValue = ScalarProcessor.ScalarFromValue(SinValue);
    }

    public abstract bool IsValid();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        return ScalarProcessor.IsZero(RadiansValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearZero()
    {
        return ScalarProcessor.IsNearZero(RadiansValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsFull()
    {
        var radians = RadiansValue;

        Debug.Assert(radians != null, nameof(radians) + " != null");

        return (radians - ScalarProcessor.PiTimes2).IsZero() ||
               (radians + ScalarProcessor.PiTimes2).IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearFull()
    {
        var radians = RadiansValue;

        Debug.Assert(radians != null, nameof(radians) + " != null");

        return (radians - ScalarProcessor.PiTimes2).IsNearZero() ||
               (radians + ScalarProcessor.PiTimes2).IsNearZero();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZeroOrFull()
    {
        return ScalarProcessor.IsOne(CosValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearZeroOrFull()
    {
        return ScalarProcessor.IsNearOne(CosValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsRight()
    {
        return ScalarProcessor.IsZero(CosValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearRight()
    {
        return ScalarProcessor.IsZero(CosValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsStraight()
    {
        return ScalarProcessor.IsMinusOne(CosValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearStraight()
    {
        return ScalarProcessor.IsNearMinusOne(CosValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsAcute()
    {
        return ScalarProcessor.IsZeroOrPositive(CosValue) && 
               ScalarProcessor.IsZeroOrPositive(SinValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsObtuse()
    {
        return ScalarProcessor.IsNegative(CosValue) && 
               ScalarProcessor.IsZeroOrPositive(SinValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsReflex()
    {
        return ScalarProcessor.IsNegative(SinValue);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsEqual(LinAngle<T> angle2)
    {
        return ScalarProcessor.Subtract(RadiansValue, angle2.RadiansValue).IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearEqual(LinAngle<T> angle2)
    {
        return ScalarProcessor.Subtract(RadiansValue, angle2.RadiansValue).IsNearZero();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsEqualPolar(LinAngle<T> angle)
    {
        var radians1 = LinAngleRange.Positive360.GetRadians(Radians);
        var radians2 = LinAngleRange.Positive360.GetRadians(angle.Radians);

        return (radians1 - radians2).IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearEqualPolar(LinAngle<T> angle)
    {
        var radians1 = LinAngleRange.Positive360.GetRadians(Radians);
        var radians2 = LinAngleRange.Positive360.GetRadians(angle.Radians);

        return (radians1 - radians2).IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsComplementary(LinAngle<T> angle)
    {
        return AngleAdd(angle.Radians).IsRight();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearComplementary(LinAngle<T> angle)
    {
        return AngleAdd(angle.Radians).IsNearRight();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsSupplementary(LinAngle<T> angle)
    {
        return AngleAdd(angle.Radians).IsStraight();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearSupplementary(LinAngle<T> angle)
    {
        return AngleAdd(angle.Radians).IsNearStraight();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsUnitVector()
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearUnitVector()
    {
        return true;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetRadians(LinAngleRange range)
    {
        return range.GetRadians(Radians);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetDegrees(LinAngleRange range)
    {
        return range.GetDegrees(Degrees);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Cos()
    {
        return ScalarProcessor.ScalarFromValue(CosValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Sin()
    {
        return ScalarProcessor.ScalarFromValue(SinValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Tan()
    {
        return ScalarProcessor.ScalarFromValue(TanValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Sec()
    {
        return ScalarProcessor.ScalarFromValue(SecValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Csc()
    {
        return ScalarProcessor.ScalarFromValue(CscValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Cot()
    {
        return ScalarProcessor.ScalarFromValue(CotValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> CosSquared()
    {
        return ScalarProcessor.ScalarFromValue(CosSquaredValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> SinSquared()
    {
        return ScalarProcessor.ScalarFromValue(SinSquaredValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> TanSquared()
    {
        return ScalarProcessor.ScalarFromValue(TanSquaredValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> SecSquared()
    {
        return ScalarProcessor.ScalarFromValue(SecSquaredValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> CscSquared()
    {
        return ScalarProcessor.ScalarFromValue(CscSquaredValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> CotSquared()
    {
        return ScalarProcessor.ScalarFromValue(CotSquaredValue);
    }
    
    /// <summary>
    /// Rational Trigonometry Spread
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> RtSpread()
    {
        return ScalarProcessor.ScalarFromValue(RtSpreadValue);
    }
    
    /// <summary>
    /// Rational Trigonometry Cross
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> RtCross()
    {
        return ScalarProcessor.ScalarFromValue(RtCrossValue);
    }

    /// <summary>
    /// Rational Trigonometry Twist
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> RtTwist()
    {
        return ScalarProcessor.ScalarFromValue(RtTwistValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinPolarAngle<T> HalfPolarAngle()
    {
        return LinPolarAngle<T>.CreateHalfAngleFromCosSin(Cos(), Sin());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinPolarAngle<T> HalfPolarAngle(LinAngleRange range)
    {
        var (cosValue, sinValue) = 
            range.GetPolarAngleFromRadians(Radians);

        return LinPolarAngle<T>.CreateHalfAngleFromCosSin(cosValue, sinValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinDirectedAngle<T> HalfDirectedAngle()
    {
        return LinDirectedAngle<T>.CreateFromRadians(Radians / 2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinDirectedAngle<T> HalfDirectedAngle(LinAngleRange range)
    {
        var radiansValue = range.GetRadians(Radians);

        return LinDirectedAngle<T>.CreateFromRadians(radiansValue / 2);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinPolarAngle<T> DoublePolarAngle()
    {
        return LinPolarAngle<T>.CreateDoubleAngleFromCosSin(Cos(), Sin());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinPolarAngle<T> DoublePolarAngle(LinAngleRange range)
    {
        var (cosValue, sinValue) = range.GetPolarAngleFromRadians(Radians);

        return LinPolarAngle<T>.CreateDoubleAngleFromCosSin(cosValue, sinValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinDirectedAngle<T> DoubleDirectedAngle()
    {
        return LinDirectedAngle<T>.CreateFromRadians(Radians * 2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinDirectedAngle<T> DoubleDirectedAngle(LinAngleRange range)
    {
        var radiansValue = range.GetRadians(Radians);

        return LinDirectedAngle<T>.CreateFromRadians(radiansValue * 2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinPolarAngle<T> TriplePolarAngle()
    {
        return LinPolarAngle<T>.CreateTripleAngleFromCosSin(Cos(), Sin());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinPolarAngle<T> TriplePolarAngle(LinAngleRange range)
    {
        var (cosValue, sinValue) = range.GetPolarAngleFromRadians(Radians);

        return LinPolarAngle<T>.CreateTripleAngleFromCosSin(cosValue, sinValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinDirectedAngle<T> TripleDirectedAngle()
    {
        return LinDirectedAngle<T>.CreateFromRadians(Radians * 3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinDirectedAngle<T> TripleDirectedAngle(LinAngleRange range)
    {
        var radiansValue = range.GetRadians(Radians);

        return LinDirectedAngle<T>.CreateFromRadians(radiansValue * 3);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> ToScalar()
    {
        return ScalarProcessor.ScalarFromValue(RadiansValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinDirectedAngle<T> ToDirectedAngle()
    {
        return this as LinDirectedAngle<T> 
               ?? LinDirectedAngle<T>.CreateFromRadians(Radians);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinDirectedAngle<T> ToDirectedAngle(LinAngleRange range)
    {
        return LinDirectedAngle<T>.CreateFromRadians(Radians, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinPolarAngle<T> ToPolarAngle()
    {
        return this as LinPolarAngle<T> 
               ?? LinPolarAngle<T>.CreateFromRadians(Radians);
    }
    
    //public LinPolarAngle<T> ToPolarAngleInPeriodicRange(T maxRadians)
    //{
    //    Debug.Assert(maxRadians is > 0d and <= ScalarProcessor.PiTimes2);

    //    var radians = RadiansValue;

    //    //value < -maxValue
    //    if (radians < -maxRadians)
    //        return LinPolarAngle<T>.CreateFromRadians(radians + Math.Ceiling(-radians / maxRadians) * maxRadians);

    //    //-maxValue <= value < 0
    //    if (radians < 0)
    //        return LinPolarAngle<T>.CreateFromRadians(radians + maxRadians);

    //    //value > maxValue
    //    if (radians > maxRadians)
    //        return LinPolarAngle<T>.CreateFromRadians(radians - Math.Truncate(radians / maxRadians) * maxRadians);

    //    //0 <= value <= maxValue
    //    return ToPolarAngle();
    //}
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinPolarVector2D<T> ToPolarVector2D()
    {
        return new LinPolarVector2D<T>(ToPolarAngle());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinPolarVector2D<T> ToPolarVector2D(IScalar<T> r)
    {
        return new LinPolarVector2D<T>(r, ToPolarAngle());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(LinAngle<T>? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (other is null) return 1;
        
        var diff = ScalarProcessor.CompareTo(RadiansValue, other.RadiansValue);

        if (diff == 0) return 0;
        if (diff < 0) return -1;
        if (diff > 0) return 1;

        return 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(object? obj)
    {
        if (obj is null) return 1;
        if (ReferenceEquals(this, obj)) return 0;
        
        return obj is LinAngle<T> other 
            ? CompareTo(other) 
            : throw new ArgumentException($"Object must be of type {nameof(LinAngle<T>)}");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(LinAngle<T>? left, LinAngle<T>? right)
    {
        return Comparer<LinAngle<T>>.Default.Compare(left, right) < 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(LinAngle<T>? left, LinAngle<T>? right)
    {
        return Comparer<LinAngle<T>>.Default.Compare(left, right) > 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(LinAngle<T>? left, LinAngle<T>? right)
    {
        return Comparer<LinAngle<T>>.Default.Compare(left, right) <= 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(LinAngle<T>? left, LinAngle<T>? right)
    {
        return Comparer<LinAngle<T>>.Default.Compare(left, right) >= 0;
    }



    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract LinAngle<T> NegativeAngle();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract LinAngle<T> OppositeAngle();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract LinAngle<T> AngleAdd(IScalar<T> angle2);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract LinAngle<T> AngleSubtract(IScalar<T> angle2);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract LinAngle<T> AngleTimes(T scalingFactor);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract LinAngle<T> AngleDivide(T scalingFactor);

    public abstract LinAngle<T> MapAngleRadians(ScalarTransformer<T> transformer);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComplexNumber<T> ToComplexNumber()
    {
        var scalarProcessor = ScalarProcessor;

        return new ComplexNumber<T>(
            scalarProcessor,
            CosValue,
            SinValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComplexNumber<T> ToComplexNumber(T modulusValue)
    {
        var scalarProcessor = ScalarProcessor;

        return new ComplexNumber<T>(
            scalarProcessor.Times(modulusValue, CosValue),
            scalarProcessor.Times(modulusValue, SinValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComplexNumber<T> ToComplexConjugateNumber()
    {
        var scalarProcessor = ScalarProcessor;

        return new ComplexNumber<T>(
            Cos(),
            scalarProcessor.Negative(SinValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComplexNumber<T> ToComplexConjugateNumber(T modulusValue)
    {
        var scalarProcessor = ScalarProcessor;

        return new ComplexNumber<T>(
            scalarProcessor.Times(modulusValue, CosValue),
            scalarProcessor.NegativeTimes(modulusValue, SinValue)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<LinVector2D<T>> RotateBasisFrame2D()
    {
        return new Pair<LinVector2D<T>>(
            Rotate(LinVector2D<T>.E1(ScalarProcessor)),
            Rotate(LinVector2D<T>.E2(ScalarProcessor))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> Rotate(int x, int y)
    {
        var angleCos = Cos();
        var angleSin = Sin();

        var x1 = x * angleCos - y * angleSin;
        var y1 = x * angleSin + y * angleCos;

        return LinVector2D<T>.Create(x1, y1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> Rotate(T x, T y)
    {
        var angleCos = Cos();
        var angleSin = Sin();

        Debug.Assert(x is not null && y is not null);

        var x1 = x * angleCos - y * angleSin;
        var y1 = x * angleSin + y * angleCos;

        return LinVector2D<T>.Create(x1, y1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> Rotate(IScalar<T> sx, IScalar<T> sy)
    {
        var x = sx.ScalarValue;
        var y = sy.ScalarValue;

        Debug.Assert(x is not null && y is not null);

        var angleCos = Cos();
        var angleSin = Sin();

        var x1 = x * angleCos - y * angleSin;
        var y1 = x * angleSin + y * angleCos;

        return LinVector2D<T>.Create(x1, y1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> Rotate(LinBasisVector axis)
    {
        var (x, y) = axis.ToVector2D(ScalarProcessor);

        var angleCos = Cos();
        var angleSin = Sin();

        var x1 = x * angleCos - y * angleSin;
        var y1 = x * angleSin + y * angleCos;

        return LinVector2D<T>.Create(x1, y1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> Rotate(IPair<Scalar<T>> vector)
    {
        var x = vector.Item1;
        var y = vector.Item2;

        var angleCos = Cos();
        var angleSin = Sin();

        var x1 = x * angleCos - y * angleSin;
        var y1 = x * angleSin + y * angleCos;

        return LinVector2D<T>.Create(x1, y1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<LinVector2D<T>> Rotate(IPair<Scalar<T>> vector1, IPair<Scalar<T>> vector2)
    {
        return new Pair<LinVector2D<T>>(
            Rotate(vector1),
            Rotate(vector2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Triplet<LinVector2D<T>> Rotate(IPair<Scalar<T>> vector1, IPair<Scalar<T>> vector2, IPair<Scalar<T>> vector3)
    {
        return new Triplet<LinVector2D<T>>(
            Rotate(vector1),
            Rotate(vector2),
            Rotate(vector3)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<LinVector2D<T>> Rotate(params IPair<Scalar<T>>[] vectorArray)
    {
        return vectorArray
            .Select(Rotate)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<LinVector2D<T>> Rotate(IEnumerable<IPair<Scalar<T>>> vectorList)
    {
        return vectorList.Select(Rotate);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinQuaternion<T> ToQuaternion(ITriplet<Scalar<T>> normal)
    {
        return LinQuaternion<T>.CreateFromNormalAndAngle(
            normal.ToUnitVector(),
            ToPolarAngle()
        );
    }

}
