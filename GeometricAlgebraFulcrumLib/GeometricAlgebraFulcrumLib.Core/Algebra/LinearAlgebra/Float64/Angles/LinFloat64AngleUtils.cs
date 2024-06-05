using System.Collections.Immutable;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;

public static class LinFloat64AngleUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DegreesToRadians(this int angleInDegrees)
    {
        return angleInDegrees * LinFloat64Angle.DegreeToRadianFactor;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DegreesToRadians(this uint angleInDegrees)
    {
        return angleInDegrees * LinFloat64Angle.DegreeToRadianFactor;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DegreesToRadians(this long angleInDegrees)
    {
        return angleInDegrees * LinFloat64Angle.DegreeToRadianFactor;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DegreesToRadians(this ulong angleInDegrees)
    {
        return angleInDegrees * LinFloat64Angle.DegreeToRadianFactor;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DegreesToRadians(this float angleInDegrees)
    {
        return angleInDegrees * LinFloat64Angle.DegreeToRadianFactor;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DegreesToRadians(this double angleInDegrees)
    {
        return angleInDegrees * LinFloat64Angle.DegreeToRadianFactor;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double RadiansToDegrees(this float angle)
    {
        return angle * LinFloat64Angle.RadianToDegreeFactor;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double RadiansToDegrees(this double angle)
    {
        return angle * LinFloat64Angle.RadianToDegreeFactor;
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
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

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64PolarAngle ClampAngleInDegrees(this IFloat64Scalar value)
    //{
    //    return value.ScalarValue.ClampAngleInDegrees();
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64PolarAngle ClampAngleInRadians(this double value)
    //{
    //    const double maxValue = 2 * Math.PI;

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

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64PolarAngle ClampAngleInRadians(this IFloat64Scalar value)
    //{
    //    return value.ScalarValue.ClampAngleInRadians();
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64Angle ClampNegativeAngle(this double angleInRadian)
    //{
    //    const double maxValue = 2 * Math.PI;

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
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64Angle ClampNegative(this LinFloat64Angle angle)
    //{
    //    const double maxValue = 360d;

    //    var value = angle.DegreesValue + 180d;

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
    public static double CosToRadiansSin(this double cosAngle)
    {
        Debug.Assert(cosAngle is >= -1 and <= 1);

        return Math.Sqrt(1 - cosAngle * cosAngle);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double CosToRadiansTan(this double cosAngle)
    {
        Debug.Assert(cosAngle is >= -1 and <= 1);

        return Math.Sqrt(1 - cosAngle * cosAngle) / cosAngle;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double CosToHalfRadiansCos(this double cosAngle)
    {
        Debug.Assert(cosAngle is >= -1 and <= 1);

        return Math.Sqrt((1 + cosAngle) / 2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double CosToHalfRadiansSin(this double cosAngle)
    {
        Debug.Assert(cosAngle is >= -1 and <= 1);

        return Math.Sqrt((1 - cosAngle) / 2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double CosToHalfRadiansTan(this double cosAngle)
    {
        Debug.Assert(cosAngle is >= -1 and <= 1);

        return Math.Sqrt((1 - cosAngle) / (1 + cosAngle));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<double> CosToHalfRadiansCosSin(this double cosAngle)
    {
        Debug.Assert(cosAngle is >= -1 and <= 1);

        return new Pair<double>(
            Math.Sqrt((1 + cosAngle) / 2),
            Math.Sqrt((1 - cosAngle) / 2)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double CosToDoubleRadiansCos(this double cosAngle)
    {
        Debug.Assert(cosAngle is >= -1 and <= 1);

        return 2 * cosAngle * cosAngle - 1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double CosToDoubleRadiansSin(this double cosAngle)
    {
        Debug.Assert(cosAngle is >= -1 and <= 1);

        var sinAngle = Math.Sqrt(1 - cosAngle * cosAngle);

        return 2 * cosAngle * sinAngle;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double CosToDoubleRadiansTan(this double cosAngle)
    {
        Debug.Assert(cosAngle is >= -1 and <= 1);

        var sinAngle = Math.Sqrt(1 - cosAngle * cosAngle);

        var cosDoubleRadians = 2 * cosAngle * cosAngle - 1;
        var sinDoubleRadians = 2 * cosAngle * sinAngle;

        return sinDoubleRadians / cosDoubleRadians;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<double> CosToDoubleRadiansCosSin(this double cosAngle)
    {
        Debug.Assert(cosAngle is >= -1 and <= 1);

        var sinAngle = Math.Sqrt(1 - cosAngle * cosAngle);

        var cosDoubleRadians = 2 * cosAngle * cosAngle - 1;
        var sinDoubleRadians = 2 * cosAngle * sinAngle;

        return new Pair<double>(cosDoubleRadians, sinDoubleRadians);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double CosToTripleRadiansCos(this double cosAngle)
    {
        Debug.Assert(cosAngle is >= -1 and <= 1);

        return (4 * cosAngle * cosAngle - 3) * cosAngle;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double CosToTripleRadiansSin(this double cosAngle)
    {
        Debug.Assert(cosAngle is >= -1 and <= 1);

        return cosAngle.CosToRadiansSin().SinToTripleRadiansSin();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double CosToTripleRadiansTan(this double cosAngle)
    {
        Debug.Assert(cosAngle is >= -1 and <= 1);

        return cosAngle.CosToTripleRadiansSin() / 
               cosAngle.CosToTripleRadiansCos();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<double> CosToTripleRadiansCosSin(this double cosAngle)
    {
        Debug.Assert(cosAngle is >= -1 and <= 1);

        return new Pair<double>(
            cosAngle.CosToTripleRadiansCos(), 
            cosAngle.CosToTripleRadiansSin()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SinToRadiansCos(this double sinAngle)
    {
        Debug.Assert(sinAngle is >= -1 and <= 1);

        return Math.Sqrt(1 - sinAngle * sinAngle);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SinToRadiansTan(this double sinAngle)
    {
        Debug.Assert(sinAngle is >= -1 and <= 1);

        return sinAngle / Math.Sqrt(1 - sinAngle * sinAngle);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SinToHalfRadiansCos(this double sinAngle)
    {
        Debug.Assert(sinAngle is >= -1 and <= 1);

        return sinAngle.SinToRadiansCos().CosToHalfRadiansCos();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SinToHalfRadiansSin(this double sinAngle)
    {
        Debug.Assert(sinAngle is >= -1 and <= 1);

        return sinAngle.SinToRadiansCos().CosToHalfRadiansSin();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SinToHalfRadiansTan(this double sinAngle)
    {
        Debug.Assert(sinAngle is >= -1 and <= 1);

        return sinAngle.SinToRadiansCos().CosToHalfRadiansTan();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<double> SinToHalfRadiansCosSin(this double sinAngle)
    {
        Debug.Assert(sinAngle is >= -1 and <= 1);

        return sinAngle.SinToRadiansCos().CosToHalfRadiansCosSin();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SinToDoubleRadiansCos(this double sinAngle)
    {
        Debug.Assert(sinAngle is >= -1 and <= 1);

        return 1 - 2 * sinAngle * sinAngle;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SinToDoubleRadiansSin(this double sinAngle)
    {
        Debug.Assert(sinAngle is >= -1 and <= 1);

        var cosAngle = Math.Sqrt(1 - sinAngle * sinAngle);

        return 2 * sinAngle * cosAngle;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SinToDoubleRadiansTan(this double sinAngle)
    {
        Debug.Assert(sinAngle is >= -1 and <= 1);

        var cosAngle = Math.Sqrt(1 - sinAngle * sinAngle);

        var cosDoubleRadians = 1 - 2 * sinAngle * sinAngle;
        var sinDoubleRadians = 2 * sinAngle * cosAngle;

        return sinDoubleRadians / cosDoubleRadians;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<double> SinToDoubleRadiansCosSin(this double sinAngle)
    {
        Debug.Assert(sinAngle is >= -1 and <= 1);

        var cosAngle = Math.Sqrt(1 - sinAngle * sinAngle);

        var cosDoubleRadians = 1 - 2 * sinAngle * sinAngle;
        var sinDoubleRadians = 2 * sinAngle * cosAngle;

        return new Pair<double>(cosDoubleRadians, sinDoubleRadians);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SinToTripleRadiansCos(this double sinAngle)
    {
        Debug.Assert(sinAngle is >= -1 and <= 1);

        return sinAngle.SinToRadiansCos().CosToTripleRadiansCos();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SinToTripleRadiansSin(this double sinAngle)
    {
        Debug.Assert(sinAngle is >= -1 and <= 1);

        return (3 - 4 * sinAngle * sinAngle) * sinAngle;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SinToTripleRadiansTan(this double sinAngle)
    {
        Debug.Assert(sinAngle is >= -1 and <= 1);

        return sinAngle.SinToTripleRadiansSin() / 
               sinAngle.SinToTripleRadiansCos();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<double> SinToTripleRadiansCosSin(this double sinAngle)
    {
        Debug.Assert(sinAngle is >= -1 and <= 1);

        return new Pair<double>(
            sinAngle.SinToTripleRadiansCos(), 
            sinAngle.SinToTripleRadiansSin()
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double TanToRadiansCos(this double angleTan)
    {
        var angleTanSign = Math.Sign(angleTan);
        var angleTanSquared = angleTan * angleTan;
        var d = 1 + angleTanSquared;

        return angleTanSign / d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double TanToRadiansSin(this double angleTan)
    {
        var angleTanSquared = angleTan * angleTan;
        var d = 1 + angleTanSquared;

        return angleTanSquared / d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<double> TanToRadiansCosSin(this double angleTan)
    {
        var angleTanSign = Math.Sign(angleTan);
        var angleTanSquared = angleTan * angleTan;
        var d = 1 + angleTanSquared;

        var cosValue = angleTanSign / d;
        var sinValue = angleTanSquared / d;

        return new Pair<double>(cosValue, sinValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double TanSquaredToRadiansCos(this double angleTanSquared)
    {
        return 1 / (1 + angleTanSquared);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double TanSquaredToRadiansSin(this double angleTanSquared)
    {
        return angleTanSquared / (1 + angleTanSquared);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<double> TanSquaredToRadiansCosSin(this double angleTanSquared)
    {
        var d = 1 + angleTanSquared;

        var cosValue = 1 / d;
        var sinValue = angleTanSquared / d;

        return new Pair<double>(cosValue, sinValue);
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double TanToDoubleRadiansCos(this double angleTan)
    {
        var angleTanSquared = angleTan * angleTan;

        return (1 - angleTanSquared) / (1 + angleTanSquared);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double TanToDoubleRadiansSin(this double angleTan)
    {
        var angleTanSquared = angleTan * angleTan;

        return 2 * angleTan / (1 + angleTanSquared);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double TanToDoubleRadiansTan(this double angleTan)
    {
        return 2 * angleTan / (1 - angleTan * angleTan);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<double> TanToDoubleRadiansCosSin(this double angleTan)
    {
        var angleTanSquared = angleTan * angleTan;
        var cosValue = (1 - angleTanSquared) / (1 + angleTanSquared);
        var sinValue = 2 * angleTan / (1 + angleTanSquared);

        return new Pair<double>(cosValue, sinValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar CosToRadiansSin(this IFloat64Scalar cosAngle)
    {
        return cosAngle.ScalarValue.CosToRadiansSin();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar CosToRadiansTan(this IFloat64Scalar cosAngle)
    {
        return cosAngle.ScalarValue.CosToRadiansTan();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar CosToHalfRadiansCos(this IFloat64Scalar cosAngle)
    {
        return cosAngle.ScalarValue.CosToHalfRadiansCos();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar CosToHalfRadiansSin(this IFloat64Scalar cosAngle)
    {
        return cosAngle.ScalarValue.CosToHalfRadiansSin();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar CosToHalfRadiansTan(this IFloat64Scalar cosAngle)
    {
        return cosAngle.ScalarValue.CosToHalfRadiansTan();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Float64Scalar> CosToHalfRadiansCosSin(this IFloat64Scalar cosAngle)
    {
        return cosAngle.ScalarValue.CosToHalfRadiansCosSin().MapItems(Float64Scalar.Create);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar CosToDoubleRadiansCos(this IFloat64Scalar cosAngle)
    {
        return cosAngle.ScalarValue.CosToDoubleRadiansCos();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar CosToDoubleRadiansSin(this IFloat64Scalar cosAngle)
    {
        return cosAngle.ScalarValue.CosToDoubleRadiansSin();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar CosToDoubleRadiansTan(this IFloat64Scalar cosAngle)
    {
        return cosAngle.ScalarValue.CosToDoubleRadiansTan();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Float64Scalar> CosToDoubleRadiansCosSin(this IFloat64Scalar cosAngle)
    {
        return cosAngle.ScalarValue.CosToDoubleRadiansCosSin().MapItems(Float64Scalar.Create);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar CosToTripleRadiansCos(this IFloat64Scalar cosAngle)
    {
        return cosAngle.ScalarValue.CosToTripleRadiansCos();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar CosToTripleRadiansSin(this IFloat64Scalar cosAngle)
    {
        return cosAngle.ScalarValue.CosToTripleRadiansSin();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar CosToTripleRadiansTan(this IFloat64Scalar cosAngle)
    {
        return cosAngle.ScalarValue.CosToTripleRadiansTan();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Float64Scalar> CosToTripleRadiansCosSin(this IFloat64Scalar cosAngle)
    {
        return cosAngle.ScalarValue.CosToTripleRadiansCosSin().MapItems(Float64Scalar.Create);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar SinToRadiansCos(this IFloat64Scalar sinAngle)
    {
        return sinAngle.ScalarValue.SinToRadiansCos();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar SinToRadiansTan(this IFloat64Scalar sinAngle)
    {
        return sinAngle.ScalarValue.SinToRadiansTan();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar SinToHalfRadiansCos(this IFloat64Scalar sinAngle)
    {
        return sinAngle.ScalarValue.SinToHalfRadiansCos();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar SinToHalfRadiansSin(this IFloat64Scalar sinAngle)
    {
        return sinAngle.ScalarValue.SinToHalfRadiansSin();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar SinToHalfRadiansTan(this IFloat64Scalar sinAngle)
    {
        return sinAngle.ScalarValue.SinToHalfRadiansTan();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Float64Scalar> SinToHalfRadiansCosSin(this IFloat64Scalar sinAngle)
    {
        return sinAngle.ScalarValue.SinToHalfRadiansCosSin().MapItems(Float64Scalar.Create);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar SinToDoubleRadiansCos(this IFloat64Scalar sinAngle)
    {
        return sinAngle.ScalarValue.SinToDoubleRadiansCos();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar SinToDoubleRadiansSin(this IFloat64Scalar sinAngle)
    {
        return sinAngle.ScalarValue.SinToDoubleRadiansSin();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar SinToDoubleRadiansTan(this IFloat64Scalar sinAngle)
    {
        return sinAngle.ScalarValue.SinToDoubleRadiansTan();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Float64Scalar> SinToDoubleRadiansCosSin(this IFloat64Scalar sinAngle)
    {
        return sinAngle.ScalarValue.SinToDoubleRadiansCosSin().MapItems(Float64Scalar.Create);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar SinToTripleRadiansCos(this IFloat64Scalar sinAngle)
    {
        return sinAngle.ScalarValue.SinToTripleRadiansCos();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar SinToTripleRadiansSin(this IFloat64Scalar sinAngle)
    {
        return sinAngle.ScalarValue.SinToTripleRadiansSin();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar SinToTripleRadiansTan(this IFloat64Scalar sinAngle)
    {
        return sinAngle.ScalarValue.SinToTripleRadiansTan();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Float64Scalar> SinToTripleRadiansCosSin(this IFloat64Scalar sinAngle)
    {
        return sinAngle.ScalarValue.SinToTripleRadiansCosSin().MapItems(Float64Scalar.Create);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar TanToRadiansCos(this IFloat64Scalar tanAngle)
    {
        return tanAngle.ScalarValue.TanToRadiansCos();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar TanToRadiansSin(this IFloat64Scalar tanAngle)
    {
        return tanAngle.ScalarValue.TanToRadiansSin();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Float64Scalar> TanToRadiansCosSin(this IFloat64Scalar tanAngle)
    {
        return tanAngle.ScalarValue.TanToRadiansCosSin().MapItems(Float64Scalar.Create);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar TanSquaredToRadiansCos(this IFloat64Scalar angleTanSquared)
    {
        return angleTanSquared.ScalarValue.TanSquaredToRadiansCos();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar TanSquaredToRadiansSin(this IFloat64Scalar angleTanSquared)
    {
        return angleTanSquared.ScalarValue.TanSquaredToRadiansSin();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Float64Scalar> TanSquaredToRadiansCosSin(this IFloat64Scalar angleTanSquared)
    {
        return angleTanSquared.ScalarValue.TanSquaredToRadiansCosSin().MapItems(Float64Scalar.Create);
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar TanToDoubleRadiansCos(this IFloat64Scalar angleTan)
    {
        return angleTan.ScalarValue.TanToDoubleRadiansCos();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar TanToDoubleRadiansSin(this IFloat64Scalar angleTan)
    {
        return angleTan.ScalarValue.TanToDoubleRadiansSin();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Float64Scalar> TanToDoubleRadiansCosSin(this IFloat64Scalar angleTan)
    {
        return angleTan.ScalarValue.TanToDoubleRadiansCosSin().MapItems(Float64Scalar.Create);
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
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle GetDirectedAngleFromRadians(this LinAngleRange range, double angleInRadians)
    {
        var radians = range.GetRadians(angleInRadians);

        return LinFloat64DirectedAngle.CreateFromRadians(radians);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle GetDirectedAngleFromDegrees(this LinAngleRange range, double angleInDegrees)
    {
        var radians = range.GetDegrees(angleInDegrees);

        return LinFloat64DirectedAngle.CreateFromDegrees(radians);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle GetPolarAngleFromRadians(this LinAngleRange range, double angleInRadians)
    {
        var radians = range.GetRadians(angleInRadians);

        return LinFloat64PolarAngle.CreateFromRadians(radians);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle GetPolarAngleFromDegrees(this LinAngleRange range, double angleInDegrees)
    {
        var radians = range.GetDegrees(angleInDegrees);

        return LinFloat64PolarAngle.CreateFromDegrees(radians);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle GetPolarAngle(this Random randomGenerator)
    {
        var radians = randomGenerator.GetNumber(-2, 2) * Math.PI;

        return LinFloat64PolarAngle.CreateFromRadians(radians);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle GetPolarAngleInQuadrant(this Random randomGenerator, int quadrantIndex)
    {
        var radians = (randomGenerator.GetNumber(0, 1) + quadrantIndex % 4) * Math.PI / 2;

        return LinFloat64PolarAngle.CreateFromRadians(radians);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle GetPolarAngleFromArcRatio(this Random randomGenerator, double maxAngleRatio)
    {
        Debug.Assert(maxAngleRatio <= 1);

        return (randomGenerator.GetNumber(0, maxAngleRatio) * Math.PI * 2).RadiansToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle GetPolarAngleFromArcRatio(this Random randomGenerator, double minAngleRatio, double maxAngleRatio)
    {
        Debug.Assert(minAngleRatio >= -1 && maxAngleRatio <= 1);

        return (randomGenerator.GetNumber(minAngleRatio, maxAngleRatio) * Math.PI * 2).RadiansToPolarAngle();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle GetDirectedAngle(this Random randomGenerator)
    {
        return (randomGenerator.GetNumber(-2, 2) * Math.PI).RadiansToDirectedAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle GetDirectedAngleInQuadrant(this Random randomGenerator, int quadrantIndex)
    {
        return ((randomGenerator.GetNumber(0, 1) + quadrantIndex % 4) * Math.PI / 2).RadiansToDirectedAngle();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle GetDirectedAngleFromRatio(this Random randomGenerator, double maxAngleRatio)
    {
        Debug.Assert(maxAngleRatio <= 1);

        return (randomGenerator.GetNumber(0, maxAngleRatio) * Math.PI * 2).RadiansToDirectedAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle GetDirectedAngleFromRatio(this Random randomGenerator, double minAngleRatio, double maxAngleRatio)
    {
        Debug.Assert(minAngleRatio >= -1 && maxAngleRatio <= 1);

        return (randomGenerator.GetNumber(minAngleRatio, maxAngleRatio) * Math.PI * 2).RadiansToDirectedAngle();
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Angle NegativeAngle(this LinFloat64Angle angle)
    {
        return angle switch
        {
            LinFloat64DirectedAngle directedAngle => 
                directedAngle.NegativeAngle(),

            LinFloat64PolarAngle polarAngle => 
                polarAngle.NegativeAngle(),

            _ => throw new InvalidOperationException()
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Angle OppositeAngle(this LinFloat64Angle angle)
    {
        return angle switch
        {
            LinFloat64DirectedAngle directedAngle => 
                directedAngle.OppositeAngle(),

            LinFloat64PolarAngle polarAngle => 
                polarAngle.OppositeAngle(),

            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Angle AngleAdd(this LinFloat64Angle angle, double angle2)
    {
        return angle switch
        {
            LinFloat64DirectedAngle directedAngle => 
                directedAngle.AngleAdd(angle2),

            LinFloat64PolarAngle polarAngle => 
                polarAngle.AngleAdd(angle2),

            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Angle AngleSubtract(this LinFloat64Angle angle, double angle2)
    {
        return angle switch
        {
            LinFloat64DirectedAngle directedAngle => 
                directedAngle.AngleSubtract(angle2),

            LinFloat64PolarAngle polarAngle => 
                polarAngle.AngleSubtract(angle2),

            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Angle AngleTimes(this LinFloat64Angle angle, double scalingFactor)
    {
        return angle switch
        {
            LinFloat64DirectedAngle directedAngle => 
                directedAngle.AngleTimes(scalingFactor),

            LinFloat64PolarAngle polarAngle => 
                polarAngle.AngleTimes(scalingFactor),

            _ => throw new InvalidOperationException()
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Angle AngleDivide(this LinFloat64Angle angle, double scalingFactor)
    {
        return angle switch
        {
            LinFloat64DirectedAngle directedAngle => 
                directedAngle.AngleDivide(scalingFactor),

            LinFloat64PolarAngle polarAngle => 
                polarAngle.AngleDivide(scalingFactor),

            _ => throw new InvalidOperationException()
        };
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64Angle HalfAngle(this LinFloat64Angle angle, LinAngleRange range)
    //{
    //    return angle switch
    //    {
    //        LinFloat64DirectedAngle directedAngle => 
    //            directedAngle.HalfAngle(range),

    //        LinFloat64PolarAngle polarAngle => 
    //            polarAngle.HalfPolarAngle(range),

    //        _ => throw new InvalidOperationException()
    //    };
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64Angle HalfAngle(this LinFloat64Angle angle)
    //{
    //    return angle switch
    //    {
    //        LinFloat64DirectedAngle directedAngle => 
    //            directedAngle.HalfAngle(),

    //        LinFloat64PolarAngle polarAngle => 
    //            polarAngle.HalfAngle(),

    //        _ => throw new InvalidOperationException()
    //    };
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64Angle DoubleAngle(this LinFloat64Angle angle, LinAngleRange range)
    //{
    //    return angle switch
    //    {
    //        LinFloat64DirectedAngle directedAngle => 
    //            directedAngle.DoubleAngle(range),

    //        LinFloat64PolarAngle polarAngle => 
    //            polarAngle.DoublePolarAngle(range),

    //        _ => throw new InvalidOperationException()
    //    };
    //}

    //public static LinFloat64Angle DoubleAngle(this LinFloat64Angle angle)
    //{
    //    return angle switch
    //    {
    //        LinFloat64DirectedAngle directedAngle => 
    //            directedAngle.DoubleAngle(),

    //        LinFloat64PolarAngle polarAngle => 
    //            polarAngle.DoublePolarAngle(),

    //        _ => throw new InvalidOperationException()
    //    };
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64Angle TripleAngle(this LinFloat64Angle angle, LinAngleRange range)
    //{
    //    return angle switch
    //    {
    //        LinFloat64DirectedAngle directedAngle => 
    //            directedAngle.TripleAngle(range),

    //        LinFloat64PolarAngle polarAngle => 
    //            polarAngle.TriplePolarAngle(range),

    //        _ => throw new InvalidOperationException()
    //    };
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64Angle TripleAngle(this LinFloat64Angle angle)
    //{
    //    return angle switch
    //    {
    //        LinFloat64DirectedAngle directedAngle => 
    //            directedAngle.TripleAngle(),

    //        LinFloat64PolarAngle polarAngle => 
    //            polarAngle.TriplePolarAngle(),

    //        _ => throw new InvalidOperationException()
    //    };
    //}

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle GetPhaseAsDirectedAngle(this IPair<Float64Scalar> vector)
    {
        var r = vector.VectorENorm();

        return r.IsZero() 
            ? LinFloat64DirectedAngle.Angle0 
            : LinFloat64DirectedAngle.CreateFromCosSin(
                vector.Item1 / r, 
                vector.Item2 / r
            );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle GetPhaseAsDirectedAngle(this IPair<Float64Scalar> vector, LinAngleRange range)
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle GetPhaseAsPolarAngle(this IPair<Float64Scalar> vector)
    {
        var r = vector.VectorENorm();

        return r.IsZero() 
            ? LinFloat64PolarAngle.Angle0 
            : LinFloat64PolarAngle.CreateFromCosSin(
                vector.Item1 / r, 
                vector.Item2 / r
            );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar GetAngleCosWithUnit(this IPair<Float64Scalar> v1, IPair<Float64Scalar> v2)
    {
        Debug.Assert(
            v2.IsNearUnit()
        );

        var t1 =
            v1.Item1.ScalarValue * v2.Item1.ScalarValue +
            v1.Item2.ScalarValue * v2.Item2.ScalarValue;

        var t2 = Math.Sqrt(
            v1.Item1.ScalarValue * v1.Item1.ScalarValue +
            v1.Item2.ScalarValue * v1.Item2.ScalarValue
        );

        return Float64Utils.Clamp(t1 / t2, -1d, 1d);
    }

    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar GetAngleCosWithUnit(this ITriplet<Float64Scalar> v1, ITriplet<Float64Scalar> v2)
    {
        Debug.Assert(
            v2.IsNearUnit()
        );

        var t1 =
            v1.Item1.ScalarValue * v2.Item1.ScalarValue +
            v1.Item2.ScalarValue * v2.Item2.ScalarValue +
            v1.Item3.ScalarValue * v2.Item3.ScalarValue;

        var t2 = Math.Sqrt(
            v1.Item1.ScalarValue * v1.Item1.ScalarValue +
            v1.Item2.ScalarValue * v1.Item2.ScalarValue +
            v1.Item3.ScalarValue * v1.Item3.ScalarValue
        );

        return Float64Utils.Clamp(t1 / t2, -1d, 1d);
    }
    
    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar GetAngleCosWithUnit(this IQuad<Float64Scalar> v1, IQuad<Float64Scalar> v2)
    {
        Debug.Assert(
            v2.IsNearUnit()
        );

        var t1 =
            v1.Item1.ScalarValue * v2.Item1.ScalarValue +
            v1.Item2.ScalarValue * v2.Item2.ScalarValue +
            v1.Item3.ScalarValue * v2.Item3.ScalarValue +
            v1.Item4.ScalarValue * v2.Item4.ScalarValue;

        var t2 = Math.Sqrt(
            v1.Item1.ScalarValue * v1.Item1.ScalarValue +
            v1.Item2.ScalarValue * v1.Item2.ScalarValue +
            v1.Item3.ScalarValue * v1.Item3.ScalarValue +
            v1.Item4.ScalarValue * v1.Item4.ScalarValue
        );

        return Float64Utils.Clamp(t1 / t2, -1d, 1d);
    }


    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetUnitVectorsAngleCos(this IPair<Float64Scalar> v1, IPair<Float64Scalar> v2)
    {
        Debug.Assert(
            v1.IsNearUnitVector() &&
            v2.IsNearUnitVector()
        );

        return Float64Utils.Clamp(
            v1.Item1.ScalarValue * v2.Item1.ScalarValue + 
            v1.Item2.ScalarValue * v2.Item2.ScalarValue,
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetUnitVectorsAngleCos(this ITriplet<Float64Scalar> v1, ITriplet<Float64Scalar> v2)
    {
        Debug.Assert(
            v1.IsNearUnitVector() &&
            v2.IsNearUnitVector()
        );

        return Float64Utils.Clamp(v1.Item1.ScalarValue * v2.Item1.ScalarValue +
                                  v1.Item2.ScalarValue * v2.Item2.ScalarValue +
                                  v1.Item3.ScalarValue * v2.Item3.ScalarValue
            , -1, 1);
    }
    
    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetUnitVectorsAngleCos(this IQuad<Float64Scalar> v1, IQuad<Float64Scalar> v2)
    {
        Debug.Assert(
            v1.IsNearUnitVector() &&
            v2.IsNearUnitVector()
        );

        return Float64Utils.Clamp(
            v1.Item1.ScalarValue * v2.Item1.ScalarValue +
                  v1.Item2.ScalarValue * v2.Item2.ScalarValue +
                  v1.Item3.ScalarValue * v2.Item3.ScalarValue +
                  v1.Item4.ScalarValue * v2.Item4.ScalarValue, 
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetAngleCos(this IPair<Float64Scalar> v1, IPair<Float64Scalar> v2)
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetAngleCos(this ITriplet<Float64Scalar> v1, ITriplet<Float64Scalar> v2)
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetAngleCos(this IQuad<Float64Scalar> v1, IQuad<Float64Scalar> v2)
    {
        var t1 = v1.Item1 * v2.Item1 + v1.Item2 * v2.Item2 + v1.Item3 * v2.Item3 + v1.Item4 * v2.Item4;
        var t2 = v1.Item1 * v1.Item1 + v1.Item2 * v1.Item2 + v1.Item3 * v1.Item3 + v1.Item4 * v1.Item4;
        var t3 = v2.Item1 * v2.Item1 + v2.Item2 * v2.Item2 + v2.Item3 * v2.Item3 + v2.Item4 * v2.Item4;

        return (t1 / Math.Sqrt(t2 * t3)).Clamp(-1d, 1d);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetAngleCos(this LinFloat64Vector vector1, LinFloat64Vector vector2)
    {
        var uuDot = vector1.ENormSquared();
        var vvDot = vector2.ENormSquared();
        var uvDot = vector1.ESp(vector2);

        var norm = (uuDot * vvDot).Sqrt();

        return norm.IsZero()
            ? 0d
            : Float64Utils.Clamp(uvDot / norm, -1, 1);
    }


    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle GetAngleWithUnit(this IPair<Float64Scalar> v1, IPair<Float64Scalar> v2)
    {
        return v1.GetAngleCosWithUnit(v2).CosToPolarAngle();
    }

    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle GetAngleWithUnit(this ITriplet<Float64Scalar> v1, ITriplet<Float64Scalar> v2)
    {
        return v1.GetAngleCosWithUnit(v2).CosToPolarAngle();
    }
    
    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle GetAngleWithUnit(this IQuad<Float64Scalar> v1, IQuad<Float64Scalar> v2)
    {
        return v1.GetAngleCosWithUnit(v2).CosToPolarAngle();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetAngleCosWithUnit(this LinFloat64Vector vector1, ILinSignedBasisVector vector2)
    {
        Debug.Assert(
            vector2.Sign.IsNotZero
        );

        var uuDot = vector1.ENormSquared();
        var uvDot = vector1.ESp(vector2);

        var norm = uuDot.Sqrt();

        return norm.IsZero()
            ? 0d
            : Float64Utils.Clamp(uvDot / norm, -1, 1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetAngleCosWithUnit(this LinFloat64Vector vector1, LinFloat64Vector vector2)
    {
        Debug.Assert(
            vector2.IsNearUnit()
        );

        var uuDot = vector1.ENormSquared();
        var uvDot = vector1.ESp(vector2);

        var norm = uuDot.Sqrt();

        return norm.IsZero()
            ? 0d
            : Float64Utils.Clamp(uvDot / norm, -1, 1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetUnitVectorsAngleCos(this LinFloat64Vector vector1, LinFloat64Vector vector2)
    {
        return Float64Utils.Clamp(vector1.ESp(vector2), -1, 1);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle GetAngleWithUnit(this LinFloat64Vector vector1, ILinSignedBasisVector vector2)
    {
        return vector1.GetAngleCosWithUnit(vector2).CosToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle GetAngleWithUnit(this LinFloat64Vector vector1, LinFloat64Vector vector2)
    {
        return vector1.GetAngleCosWithUnit(vector2).CosToPolarAngle();
    }


    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle GetUnitVectorsAngle(this IPair<Float64Scalar> v1, IPair<Float64Scalar> v2)
    {
        return v1.GetUnitVectorsAngleCos(v2).CosToPolarAngle();
    }

    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle GetUnitVectorsAngle(this ITriplet<Float64Scalar> v1, ITriplet<Float64Scalar> v2)
    {
        return v1.GetUnitVectorsAngleCos(v2).CosToPolarAngle().RadiansToPolarAngle();
    }
    
    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle GetUnitVectorsAngle(this IQuad<Float64Scalar> v1, IQuad<Float64Scalar> v2)
    {
        return v1.GetUnitVectorsAngleCos(v2).CosToPolarAngle().RadiansToPolarAngle();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle GetUnitVectorsAngle(this LinFloat64Vector vector1, LinFloat64Vector vector2)
    {
        return vector1.GetUnitVectorsAngleCos(vector2).CosToPolarAngle();
    }


    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle GetAngle(this IPair<Float64Scalar> v1, IPair<Float64Scalar> v2)
    {
        return v1.GetAngleCos(v2).CosToPolarAngle();
    }

    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle GetAngle(this ITriplet<Float64Scalar> v1, ITriplet<Float64Scalar> v2)
    {
        return v1.GetAngleCos(v2).CosToPolarAngle();
    }
    
    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle GetAngle(this IQuad<Float64Scalar> v1, IQuad<Float64Scalar> v2)
    {
        return v1.GetAngleCos(v2).CosToPolarAngle();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle GetAngle(this LinFloat64Vector vector1, LinFloat64Vector vector2, bool assumeUnitVectors)
    {
        var v12Sp = vector1.ESp(vector2);

        var angle = assumeUnitVectors
            ? v12Sp
            : v12Sp / (vector1.ENormSquared() * vector2.ENormSquared()).Sqrt();

        return angle.CosToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle GetAngle(this LinFloat64Vector vector1, LinFloat64Vector vector2)
    {
        return vector1.GetAngleCos(vector2).CosToPolarAngle();
    }


    /// <summary>
    /// Find the angle between points (p1, p0, p2); i.e. p0 is the head of the angle
    /// </summary>
    /// <param name="p0"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle GetAngleFromPoints(this IPair<Float64Scalar> p0, IPair<Float64Scalar> p1, IPair<Float64Scalar> p2)
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle GetAngleFromPoints(this ITriplet<Float64Scalar> p0, ITriplet<Float64Scalar> p1, ITriplet<Float64Scalar> p2)
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle GetAngleFromPoints(this IQuad<Float64Scalar> p0, IQuad<Float64Scalar> p1, IQuad<Float64Scalar> p2)
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle GetDirectedAngle(this IPair<Float64Scalar> v1, IPair<Float64Scalar> v2)
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle GetDirectedAngleFromPoints(this IPair<Float64Scalar> pointA, IPair<Float64Scalar> pointB, IPair<Float64Scalar> pointC)
    {
        return GetDirectedAngle(
            pointA.VectorSubtract(pointB), 
            pointC.VectorSubtract(pointB)
        );

        //var v1 = pointA.Subtract(pointB).ToUnitComplexNumber();
        //var v2 = pointC.Subtract(pointB).ToUnitComplexNumber();

        //return (v2 / v1).GetPhaseAsPolarAngle().ToDirectedAngle();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CosToPolarAngle(this double angleCos)
    {
        return LinFloat64PolarAngle.CreateFromCos(angleCos);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CosToPolarAngle(this IFloat64Scalar angleCos)
    {
        return LinFloat64PolarAngle.CreateFromCos(angleCos.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle SinToPolarAngle(this double angleSin)
    {
        return LinFloat64PolarAngle.CreateFromSin(angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle SinToPolarAngle(this IFloat64Scalar angleSin)
    {
        return LinFloat64PolarAngle.CreateFromSin(angleSin.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle TanToPolarAngle(this double angleTan)
    {
        return LinFloat64PolarAngle.CreateFromTan(angleTan);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle TanToPolarAngle(this IFloat64Scalar angleTan)
    {
        return LinFloat64PolarAngle.CreateFromTan(angleTan.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CosToDoublePolarAngle(this double angleCos)
    {
        return LinFloat64PolarAngle.CreateDoubleAngleFromCos(angleCos);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CosToDoublePolarAngle(this IFloat64Scalar angleCos)
    {
        return LinFloat64PolarAngle.CreateDoubleAngleFromCos(angleCos.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle SinToDoublePolarAngle(this double angleSin)
    {
        return LinFloat64PolarAngle.CreateDoubleAngleFromSin(angleSin);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle SinToDoublePolarAngle(this IFloat64Scalar angleSin)
    {
        return LinFloat64PolarAngle.CreateDoubleAngleFromSin(angleSin.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle TanToDoublePolarAngle(this double angleTan)
    {
        return LinFloat64PolarAngle.CreateDoubleAngleFromTan(angleTan);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle TanToDoublePolarAngle(this IFloat64Scalar angleTan)
    {
        return LinFloat64PolarAngle.CreateDoubleAngleFromTan(angleTan.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CosToHalfPolarAngle(this double angleCos)
    {
        return LinFloat64PolarAngle.CreateHalfAngleFromCos(angleCos);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle CosToHalfPolarAngle(this IFloat64Scalar angleCos)
    {
        return LinFloat64PolarAngle.CreateHalfAngleFromCos(angleCos.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle DegreesToPolarAngle(this int angleInDegrees)
    {
        return LinFloat64PolarAngle.CreateFromDegrees(angleInDegrees);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle DegreesToPolarAngle(this float angleInDegrees)
    {
        return LinFloat64PolarAngle.CreateFromDegrees(angleInDegrees);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle DegreesToPolarAngle(this double angleInDegrees)
    {
        return LinFloat64PolarAngle.CreateFromDegrees(angleInDegrees);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle DegreesToPolarAngle(this IFloat64Scalar angleInDegrees)
    {
        return LinFloat64PolarAngle.CreateFromDegrees(angleInDegrees.ScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CosToDirectedAngle(this double angleCos)
    {
        return LinFloat64DirectedAngle.CreateFromCos(angleCos);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CosToDirectedAngle(this IFloat64Scalar angleCos)
    {
        return LinFloat64DirectedAngle.CreateFromCos(angleCos);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle SinToDirectedAngle(this double angleSin)
    {
        return LinFloat64DirectedAngle.CreateFromSin(angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle SinToDirectedAngle(this IFloat64Scalar angleSin)
    {
        return LinFloat64DirectedAngle.CreateFromSin(angleSin);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle TanToDirectedAngle(this double angleTan)
    {
        return LinFloat64DirectedAngle.CreateFromTan(angleTan);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle TanToDirectedAngle(this IFloat64Scalar angleTan)
    {
        return LinFloat64DirectedAngle.CreateFromTan(angleTan);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CosToDoubleDirectedAngle(this double angleCos)
    {
        return LinFloat64DirectedAngle.CreateDoubleAngleFromCos(angleCos);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle CosToDoubleDirectedAngle(this IFloat64Scalar angleCos)
    {
        return LinFloat64DirectedAngle.CreateDoubleAngleFromCos(angleCos.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle SinToDoubleDirectedAngle(this double angleSin)
    {
        return LinFloat64DirectedAngle.CreateDoubleAngleFromSin(angleSin);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle SinToDoubleDirectedAngle(this IFloat64Scalar angleSin)
    {
        return LinFloat64DirectedAngle.CreateDoubleAngleFromSin(angleSin.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle TanToDoubleDirectedAngle(this double angleTan)
    {
        return LinFloat64DirectedAngle.CreateDoubleAngleFromTan(angleTan);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle TanToDoubleDirectedAngle(this IFloat64Scalar angleTan)
    {
        return LinFloat64DirectedAngle.CreateDoubleAngleFromTan(angleTan.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle DegreesToDirectedAngle(this int angleInDegrees)
    {
        return LinFloat64DirectedAngle.CreateFromDegrees(angleInDegrees);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle DegreesToDirectedAngle(this int angleInDegrees, LinAngleRange range)
    {
        return LinFloat64DirectedAngle.CreateFromDegrees(angleInDegrees, range);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle DegreesToDirectedAngle(this float angleInDegrees)
    {
        return LinFloat64DirectedAngle.CreateFromDegrees(angleInDegrees);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle DegreesToDirectedAngle(this float angleInDegrees, LinAngleRange range)
    {
        return LinFloat64DirectedAngle.CreateFromDegrees(angleInDegrees, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle DegreesToDirectedAngle(this double angleInDegrees)
    {
        return LinFloat64DirectedAngle.CreateFromDegrees(angleInDegrees);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle DegreesToDirectedAngle(this double angleInDegrees, LinAngleRange range)
    {
        return LinFloat64DirectedAngle.CreateFromDegrees(angleInDegrees, range);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle DegreesToDirectedAngle(this Float64Scalar angleInDegrees)
    {
        return LinFloat64DirectedAngle.CreateFromDegrees(angleInDegrees.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle DegreesToDirectedAngle(this Float64Scalar angleInDegrees, LinAngleRange range)
    {
        return LinFloat64DirectedAngle.CreateFromDegrees(angleInDegrees.ScalarValue, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle DegreesToDirectedAngle(this IFloat64Scalar angleInDegrees)
    {
        return LinFloat64DirectedAngle.CreateFromDegrees(angleInDegrees.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle DegreesToDirectedAngle(this IFloat64Scalar angleInDegrees, LinAngleRange range)
    {
        return LinFloat64DirectedAngle.CreateFromDegrees(angleInDegrees.ScalarValue, range);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle RadiansToPolarAngle(this float angleInRadians)
    {
        return LinFloat64PolarAngle.CreateFromRadians(angleInRadians);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle RadiansToPolarAngle(this double angleInRadians)
    {
        return LinFloat64PolarAngle.CreateFromRadians(angleInRadians);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle RadiansToPolarAngle(this Float64Scalar angleInRadians)
    {
        return LinFloat64PolarAngle.CreateFromRadians(angleInRadians.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle RadiansToPolarAngle(this IFloat64Scalar angleInRadians)
    {
        return LinFloat64PolarAngle.CreateFromRadians(angleInRadians.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle RadiansToDirectedAngle(this float angleInRadians)
    {
        return LinFloat64DirectedAngle.CreateFromRadians(angleInRadians);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle RadiansToDirectedAngle(this float angleInRadians, LinAngleRange range)
    {
        return LinFloat64DirectedAngle.CreateFromRadians(angleInRadians, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle RadiansToDirectedAngle(this double angleInRadians)
    {
        return LinFloat64DirectedAngle.CreateFromRadians(angleInRadians);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle RadiansToDirectedAngle(this double angleInRadians, LinAngleRange range)
    {
        return LinFloat64DirectedAngle.CreateFromRadians(angleInRadians, range);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle RadiansToDirectedAngle(this Float64Scalar angleInRadians)
    {
        return LinFloat64DirectedAngle.CreateFromRadians(angleInRadians.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle RadiansToDirectedAngle(this Float64Scalar angleInRadians, LinAngleRange range)
    {
        return LinFloat64DirectedAngle.CreateFromRadians(angleInRadians.ScalarValue, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle RadiansToDirectedAngle(this IFloat64Scalar angleInRadians)
    {
        return LinFloat64DirectedAngle.CreateFromRadians(angleInRadians.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle RadiansToDirectedAngle(this IFloat64Scalar angleInRadians, LinAngleRange range)
    {
        return LinFloat64DirectedAngle.CreateFromRadians(angleInRadians.ScalarValue, range);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle Lerp(this double t, LinFloat64Angle angle1, LinFloat64Angle angle2)
    {
        Debug.Assert(t.IsValid() && t is >= 0 and <= 1);

        return ((1.0d - t) * angle1.DegreesValue + t * angle2.DegreesValue).DegreesToDirectedAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngle Lerp(this double t, LinFloat64Angle angle2)
    {
        Debug.Assert(t.IsValid() && t is >= 0 and <= 1);

        return (t * angle2.DegreesValue).DegreesToDirectedAngle();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<LinFloat64Vector2D> RotateBasisFrame2D(this LinFloat64Angle angle)
    {
        return new Pair<LinFloat64Vector2D>(
            angle.Rotate(LinFloat64Vector2D.E1),
            angle.Rotate(LinFloat64Vector2D.E2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D Rotate(this LinFloat64Angle angle, double x, double y)
    {
        var cosValue = angle.CosValue;
        var sinValue = angle.SinValue;

        var x1 = x * cosValue - y * sinValue;
        var y1 = x * sinValue + y * cosValue;

        return LinFloat64Vector2D.Create(x1, y1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D Rotate(this LinFloat64Angle angle, LinUnitBasisVector2D axis)
    {
        var cosValue = angle.CosValue;
        var sinValue = angle.SinValue;

        var (x, y) = axis.ToLinVector2D();

        var x1 = x * cosValue - y * sinValue;
        var y1 = x * sinValue + y * cosValue;

        return LinFloat64Vector2D.Create(x1, y1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D Rotate(this LinFloat64Angle angle, IPair<Float64Scalar> vector)
    {
        var cosValue = angle.CosValue;
        var sinValue = angle.SinValue;

        var x = vector.Item1;
        var y = vector.Item2;

        var x1 = x * cosValue - y * sinValue;
        var y1 = x * sinValue + y * cosValue;

        return LinFloat64Vector2D.Create(x1, y1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<LinFloat64Vector2D> Rotate(this LinFloat64Angle angle, IPair<Float64Scalar> vector1, IPair<Float64Scalar> vector2)
    {
        return new Pair<LinFloat64Vector2D>(
            angle.Rotate(vector1),
            angle.Rotate(vector2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<LinFloat64Vector2D> Rotate(this LinFloat64Angle angle, IPair<Float64Scalar> vector1, IPair<Float64Scalar> vector2, IPair<Float64Scalar> vector3)
    {
        return new Triplet<LinFloat64Vector2D>(
            angle.Rotate(vector1),
            angle.Rotate(vector2),
            angle.Rotate(vector3)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<LinFloat64Vector2D> Rotate(this LinFloat64Angle angle, params IPair<Float64Scalar>[] vectorArray)
    {
        return vectorArray
            .Select(angle.Rotate)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<LinFloat64Vector2D> Rotate(this LinFloat64Angle angle, IEnumerable<IPair<Float64Scalar>> vectorList)
    {
        return vectorList.Select(angle.Rotate);
    }
}