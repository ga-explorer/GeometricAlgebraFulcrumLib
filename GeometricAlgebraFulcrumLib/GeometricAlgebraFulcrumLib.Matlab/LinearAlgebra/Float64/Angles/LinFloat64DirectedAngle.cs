using System;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Angles;

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

    
    public static LinFloat64DirectedAngle CreateFromDegrees(double angleInDegrees, LinAngleRange range)
    {
        var degrees = range.GetDegrees(angleInDegrees);

        return new LinFloat64DirectedAngle(
            degrees * DegreeToRadianFactor
        );
    }
    
    
    public static LinFloat64DirectedAngle CreatePositiveFromDegrees(double angleInDegrees)
    {
        var radians = LinAngleRange.Positive360.GetDegrees(angleInDegrees) * DegreeToRadianFactor;

        return new LinFloat64DirectedAngle(radians);
    }
    
    
    public static LinFloat64DirectedAngle CreateNegativeFromDegrees(double angleInDegrees)
    {
        var radians = LinAngleRange.Negative360.GetDegrees(angleInDegrees) * DegreeToRadianFactor;

        return new LinFloat64DirectedAngle(radians);
    }

    
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

    
    public static LinFloat64DirectedAngle CreateFromRadians(double angleInRadians, LinAngleRange range)
    {
        var radians = range.GetRadians(angleInRadians);

        return new LinFloat64DirectedAngle(radians);
    }
    
    
    public static LinFloat64DirectedAngle CreatePositiveFromRadians(double angleInRadians)
    {
        var radians = LinAngleRange.Positive360.GetRadians(angleInRadians);

        return new LinFloat64DirectedAngle(radians);
    }
    
    
    public static LinFloat64DirectedAngle CreateNegativeFromRadians(double angleInRadians)
    {
        var radians = LinAngleRange.Negative360.GetRadians(angleInRadians);

        return new LinFloat64DirectedAngle(radians);
    }

    
    
    public static LinFloat64DirectedAngle CreateFromCosSin(double angleCos, double angleSin)
    {
        return new LinFloat64DirectedAngle(angleCos, angleSin);
    }
    
    
    public static LinFloat64DirectedAngle CreateFromCosSin(double angleCos, double angleSin, LinAngleRange range)
    {
        return new LinFloat64DirectedAngle(angleCos, angleSin, range);
    }

    
    public static LinFloat64DirectedAngle CreateFromCos(double angleCos)
    {
        var angleSin = angleCos.CosToRadiansSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin);
    }
    
    
    public static LinFloat64DirectedAngle CreateFromCos(double angleCos, LinAngleRange range)
    {
        var angleSin = angleCos.CosToRadiansSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin, range);
    }

    
    public static LinFloat64DirectedAngle CreateFromCos(IFloat64Scalar angleCos)
    {
        return CreateFromCos(angleCos.ScalarValue);
    }
    
    
    public static LinFloat64DirectedAngle CreateFromCos(IFloat64Scalar angleCos, LinAngleRange range)
    {
        return CreateFromCos(angleCos.ScalarValue, range);
    }

    
    public static LinFloat64DirectedAngle CreateFromSin(double angleSin)
    {
        var angleCos = angleSin.SinToRadiansCos();

        return new LinFloat64DirectedAngle(angleCos, angleSin);
    }
    
    
    public static LinFloat64DirectedAngle CreateFromSin(double angleSin, LinAngleRange range)
    {
        var angleCos = angleSin.SinToRadiansCos();

        return new LinFloat64DirectedAngle(angleCos, angleSin, range);
    }

    
    public static LinFloat64DirectedAngle CreateFromSin(IFloat64Scalar angleSin)
    {
        return CreateFromSin(angleSin.ScalarValue);
    }
    
    
    public static LinFloat64DirectedAngle CreateFromSin(IFloat64Scalar angleSin, LinAngleRange range)
    {
        return CreateFromSin(angleSin.ScalarValue, range);
    }

    
    public static LinFloat64DirectedAngle CreateFromTan(double angleTan)
    {
        var (angleCos, angleSin) = angleTan.TanToRadiansCosSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin);
    }
    
    
    public static LinFloat64DirectedAngle CreateFromTan(double angleTan, LinAngleRange range)
    {
        var (angleCos, angleSin) = angleTan.TanToRadiansCosSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin, range);
    }
    
    
    public static LinFloat64DirectedAngle CreateFromTan(IFloat64Scalar angleTan)
    {
        return CreateFromTan(angleTan.ScalarValue);
    }
    
    
    public static LinFloat64DirectedAngle CreateFromTan(IFloat64Scalar angleTan, LinAngleRange range)
    {
        return CreateFromTan(angleTan.ScalarValue, range);
    }

    /// <summary>
    /// This assumes the angle is in the first quadrant
    /// </summary>
    /// <param name="angleTanSquared"></param>
    /// <returns></returns>
    
    public static LinFloat64DirectedAngle CreateFromTanSquared(double angleTanSquared)
    {
        var (angleCos, angleSin) = angleTanSquared.TanSquaredToRadiansCosSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin);
    }
    
    
    public static LinFloat64DirectedAngle CreateFromTanSquared(double angleTanSquared, LinAngleRange range)
    {
        var (angleCos, angleSin) = angleTanSquared.TanSquaredToRadiansCosSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin, range);
    }
    
    
    public static LinFloat64DirectedAngle CreateFromTanSquared(IFloat64Scalar angleTan)
    {
        return CreateFromTanSquared(angleTan.ScalarValue);
    }
    
    
    public static LinFloat64DirectedAngle CreateFromTanSquared(IFloat64Scalar angleTan, LinAngleRange range)
    {
        return CreateFromTanSquared(angleTan.ScalarValue, range);
    }


    
    public static LinFloat64DirectedAngle CreateHalfAngleFromCosSin(double doubleAngleCos, double doubleAngleSin)
    {
        var (angleCos, angleSin) = doubleAngleCos.CosToHalfRadiansCosSin();

        if (doubleAngleSin < 0) angleCos = -angleCos;

        return new LinFloat64DirectedAngle(angleCos, angleSin);
    }
    
    
    public static LinFloat64DirectedAngle CreateHalfAngleFromCosSin(double doubleAngleCos, double doubleAngleSin, LinAngleRange range)
    {
        var (angleCos, angleSin) = doubleAngleCos.CosToHalfRadiansCosSin();

        if (doubleAngleSin < 0) angleCos = -angleCos;

        return new LinFloat64DirectedAngle(angleCos, angleSin, range);
    }

    
    public static LinFloat64DirectedAngle CreateHalfAngleFromCos(double doubleAngleCos)
    {
        var (angleCos, angleSin) = doubleAngleCos.CosToHalfRadiansCosSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin);
    }
    
    
    public static LinFloat64DirectedAngle CreateHalfAngleFromCos(double doubleAngleCos, LinAngleRange range)
    {
        var (angleCos, angleSin) = doubleAngleCos.CosToHalfRadiansCosSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin, range);
    }

    
    public static LinFloat64DirectedAngle CreateHalfAngleFromCos(IFloat64Scalar halfAngleCos)
    {
        return CreateHalfAngleFromCos(halfAngleCos.ScalarValue);
    }
    
    
    public static LinFloat64DirectedAngle CreateHalfAngleFromCos(IFloat64Scalar halfAngleCos, LinAngleRange range)
    {
        return CreateHalfAngleFromCos(halfAngleCos.ScalarValue, range);
    }

    
    public static LinFloat64DirectedAngle CreateHalfAngleFromSin(double doubleAngleSin)
    {
        var (angleCos, angleSin) = doubleAngleSin.SinToHalfRadiansCosSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin);
    }
    
    
    public static LinFloat64DirectedAngle CreateHalfAngleFromSin(double doubleAngleSin, LinAngleRange range)
    {
        var (angleCos, angleSin) = doubleAngleSin.SinToHalfRadiansCosSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin, range);
    }

    
    public static LinFloat64DirectedAngle CreateHalfAngleFromSin(IFloat64Scalar halfAngleSin)
    {
        return CreateHalfAngleFromSin(halfAngleSin.ScalarValue);
    }
    
    
    public static LinFloat64DirectedAngle CreateHalfAngleFromSin(IFloat64Scalar halfAngleSin, LinAngleRange range)
    {
        return CreateHalfAngleFromSin(halfAngleSin.ScalarValue, range);
    }


    
    public static LinFloat64DirectedAngle CreateDoubleAngleFromCosSin(double halfAngleCos, double halfAngleSin)
    {
        var angleCos = 2 * halfAngleCos * halfAngleCos - 1;
        var angleSin = 2 * halfAngleCos * halfAngleSin;

        return new LinFloat64DirectedAngle(angleCos, angleSin);
    }
    
    
    public static LinFloat64DirectedAngle CreateDoubleAngleFromCosSin(double halfAngleCos, double halfAngleSin, LinAngleRange range)
    {
        var angleCos = 2 * halfAngleCos * halfAngleCos - 1;
        var angleSin = 2 * halfAngleCos * halfAngleSin;

        return new LinFloat64DirectedAngle(angleCos, angleSin, range);
    }

    
    public static LinFloat64DirectedAngle CreateDoubleAngleFromCos(double halfAngleCos)
    {
        var (angleCos, angleSin) = halfAngleCos.CosToDoubleRadiansCosSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin);
    }
    
    
    public static LinFloat64DirectedAngle CreateDoubleAngleFromCos(double halfAngleCos, LinAngleRange range)
    {
        var (angleCos, angleSin) = halfAngleCos.CosToDoubleRadiansCosSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin, range);
    }

    
    public static LinFloat64DirectedAngle CreateDoubleAngleFromCos(IFloat64Scalar halfAngleCos)
    {
        return CreateDoubleAngleFromCos(halfAngleCos.ScalarValue);
    }
    
    
    public static LinFloat64DirectedAngle CreateDoubleAngleFromCos(IFloat64Scalar halfAngleCos, LinAngleRange range)
    {
        return CreateDoubleAngleFromCos(halfAngleCos.ScalarValue, range);
    }

    
    public static LinFloat64DirectedAngle CreateDoubleAngleFromSin(double halfAngleSin)
    {
        var (angleCos, angleSin) = halfAngleSin.SinToDoubleRadiansCosSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin);
    }
    
    
    public static LinFloat64DirectedAngle CreateDoubleAngleFromSin(double halfAngleSin, LinAngleRange range)
    {
        var (angleCos, angleSin) = halfAngleSin.SinToDoubleRadiansCosSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin, range);
    }

    
    public static LinFloat64DirectedAngle CreateDoubleAngleFromSin(IFloat64Scalar halfAngleSin)
    {
        return CreateDoubleAngleFromSin(halfAngleSin.ScalarValue);
    }
    
    
    public static LinFloat64DirectedAngle CreateDoubleAngleFromSin(IFloat64Scalar halfAngleSin, LinAngleRange range)
    {
        return CreateDoubleAngleFromSin(halfAngleSin.ScalarValue, range);
    }

    
    public static LinFloat64DirectedAngle CreateDoubleAngleFromTan(double halfAngleTan)
    {
        var (angleCos, angleSin) = halfAngleTan.TanToDoubleRadiansCosSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin);
    }
    
    
    public static LinFloat64DirectedAngle CreateDoubleAngleFromTan(double halfAngleTan, LinAngleRange range)
    {
        var (angleCos, angleSin) = halfAngleTan.TanToDoubleRadiansCosSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin, range);
    }

    
    public static LinFloat64DirectedAngle CreateDoubleAngleFromTan(IFloat64Scalar halfAngleSin)
    {
        return CreateDoubleAngleFromTan(halfAngleSin.ScalarValue);
    }
    
    
    public static LinFloat64DirectedAngle CreateDoubleAngleFromTan(IFloat64Scalar halfAngleSin, LinAngleRange range)
    {
        return CreateDoubleAngleFromTan(halfAngleSin.ScalarValue, range);
    }


    
    public static LinFloat64DirectedAngle CreateTripleAngleFromCosSin(double thirdAngleCos, double thirdAngleSin)
    {
        var angleCos = thirdAngleCos.CosToTripleRadiansCos();
        var angleSin = thirdAngleSin.SinToTripleRadiansSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin);
    }
    
    
    public static LinFloat64DirectedAngle CreateTripleAngleFromCosSin(double thirdAngleCos, double thirdAngleSin, LinAngleRange range)
    {
        var angleCos = thirdAngleCos.CosToTripleRadiansCos();
        var angleSin = thirdAngleSin.SinToTripleRadiansSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin, range);
    }

    
    public static LinFloat64DirectedAngle CreateTripleAngleFromCos(double thirdAngleCos)
    {
        var (angleCos, angleSin) = thirdAngleCos.CosToTripleRadiansCosSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin);
    }
    
    
    public static LinFloat64DirectedAngle CreateTripleAngleFromCos(double thirdAngleCos, LinAngleRange range)
    {
        var (angleCos, angleSin) = thirdAngleCos.CosToTripleRadiansCosSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin, range);
    }

    
    public static LinFloat64DirectedAngle CreateTripleAngleFromCos(IFloat64Scalar thirdAngleCos)
    {
        return CreateTripleAngleFromCos(thirdAngleCos.ScalarValue);
    }
    
    
    public static LinFloat64DirectedAngle CreateTripleAngleFromCos(IFloat64Scalar thirdAngleCos, LinAngleRange range)
    {
        return CreateTripleAngleFromCos(thirdAngleCos.ScalarValue, range);
    }

    
    public static LinFloat64DirectedAngle CreateTripleAngleFromSin(double thirdAngleSin)
    {
        var (angleCos, angleSin) = thirdAngleSin.SinToTripleRadiansCosSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin);
    }
    
    
    public static LinFloat64DirectedAngle CreateTripleAngleFromSin(double thirdAngleSin, LinAngleRange range)
    {
        var (angleCos, angleSin) = thirdAngleSin.SinToTripleRadiansCosSin();

        return new LinFloat64DirectedAngle(angleCos, angleSin, range);
    }

    
    public static LinFloat64DirectedAngle CreateTripleAngleFromSin(IFloat64Scalar thirdAngleSin)
    {
        return CreateTripleAngleFromCos(thirdAngleSin.ScalarValue);
    }
    
    
    public static LinFloat64DirectedAngle CreateTripleAngleFromSin(IFloat64Scalar thirdAngleSin, LinAngleRange range)
    {
        return CreateTripleAngleFromCos(thirdAngleSin.ScalarValue, range);
    }


    
    public static LinFloat64DirectedAngle CreateFromVector(double x, double y)
    {
        var r = Math.Sqrt(x * x + y * y);

        Debug.Assert(r.IsValid() && r.IsFinite());

        return r.IsZero() 
            ? Angle0 
            : new LinFloat64DirectedAngle(x / r, y / r);
    }
    
    
    public static LinFloat64DirectedAngle CreateFromVector(double x, double y, LinAngleRange range)
    {
        var r = Math.Sqrt(x * x + y * y);

        Debug.Assert(r.IsValid() && r.IsFinite());

        return r.IsZero() 
            ? Angle0 
            : new LinFloat64DirectedAngle(x / r, y / r, range);
    }

    
    public static LinFloat64DirectedAngle CreateFromVector(IFloat64Scalar x, IFloat64Scalar y)
    {
        return CreateFromVector(x.ScalarValue, y.ScalarValue);
    }
    
    
    public static LinFloat64DirectedAngle CreateFromVector(IFloat64Scalar x, IFloat64Scalar y, LinAngleRange range)
    {
        return CreateFromVector(x.ScalarValue, y.ScalarValue, range);
    }

    
    public static LinFloat64DirectedAngle CreateFromVector(IPair<double> vector)
    {
        return CreateFromVector(vector.Item1, vector.Item2);
    }
    
    
    public static LinFloat64DirectedAngle CreateFromVector(IPair<double> vector, LinAngleRange range)
    {
        return CreateFromVector(vector.Item1, vector.Item2, range);
    }
    
    
    public static LinFloat64DirectedAngle CreateFromUnitVectors(IPair<double> v1, IPair<double> v2)
    {
        return CreateFromCos(v1.VectorESp(v2));
    }
    
    
    public static LinFloat64DirectedAngle CreateFromUnitVectors(IPair<double> v1, IPair<double> v2, LinAngleRange range)
    {
        return CreateFromCos(v1.VectorESp(v2), range);
    }

    
    public static LinFloat64DirectedAngle CreateFromUnitVectors(ITriplet<double> v1, ITriplet<double> v2)
    {
        return CreateFromCos(v1.VectorESp(v2));
    }
    
    
    public static LinFloat64DirectedAngle CreateFromUnitVectors(ITriplet<double> v1, ITriplet<double> v2, LinAngleRange range)
    {
        return CreateFromCos(v1.VectorESp(v2), range);
    }

    
    public static LinFloat64DirectedAngle CreateFromUnitVectors(IQuad<double> v1, IQuad<double> v2)
    {
        return CreateFromCos(v1.VectorESp(v2));
    }
    
    
    public static LinFloat64DirectedAngle CreateFromUnitVectors(IQuad<double> v1, IQuad<double> v2, LinAngleRange range)
    {
        return CreateFromCos(v1.VectorESp(v2), range);
    }


    
    public static implicit operator double(LinFloat64DirectedAngle angle)
    {
        return angle.RadiansValue;
    }

    
    public static implicit operator LinFloat64DirectedAngle(double angleInRadians)
    {
        return CreateFromDegrees(
            angleInRadians
        );
    }


    
    public static LinFloat64DirectedAngle operator -(LinFloat64DirectedAngle angle)
    {
        return CreateFromRadians(-angle.RadiansValue);
    }

    
    
    public static LinFloat64DirectedAngle operator +(LinFloat64DirectedAngle angle1, LinFloat64Angle angle2)
    {
        return CreateFromRadians(angle1.RadiansValue + angle2.RadiansValue);
    }

    
    public static LinFloat64DirectedAngle operator +(LinFloat64Angle angle1, LinFloat64DirectedAngle angle2)
    {
        return CreateFromRadians(angle1.RadiansValue + angle2.RadiansValue);
    }

    
    public static LinFloat64DirectedAngle operator +(LinFloat64DirectedAngle angle1, LinFloat64DirectedAngle angle2)
    {
        return CreateFromRadians(angle1.RadiansValue + angle2.RadiansValue);
    }

    
    public static LinFloat64DirectedAngle operator +(LinFloat64DirectedAngle angle1, double angleInRadians2)
    {
        return CreateFromRadians(
            angle1.RadiansValue + angleInRadians2
        );
    }

    
    public static LinFloat64DirectedAngle operator +(double angleInRadians1, LinFloat64DirectedAngle angle2)
    {
        return CreateFromRadians(
            angleInRadians1 + angle2.RadiansValue
        );
    }

    
    
    public static LinFloat64DirectedAngle operator -(LinFloat64DirectedAngle angle1, LinFloat64Angle angle2)
    {
        return CreateFromRadians(angle1.RadiansValue - angle2.RadiansValue);
    }

    
    public static LinFloat64DirectedAngle operator -(LinFloat64Angle angle1, LinFloat64DirectedAngle angle2)
    {
        return CreateFromRadians(angle1.RadiansValue - angle2.RadiansValue);
    }

    
    public static LinFloat64DirectedAngle operator -(LinFloat64DirectedAngle angle1, LinFloat64DirectedAngle angle2)
    {
        return CreateFromRadians(angle1.RadiansValue - angle2.RadiansValue);
    }

    
    public static LinFloat64DirectedAngle operator -(LinFloat64DirectedAngle angle1, double angleInRadians2)
    {
        return CreateFromRadians(angle1.RadiansValue - angleInRadians2);
    }

    
    public static LinFloat64DirectedAngle operator -(double angleInRadians1, LinFloat64DirectedAngle angle2)
    {
        return CreateFromRadians(angleInRadians1 - angle2.RadiansValue);
    }


    
    public static LinFloat64DirectedAngle operator *(LinFloat64DirectedAngle angle, int number)
    {
        return CreateFromRadians(angle.RadiansValue * number);
    }

    
    public static LinFloat64DirectedAngle operator *(LinFloat64DirectedAngle angle, double number)
    {
        return CreateFromRadians(angle.RadiansValue * number);
    }

    
    public static LinFloat64DirectedAngle operator *(int number, LinFloat64DirectedAngle angle)
    {
        return CreateFromRadians(number * angle.RadiansValue);
    }

    
    public static LinFloat64DirectedAngle operator *(double number, LinFloat64DirectedAngle angle)
    {
        return CreateFromRadians(number * angle.RadiansValue);
    }


    
    public static LinFloat64DirectedAngle operator /(LinFloat64DirectedAngle angle, int number)
    {
        return CreateFromRadians(angle.RadiansValue / number);
    }

    
    public static LinFloat64DirectedAngle operator /(LinFloat64DirectedAngle angle, double number)
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

    
    
    private LinFloat64DirectedAngle(double radians)
    {
        RadiansValue = radians;

        Debug.Assert(IsValid());
    }

    
    private LinFloat64DirectedAngle(double x, double y)
    {
        RadiansValue = x.ArcTan2(y);

        Debug.Assert(IsValid());
    }

    
    private LinFloat64DirectedAngle(double x, double y, LinAngleRange range)
    {
        RadiansValue = range.GetRadians(x.ArcTan2(y));

        Debug.Assert(IsValid());
    }

    
    
    public override bool IsValid()
    {
        return !RadiansValue.IsNaNOrInfinite() &&
               RadiansValue is >= -PiTimes2 and <= PiTimes2;
    }
    

    
    public new LinFloat64DirectedAngle NegativeAngle()
    {
        return CreateFromRadians(-RadiansValue);
    }
    
    
    public new LinFloat64DirectedAngle OppositeAngle()
    {
        return CreateFromRadians(RadiansValue + Pi);
    }

    
    public new LinFloat64DirectedAngle AngleAdd(double angle2)
    {
        return CreateFromRadians(RadiansValue + angle2);
    }

    
    public new LinFloat64DirectedAngle AngleSubtract(double angle2)
    {
        return CreateFromRadians(RadiansValue - angle2);
    }

    
    public new LinFloat64DirectedAngle AngleTimes(double scalingFactor)
    {
        return CreateFromRadians(RadiansValue * scalingFactor);
    }
    
    
    public new LinFloat64DirectedAngle AngleDivide(double scalingFactor)
    {
        return CreateFromRadians(RadiansValue / scalingFactor);
    }

    
    
    public override string ToString()
    {
        return $"{DegreesValue:G5} degrees";
    }
}