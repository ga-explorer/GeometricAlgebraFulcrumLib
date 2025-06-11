using System;
using System.Diagnostics;
using System.Numerics;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Random;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Angles;

public static class LinFloat64AngleUtils
{
    
    public static double DegreesToRadians(this int angleInDegrees)
    {
        return angleInDegrees * LinFloat64Angle.DegreeToRadianFactor;
    }
    
    
    public static double DegreesToRadians(this uint angleInDegrees)
    {
        return angleInDegrees * LinFloat64Angle.DegreeToRadianFactor;
    }
    
    
    public static double DegreesToRadians(this long angleInDegrees)
    {
        return angleInDegrees * LinFloat64Angle.DegreeToRadianFactor;
    }
    
    
    public static double DegreesToRadians(this ulong angleInDegrees)
    {
        return angleInDegrees * LinFloat64Angle.DegreeToRadianFactor;
    }

    
    public static double DegreesToRadians(this float angleInDegrees)
    {
        return angleInDegrees * LinFloat64Angle.DegreeToRadianFactor;
    }

    
    public static double DegreesToRadians(this double angleInDegrees)
    {
        return angleInDegrees * LinFloat64Angle.DegreeToRadianFactor;
    }

    
    public static double RadiansToDegrees(this float angle)
    {
        return angle * LinFloat64Angle.RadianToDegreeFactor;
    }

    
    public static double RadiansToDegrees(this double angle)
    {
        return angle * LinFloat64Angle.RadianToDegreeFactor;
    }


    //
    //public static LinFloat64PolarAngle ClampAngleInDegrees(this double value)
    //{
    //    const int maxValue = 360;

    //    var degrees = value switch
    //    {
    //        //value < -maxValue
    //        < -maxValue => value + Math.Ceiling(-value / maxValue) * maxValue,

    //        //-maxValue <= value < 0
    //        < 0 => value + maxValue,

    //        //value > maxValue
    //        > maxValue => value - Math.Truncate(value / maxValue) * maxValue,

    //        //0 <= value <= maxValue
    //        _ => value
    //    };

    //    return LinFloat64PolarAngle.CreateFromDegrees(degrees);
    //}

    //
    //public static LinFloat64PolarAngle ClampAngleInDegrees(this IFloat64Scalar value)
    //{
    //    return value.ScalarValue.ClampAngleInDegrees();
    //}

    //
    //public static LinFloat64PolarAngle ClampAngleInRadians(this double value)
    //{
    //    const double maxValue = (2 * Math.PI);

    //    var radians = value switch
    //    {
    //        //value < -maxValue
    //        < -maxValue => value + Math.Ceiling(-value / maxValue) * maxValue,

    //        //-maxValue <= value < 0
    //        < 0 => value + maxValue,

    //        //value > maxValue
    //        > maxValue => value - Math.Truncate(value / maxValue) * maxValue,

    //        //0 <= value <= maxValue
    //        _ => value
    //    };

    //    return LinFloat64PolarAngle.CreateFromRadians(radians);
    //}

    //
    //public static LinFloat64PolarAngle ClampAngleInRadians(this IFloat64Scalar value)
    //{
    //    return value.ScalarValue.ClampAngleInRadians();
    //}

    //
    //public static LinFloat64Angle ClampNegativeAngle(this double angleInRadian)
    //{
    //    const double maxValue = (2 * Math.PI);

    //    angleInRadian += Math.PI;

    //    var radians = angleInRadian switch
    //    {
    //        //value < -maxValue
    //        < -maxValue => angleInRadian + Math.Ceiling(-angleInRadian / maxValue) * maxValue,

    //        //-maxValue <= value < 0
    //        < 0 => angleInRadian + maxValue,

    //        //value > maxValue
    //        > maxValue => angleInRadian - Math.Truncate(angleInRadian / maxValue) * maxValue,

    //        //0 <= value <= maxValue
    //        _ => angleInRadian
    //    };

    //    return LinFloat64DirectedAngle.CreateFromRadians(radians);
    //}
    

    
    public static double CosToRadiansSin(this double cosAngle)
    {
        Debug.Assert(cosAngle is >= -1 and <= 1);

        return Math.Sqrt(1 - cosAngle * cosAngle);
    }

    
    public static double CosToRadiansTan(this double cosAngle)
    {
        Debug.Assert(cosAngle is >= -1 and <= 1);

        return Math.Sqrt(1 - cosAngle * cosAngle) / cosAngle;
    }


    
    public static double CosToHalfRadiansCos(this double cosAngle)
    {
        Debug.Assert(cosAngle is >= -1 and <= 1);

        return Math.Sqrt((1 + cosAngle) / 2);
    }

    
    public static double CosToHalfRadiansSin(this double cosAngle)
    {
        Debug.Assert(cosAngle is >= -1 and <= 1);

        return Math.Sqrt((1 - cosAngle) / 2);
    }

    
    public static double CosToHalfRadiansTan(this double cosAngle)
    {
        Debug.Assert(cosAngle is >= -1 and <= 1);

        return Math.Sqrt((1 - cosAngle) / (1 + cosAngle));
    }

    
    public static Pair<double> CosToHalfRadiansCosSin(this double cosAngle)
    {
        Debug.Assert(cosAngle is >= -1 and <= 1);

        return new Pair<double>(
            Math.Sqrt((1 + cosAngle) / 2),
            Math.Sqrt((1 - cosAngle) / 2)
        );
    }


    
    public static double CosToDoubleRadiansCos(this double cosAngle)
    {
        Debug.Assert(cosAngle is >= -1 and <= 1);

        return 2 * cosAngle * cosAngle - 1;
    }

    
    public static double CosToDoubleRadiansSin(this double cosAngle)
    {
        Debug.Assert(cosAngle is >= -1 and <= 1);

        var sinAngle = Math.Sqrt(1 - cosAngle * cosAngle);

        return 2 * cosAngle * sinAngle;
    }

    
    public static double CosToDoubleRadiansTan(this double cosAngle)
    {
        Debug.Assert(cosAngle is >= -1 and <= 1);

        var sinAngle = Math.Sqrt(1 - cosAngle * cosAngle);

        var cosDoubleRadians = 2 * cosAngle * cosAngle - 1;
        var sinDoubleRadians = 2 * cosAngle * sinAngle;

        return sinDoubleRadians / cosDoubleRadians;
    }

    
    public static Pair<double> CosToDoubleRadiansCosSin(this double cosAngle)
    {
        Debug.Assert(cosAngle is >= -1 and <= 1);

        var sinAngle = Math.Sqrt(1 - cosAngle * cosAngle);

        var cosDoubleRadians = 2 * cosAngle * cosAngle - 1;
        var sinDoubleRadians = 2 * cosAngle * sinAngle;

        return new Pair<double>(cosDoubleRadians, sinDoubleRadians);
    }


    
    public static double CosToTripleRadiansCos(this double cosAngle)
    {
        Debug.Assert(cosAngle is >= -1 and <= 1);

        return (4 * cosAngle * cosAngle - 3) * cosAngle;
    }

    
    public static double CosToTripleRadiansSin(this double cosAngle)
    {
        Debug.Assert(cosAngle is >= -1 and <= 1);

        return cosAngle.CosToRadiansSin().SinToTripleRadiansSin();
    }

    
    public static double CosToTripleRadiansTan(this double cosAngle)
    {
        Debug.Assert(cosAngle is >= -1 and <= 1);

        return cosAngle.CosToTripleRadiansSin() / 
               cosAngle.CosToTripleRadiansCos();
    }

    
    public static Pair<double> CosToTripleRadiansCosSin(this double cosAngle)
    {
        Debug.Assert(cosAngle is >= -1 and <= 1);

        return new Pair<double>(
            cosAngle.CosToTripleRadiansCos(), 
            cosAngle.CosToTripleRadiansSin()
        );
    }


    
    public static double SinToRadiansCos(this double sinAngle)
    {
        Debug.Assert(sinAngle is >= -1 and <= 1);

        return Math.Sqrt(1 - sinAngle * sinAngle);
    }

    
    public static double SinToRadiansTan(this double sinAngle)
    {
        Debug.Assert(sinAngle is >= -1 and <= 1);

        return sinAngle / Math.Sqrt(1 - sinAngle * sinAngle);
    }


    
    public static double SinToHalfRadiansCos(this double sinAngle)
    {
        Debug.Assert(sinAngle is >= -1 and <= 1);

        return sinAngle.SinToRadiansCos().CosToHalfRadiansCos();
    }

    
    public static double SinToHalfRadiansSin(this double sinAngle)
    {
        Debug.Assert(sinAngle is >= -1 and <= 1);

        return sinAngle.SinToRadiansCos().CosToHalfRadiansSin();
    }

    
    public static double SinToHalfRadiansTan(this double sinAngle)
    {
        Debug.Assert(sinAngle is >= -1 and <= 1);

        return sinAngle.SinToRadiansCos().CosToHalfRadiansTan();
    }

    
    public static Pair<double> SinToHalfRadiansCosSin(this double sinAngle)
    {
        Debug.Assert(sinAngle is >= -1 and <= 1);

        return sinAngle.SinToRadiansCos().CosToHalfRadiansCosSin();
    }


    
    public static double SinToDoubleRadiansCos(this double sinAngle)
    {
        Debug.Assert(sinAngle is >= -1 and <= 1);

        return 1 - 2 * sinAngle * sinAngle;
    }

    
    public static double SinToDoubleRadiansSin(this double sinAngle)
    {
        Debug.Assert(sinAngle is >= -1 and <= 1);

        var cosAngle = Math.Sqrt(1 - sinAngle * sinAngle);

        return 2 * sinAngle * cosAngle;
    }

    
    public static double SinToDoubleRadiansTan(this double sinAngle)
    {
        Debug.Assert(sinAngle is >= -1 and <= 1);

        var cosAngle = Math.Sqrt(1 - sinAngle * sinAngle);

        var cosDoubleRadians = 1 - 2 * sinAngle * sinAngle;
        var sinDoubleRadians = 2 * sinAngle * cosAngle;

        return sinDoubleRadians / cosDoubleRadians;
    }

    
    public static Pair<double> SinToDoubleRadiansCosSin(this double sinAngle)
    {
        Debug.Assert(sinAngle is >= -1 and <= 1);

        var cosAngle = Math.Sqrt(1 - sinAngle * sinAngle);

        var cosDoubleRadians = 1 - 2 * sinAngle * sinAngle;
        var sinDoubleRadians = 2 * sinAngle * cosAngle;

        return new Pair<double>(cosDoubleRadians, sinDoubleRadians);
    }


    
    public static double SinToTripleRadiansCos(this double sinAngle)
    {
        Debug.Assert(sinAngle is >= -1 and <= 1);

        return sinAngle.SinToRadiansCos().CosToTripleRadiansCos();
    }

    
    public static double SinToTripleRadiansSin(this double sinAngle)
    {
        Debug.Assert(sinAngle is >= -1 and <= 1);

        return (3 - 4 * sinAngle * sinAngle) * sinAngle;
    }

    
    public static double SinToTripleRadiansTan(this double sinAngle)
    {
        Debug.Assert(sinAngle is >= -1 and <= 1);

        return sinAngle.SinToTripleRadiansSin() / 
               sinAngle.SinToTripleRadiansCos();
    }

    
    public static Pair<double> SinToTripleRadiansCosSin(this double sinAngle)
    {
        Debug.Assert(sinAngle is >= -1 and <= 1);

        return new Pair<double>(
            sinAngle.SinToTripleRadiansCos(), 
            sinAngle.SinToTripleRadiansSin()
        );
    }

    
    
    public static double TanToRadiansCos(this double angleTan)
    {
        var angleTanSign = Math.Sign(angleTan);
        var angleTanSquared = angleTan * angleTan;
        var d = 1 + angleTanSquared;

        return angleTanSign / d;
    }

    
    public static double TanToRadiansSin(this double angleTan)
    {
        var angleTanSquared = angleTan * angleTan;
        var d = 1 + angleTanSquared;

        return angleTanSquared / d;
    }

    
    public static Pair<double> TanToRadiansCosSin(this double angleTan)
    {
        var angleTanSign = Math.Sign(angleTan);
        var angleTanSquared = angleTan * angleTan;
        var d = 1 + angleTanSquared;

        var cosValue = angleTanSign / d;
        var sinValue = angleTanSquared / d;

        return new Pair<double>(cosValue, sinValue);
    }

    
    
    public static double TanSquaredToRadiansCos(this double angleTanSquared)
    {
        return 1 / (1 + angleTanSquared);
    }

    
    public static double TanSquaredToRadiansSin(this double angleTanSquared)
    {
        return angleTanSquared / (1 + angleTanSquared);
    }

    
    public static Pair<double> TanSquaredToRadiansCosSin(this double angleTanSquared)
    {
        var d = 1 + angleTanSquared;

        var cosValue = 1 / d;
        var sinValue = angleTanSquared / d;

        return new Pair<double>(cosValue, sinValue);
    }
    
    
    
    public static double TanToDoubleRadiansCos(this double angleTan)
    {
        var angleTanSquared = angleTan * angleTan;

        return (1 - angleTanSquared) / (1 + angleTanSquared);
    }

    
    public static double TanToDoubleRadiansSin(this double angleTan)
    {
        var angleTanSquared = angleTan * angleTan;

        return 2 * angleTan / (1 + angleTanSquared);
    }
    
    
    public static double TanToDoubleRadiansTan(this double angleTan)
    {
        return 2 * angleTan / (1 - angleTan * angleTan);
    }

    
    public static Pair<double> TanToDoubleRadiansCosSin(this double angleTan)
    {
        var angleTanSquared = angleTan * angleTan;
        var cosValue = (1 - angleTanSquared) / (1 + angleTanSquared);
        var sinValue = 2 * angleTan / (1 + angleTanSquared);

        return new Pair<double>(cosValue, sinValue);
    }


    
    public static double CosToRadiansSin(this IFloat64Scalar cosAngle)
    {
        return cosAngle.ScalarValue.CosToRadiansSin();
    }
    
    
    public static double CosToRadiansTan(this IFloat64Scalar cosAngle)
    {
        return cosAngle.ScalarValue.CosToRadiansTan();
    }

    
    
    public static double CosToHalfRadiansCos(this IFloat64Scalar cosAngle)
    {
        return cosAngle.ScalarValue.CosToHalfRadiansCos();
    }
    
    
    public static double CosToHalfRadiansSin(this IFloat64Scalar cosAngle)
    {
        return cosAngle.ScalarValue.CosToHalfRadiansSin();
    }
    
    
    public static double CosToHalfRadiansTan(this IFloat64Scalar cosAngle)
    {
        return cosAngle.ScalarValue.CosToHalfRadiansTan();
    }

    
    public static Pair<double> CosToHalfRadiansCosSin(this IFloat64Scalar cosAngle)
    {
        return cosAngle.ScalarValue.CosToHalfRadiansCosSin();
    }
    

    
    public static double CosToDoubleRadiansCos(this IFloat64Scalar cosAngle)
    {
        return cosAngle.ScalarValue.CosToDoubleRadiansCos();
    }
    
    
    public static double CosToDoubleRadiansSin(this IFloat64Scalar cosAngle)
    {
        return cosAngle.ScalarValue.CosToDoubleRadiansSin();
    }
    
    
    public static double CosToDoubleRadiansTan(this IFloat64Scalar cosAngle)
    {
        return cosAngle.ScalarValue.CosToDoubleRadiansTan();
    }
    
    
    public static Pair<double> CosToDoubleRadiansCosSin(this IFloat64Scalar cosAngle)
    {
        return cosAngle.ScalarValue.CosToDoubleRadiansCosSin();
    }

    
    
    public static double CosToTripleRadiansCos(this IFloat64Scalar cosAngle)
    {
        return cosAngle.ScalarValue.CosToTripleRadiansCos();
    }
    
    
    public static double CosToTripleRadiansSin(this IFloat64Scalar cosAngle)
    {
        return cosAngle.ScalarValue.CosToTripleRadiansSin();
    }
    
    
    public static double CosToTripleRadiansTan(this IFloat64Scalar cosAngle)
    {
        return cosAngle.ScalarValue.CosToTripleRadiansTan();
    }
    
    
    public static Pair<double> CosToTripleRadiansCosSin(this IFloat64Scalar cosAngle)
    {
        return cosAngle.ScalarValue.CosToTripleRadiansCosSin();
    }


    
    public static double SinToRadiansCos(this IFloat64Scalar sinAngle)
    {
        return sinAngle.ScalarValue.SinToRadiansCos();
    }
    
    
    public static double SinToRadiansTan(this IFloat64Scalar sinAngle)
    {
        return sinAngle.ScalarValue.SinToRadiansTan();
    }

    
    
    public static double SinToHalfRadiansCos(this IFloat64Scalar sinAngle)
    {
        return sinAngle.ScalarValue.SinToHalfRadiansCos();
    }
    
    
    public static double SinToHalfRadiansSin(this IFloat64Scalar sinAngle)
    {
        return sinAngle.ScalarValue.SinToHalfRadiansSin();
    }
    
    
    public static double SinToHalfRadiansTan(this IFloat64Scalar sinAngle)
    {
        return sinAngle.ScalarValue.SinToHalfRadiansTan();
    }

    
    public static Pair<double> SinToHalfRadiansCosSin(this IFloat64Scalar sinAngle)
    {
        return sinAngle.ScalarValue.SinToHalfRadiansCosSin();
    }

    
    
    public static double SinToDoubleRadiansCos(this IFloat64Scalar sinAngle)
    {
        return sinAngle.ScalarValue.SinToDoubleRadiansCos();
    }
    
    
    public static double SinToDoubleRadiansSin(this IFloat64Scalar sinAngle)
    {
        return sinAngle.ScalarValue.SinToDoubleRadiansSin();
    }
    
    
    public static double SinToDoubleRadiansTan(this IFloat64Scalar sinAngle)
    {
        return sinAngle.ScalarValue.SinToDoubleRadiansTan();
    }
    
    
    public static Pair<double> SinToDoubleRadiansCosSin(this IFloat64Scalar sinAngle)
    {
        return sinAngle.ScalarValue.SinToDoubleRadiansCosSin();
    }


    
    public static double SinToTripleRadiansCos(this IFloat64Scalar sinAngle)
    {
        return sinAngle.ScalarValue.SinToTripleRadiansCos();
    }

    
    public static double SinToTripleRadiansSin(this IFloat64Scalar sinAngle)
    {
        return sinAngle.ScalarValue.SinToTripleRadiansSin();
    }
    
    
    public static double SinToTripleRadiansTan(this IFloat64Scalar sinAngle)
    {
        return sinAngle.ScalarValue.SinToTripleRadiansTan();
    }
    
    
    public static Pair<double> SinToTripleRadiansCosSin(this IFloat64Scalar sinAngle)
    {
        return sinAngle.ScalarValue.SinToTripleRadiansCosSin();
    }

    
    
    public static double TanToRadiansCos(this IFloat64Scalar tanAngle)
    {
        return tanAngle.ScalarValue.TanToRadiansCos();
    }
    
    
    public static double TanToRadiansSin(this IFloat64Scalar tanAngle)
    {
        return tanAngle.ScalarValue.TanToRadiansSin();
    }

    
    public static Pair<double> TanToRadiansCosSin(this IFloat64Scalar tanAngle)
    {
        return tanAngle.ScalarValue.TanToRadiansCosSin();
    }

    
    
    public static double TanSquaredToRadiansCos(this IFloat64Scalar angleTanSquared)
    {
        return angleTanSquared.ScalarValue.TanSquaredToRadiansCos();
    }

    
    public static double TanSquaredToRadiansSin(this IFloat64Scalar angleTanSquared)
    {
        return angleTanSquared.ScalarValue.TanSquaredToRadiansSin();
    }

    
    public static Pair<double> TanSquaredToRadiansCosSin(this IFloat64Scalar angleTanSquared)
    {
        return angleTanSquared.ScalarValue.TanSquaredToRadiansCosSin();
    }
    
    
    
    public static double TanToDoubleRadiansCos(this IFloat64Scalar angleTan)
    {
        return angleTan.ScalarValue.TanToDoubleRadiansCos();
    }

    
    public static double TanToDoubleRadiansSin(this IFloat64Scalar angleTan)
    {
        return angleTan.ScalarValue.TanToDoubleRadiansSin();
    }

    
    public static Pair<double> TanToDoubleRadiansCosSin(this IFloat64Scalar angleTan)
    {
        return angleTan.ScalarValue.TanToDoubleRadiansCosSin();
    }


    public static double GetRadians(this LinAngleRange range, double angleInRadians)
    {
        // A full-range angle is in the range [-360, 360] degrees
        var radians =
            angleInRadians switch
            {
                < -LinFloat64Angle.PiTimes2 => angleInRadians % LinFloat64Angle.PiTimes4 + LinFloat64Angle.PiTimes2,
                > LinFloat64Angle.PiTimes2 => angleInRadians % LinFloat64Angle.PiTimes2,
                _ => angleInRadians
            };

        if (range == LinAngleRange.Positive360)
        {
            // A positive-range angle is in the range [0, 360] degrees
            if (radians < 0)
                radians += LinFloat64Angle.PiTimes2;
        }

        else if (range == LinAngleRange.Negative360)
        {
            // A negative-range angle is in the range [-360, 0] degrees
            if (radians > 0)
                radians -= LinFloat64Angle.PiTimes2;
        }

        else if (range == LinAngleRange.Symmetric180)
        {
            // A symmetric-range angle is in the range [-180, 180] degrees
            if (radians < -LinFloat64Angle.Pi)
                radians += LinFloat64Angle.PiTimes2;

            else if (radians > LinFloat64Angle.Pi)
                radians -= LinFloat64Angle.PiTimes2;
        }

        return radians;
    }

    public static double GetDegrees(this LinAngleRange range, double angleInDegrees)
    {
        // A full-range angle is in the range [-360, 360] degrees
        var degrees =
            angleInDegrees switch
            {
                < -360d => angleInDegrees % 720d + 360d,
                > 360 => angleInDegrees % 360d,
                _ => angleInDegrees
            };

        if (range == LinAngleRange.Positive360)
        {
            // A positive-range angle is in the range [0, 360] degrees
            if (degrees < 0)
                degrees += 360;
        }

        else if (range == LinAngleRange.Negative360)
        {
            // A negative-range angle is in the range [-360, 0] degrees
            if (degrees > 0)
                degrees -= 360;
        }

        else if (range == LinAngleRange.Symmetric180)
        {
            // A symmetric-range angle is in the range [-180, 180] degrees
            if (degrees < -180)
                degrees += 360;

            else if (degrees > 180)
                degrees -= 360;
        }

        return degrees;
    }
    
    
    public static LinFloat64DirectedAngle GetDirectedAngleFromRadians(this LinAngleRange range, double angleInRadians)
    {
        var radians = range.GetRadians(angleInRadians);

        return LinFloat64DirectedAngle.CreateFromRadians(radians);
    }
    
    
    public static LinFloat64DirectedAngle GetDirectedAngleFromDegrees(this LinAngleRange range, double angleInDegrees)
    {
        var radians = range.GetDegrees(angleInDegrees);

        return LinFloat64DirectedAngle.CreateFromDegrees(radians);
    }

    
    public static LinFloat64PolarAngle GetPolarAngleFromRadians(this LinAngleRange range, double angleInRadians)
    {
        var radians = range.GetRadians(angleInRadians);

        return LinFloat64PolarAngle.CreateFromRadians(radians);
    }
    
    
    public static LinFloat64PolarAngle GetPolarAngleFromDegrees(this LinAngleRange range, double angleInDegrees)
    {
        var radians = range.GetDegrees(angleInDegrees);

        return LinFloat64PolarAngle.CreateFromDegrees(radians);
    }


    
    public static LinFloat64PolarAngle GetPolarAngle(this Random randomGenerator)
    {
        var radians = randomGenerator.GetFloat64(-2, 2) * Math.PI;

        return LinFloat64PolarAngle.CreateFromRadians(radians);
    }
    
    
    public static LinFloat64PolarAngle GetPolarAngleInQuadrant(this Random randomGenerator, int quadrantIndex)
    {
        var radians = (randomGenerator.GetFloat64(0, 1) + quadrantIndex % 4) * Math.PI / 2;

        return LinFloat64PolarAngle.CreateFromRadians(radians);
    }
    
    
    public static LinFloat64PolarAngle GetPolarAngleFromArcRatio(this Random randomGenerator, double maxAngleRatio)
    {
        Debug.Assert(maxAngleRatio <= 1);

        return (randomGenerator.GetFloat64(0, maxAngleRatio) * (2 * Math.PI)).RadiansToPolarAngle();
    }

    
    public static LinFloat64PolarAngle GetPolarAngleFromArcRatio(this Random randomGenerator, double minAngleRatio, double maxAngleRatio)
    {
        Debug.Assert(minAngleRatio >= -1 && maxAngleRatio <= 1);

        return (randomGenerator.GetFloat64(minAngleRatio, maxAngleRatio) * (2 * Math.PI)).RadiansToPolarAngle();
    }


    
    public static LinFloat64DirectedAngle GetDirectedAngle(this Random randomGenerator)
    {
        return (randomGenerator.GetFloat64(-2, 2) * Math.PI).RadiansToDirectedAngle();
    }

    
    public static LinFloat64DirectedAngle GetDirectedAngleInQuadrant(this Random randomGenerator, int quadrantIndex)
    {
        return ((randomGenerator.GetFloat64(0, 1) + quadrantIndex % 4) * Math.PI / 2).RadiansToDirectedAngle();
    }
    
    
    public static LinFloat64DirectedAngle GetDirectedAngleFromRatio(this Random randomGenerator, double maxAngleRatio)
    {
        Debug.Assert(maxAngleRatio <= 1);

        return (randomGenerator.GetFloat64(0, maxAngleRatio) * (2 * Math.PI)).RadiansToDirectedAngle();
    }

    
    public static LinFloat64DirectedAngle GetDirectedAngleFromRatio(this Random randomGenerator, double minAngleRatio, double maxAngleRatio)
    {
        Debug.Assert(minAngleRatio >= -1 && maxAngleRatio <= 1);

        return (randomGenerator.GetFloat64(minAngleRatio, maxAngleRatio) * (2 * Math.PI)).RadiansToDirectedAngle();
    }
    
    

    
    
    public static LinFloat64DirectedAngle GetPhaseAsDirectedAngle(this IPair<double> vector)
    {
        var r = vector.VectorENorm();

        return r.IsZero() 
            ? LinFloat64DirectedAngle.Angle0 
            : LinFloat64DirectedAngle.CreateFromCosSin(
                vector.Item1 / r, 
                vector.Item2 / r
            );
    }
    
    
    public static LinFloat64DirectedAngle GetPhaseAsDirectedAngle(this IPair<double> vector, LinAngleRange range)
    {
        var r = vector.VectorENorm();

        return r.IsZero() 
            ? LinFloat64DirectedAngle.Angle0 
            : LinFloat64DirectedAngle.CreateFromCosSin(
                vector.Item1 / r, 
                vector.Item2 / r,
                range
            );
    }

    
    public static LinFloat64PolarAngle GetPhaseAsPolarAngle(this IPair<double> vector)
    {
        var r = vector.VectorENorm();

        return r.IsZero() 
            ? LinFloat64PolarAngle.Angle0 
            : LinFloat64PolarAngle.CreateFromCosSin(
                vector.Item1 / r, 
                vector.Item2 / r
            );
    }


    
    public static LinFloat64DirectedAngle GetPhaseAsDirectedAngle(this Complex complexNumber)
    {
        var r = complexNumber.Magnitude;

        return r.IsZero() 
            ? LinFloat64DirectedAngle.Angle0 
            : LinFloat64DirectedAngle.CreateFromCosSin(
                complexNumber.Real / r, 
                complexNumber.Imaginary / r
            );
    }
    
    
    public static LinFloat64DirectedAngle GetPhaseAsDirectedAngle(this Complex complexNumber, LinAngleRange range)
    {
        var r = complexNumber.Magnitude;

        return r.IsZero() 
            ? LinFloat64DirectedAngle.Angle0 
            : LinFloat64DirectedAngle.CreateFromCosSin(
                complexNumber.Real / r, 
                complexNumber.Imaginary / r,
                range
            );
    }

    
    public static LinFloat64PolarAngle GetPhaseAsPolarAngle(this Complex complexNumber)
    {
        var r = complexNumber.Magnitude;

        return r.IsZero() 
            ? LinFloat64PolarAngle.Angle0 
            : LinFloat64PolarAngle.CreateFromCosSin(
                complexNumber.Real / r, 
                complexNumber.Imaginary / r
            );
    }


    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    
    public static double GetAngleCosWithUnit(this IPair<double> v1, IPair<double> v2)
    {
        Debug.Assert(
            v2.IsNearUnit()
        );

        var t1 =
            v1.Item1 * v2.Item1 +
            v1.Item2 * v2.Item2;

        var t2 = Math.Sqrt(
            v1.Item1 * v1.Item1 +
            v1.Item2 * v1.Item2
        );

        return Float64Utils.Clamp(t1 / t2, -1d, 1d);
    }
    
    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <returns></returns>
    
    public static double GetAngleCosWithE1(this ITriplet<double> v1)
    {
        var t1 =
            v1.Item1;

        var t2 = Math.Sqrt(
            v1.Item1 * v1.Item1 +
            v1.Item2 * v1.Item2 +
            v1.Item3 * v1.Item3
        );

        return Float64Utils.Clamp(t1 / t2, -1d, 1d);
    }
    
    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <returns></returns>
    
    public static double GetAngleCosWithE2(this ITriplet<double> v1)
    {
        var t1 =
            v1.Item2;

        var t2 = Math.Sqrt(
            v1.Item1 * v1.Item1 +
            v1.Item2 * v1.Item2 +
            v1.Item3 * v1.Item3
        );

        return Float64Utils.Clamp(t1 / t2, -1d, 1d);
    }
    
    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <returns></returns>
    
    public static double GetAngleCosWithE3(this ITriplet<double> v1)
    {
        var t1 =
            v1.Item3;

        var t2 = Math.Sqrt(
            v1.Item1 * v1.Item1 +
            v1.Item2 * v1.Item2 +
            v1.Item3 * v1.Item3
        );

        return Float64Utils.Clamp(t1 / t2, -1d, 1d);
    }

    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    
    public static double GetAngleCosWithUnit(this ITriplet<double> v1, ITriplet<double> v2)
    {
        Debug.Assert(
            v2.IsNearUnit()
        );

        var t1 =
            v1.Item1 * v2.Item1 +
            v1.Item2 * v2.Item2 +
            v1.Item3 * v2.Item3;

        var t2 = Math.Sqrt(
            v1.Item1 * v1.Item1 +
            v1.Item2 * v1.Item2 +
            v1.Item3 * v1.Item3
        );

        return Float64Utils.Clamp(t1 / t2, -1d, 1d);
    }
    
    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    
    public static double GetAngleCosWithUnit(this IQuad<double> v1, IQuad<double> v2)
    {
        Debug.Assert(
            v2.IsNearUnit()
        );

        var t1 =
            v1.Item1 * v2.Item1 +
            v1.Item2 * v2.Item2 +
            v1.Item3 * v2.Item3 +
            v1.Item4 * v2.Item4;

        var t2 = Math.Sqrt(
            v1.Item1 * v1.Item1 +
            v1.Item2 * v1.Item2 +
            v1.Item3 * v1.Item3 +
            v1.Item4 * v1.Item4
        );

        return Float64Utils.Clamp(t1 / t2, -1d, 1d);
    }


    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    
    public static double GetUnitVectorsAngleCos(this IPair<double> v1, IPair<double> v2)
    {
        Debug.Assert(
            v1.IsNearUnitVector() &&
            v2.IsNearUnitVector()
        );

        return Float64Utils.Clamp(
            v1.Item1 * v2.Item1 + 
            v1.Item2 * v2.Item2,
            -1,
            1
        );
    }

    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    
    public static double GetUnitVectorsAngleCos(this ITriplet<double> v1, ITriplet<double> v2)
    {
        Debug.Assert(
            v1.IsNearUnitVector() &&
            v2.IsNearUnitVector()
        );

        return Float64Utils.Clamp(v1.Item1 * v2.Item1 +
                                  v1.Item2 * v2.Item2 +
                                  v1.Item3 * v2.Item3
            , -1, 1);
    }
    
    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    
    public static double GetUnitVectorsAngleCos(this IQuad<double> v1, IQuad<double> v2)
    {
        Debug.Assert(
            v1.IsNearUnitVector() &&
            v2.IsNearUnitVector()
        );

        return Float64Utils.Clamp(
            v1.Item1 * v2.Item1 +
                  v1.Item2 * v2.Item2 +
                  v1.Item3 * v2.Item3 +
                  v1.Item4 * v2.Item4, 
            -1, 
            1
        );
    }

    
    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    
    public static double GetAngleCos(this IPair<double> v1, IPair<double> v2)
    {
        var t1 = v1.Item1 * v2.Item1 + v1.Item2 * v2.Item2;
        var t2 = v1.Item1 * v1.Item1 + v1.Item2 * v1.Item2;
        var t3 = v2.Item1 * v2.Item1 + v2.Item2 * v2.Item2;

        return (t1 / (t2 * t3).Sqrt()).Clamp(-1d, 1d);
    }
    
    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    
    public static double GetAngleCos(this ITriplet<double> v1, LinBasisVector v2)
    {
        return (v1.GetComponent(v2) / v1.VectorENorm()).Clamp(-1d, 1d);
    }

    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    
    public static double GetAngleCos(this ITriplet<double> v1, ITriplet<double> v2)
    {
        var t1 = v1.Item1 * v2.Item1 + v1.Item2 * v2.Item2 + v1.Item3 * v2.Item3;
        var t2 = v1.Item1 * v1.Item1 + v1.Item2 * v1.Item2 + v1.Item3 * v1.Item3;
        var t3 = v2.Item1 * v2.Item1 + v2.Item2 * v2.Item2 + v2.Item3 * v2.Item3;

        return (t1 / Math.Sqrt(t2 * t3)).Clamp(-1d, 1d);
    }
    
    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    
    public static double GetAngleCos(this IQuad<double> v1, IQuad<double> v2)
    {
        var t1 = v1.Item1 * v2.Item1 + v1.Item2 * v2.Item2 + v1.Item3 * v2.Item3 + v1.Item4 * v2.Item4;
        var t2 = v1.Item1 * v1.Item1 + v1.Item2 * v1.Item2 + v1.Item3 * v1.Item3 + v1.Item4 * v1.Item4;
        var t3 = v2.Item1 * v2.Item1 + v2.Item2 * v2.Item2 + v2.Item3 * v2.Item3 + v2.Item4 * v2.Item4;

        return (t1 / Math.Sqrt(t2 * t3)).Clamp(-1d, 1d);
    }


    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    
    public static LinFloat64PolarAngle GetAngleWithUnit(this IPair<double> v1, IPair<double> v2)
    {
        return v1.GetAngleCosWithUnit(v2).CosToPolarAngle();
    }
    
    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <returns></returns>
    
    public static LinFloat64PolarAngle GetAngleWithE1(this ITriplet<double> v1)
    {
        return v1.GetAngleCosWithE1().CosToPolarAngle();
    }
    
    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <returns></returns>
    
    public static LinFloat64PolarAngle GetAngleWithE2(this ITriplet<double> v1)
    {
        return v1.GetAngleCosWithE2().CosToPolarAngle();
    }

    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <returns></returns>
    
    public static LinFloat64PolarAngle GetAngleWithE3(this ITriplet<double> v1)
    {
        return v1.GetAngleCosWithE3().CosToPolarAngle();
    }

    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    
    public static LinFloat64PolarAngle GetAngleWithUnit(this ITriplet<double> v1, ITriplet<double> v2)
    {
        return v1.GetAngleCosWithUnit(v2).CosToPolarAngle();
    }
    
    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    
    public static LinFloat64PolarAngle GetAngleWithUnit(this IQuad<double> v1, IQuad<double> v2)
    {
        return v1.GetAngleCosWithUnit(v2).CosToPolarAngle();
    }
    

    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    
    public static LinFloat64PolarAngle GetUnitVectorsAngle(this IPair<double> v1, IPair<double> v2)
    {
        return v1.GetUnitVectorsAngleCos(v2).CosToPolarAngle();
    }

    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    
    public static LinFloat64PolarAngle GetUnitVectorsAngle(this ITriplet<double> v1, ITriplet<double> v2)
    {
        return v1.GetUnitVectorsAngleCos(v2).CosToPolarAngle().RadiansToPolarAngle();
    }
    
    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    
    public static LinFloat64PolarAngle GetUnitVectorsAngle(this IQuad<double> v1, IQuad<double> v2)
    {
        return v1.GetUnitVectorsAngleCos(v2).CosToPolarAngle().RadiansToPolarAngle();
    }
    

    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    
    public static LinFloat64PolarAngle GetAngle(this IPair<double> v1, IPair<double> v2)
    {
        return v1.GetAngleCos(v2).CosToPolarAngle();
    }

    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    
    public static LinFloat64PolarAngle GetAngle(this ITriplet<double> v1, ITriplet<double> v2)
    {
        return v1.GetAngleCos(v2).CosToPolarAngle();
    }
    
    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    
    public static LinFloat64PolarAngle GetAngle(this IQuad<double> v1, IQuad<double> v2)
    {
        return v1.GetAngleCos(v2).CosToPolarAngle();
    }
    

    /// <summary>
    /// Find the angle between points (p1, p0, p2); i.e. p0 is the head of the angle
    /// </summary>
    /// <param name="p0"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    
    public static LinFloat64PolarAngle GetAngleFromPoints(this IPair<double> p0, IPair<double> p1, IPair<double> p2)
    {
        var v1 = LinFloat64Vector2D.Create(
            p1.Item1 - p0.Item1,
            p1.Item2 - p0.Item2
        );

        var v2 = LinFloat64Vector2D.Create(
            p2.Item1 - p0.Item1,
            p2.Item2 - p0.Item2
        );

        return v1.GetAngle(v2);
    }

    /// <summary>
    /// Find the angle between points (p1, p0, p2); i.e. p0 is the head of the angle
    /// </summary>
    /// <param name="p0"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    
    public static LinFloat64PolarAngle GetAngleFromPoints(this ITriplet<double> p0, ITriplet<double> p1, ITriplet<double> p2)
    {
        var v1 = LinFloat64Vector3D.Create(
            p1.Item1 - p0.Item1,
            p1.Item2 - p0.Item2,
            p1.Item3 - p0.Item3
        );

        var v2 = LinFloat64Vector3D.Create(
            p2.Item1 - p0.Item1,
            p2.Item2 - p0.Item2,
            p2.Item3 - p0.Item3
        );

        return v1.GetAngle(v2);
    }
    
    /// <summary>
    /// Find the angle between points (p1, p0, p2); i.e. p0 is the head of the angle
    /// </summary>
    /// <param name="p0"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    
    public static LinFloat64PolarAngle GetAngleFromPoints(this IQuad<double> p0, IQuad<double> p1, IQuad<double> p2)
    {
        var v1 = LinFloat64Vector4D.Create(
            p1.Item1 - p0.Item1,
            p1.Item2 - p0.Item2,
            p1.Item3 - p0.Item3,
            p1.Item4 - p0.Item4
        );

        var v2 = LinFloat64Vector4D.Create(
            p2.Item1 - p0.Item1,
            p2.Item2 - p0.Item2,
            p2.Item3 - p0.Item3,
            p2.Item4 - p0.Item4
        );

        return v1.GetAngle(v2);
    }

    
    /// <summary>
    /// Find the directed angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    
    public static LinFloat64DirectedAngle GetDirectedAngle(this IPair<double> v1, IPair<double> v2)
    {
        var angle1 = v1.GetPhaseAsDirectedAngle();
        var angle2 = v2.GetPhaseAsDirectedAngle();

        return angle2.AngleSubtract(angle1.RadiansValue);

        //var angle1 = Math.Atan2(v1.Item2.ScalarValue, v1.Item1.ScalarValue);
        //var angle2 = Math.Atan2(v2.Item2.ScalarValue, v2.Item1.ScalarValue);

        //return (angle2 - angle1).RadiansToDirectedAngle();
    }

    /// <summary>
    /// Find the directed angle from three points ABC
    /// </summary>
    /// <param name="pointA"></param>
    /// <param name="pointB"></param>
    /// <param name="pointC"></param>
    /// <returns></returns>
    
    public static LinFloat64DirectedAngle GetDirectedAngleFromPoints(this IPair<double> pointA, IPair<double> pointB, IPair<double> pointC)
    {
        return GetDirectedAngle(
            pointA.VectorSubtract(pointB), 
            pointC.VectorSubtract(pointB)
        );

        //var v1 = pointA.Subtract(pointB).ToUnitComplexNumber();
        //var v2 = pointC.Subtract(pointB).ToUnitComplexNumber();

        //return (v2 / v1).GetPhaseAsPolarAngle().ToDirectedAngle();
    }


    
    public static LinFloat64PolarAngle CosToPolarAngle(this double angleCos)
    {
        return LinFloat64PolarAngle.CreateFromCos(angleCos);
    }
    
    
    public static LinFloat64PolarAngle CosToPolarAngle(this IFloat64Scalar angleCos)
    {
        return LinFloat64PolarAngle.CreateFromCos(angleCos.ScalarValue);
    }

    
    public static LinFloat64PolarAngle SinToPolarAngle(this double angleSin)
    {
        return LinFloat64PolarAngle.CreateFromSin(angleSin);
    }
    
    
    public static LinFloat64PolarAngle SinToPolarAngle(this IFloat64Scalar angleSin)
    {
        return LinFloat64PolarAngle.CreateFromSin(angleSin.ScalarValue);
    }

    
    public static LinFloat64PolarAngle TanToPolarAngle(this double angleTan)
    {
        return LinFloat64PolarAngle.CreateFromTan(angleTan);
    }
    
    
    public static LinFloat64PolarAngle TanToPolarAngle(this IFloat64Scalar angleTan)
    {
        return LinFloat64PolarAngle.CreateFromTan(angleTan.ScalarValue);
    }
    
    
    public static LinFloat64PolarAngle CosToDoublePolarAngle(this double angleCos)
    {
        return LinFloat64PolarAngle.CreateDoubleAngleFromCos(angleCos);
    }

    
    public static LinFloat64PolarAngle CosToDoublePolarAngle(this IFloat64Scalar angleCos)
    {
        return LinFloat64PolarAngle.CreateDoubleAngleFromCos(angleCos.ScalarValue);
    }
    
    
    public static LinFloat64PolarAngle SinToDoublePolarAngle(this double angleSin)
    {
        return LinFloat64PolarAngle.CreateDoubleAngleFromSin(angleSin);
    }

    
    public static LinFloat64PolarAngle SinToDoublePolarAngle(this IFloat64Scalar angleSin)
    {
        return LinFloat64PolarAngle.CreateDoubleAngleFromSin(angleSin.ScalarValue);
    }
    
    
    public static LinFloat64PolarAngle TanToDoublePolarAngle(this double angleTan)
    {
        return LinFloat64PolarAngle.CreateDoubleAngleFromTan(angleTan);
    }

    
    public static LinFloat64PolarAngle TanToDoublePolarAngle(this IFloat64Scalar angleTan)
    {
        return LinFloat64PolarAngle.CreateDoubleAngleFromTan(angleTan.ScalarValue);
    }
    
    
    public static LinFloat64PolarAngle CosToHalfPolarAngle(this double angleCos)
    {
        return LinFloat64PolarAngle.CreateHalfAngleFromCos(angleCos);
    }
    
    
    public static LinFloat64PolarAngle CosToHalfPolarAngle(this IFloat64Scalar angleCos)
    {
        return LinFloat64PolarAngle.CreateHalfAngleFromCos(angleCos.ScalarValue);
    }

    
    public static LinFloat64PolarAngle DegreesToPolarAngle(this int angleInDegrees)
    {
        return LinFloat64PolarAngle.CreateFromDegrees(angleInDegrees);
    }

    
    public static LinFloat64PolarAngle DegreesToPolarAngle(this float angleInDegrees)
    {
        return LinFloat64PolarAngle.CreateFromDegrees(angleInDegrees);
    }

    
    public static LinFloat64PolarAngle DegreesToPolarAngle(this double angleInDegrees)
    {
        return LinFloat64PolarAngle.CreateFromDegrees(angleInDegrees);
    }
    
    
    public static LinFloat64PolarAngle DegreesToPolarAngle(this IFloat64Scalar angleInDegrees)
    {
        return LinFloat64PolarAngle.CreateFromDegrees(angleInDegrees.ScalarValue);
    }

    
    
    public static LinFloat64DirectedAngle CosToDirectedAngle(this double angleCos)
    {
        return LinFloat64DirectedAngle.CreateFromCos(angleCos);
    }
    
    
    public static LinFloat64DirectedAngle CosToDirectedAngle(this IFloat64Scalar angleCos)
    {
        return LinFloat64DirectedAngle.CreateFromCos(angleCos);
    }

    
    public static LinFloat64DirectedAngle SinToDirectedAngle(this double angleSin)
    {
        return LinFloat64DirectedAngle.CreateFromSin(angleSin);
    }
    
    
    public static LinFloat64DirectedAngle SinToDirectedAngle(this IFloat64Scalar angleSin)
    {
        return LinFloat64DirectedAngle.CreateFromSin(angleSin);
    }

    
    public static LinFloat64DirectedAngle TanToDirectedAngle(this double angleTan)
    {
        return LinFloat64DirectedAngle.CreateFromTan(angleTan);
    }
    
    
    public static LinFloat64DirectedAngle TanToDirectedAngle(this IFloat64Scalar angleTan)
    {
        return LinFloat64DirectedAngle.CreateFromTan(angleTan);
    }
    
    
    public static LinFloat64DirectedAngle CosToDoubleDirectedAngle(this double angleCos)
    {
        return LinFloat64DirectedAngle.CreateDoubleAngleFromCos(angleCos);
    }

    
    public static LinFloat64DirectedAngle CosToDoubleDirectedAngle(this IFloat64Scalar angleCos)
    {
        return LinFloat64DirectedAngle.CreateDoubleAngleFromCos(angleCos.ScalarValue);
    }
    
    
    public static LinFloat64DirectedAngle SinToDoubleDirectedAngle(this double angleSin)
    {
        return LinFloat64DirectedAngle.CreateDoubleAngleFromSin(angleSin);
    }

    
    public static LinFloat64DirectedAngle SinToDoubleDirectedAngle(this IFloat64Scalar angleSin)
    {
        return LinFloat64DirectedAngle.CreateDoubleAngleFromSin(angleSin.ScalarValue);
    }
    
    
    public static LinFloat64DirectedAngle TanToDoubleDirectedAngle(this double angleTan)
    {
        return LinFloat64DirectedAngle.CreateDoubleAngleFromTan(angleTan);
    }

    
    public static LinFloat64DirectedAngle TanToDoubleDirectedAngle(this IFloat64Scalar angleTan)
    {
        return LinFloat64DirectedAngle.CreateDoubleAngleFromTan(angleTan.ScalarValue);
    }

    
    public static LinFloat64DirectedAngle DegreesToDirectedAngle(this int angleInDegrees)
    {
        return LinFloat64DirectedAngle.CreateFromDegrees(angleInDegrees);
    }

    
    public static LinFloat64DirectedAngle DegreesToDirectedAngle(this int angleInDegrees, LinAngleRange range)
    {
        return LinFloat64DirectedAngle.CreateFromDegrees(angleInDegrees, range);
    }
    
    
    public static LinFloat64DirectedAngle DegreesToDirectedAngle(this float angleInDegrees)
    {
        return LinFloat64DirectedAngle.CreateFromDegrees(angleInDegrees);
    }

    
    public static LinFloat64DirectedAngle DegreesToDirectedAngle(this float angleInDegrees, LinAngleRange range)
    {
        return LinFloat64DirectedAngle.CreateFromDegrees(angleInDegrees, range);
    }

    
    public static LinFloat64DirectedAngle DegreesToDirectedAngle(this double angleInDegrees)
    {
        return LinFloat64DirectedAngle.CreateFromDegrees(angleInDegrees);
    }

    
    public static LinFloat64DirectedAngle DegreesToDirectedAngle(this double angleInDegrees, LinAngleRange range)
    {
        return LinFloat64DirectedAngle.CreateFromDegrees(angleInDegrees, range);
    }
    
    
    public static LinFloat64DirectedAngle DegreesToDirectedAngle(this IFloat64Scalar angleInDegrees)
    {
        return LinFloat64DirectedAngle.CreateFromDegrees(angleInDegrees.ScalarValue);
    }

    
    public static LinFloat64DirectedAngle DegreesToDirectedAngle(this IFloat64Scalar angleInDegrees, LinAngleRange range)
    {
        return LinFloat64DirectedAngle.CreateFromDegrees(angleInDegrees.ScalarValue, range);
    }

    
    
    public static LinFloat64PolarAngle RadiansToPolarAngle(this float angleInRadians)
    {
        return LinFloat64PolarAngle.CreateFromRadians(angleInRadians);
    }
    
    
    public static LinFloat64PolarAngle RadiansToPolarAngle(this double angleInRadians)
    {
        return LinFloat64PolarAngle.CreateFromRadians(angleInRadians);
    }
    
    
    public static LinFloat64PolarAngle RadiansToPolarAngle(this IFloat64Scalar angleInRadians)
    {
        return LinFloat64PolarAngle.CreateFromRadians(angleInRadians.ScalarValue);
    }


    
    public static LinFloat64DirectedAngle RadiansToDirectedAngle(this float angleInRadians)
    {
        return LinFloat64DirectedAngle.CreateFromRadians(angleInRadians);
    }

    
    public static LinFloat64DirectedAngle RadiansToDirectedAngle(this float angleInRadians, LinAngleRange range)
    {
        return LinFloat64DirectedAngle.CreateFromRadians(angleInRadians, range);
    }

    
    public static LinFloat64DirectedAngle RadiansToDirectedAngle(this double angleInRadians)
    {
        return LinFloat64DirectedAngle.CreateFromRadians(angleInRadians);
    }

    
    public static LinFloat64DirectedAngle RadiansToDirectedAngle(this double angleInRadians, LinAngleRange range)
    {
        return LinFloat64DirectedAngle.CreateFromRadians(angleInRadians, range);
    }
    
    
    public static LinFloat64DirectedAngle RadiansToDirectedAngle(this IFloat64Scalar angleInRadians)
    {
        return LinFloat64DirectedAngle.CreateFromRadians(angleInRadians.ScalarValue);
    }

    
    public static LinFloat64DirectedAngle RadiansToDirectedAngle(this IFloat64Scalar angleInRadians, LinAngleRange range)
    {
        return LinFloat64DirectedAngle.CreateFromRadians(angleInRadians.ScalarValue, range);
    }


    
    public static LinFloat64DirectedAngle Lerp(this double t, LinFloat64Angle angle1, LinFloat64Angle angle2)
    {
        Debug.Assert(t.IsValid() && t is >= 0 and <= 1);

        return ((1.0d - t) * angle1.DegreesValue + t * angle2.DegreesValue).DegreesToDirectedAngle();
    }

    
    public static LinFloat64DirectedAngle Lerp(this double t, LinFloat64Angle angle2)
    {
        Debug.Assert(t.IsValid() && t is >= 0 and <= 1);

        return (t * angle2.DegreesValue).DegreesToDirectedAngle();
    }

    
}