using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;

/// <summary>
/// In this record only the angle cos and sin are stored, the radians
/// value is computed on demand
/// </summary>
public sealed record LinFloat64PolarAngle :
    LinFloat64Angle
{
    private static readonly double Cos45 = Math.Sqrt(0.5);

    private static readonly double Sin45 = Math.Sqrt(0.5);

    private static readonly double Cos30 = Math.Sqrt(0.75);

    private static readonly double Sin30 = 0.5;

    private static readonly double Cos60 = 0.5;

    private static readonly double Sin60 = Math.Sqrt(0.75);


    public static LinFloat64PolarAngle Angle0 { get; } = CreateFromCosSin(1, 0);

    public static LinFloat64PolarAngle Angle30 { get; } = CreateFromCosSin(Cos30, Sin30);

    public static LinFloat64PolarAngle Angle45 { get; } = CreateFromCosSin(Cos45, Sin45);

    public static LinFloat64PolarAngle Angle60 { get; } = CreateFromCosSin(Cos60, Sin60);

    public static LinFloat64PolarAngle Angle90 { get; } = CreateFromCosSin(0, 1);

    public static LinFloat64PolarAngle Angle120 { get; } = CreateFromCosSin(-Cos60, Sin60);

    public static LinFloat64PolarAngle Angle135 { get; } = CreateFromCosSin(-Cos45, Sin45);

    public static LinFloat64PolarAngle Angle150 { get; } = CreateFromCosSin(-Cos30, Sin30);

    public static LinFloat64PolarAngle Angle180 { get; } = CreateFromCosSin(-1, 0);

    public static LinFloat64PolarAngle Angle210 { get; } = CreateFromCosSin(-Cos30, -Sin30);

    public static LinFloat64PolarAngle Angle225 { get; } = CreateFromCosSin(-Cos45, -Sin45);

    public static LinFloat64PolarAngle Angle240 { get; } = CreateFromCosSin(-Cos60, -Sin60);

    public static LinFloat64PolarAngle Angle270 { get; } = CreateFromCosSin(0, -1);

    public static LinFloat64PolarAngle Angle300 { get; } = CreateFromCosSin(Cos60, -Sin60);

    public static LinFloat64PolarAngle Angle315 { get; } = CreateFromCosSin(Cos45, -Sin45);

    public static LinFloat64PolarAngle Angle330 { get; } = CreateFromCosSin(Cos30, -Sin30);

    public static LinFloat64PolarAngle Angle360 { get; } = CreateFromCosSin(1, 0);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CreateFromDegrees(int angleInDegrees)
    {
        var angleInRadians = angleInDegrees.DegreesToRadians();

        return new LinFloat64PolarAngle(angleInRadians);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CreateFromDegrees(double angleInDegrees)
    {
        var angleInRadians = angleInDegrees.DegreesToRadians();

        return new LinFloat64PolarAngle(angleInRadians);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CreateFromDegrees(IFloat64Scalar angleInDegrees)
    {
        return CreateFromDegrees(angleInDegrees.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CreateFromRadians(double angleInRadians)
    {
        return new LinFloat64PolarAngle(angleInRadians);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CreateFromRadians(IFloat64Scalar angleInRadians)
    {
        return new LinFloat64PolarAngle(angleInRadians.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CreateFromCosSin(double angleCos, double angleSin)
    {
        return new LinFloat64PolarAngle(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CreateFromCos(double angleCos)
    {
        var angleSin = angleCos.CosToRadiansSin();

        return new LinFloat64PolarAngle(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CreateFromCos(IFloat64Scalar angleCos)
    {
        return CreateFromCos(angleCos.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CreateFromSin(double angleSin)
    {
        var angleCos = angleSin.SinToRadiansCos();

        return new LinFloat64PolarAngle(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CreateFromSin(IFloat64Scalar angleSin)
    {
        return CreateFromSin(angleSin.ScalarValue);
    }

    /// <summary>
    /// This assumes the angle is in the first two quadrant
    /// </summary>
    /// <param name="angleTan"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CreateFromTan(double angleTan)
    {
        var (angleCos, angleSin) = angleTan.TanToRadiansCosSin();

        return new LinFloat64PolarAngle(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CreateFromTan(IFloat64Scalar angleTan)
    {
        return CreateFromTan(angleTan.ScalarValue);
    }

    /// <summary>
    /// This assumes the angle is in the first quadrant
    /// </summary>
    /// <param name="angleTanSquared"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CreateFromTanSquared(double angleTanSquared)
    {
        var (angleCos, angleSin) = angleTanSquared.TanSquaredToRadiansCosSin();

        return new LinFloat64PolarAngle(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CreateFromTanSquared(IFloat64Scalar angleTan)
    {
        return CreateFromTanSquared(angleTan.ScalarValue);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CreateHalfAngleFromCosSin(double doubleAngleCos, double doubleAngleSin)
    {
        var (angleCos, angleSin) = doubleAngleCos.CosToHalfRadiansCosSin();

        if (doubleAngleSin < 0) angleCos = -angleCos;

        return new LinFloat64PolarAngle(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CreateHalfAngleFromCos(double doubleAngleCos)
    {
        var (angleCos, angleSin) = doubleAngleCos.CosToHalfRadiansCosSin();

        return new LinFloat64PolarAngle(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CreateHalfAngleFromCos(IFloat64Scalar halfAngleCos)
    {
        return CreateHalfAngleFromCos(halfAngleCos.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CreateHalfAngleFromSin(double doubleAngleSin)
    {
        var (angleCos, angleSin) = doubleAngleSin.SinToHalfRadiansCosSin();

        return new LinFloat64PolarAngle(angleCos, angleSin);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CreateHalfAngleFromSin(IFloat64Scalar halfAngleSin)
    {
        return CreateHalfAngleFromSin(halfAngleSin.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CreateDoubleAngleFromCosSin(double halfAngleCos, double halfAngleSin)
    {
        var angleCos = 2 * halfAngleCos * halfAngleCos - 1;
        var angleSin = 2 * halfAngleCos * halfAngleSin;

        return new LinFloat64PolarAngle(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CreateDoubleAngleFromCos(double halfAngleCos)
    {
        var (angleCos, angleSin) = halfAngleCos.CosToDoubleRadiansCosSin();

        return new LinFloat64PolarAngle(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CreateDoubleAngleFromCos(IFloat64Scalar halfAngleCos)
    {
        return CreateDoubleAngleFromCos(halfAngleCos.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CreateDoubleAngleFromSin(double halfAngleSin)
    {
        var (angleCos, angleSin) = halfAngleSin.SinToDoubleRadiansCosSin();

        return new LinFloat64PolarAngle(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CreateDoubleAngleFromSin(IFloat64Scalar halfAngleSin)
    {
        return CreateDoubleAngleFromSin(halfAngleSin.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CreateDoubleAngleFromTan(double halfAngleTan)
    {
        var (angleCos, angleSin) = halfAngleTan.TanToDoubleRadiansCosSin();

        return new LinFloat64PolarAngle(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CreateDoubleAngleFromTan(IFloat64Scalar halfAngleSin)
    {
        return CreateDoubleAngleFromTan(halfAngleSin.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CreateTripleAngleFromCosSin(double thirdAngleCos, double thirdAngleSin)
    {
        var angleCos = thirdAngleCos.CosToTripleRadiansCos();
        var angleSin = thirdAngleSin.SinToTripleRadiansSin();

        return new LinFloat64PolarAngle(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CreateTripleAngleFromCos(double thirdAngleCos)
    {
        var (angleCos, angleSin) = thirdAngleCos.CosToTripleRadiansCosSin();

        return new LinFloat64PolarAngle(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CreateTripleAngleFromCos(IFloat64Scalar thirdAngleCos)
    {
        return CreateTripleAngleFromCos(thirdAngleCos.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CreateTripleAngleFromSin(double thirdAngleSin)
    {
        var (angleCos, angleSin) = thirdAngleSin.SinToTripleRadiansCosSin();

        return new LinFloat64PolarAngle(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CreateTripleAngleFromSin(IFloat64Scalar thirdAngleSin)
    {
        return CreateTripleAngleFromCos(thirdAngleSin.ScalarValue);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CreateFromVector(double x, double y)
    {
        var r = Math.Sqrt(x * x + y * y);

        Debug.Assert(r.IsValid() && r.IsFinite());

        return r.IsZero() 
            ? Angle0 
            : new LinFloat64PolarAngle(x / r, y / r);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CreateFromVector(Float64Scalar x, Float64Scalar y)
    {
        return CreateFromVector(x.ScalarValue, y.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CreateFromVector(IFloat64Scalar x, IFloat64Scalar y)
    {
        return CreateFromVector(x.ScalarValue, y.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CreateFromVector(IPair<double> vector)
    {
        return CreateFromVector(
            vector.Item1, 
            vector.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CreateFromVector(IPair<Float64Scalar> vector)
    {
        return CreateFromVector(
            vector.Item1.ScalarValue, 
            vector.Item2.ScalarValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CreateFromUnitVectors(IPair<Float64Scalar> v1, IPair<Float64Scalar> v2)
    {
        return CreateFromCos(v1.VectorESp(v2).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CreateFromUnitVectors(ITriplet<Float64Scalar> v1, ITriplet<Float64Scalar> v2)
    {
        return CreateFromCos(v1.VectorESp(v2).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CreateFromUnitVectors(IQuad<Float64Scalar> v1, IQuad<Float64Scalar> v2)
    {
        return CreateFromCos(v1.VectorESp(v2).ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle operator -(LinFloat64PolarAngle a1)
    {
        return new LinFloat64PolarAngle(a1.CosValue, -a1.SinValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle operator +(double a1, LinFloat64PolarAngle a2)
    {
        var a1Cos = Math.Cos(a1);
        var a1Sin = Math.Sin(a1);

        var a2Cos = a2.CosValue;
        var a2Sin = a2.SinValue;

        var angleCos = a1Cos * a2Cos - a1Sin * a2Sin;
        var angleSin = a1Sin * a2Cos + a1Cos * a2Sin;

        return new LinFloat64PolarAngle(angleCos, angleSin);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle operator +(IFloat64Scalar a1, LinFloat64PolarAngle a2)
    {
        var a1Cos = Math.Cos(a1.ScalarValue);
        var a1Sin = Math.Sin(a1.ScalarValue);

        var a2Cos = a2.CosValue;
        var a2Sin = a2.SinValue;

        var angleCos = a1Cos * a2Cos - a1Sin * a2Sin;
        var angleSin = a1Sin * a2Cos + a1Cos * a2Sin;

        return new LinFloat64PolarAngle(angleCos, angleSin);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle operator +(LinFloat64PolarAngle a1, double a2)
    {
        var a1Cos = a1.CosValue;
        var a1Sin = a1.SinValue;

        var a2Cos = Math.Cos(a2);
        var a2Sin = Math.Sin(a2);

        var angleCos = a1Cos * a2Cos - a1Sin * a2Sin;
        var angleSin = a1Sin * a2Cos + a1Cos * a2Sin;

        return new LinFloat64PolarAngle(angleCos, angleSin);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle operator +(LinFloat64PolarAngle a1, IFloat64Scalar a2)
    {
        var a1Cos = a1.CosValue;
        var a1Sin = a1.SinValue;

        var a2Cos = Math.Cos(a2.ScalarValue);
        var a2Sin = Math.Sin(a2.ScalarValue);

        var angleCos = a1Cos * a2Cos - a1Sin * a2Sin;
        var angleSin = a1Sin * a2Cos + a1Cos * a2Sin;

        return new LinFloat64PolarAngle(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle operator +(LinFloat64PolarAngle a1, LinFloat64Angle a2)
    {
        var a1Cos = a1.CosValue;
        var a1Sin = a1.SinValue;

        var a2Cos = a2.CosValue;
        var a2Sin = a2.SinValue;

        var angleCos = a1Cos * a2Cos - a1Sin * a2Sin;
        var angleSin = a1Sin * a2Cos + a1Cos * a2Sin;

        return new LinFloat64PolarAngle(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle operator +(LinFloat64Angle a1, LinFloat64PolarAngle a2)
    {
        var a1Cos = a1.CosValue;
        var a1Sin = a1.SinValue;

        var a2Cos = a2.CosValue;
        var a2Sin = a2.SinValue;

        var angleCos = a1Cos * a2Cos - a1Sin * a2Sin;
        var angleSin = a1Sin * a2Cos + a1Cos * a2Sin;

        return new LinFloat64PolarAngle(angleCos, angleSin);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle operator +(LinFloat64PolarAngle a1, LinFloat64PolarAngle a2)
    {
        var a1Cos = a1.CosValue;
        var a1Sin = a1.SinValue;

        var a2Cos = a2.CosValue;
        var a2Sin = a2.SinValue;

        var angleCos = a1Cos * a2Cos - a1Sin * a2Sin;
        var angleSin = a1Sin * a2Cos + a1Cos * a2Sin;

        return new LinFloat64PolarAngle(angleCos, angleSin);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle operator -(double a1, LinFloat64PolarAngle a2)
    {
        var a1Cos = Math.Cos(a1);
        var a1Sin = Math.Sin(a1);

        var a2Cos = a2.CosValue;
        var a2Sin = a2.SinValue;

        var angleCos = a1Cos * a2Cos + a1Sin * a2Sin;
        var angleSin = a1Sin * a2Cos - a1Cos * a2Sin;

        return new LinFloat64PolarAngle(angleCos, angleSin);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle operator -(IFloat64Scalar a1, LinFloat64PolarAngle a2)
    {
        var a1Cos = Math.Cos(a1.ScalarValue);
        var a1Sin = Math.Sin(a1.ScalarValue);

        var a2Cos = a2.CosValue;
        var a2Sin = a2.SinValue;

        var angleCos = a1Cos * a2Cos + a1Sin * a2Sin;
        var angleSin = a1Sin * a2Cos - a1Cos * a2Sin;

        return new LinFloat64PolarAngle(angleCos, angleSin);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle operator -(LinFloat64PolarAngle a1, double a2)
    {
        var a1Cos = a1.CosValue;
        var a1Sin = a1.SinValue;

        var a2Cos = Math.Cos(a2);
        var a2Sin = Math.Sin(a2);

        var angleCos = a1Cos * a2Cos + a1Sin * a2Sin;
        var angleSin = a1Sin * a2Cos - a1Cos * a2Sin;

        return new LinFloat64PolarAngle(angleCos, angleSin);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle operator -(LinFloat64PolarAngle a1, IFloat64Scalar a2)
    {
        var a1Cos = a1.CosValue;
        var a1Sin = a1.SinValue;

        var a2Cos = Math.Cos(a2.ScalarValue);
        var a2Sin = Math.Sin(a2.ScalarValue);

        var angleCos = a1Cos * a2Cos + a1Sin * a2Sin;
        var angleSin = a1Sin * a2Cos - a1Cos * a2Sin;

        return new LinFloat64PolarAngle(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle operator -(LinFloat64PolarAngle a1, LinFloat64Angle a2)
    {
        var a1Cos = a1.CosValue;
        var a1Sin = a1.SinValue;

        var a2Cos = a2.CosValue;
        var a2Sin = a2.SinValue;

        var angleCos = a1Cos * a2Cos + a1Sin * a2Sin;
        var angleSin = a1Sin * a2Cos - a1Cos * a2Sin;

        return new LinFloat64PolarAngle(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle operator -(LinFloat64Angle a1, LinFloat64PolarAngle a2)
    {
        var a1Cos = a1.CosValue;
        var a1Sin = a1.SinValue;

        var a2Cos = a2.CosValue;
        var a2Sin = a2.SinValue;

        var angleCos = a1Cos * a2Cos + a1Sin * a2Sin;
        var angleSin = a1Sin * a2Cos - a1Cos * a2Sin;

        return new LinFloat64PolarAngle(angleCos, angleSin);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle operator -(LinFloat64PolarAngle a1, LinFloat64PolarAngle a2)
    {
        var a1Cos = a1.CosValue;
        var a1Sin = a1.SinValue;

        var a2Cos = a2.CosValue;
        var a2Sin = a2.SinValue;

        var angleCos = a1Cos * a2Cos + a1Sin * a2Sin;
        var angleSin = a1Sin * a2Cos - a1Cos * a2Sin;

        return new LinFloat64PolarAngle(angleCos, angleSin);
    }


    public override double CosValue { get; }

    public override double SinValue { get; }

    public override double TanValue
        => SinValue / CosValue;

    public override double RadiansValue 
        => CosValue.ArcTan2(SinValue);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64PolarAngle(double radians)
    {
        CosValue = Math.Cos(radians);
        SinValue = Math.Sin(radians);

        Debug.Assert(IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64PolarAngle(double angleCos, double angleSin)
    {
        CosValue = angleCos;
        SinValue = angleSin;

        Debug.Assert(IsValid());
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return CosValue.IsValid() &&
               SinValue.IsValid() &&
               (CosValue * CosValue + SinValue * SinValue).IsNearOne();
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarAngle NegativeAngle()
    {
        return new LinFloat64PolarAngle(CosValue, -SinValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarAngle OppositeAngle()
    {
        return new LinFloat64PolarAngle(-CosValue, -SinValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarAngle AngleAdd(double angle2)
    {
        var a2Cos = Math.Cos(angle2);
        var a2Sin = Math.Sin(angle2);

        var angleCos = CosValue * a2Cos - SinValue * a2Sin;
        var angleSin = SinValue * a2Cos + CosValue * a2Sin;

        return new LinFloat64PolarAngle(angleCos, angleSin);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarAngle AngleSubtract(double angle2)
    {
        var a2Cos = Math.Cos(angle2);
        var a2Sin = Math.Sin(angle2);

        var angleCos = CosValue * a2Cos + SinValue * a2Sin;
        var angleSin = SinValue * a2Cos - CosValue * a2Sin;

        return new LinFloat64PolarAngle(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarAngle AngleTimes(double scalingFactor)
    {
        return CreateFromRadians(RadiansValue * scalingFactor);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarAngle AngleDivide(double scalingFactor)
    {
        return CreateFromRadians(RadiansValue / scalingFactor);
    }
    
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public int CompareTo(ILinFloat64Angle? other)
    //{
    //    if (ReferenceEquals(this, other)) return 0;
    //    if (other is null) return 1;

    //    return RadiansValue.CompareTo(other.RadiansValue);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public int CompareTo(LinFloat64PolarAngle? other)
    //{
    //    if (ReferenceEquals(this, other)) return 0;
    //    if (other is null) return 1;

    //    return RadiansValue.CompareTo(other.RadiansValue);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public int CompareTo(object? obj)
    //{
    //    if (obj is null) return 1;
    //    if (ReferenceEquals(this, obj)) return 0;
    //    return obj is LinFloat64PolarAngle other 
    //        ? CompareTo(other) 
    //        : throw new ArgumentException($"Object must be of type {nameof(LinFloat64PolarAngle)}");
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool operator <(LinFloat64PolarAngle? left, LinFloat64PolarAngle? right)
    //{
    //    return Comparer<LinFloat64PolarAngle>.Default.Compare(left, right) < 0;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool operator >(LinFloat64PolarAngle? left, LinFloat64PolarAngle? right)
    //{
    //    return Comparer<LinFloat64PolarAngle>.Default.Compare(left, right) > 0;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool operator <=(LinFloat64PolarAngle? left, LinFloat64PolarAngle? right)
    //{
    //    return Comparer<LinFloat64PolarAngle>.Default.Compare(left, right) <= 0;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool operator >=(LinFloat64PolarAngle? left, LinFloat64PolarAngle? right)
    //{
    //    return Comparer<LinFloat64PolarAngle>.Default.Compare(left, right) >= 0;
    //}

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return $"{DegreesValue:G5} degrees";
    }
}