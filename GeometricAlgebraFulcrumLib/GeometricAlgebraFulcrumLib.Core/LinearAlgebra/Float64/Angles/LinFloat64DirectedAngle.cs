using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Angles;

public sealed record LinFloat64DirectedAngle :
    LinFloat64Angle
{
    public static LinFloat64DirectedAngle Angle0 { get; }
        = new LinFloat64DirectedAngle(Angle0Radians);

    public static LinFloat64DirectedAngle Angle30 { get; }
        = new LinFloat64DirectedAngle(Angle30Radians);

    public static LinFloat64DirectedAngle Angle45 { get; }
        = new LinFloat64DirectedAngle(Angle45Radians);

    public static LinFloat64DirectedAngle Angle60 { get; }
        = new LinFloat64DirectedAngle(Angle60Radians);

    public static LinFloat64DirectedAngle Angle90 { get; }
        = new LinFloat64DirectedAngle(Angle90Radians);

    public static LinFloat64DirectedAngle Angle120 { get; }
        = new LinFloat64DirectedAngle(Angle120Radians);

    public static LinFloat64DirectedAngle Angle135 { get; }
        = new LinFloat64DirectedAngle(Angle135Radians);

    public static LinFloat64DirectedAngle Angle150 { get; }
        = new LinFloat64DirectedAngle(Angle150Radians);

    public static LinFloat64DirectedAngle Angle180 { get; }
        = new LinFloat64DirectedAngle(Angle180Radians);

    public static LinFloat64DirectedAngle Angle225 { get; }
        = new LinFloat64DirectedAngle(Angle225Radians);

    public static LinFloat64DirectedAngle Angle210 { get; }
        = new LinFloat64DirectedAngle(Angle210Radians);

    public static LinFloat64DirectedAngle Angle240 { get; }
        = new LinFloat64DirectedAngle(Angle240Radians);

    public static LinFloat64DirectedAngle Angle270 { get; }
        = new LinFloat64DirectedAngle(Angle270Radians);

    public static LinFloat64DirectedAngle Angle300 { get; }
        = new LinFloat64DirectedAngle(Angle300Radians);

    public static LinFloat64DirectedAngle Angle315 { get; }
        = new LinFloat64DirectedAngle(Angle315Radians);

    public static LinFloat64DirectedAngle Angle330 { get; }
        = new LinFloat64DirectedAngle(Angle330Radians);

    public static LinFloat64DirectedAngle Angle360 { get; }
        = new LinFloat64DirectedAngle(Angle360Radians);
    
    public static LinFloat64DirectedAngle AngleMinus30 { get; }
        = new LinFloat64DirectedAngle(-Angle30Radians);

    public static LinFloat64DirectedAngle AngleMinus45 { get; }
        = new LinFloat64DirectedAngle(-Angle45Radians);

    public static LinFloat64DirectedAngle AngleMinus60 { get; }
        = new LinFloat64DirectedAngle(-Angle60Radians);

    public static LinFloat64DirectedAngle AngleMinus90 { get; }
        = new LinFloat64DirectedAngle(-Angle90Radians);

    public static LinFloat64DirectedAngle AngleMinus120 { get; }
        = new LinFloat64DirectedAngle(-Angle120Radians);

    public static LinFloat64DirectedAngle AngleMinus135 { get; }
        = new LinFloat64DirectedAngle(-Angle135Radians);

    public static LinFloat64DirectedAngle AngleMinus150 { get; }
        = new LinFloat64DirectedAngle(-Angle150Radians);

    public static LinFloat64DirectedAngle AngleMinus180 { get; }
        = new LinFloat64DirectedAngle(-Angle180Radians);

    public static LinFloat64DirectedAngle AngleMinus225 { get; }
        = new LinFloat64DirectedAngle(-Angle225Radians);

    public static LinFloat64DirectedAngle AngleMinus210 { get; }
        = new LinFloat64DirectedAngle(-Angle210Radians);

    public static LinFloat64DirectedAngle AngleMinus240 { get; }
        = new LinFloat64DirectedAngle(-Angle240Radians);

    public static LinFloat64DirectedAngle AngleMinus270 { get; }
        = new LinFloat64DirectedAngle(-Angle270Radians);

    public static LinFloat64DirectedAngle AngleMinus300 { get; }
        = new LinFloat64DirectedAngle(-Angle300Radians);

    public static LinFloat64DirectedAngle AngleMinus315 { get; }
        = new LinFloat64DirectedAngle(-Angle315Radians);

    public static LinFloat64DirectedAngle AngleMinus330 { get; }
        = new LinFloat64DirectedAngle(-Angle330Radians);

    public static LinFloat64DirectedAngle AngleMinus360 { get; }
        = new LinFloat64DirectedAngle(-Angle360Radians);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateFromDegrees(int angleInDegrees)
    {
        // A full-range angle is in the range [-360, 360] degrees
        var degrees =
            angleInDegrees switch
            {
                < -360 => angleInDegrees % 720 + 360,
                > 360 => angleInDegrees % 360,
                _ => angleInDegrees
            };

        return new LinFloat64DirectedAngle(
            degrees * DegreeToRadianFactor
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateFromDegrees(double angleInDegrees)
    {
        // A full-range angle is in the range [-360, 360] degrees
        var degrees =
            angleInDegrees switch
            {
                < -360 => angleInDegrees % 720d + 360d,
                > 360 => angleInDegrees % 360d,
                _ => angleInDegrees
            };

        return new LinFloat64DirectedAngle(
            degrees * DegreeToRadianFactor
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateFromDegrees(double angleInDegrees, LinAngleRange range)
    {
        var degrees = range.GetDegrees(angleInDegrees);

        return new LinFloat64DirectedAngle(
            degrees * DegreeToRadianFactor
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreatePositiveFromDegrees(double angleInDegrees)
    {
        var radians = LinAngleRange.Positive360.GetDegrees(angleInDegrees) * DegreeToRadianFactor;

        return new LinFloat64DirectedAngle(radians);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateNegativeFromDegrees(double angleInDegrees)
    {
        var radians = LinAngleRange.Negative360.GetDegrees(angleInDegrees) * DegreeToRadianFactor;

        return new LinFloat64DirectedAngle(radians);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateFromRadians(double angleInRadians)
    {
        // A full-range angle is in the range [-360, 360] degrees
        var radians =
            angleInRadians switch
            {
                < -PiTimes2 => angleInRadians % PiTimes4 + PiTimes2,
                > PiTimes2 => angleInRadians % PiTimes2,
                _ => angleInRadians
            };

        return new LinFloat64DirectedAngle(radians);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateFromRadians(double angleInRadians, LinAngleRange range)
    {
        var radians = range.GetRadians(angleInRadians);

        return new LinFloat64DirectedAngle(radians);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreatePositiveFromRadians(double angleInRadians)
    {
        var radians = LinAngleRange.Positive360.GetRadians(angleInRadians);

        return new LinFloat64DirectedAngle(radians);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateNegativeFromRadians(double angleInRadians)
    {
        var radians = LinAngleRange.Negative360.GetRadians(angleInRadians);

        return new LinFloat64DirectedAngle(radians);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateFromCosSin(double angleCos, double angleSin)
    {
        return new LinFloat64DirectedAngle(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateFromCosSin(double angleCos, double angleSin, LinAngleRange range)
    {
        return new LinFloat64DirectedAngle(angleCos, angleSin, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateFromCos(double angleCos)
    {
        var angleSin = angleCos.CosToRadiansSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateFromCos(double angleCos, LinAngleRange range)
    {
        var angleSin = angleCos.CosToRadiansSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateFromCos(IFloat64Scalar angleCos)
    {
        return CreateFromCos(angleCos.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateFromCos(IFloat64Scalar angleCos, LinAngleRange range)
    {
        return CreateFromCos(angleCos.ScalarValue, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateFromSin(double angleSin)
    {
        var angleCos = angleSin.SinToRadiansCos();

        return new LinFloat64DirectedAngle(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateFromSin(double angleSin, LinAngleRange range)
    {
        var angleCos = angleSin.SinToRadiansCos();

        return new LinFloat64DirectedAngle(angleCos, angleSin, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateFromSin(IFloat64Scalar angleSin)
    {
        return CreateFromSin(angleSin.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateFromSin(IFloat64Scalar angleSin, LinAngleRange range)
    {
        return CreateFromSin(angleSin.ScalarValue, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateFromTan(double angleTan)
    {
        var (angleCos, angleSin) = angleTan.TanToRadiansCosSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateFromTan(double angleTan, LinAngleRange range)
    {
        var (angleCos, angleSin) = angleTan.TanToRadiansCosSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin, range);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateFromTan(IFloat64Scalar angleTan)
    {
        return CreateFromTan(angleTan.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateFromTan(IFloat64Scalar angleTan, LinAngleRange range)
    {
        return CreateFromTan(angleTan.ScalarValue, range);
    }

    /// <summary>
    /// This assumes the angle is in the first quadrant
    /// </summary>
    /// <param name="angleTanSquared"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateFromTanSquared(double angleTanSquared)
    {
        var (angleCos, angleSin) = angleTanSquared.TanSquaredToRadiansCosSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateFromTanSquared(double angleTanSquared, LinAngleRange range)
    {
        var (angleCos, angleSin) = angleTanSquared.TanSquaredToRadiansCosSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin, range);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateFromTanSquared(IFloat64Scalar angleTan)
    {
        return CreateFromTanSquared(angleTan.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateFromTanSquared(IFloat64Scalar angleTan, LinAngleRange range)
    {
        return CreateFromTanSquared(angleTan.ScalarValue, range);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateHalfAngleFromCosSin(double doubleAngleCos, double doubleAngleSin)
    {
        var (angleCos, angleSin) = doubleAngleCos.CosToHalfRadiansCosSin();

        if (doubleAngleSin < 0) angleCos = -angleCos;

        return new LinFloat64DirectedAngle(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateHalfAngleFromCosSin(double doubleAngleCos, double doubleAngleSin, LinAngleRange range)
    {
        var (angleCos, angleSin) = doubleAngleCos.CosToHalfRadiansCosSin();

        if (doubleAngleSin < 0) angleCos = -angleCos;

        return new LinFloat64DirectedAngle(angleCos, angleSin, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateHalfAngleFromCos(double doubleAngleCos)
    {
        var (angleCos, angleSin) = doubleAngleCos.CosToHalfRadiansCosSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateHalfAngleFromCos(double doubleAngleCos, LinAngleRange range)
    {
        var (angleCos, angleSin) = doubleAngleCos.CosToHalfRadiansCosSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateHalfAngleFromCos(IFloat64Scalar halfAngleCos)
    {
        return CreateHalfAngleFromCos(halfAngleCos.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateHalfAngleFromCos(IFloat64Scalar halfAngleCos, LinAngleRange range)
    {
        return CreateHalfAngleFromCos(halfAngleCos.ScalarValue, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateHalfAngleFromSin(double doubleAngleSin)
    {
        var (angleCos, angleSin) = doubleAngleSin.SinToHalfRadiansCosSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateHalfAngleFromSin(double doubleAngleSin, LinAngleRange range)
    {
        var (angleCos, angleSin) = doubleAngleSin.SinToHalfRadiansCosSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateHalfAngleFromSin(IFloat64Scalar halfAngleSin)
    {
        return CreateHalfAngleFromSin(halfAngleSin.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateHalfAngleFromSin(IFloat64Scalar halfAngleSin, LinAngleRange range)
    {
        return CreateHalfAngleFromSin(halfAngleSin.ScalarValue, range);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateDoubleAngleFromCosSin(double halfAngleCos, double halfAngleSin)
    {
        var angleCos = 2 * halfAngleCos * halfAngleCos - 1;
        var angleSin = 2 * halfAngleCos * halfAngleSin;

        return new LinFloat64DirectedAngle(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateDoubleAngleFromCosSin(double halfAngleCos, double halfAngleSin, LinAngleRange range)
    {
        var angleCos = 2 * halfAngleCos * halfAngleCos - 1;
        var angleSin = 2 * halfAngleCos * halfAngleSin;

        return new LinFloat64DirectedAngle(angleCos, angleSin, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateDoubleAngleFromCos(double halfAngleCos)
    {
        var (angleCos, angleSin) = halfAngleCos.CosToDoubleRadiansCosSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateDoubleAngleFromCos(double halfAngleCos, LinAngleRange range)
    {
        var (angleCos, angleSin) = halfAngleCos.CosToDoubleRadiansCosSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateDoubleAngleFromCos(IFloat64Scalar halfAngleCos)
    {
        return CreateDoubleAngleFromCos(halfAngleCos.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateDoubleAngleFromCos(IFloat64Scalar halfAngleCos, LinAngleRange range)
    {
        return CreateDoubleAngleFromCos(halfAngleCos.ScalarValue, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateDoubleAngleFromSin(double halfAngleSin)
    {
        var (angleCos, angleSin) = halfAngleSin.SinToDoubleRadiansCosSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateDoubleAngleFromSin(double halfAngleSin, LinAngleRange range)
    {
        var (angleCos, angleSin) = halfAngleSin.SinToDoubleRadiansCosSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateDoubleAngleFromSin(IFloat64Scalar halfAngleSin)
    {
        return CreateDoubleAngleFromSin(halfAngleSin.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateDoubleAngleFromSin(IFloat64Scalar halfAngleSin, LinAngleRange range)
    {
        return CreateDoubleAngleFromSin(halfAngleSin.ScalarValue, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateDoubleAngleFromTan(double halfAngleTan)
    {
        var (angleCos, angleSin) = halfAngleTan.TanToDoubleRadiansCosSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateDoubleAngleFromTan(double halfAngleTan, LinAngleRange range)
    {
        var (angleCos, angleSin) = halfAngleTan.TanToDoubleRadiansCosSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateDoubleAngleFromTan(IFloat64Scalar halfAngleSin)
    {
        return CreateDoubleAngleFromTan(halfAngleSin.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateDoubleAngleFromTan(IFloat64Scalar halfAngleSin, LinAngleRange range)
    {
        return CreateDoubleAngleFromTan(halfAngleSin.ScalarValue, range);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateTripleAngleFromCosSin(double thirdAngleCos, double thirdAngleSin)
    {
        var angleCos = thirdAngleCos.CosToTripleRadiansCos();
        var angleSin = thirdAngleSin.SinToTripleRadiansSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateTripleAngleFromCosSin(double thirdAngleCos, double thirdAngleSin, LinAngleRange range)
    {
        var angleCos = thirdAngleCos.CosToTripleRadiansCos();
        var angleSin = thirdAngleSin.SinToTripleRadiansSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateTripleAngleFromCos(double thirdAngleCos)
    {
        var (angleCos, angleSin) = thirdAngleCos.CosToTripleRadiansCosSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateTripleAngleFromCos(double thirdAngleCos, LinAngleRange range)
    {
        var (angleCos, angleSin) = thirdAngleCos.CosToTripleRadiansCosSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateTripleAngleFromCos(IFloat64Scalar thirdAngleCos)
    {
        return CreateTripleAngleFromCos(thirdAngleCos.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateTripleAngleFromCos(IFloat64Scalar thirdAngleCos, LinAngleRange range)
    {
        return CreateTripleAngleFromCos(thirdAngleCos.ScalarValue, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateTripleAngleFromSin(double thirdAngleSin)
    {
        var (angleCos, angleSin) = thirdAngleSin.SinToTripleRadiansCosSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateTripleAngleFromSin(double thirdAngleSin, LinAngleRange range)
    {
        var (angleCos, angleSin) = thirdAngleSin.SinToTripleRadiansCosSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateTripleAngleFromSin(IFloat64Scalar thirdAngleSin)
    {
        return CreateTripleAngleFromCos(thirdAngleSin.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateTripleAngleFromSin(IFloat64Scalar thirdAngleSin, LinAngleRange range)
    {
        return CreateTripleAngleFromCos(thirdAngleSin.ScalarValue, range);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateFromVector(double x, double y)
    {
        var r = Math.Sqrt(x * x + y * y);

        Debug.Assert(r.IsValid() && r.IsFinite());

        return r.IsZero() 
            ? Angle0 
            : new LinFloat64DirectedAngle(x / r, y / r);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateFromVector(double x, double y, LinAngleRange range)
    {
        var r = Math.Sqrt(x * x + y * y);

        Debug.Assert(r.IsValid() && r.IsFinite());

        return r.IsZero() 
            ? Angle0 
            : new LinFloat64DirectedAngle(x / r, y / r, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateFromVector(Float64Scalar x, Float64Scalar y)
    {
        return CreateFromVector(x.ScalarValue, y.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateFromVector(Float64Scalar x, Float64Scalar y, LinAngleRange range)
    {
        return CreateFromVector(x.ScalarValue, y.ScalarValue, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateFromVector(IFloat64Scalar x, IFloat64Scalar y)
    {
        return CreateFromVector(x.ScalarValue, y.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateFromVector(IFloat64Scalar x, IFloat64Scalar y, LinAngleRange range)
    {
        return CreateFromVector(x.ScalarValue, y.ScalarValue, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateFromVector(IPair<double> vector)
    {
        return CreateFromVector(vector.Item1, vector.Item2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateFromVector(IPair<double> vector, LinAngleRange range)
    {
        return CreateFromVector(vector.Item1, vector.Item2, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateFromVector(IPair<Float64Scalar> vector)
    {
        return CreateFromVector(vector.Item1.ScalarValue, vector.Item2.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateFromVector(IPair<Float64Scalar> vector, LinAngleRange range)
    {
        return CreateFromVector(vector.Item1.ScalarValue, vector.Item2.ScalarValue, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateFromUnitVectors(IPair<Float64Scalar> v1, IPair<Float64Scalar> v2)
    {
        return CreateFromCos(v1.VectorESp(v2).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateFromUnitVectors(IPair<Float64Scalar> v1, IPair<Float64Scalar> v2, LinAngleRange range)
    {
        return CreateFromCos(v1.VectorESp(v2).ScalarValue, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateFromUnitVectors(ITriplet<Float64Scalar> v1, ITriplet<Float64Scalar> v2)
    {
        return CreateFromCos(v1.VectorESp(v2).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateFromUnitVectors(ITriplet<Float64Scalar> v1, ITriplet<Float64Scalar> v2, LinAngleRange range)
    {
        return CreateFromCos(v1.VectorESp(v2).ScalarValue, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateFromUnitVectors(IQuad<Float64Scalar> v1, IQuad<Float64Scalar> v2)
    {
        return CreateFromCos(v1.VectorESp(v2).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CreateFromUnitVectors(IQuad<Float64Scalar> v1, IQuad<Float64Scalar> v2, LinAngleRange range)
    {
        return CreateFromCos(v1.VectorESp(v2).ScalarValue, range);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator double(LinFloat64DirectedAngle angle)
    {
        return angle.RadiansValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator LinFloat64DirectedAngle(double angleInRadians)
    {
        return CreateFromDegrees(
            angleInRadians
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle operator -(LinFloat64DirectedAngle angle)
    {
        return CreateFromRadians(-angle.RadiansValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle operator +(LinFloat64DirectedAngle angle1, LinFloat64Angle angle2)
    {
        return CreateFromRadians(angle1.RadiansValue + angle2.RadiansValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle operator +(LinFloat64Angle angle1, LinFloat64DirectedAngle angle2)
    {
        return CreateFromRadians(angle1.RadiansValue + angle2.RadiansValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle operator +(LinFloat64DirectedAngle angle1, LinFloat64DirectedAngle angle2)
    {
        return CreateFromRadians(angle1.RadiansValue + angle2.RadiansValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle operator +(LinFloat64DirectedAngle angle1, double angleInRadians2)
    {
        return CreateFromRadians(
            angle1.RadiansValue + angleInRadians2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle operator +(LinFloat64DirectedAngle angle1, Float64Scalar angleInRadians2)
    {
        return CreateFromRadians(
            angle1.RadiansValue + angleInRadians2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle operator +(double angleInRadians1, LinFloat64DirectedAngle angle2)
    {
        return CreateFromRadians(
            angleInRadians1 + angle2.RadiansValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle operator +(Float64Scalar angleInRadians1, LinFloat64DirectedAngle angle2)
    {
        return CreateFromRadians(
            angleInRadians1 + angle2.RadiansValue
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle operator -(LinFloat64DirectedAngle angle1, LinFloat64Angle angle2)
    {
        return CreateFromRadians(angle1.RadiansValue - angle2.RadiansValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle operator -(LinFloat64Angle angle1, LinFloat64DirectedAngle angle2)
    {
        return CreateFromRadians(angle1.RadiansValue - angle2.RadiansValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle operator -(LinFloat64DirectedAngle angle1, LinFloat64DirectedAngle angle2)
    {
        return CreateFromRadians(angle1.RadiansValue - angle2.RadiansValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle operator -(LinFloat64DirectedAngle angle1, double angleInRadians2)
    {
        return CreateFromRadians(angle1.RadiansValue - angleInRadians2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle operator -(LinFloat64DirectedAngle angle1, Float64Scalar angleInRadians2)
    {
        return CreateFromRadians(
            angle1.RadiansValue - angleInRadians2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle operator -(double angleInRadians1, LinFloat64DirectedAngle angle2)
    {
        return CreateFromRadians(angleInRadians1 - angle2.RadiansValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle operator -(Float64Scalar angleInRadians1, LinFloat64DirectedAngle angle2)
    {
        return CreateFromRadians(
            angleInRadians1 - angle2.RadiansValue
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle operator *(LinFloat64DirectedAngle angle, int number)
    {
        return CreateFromRadians(angle.RadiansValue * number);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle operator *(LinFloat64DirectedAngle angle, double number)
    {
        return CreateFromRadians(angle.RadiansValue * number);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle operator *(LinFloat64DirectedAngle angle, Float64Scalar number)
    {
        return CreateFromRadians(angle.RadiansValue * number);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle operator *(int number, LinFloat64DirectedAngle angle)
    {
        return CreateFromRadians(number * angle.RadiansValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle operator *(double number, LinFloat64DirectedAngle angle)
    {
        return CreateFromRadians(number * angle.RadiansValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle operator *(Float64Scalar number, LinFloat64DirectedAngle angle)
    {
        return CreateFromRadians(number * angle.RadiansValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle operator /(LinFloat64DirectedAngle angle, int number)
    {
        return CreateFromRadians(angle.RadiansValue / number);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle operator /(LinFloat64DirectedAngle angle, double number)
    {
        return CreateFromRadians(angle.RadiansValue / number);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle operator /(LinFloat64DirectedAngle angle, Float64Scalar number)
    {
        return CreateFromRadians(angle.RadiansValue / number);
    }


    public override double CosValue 
        => Math.Cos(RadiansValue);

    public override double SinValue 
        => Math.Sin(RadiansValue);
    
    public override double TanValue 
        => Math.Tan(RadiansValue);
    
    public override double RadiansValue { get; }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64DirectedAngle(double radians)
    {
        RadiansValue = radians;

        Debug.Assert(IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64DirectedAngle(double x, double y)
    {
        RadiansValue = x.ArcTan2(y);

        Debug.Assert(IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64DirectedAngle(double x, double y, LinAngleRange range)
    {
        RadiansValue = range.GetRadians(x.ArcTan2(y));

        Debug.Assert(IsValid());
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return !RadiansValue.IsNaNOrInfinite() &&
               RadiansValue is >= -PiTimes2 and <= PiTimes2;
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64DirectedAngle NegativeAngle()
    {
        return CreateFromRadians(-RadiansValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64DirectedAngle OppositeAngle()
    {
        return CreateFromRadians(RadiansValue + Pi);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64DirectedAngle AngleAdd(double angle2)
    {
        return CreateFromRadians(RadiansValue + angle2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64DirectedAngle AngleSubtract(double angle2)
    {
        return CreateFromRadians(RadiansValue - angle2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64DirectedAngle AngleTimes(double scalingFactor)
    {
        return CreateFromRadians(RadiansValue * scalingFactor);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64DirectedAngle AngleDivide(double scalingFactor)
    {
        return CreateFromRadians(RadiansValue / scalingFactor);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return $"{DegreesValue:G5} degrees";
    }
}