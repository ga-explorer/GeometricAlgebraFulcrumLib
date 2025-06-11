using System;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

public static class Float64ScalarUtils
{
    
    public static bool IsValid(this IFloat64Scalar scalar)
    {
        return scalar.ScalarValue.IsValid();
    }

    
    public static bool IsPositive(this IFloat64Scalar scalar)
    {
        return scalar.ScalarValue > 0;
    }

    
    public static bool IsNegative(this IFloat64Scalar scalar)
    {
        return scalar.ScalarValue < 0;
    }

    
    public static bool IsNotZeroOrNearPositive(this IFloat64Scalar scalar, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return scalar.ScalarValue < -zeroEpsilon;
    }

    
    public static bool IsNotZeroOrNearNegative(this IFloat64Scalar scalar, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return scalar.ScalarValue > zeroEpsilon;
    }
    
    
    public static bool IsZeroOrPositive(this IFloat64Scalar scalar)
    {
        return scalar.ScalarValue >= 0;
    }

    
    public static bool IsZeroOrNegative(this IFloat64Scalar scalar)
    {
        return scalar.ScalarValue <= 0;
    }
    
    
    public static bool IsFinite(this IFloat64Scalar scalar)
    {
        return scalar.ScalarValue.IsFinite();
    }
    

    
    public static bool IsZero(this IFloat64Scalar scalar)
    {
        return scalar.ScalarValue.IsZero();
    }

    
    public static bool IsNearZero(this IFloat64Scalar scalar, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return scalar.ScalarValue.IsNearZero(zeroEpsilon);
    }
    
    
    public static bool IsOne(this IFloat64Scalar scalar)
    {
        return (scalar.ScalarValue - 1).IsZero();
    }

    
    public static bool IsNearOne(this IFloat64Scalar scalar, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return (scalar.ScalarValue - 1).IsNearZero(zeroEpsilon);
    }
    
    
    public static bool IsMinusOne(this IFloat64Scalar scalar)
    {
        return (scalar.ScalarValue + 1).IsZero();
    }

    
    public static bool IsNearMinusOne(this IFloat64Scalar scalar, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return (scalar.ScalarValue + 1).IsNearZero(zeroEpsilon);
    }
    

    
    public static bool IsEqualTo(this IFloat64Scalar scalar1, double scalar2)
    {
        return (scalar1.ScalarValue - scalar2).IsZero();
    }
    
    
    public static bool IsEqualTo(this IFloat64Scalar scalar1, IFloat64Scalar scalar2)
    {
        return (scalar1.ScalarValue - scalar2.ScalarValue).IsZero();
    }
    
    
    public static bool IsEqualTo(this int scalar1, IFloat64Scalar scalar2)
    {
        return (scalar1 - scalar2.ScalarValue).IsZero();
    }

    
    public static bool IsEqualTo(this uint scalar1, IFloat64Scalar scalar2)
    {
        return (scalar1 - scalar2.ScalarValue).IsZero();
    }

    
    public static bool IsEqualTo(this long scalar1, IFloat64Scalar scalar2)
    {
        return (scalar1 - scalar2.ScalarValue).IsZero();
    }

    
    public static bool IsEqualTo(this ulong scalar1, IFloat64Scalar scalar2)
    {
        return (scalar1 - scalar2.ScalarValue).IsZero();
    }

    
    public static bool IsEqualTo(this float scalar1, IFloat64Scalar scalar2)
    {
        return (scalar1 - scalar2.ScalarValue).IsZero();
    }

    
    public static bool IsEqualTo(this double scalar1, IFloat64Scalar scalar2)
    {
        return (scalar1 - scalar2.ScalarValue).IsZero();
    }
    
    
    
    public static bool IsNearEqualTo(this IFloat64Scalar scalar1, double scalar2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return (scalar1.ScalarValue - scalar2).IsNearZero(zeroEpsilon);
    }
    
    
    public static bool IsNearEqualTo(this IFloat64Scalar scalar1, IFloat64Scalar scalar2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return (scalar1.ScalarValue - scalar2.ScalarValue).IsNearZero(zeroEpsilon);
    }
    
    
    public static bool IsNearEqualTo(this int scalar1, IFloat64Scalar scalar2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return (scalar1 - scalar2.ScalarValue).IsNearZero(zeroEpsilon);
    }

    
    public static bool IsNearEqualTo(this uint scalar1, IFloat64Scalar scalar2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return (scalar1 - scalar2.ScalarValue).IsNearZero(zeroEpsilon);
    }

    
    public static bool IsNearEqualTo(this long scalar1, IFloat64Scalar scalar2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return (scalar1 - scalar2.ScalarValue).IsNearZero(zeroEpsilon);
    }

    
    public static bool IsNearEqualTo(this ulong scalar1, IFloat64Scalar scalar2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return (scalar1 - scalar2.ScalarValue).IsNearZero(zeroEpsilon);
    }

    
    public static bool IsNearEqualTo(this float scalar1, IFloat64Scalar scalar2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return (scalar1 - scalar2.ScalarValue).IsNearZero(zeroEpsilon);
    }

    
    public static bool IsNearEqualTo(this double scalar1, IFloat64Scalar scalar2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return (scalar1 - scalar2.ScalarValue).IsNearZero(zeroEpsilon);
    }
    

    
    public static bool IsLessThan(this IFloat64Scalar scalar1, double scalar2)
    {
        return scalar1.ScalarValue < scalar2;
    }
    
    
    public static bool IsLessThan(this IFloat64Scalar scalar1, IFloat64Scalar scalar2)
    {
        return scalar1.ScalarValue < scalar2.ScalarValue;
    }
    
    
    public static bool IsLessThan(this double scalar1, IFloat64Scalar scalar2)
    {
        return scalar1 < scalar2.ScalarValue;
    }
    

    
    public static bool IsLessThanOrEqualTo(this IFloat64Scalar scalar1, double scalar2)
    {
        return scalar1.ScalarValue <= scalar2;
    }
    
    
    public static bool IsLessThanOrEqualTo(this IFloat64Scalar scalar1, IFloat64Scalar scalar2)
    {
        return scalar1.ScalarValue <= scalar2.ScalarValue;
    }
    
    
    public static bool IsLessThanOrEqualTo(this double scalar1, IFloat64Scalar scalar2)
    {
        return scalar1 <= scalar2.ScalarValue;
    }
    

    
    public static bool IsMoreThan(this IFloat64Scalar scalar1, double scalar2)
    {
        return scalar1.ScalarValue > scalar2;
    }
    
    
    public static bool IsMoreThan(this IFloat64Scalar scalar1, IFloat64Scalar scalar2)
    {
        return scalar1.ScalarValue > scalar2.ScalarValue;
    }
    
    
    public static bool IsMoreThan(this double scalar1, IFloat64Scalar scalar2)
    {
        return scalar1 > scalar2.ScalarValue;
    }
    

    
    public static bool IsMoreThanOrEqualTo(this IFloat64Scalar scalar1, double scalar2)
    {
        return scalar1.ScalarValue >= scalar2;
    }
    
    
    public static bool IsMoreThanOrEqualTo(this IFloat64Scalar scalar1, IFloat64Scalar scalar2)
    {
        return scalar1.ScalarValue >= scalar2.ScalarValue;
    }
    
    
    public static bool IsMoreThanOrEqualTo(this double scalar1, IFloat64Scalar scalar2)
    {
        return scalar1 >= scalar2.ScalarValue;
    }
    
    
    
    public static LinFloat64Scalar3D GetScalar3D(this Random random)
    {
        return LinFloat64Scalar3D.Create(
            random.NextDouble()
        );
    }

    
    
    public static double MapScalar(this IFloat64Scalar scalar, Func<double, double> scalarMapping)
    {
        return scalarMapping(scalar.ScalarValue);
    }
    
    
    
    public static double NaNToZero(this IFloat64Scalar number)
    {
        return number.ScalarValue.NaNToZero();
    }

    
    public static double NaNInfinityToZero(this IFloat64Scalar number)
    {
        return number.ScalarValue.NaNInfinityToZero();
    }

    
    public static double NearZeroToZero(this IFloat64Scalar number, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return number.ScalarValue.NearZeroToZero(zeroEpsilon);
    }


    
    public static double Positive(this IFloat64Scalar scalar)
    {
        return scalar.ToScalar();
    }

    
    public static double Negative(this IFloat64Scalar scalar)
    {
        return -scalar.ScalarValue;
    }
    
    
    public static double Inverse(this IFloat64Scalar scalar)
    {
        return 1d / scalar.ScalarValue;
    }

    
    public static double Abs(this IFloat64Scalar scalar)
    {
        return Math.Abs(scalar.ScalarValue);
    }

    
    public static double Square(this IFloat64Scalar scalar)
    {
        return scalar.ScalarValue * scalar.ScalarValue;
    }

    
    public static double Cube(this IFloat64Scalar scalar)
    {
        return scalar.ScalarValue * scalar.ScalarValue * scalar.ScalarValue;
    }

    
    public static double Sign(this IFloat64Scalar scalar)
    {
        return Math.Sign(scalar.ScalarValue);
    }
    
    
    public static double UnitStep(this IFloat64Scalar scalar)
    {
        return scalar.ScalarValue >= 0 ? 1 : 0;
    }

    
    public static double Sqrt(this IFloat64Scalar scalar)
    {
        return Math.Sqrt(scalar.ScalarValue);
    }

    
    public static double SqrtOfNegative(this IFloat64Scalar scalar)
    {
        return Math.Sqrt(scalar.Negative());
    }
    
    
    public static double SqrtOfAbs(this IFloat64Scalar scalar)
    {
        return Math.Sqrt(Math.Abs(scalar.ScalarValue));
    }

    
    public static double Exp(this IFloat64Scalar scalar)
    {
        return Math.Exp(scalar.ScalarValue);
    }

    
    public static double LogE(this IFloat64Scalar scalar)
    {
        return Math.Log(scalar.ScalarValue);
    }

    
    public static double Log2(this IFloat64Scalar scalar)
    {
        return scalar.ScalarValue.Log2();
    }

    
    public static double Log10(this IFloat64Scalar scalar)
    {
        return Math.Log10(scalar.ScalarValue);
    }

    
    public static double Cos(this IFloat64Scalar scalar)
    {
        return Math.Cos(scalar.ScalarValue);
    }

    
    public static double Sin(this IFloat64Scalar scalar)
    {
        return Math.Sin(scalar.ScalarValue);
    }

    
    public static double Tan(this IFloat64Scalar scalar)
    {
        return Math.Tan(scalar.ScalarValue);
    }
    
    
    public static double Sec(this IFloat64Scalar scalar)
    {
        return 1 / Math.Cos(scalar.ScalarValue);
    }
    
    
    public static double Csc(this IFloat64Scalar scalar)
    {
        return 1 / Math.Sin(scalar.ScalarValue);
    }
    
    
    public static double Cot(this IFloat64Scalar scalar)
    {
        return 1 / Math.Tan(scalar.ScalarValue);
    }
    
    
    public static double Cosh(this IFloat64Scalar scalar)
    {
        return Math.Cosh(scalar.ScalarValue);
    }

    
    public static double Sinh(this IFloat64Scalar scalar)
    {
        return Math.Sinh(scalar.ScalarValue);
    }

    
    public static double Tanh(this IFloat64Scalar scalar)
    {
        return Math.Tanh(scalar.ScalarValue);
    }
    
    
    public static double Sech(this IFloat64Scalar scalar)
    {
        return 1 / Math.Cosh(scalar.ScalarValue);
    }
    
    
    public static double Csch(this IFloat64Scalar scalar)
    {
        return 1 / Math.Sinh(scalar.ScalarValue);
    }
    
    
    public static double Coth(this IFloat64Scalar scalar)
    {
        return 1 / Math.Tanh(scalar.ScalarValue);
    }


    
    public static LinFloat64PolarAngle ArcCos(this IFloat64Scalar scalar)
    {
        return scalar.ScalarValue.CosToPolarAngle();
    }

    
    public static LinFloat64PolarAngle ArcSin(this IFloat64Scalar scalar)
    {
        return scalar.ScalarValue.SinToPolarAngle();
    }

    
    public static LinFloat64PolarAngle ArcTan(this IFloat64Scalar scalar)
    {
        return scalar.ScalarValue.TanToPolarAngle();
    }
    
    
    public static LinFloat64PolarAngle ArcSec(this IFloat64Scalar scalar)
    {
        return (1 / scalar.ScalarValue).CosToPolarAngle();
    }
    
    
    public static LinFloat64PolarAngle ArcCsc(this IFloat64Scalar scalar)
    {
        return (1 / scalar.ScalarValue).SinToPolarAngle();
    }

    
    public static LinFloat64PolarAngle ArcCot(this IFloat64Scalar scalar)
    {
        return (1 / scalar.ScalarValue).TanToPolarAngle();
    }

    
    
    public static LinFloat64PolarAngle ArcTan2(this IFloat64Scalar scalarX, int scalarY)
    {
        return LinFloat64PolarAngle.CreateFromRadians(
            scalarX.ScalarValue.ArcTan2(scalarY)
        );
    }

    
    public static LinFloat64PolarAngle ArcTan2(this IFloat64Scalar scalarX, double scalarY)
    {
        return LinFloat64PolarAngle.CreateFromRadians(
            scalarX.ScalarValue.ArcTan2(scalarY)
        );
    }
    
    
    public static LinFloat64PolarAngle ArcTan2(this IFloat64Scalar scalarX, IFloat64Scalar scalarY)
    {
        return LinFloat64PolarAngle.CreateFromVector(scalarX.ScalarValue, scalarY.ScalarValue);
    }
    
    
    public static LinFloat64PolarAngle ArcTan2(this double scalarX, IFloat64Scalar scalarY)
    {
        return LinFloat64PolarAngle.CreateFromRadians(
            scalarX.ArcTan2(scalarY.ScalarValue)
        );
    }
    
    
    
    public static double Power(this IFloat64Scalar scalar, IntegerSign exponentScalar)
    {
        return scalar.ScalarValue.Power(
            exponentScalar.ToFloat64()
        );
    }

    
    public static double Power(this IFloat64Scalar scalar, int exponentScalar)
    {
        return scalar.ScalarValue.Power(
            exponentScalar
        );
    }
    
    
    public static double Power(this IFloat64Scalar scalar, double exponentScalar)
    {
        return Math.Pow(scalar.ScalarValue, exponentScalar);
    }
    
    
    public static double Power(this IFloat64Scalar scalar, IFloat64Scalar exponentScalar)
    {
        return scalar.ScalarValue.Power(exponentScalar.ScalarValue);
    }
    
    
    public static double Power(this IntegerSign scalar, IFloat64Scalar exponentScalar)
    {
        return scalar.ToFloat64().Power(exponentScalar.ScalarValue);
    }
    
    //
    //public static double Power(this int scalar, int exponentScalar)
    //{
    //    return scalar.Power(exponentScalar);
    //}
    
    //
    //public static double Power(this int scalar, double exponentScalar)
    //{
    //    return scalar.Power(exponentScalar);
    //}

    
    public static double Power(this int scalar, IFloat64Scalar exponentScalar)
    {
        return ((double)scalar).Power(exponentScalar.ScalarValue);
    }

    
    public static double Power(this uint scalar, IFloat64Scalar exponentScalar)
    {
        return ((double)scalar).Power(exponentScalar.ScalarValue);
    }

    
    public static double Power(this long scalar, IFloat64Scalar exponentScalar)
    {
        return ((double)scalar).Power(exponentScalar.ScalarValue);
    }

    
    public static double Power(this ulong scalar, IFloat64Scalar exponentScalar)
    {
        return ((double)scalar).Power(exponentScalar.ScalarValue);
    }

    
    public static double Power(this float scalar, IFloat64Scalar exponentScalar)
    {
        return ((double)scalar).Power(exponentScalar.ScalarValue);
    }

    
    public static double Power(this double scalar, IFloat64Scalar exponentScalar)
    {
        return scalar.Power(exponentScalar.ScalarValue);
    }
    
    
    
    public static double Log(this IFloat64Scalar scalar, int baseScalar)
    {
        return scalar.ScalarValue.Log(
            baseScalar
        );
    }
    
    
    public static double Log(this IFloat64Scalar scalar, double baseScalar)
    {
        return scalar.ScalarValue.Log(
            baseScalar
        );
    }
    
    
    public static double Log(this IFloat64Scalar scalar, IFloat64Scalar baseScalar)
    {
        return scalar.Log(baseScalar.ScalarValue);
    }
    
    
    public static double Log(this int scalar, IFloat64Scalar baseScalar)
    {
        return ((double)scalar).Log(baseScalar.ScalarValue);
    }
    
    
    public static double Log(this uint scalar, IFloat64Scalar baseScalar)
    {
        return ((double)scalar).Log(baseScalar.ScalarValue);
    }
    
    
    public static double Log(this long scalar, IFloat64Scalar baseScalar)
    {
        return ((double)scalar).Log(baseScalar.ScalarValue);
    }
    
    
    public static double Log(this ulong scalar, IFloat64Scalar baseScalar)
    {
        return ((double)scalar).Log(baseScalar.ScalarValue);
    }
    
    
    public static double Log(this float scalar, IFloat64Scalar baseScalar)
    {
        return ((double)scalar).Log(baseScalar.ScalarValue);
    }
    
    
    public static double Log(this double scalar, IFloat64Scalar baseScalar)
    {
        return scalar.Log(baseScalar.ScalarValue);
    }
    
    
    
    public static double Add(this IFloat64Scalar scalar1, IntegerSign scalar)
    {
        return scalar1.ScalarValue + scalar.ToFloat64();
    }

    
    public static double Add(this IFloat64Scalar scalar1, double scalar)
    {
        return scalar1.ScalarValue + scalar;
    }
    
    
    public static double Add(this IFloat64Scalar scalar1, IFloat64Scalar scalar)
    {
        return scalar1.ScalarValue + scalar.ScalarValue;
    }
    
    
    public static double Add(this IntegerSign scalar1, IFloat64Scalar scalar)
    {
        return scalar1.ToFloat64() + scalar.ScalarValue;
    }

    
    public static double Add(this int scalar1, IFloat64Scalar scalar)
    {
        return scalar1 + scalar.ScalarValue;
    }

    
    public static double Add(this uint scalar1, IFloat64Scalar scalar)
    {
        return scalar1 + scalar.ScalarValue;
    }

    
    public static double Add(this long scalar1, IFloat64Scalar scalar)
    {
        return scalar1 + scalar.ScalarValue;
    }

    
    public static double Add(this ulong scalar1, IFloat64Scalar scalar)
    {
        return scalar1 + scalar.ScalarValue;
    }

    
    public static double Add(this float scalar1, IFloat64Scalar scalar)
    {
        return scalar1 + scalar.ScalarValue;
    }

    
    public static double Add(this double scalar1, IFloat64Scalar scalar)
    {
        return scalar1 + scalar.ScalarValue;
    }

    
    
    public static double Subtract(this IFloat64Scalar scalar1, IntegerSign scalar)
    {
        return scalar1.ScalarValue - scalar.ToFloat64();
    }
    
    
    public static double Subtract(this IFloat64Scalar scalar1, double scalar)
    {
        return scalar1.ScalarValue - scalar;
    }
    
    
    public static double Subtract(this IFloat64Scalar scalar1, IFloat64Scalar scalar)
    {
        return scalar1.ScalarValue - scalar.ScalarValue;
    }
    
    
    public static double Subtract(this IntegerSign scalar1, IFloat64Scalar scalar)
    {
        return scalar1.ToFloat64() - scalar.ScalarValue;
    }

    
    public static double Subtract(this int scalar1, IFloat64Scalar scalar)
    {
        return scalar1 - scalar.ScalarValue;
    }

    
    public static double Subtract(this uint scalar1, IFloat64Scalar scalar)
    {
        return scalar1 - scalar.ScalarValue;
    }

    
    public static double Subtract(this long scalar1, IFloat64Scalar scalar)
    {
        return scalar1 - scalar.ScalarValue;
    }

    
    public static double Subtract(this ulong scalar1, IFloat64Scalar scalar)
    {
        return scalar1 - scalar.ScalarValue;
    }

    
    public static double Subtract(this float scalar1, IFloat64Scalar scalar)
    {
        return scalar1 - scalar.ScalarValue;
    }

    
    public static double Subtract(this double scalar1, IFloat64Scalar scalar)
    {
        return scalar1 - scalar.ScalarValue;
    }

    
    
    public static double Times(this IFloat64Scalar scalar1, IntegerSign scalar)
    {
        return scalar1.ScalarValue * scalar.ToFloat64();
    }
    
    
    public static double Times(this IFloat64Scalar scalar1, double scalar)
    {
        return scalar1.ScalarValue * scalar;
    }
    
    
    public static double Times(this IFloat64Scalar scalar1, IFloat64Scalar scalar)
    {
        return scalar1.ScalarValue * scalar.ScalarValue;
    }
    
    
    public static double Times(this IntegerSign scalar1, IFloat64Scalar scalar)
    {
        return scalar1.ToFloat64() * scalar.ScalarValue;
    }

    
    public static double Times(this int scalar1, IFloat64Scalar scalar)
    {
        return scalar1 * scalar.ScalarValue;
    }

    
    public static double Times(this uint scalar1, IFloat64Scalar scalar)
    {
        return scalar1 * scalar.ScalarValue;
    }

    
    public static double Times(this long scalar1, IFloat64Scalar scalar)
    {
        return scalar1 * scalar.ScalarValue;
    }

    
    public static double Times(this ulong scalar1, IFloat64Scalar scalar)
    {
        return scalar1 * scalar.ScalarValue;
    }

    
    public static double Times(this float scalar1, IFloat64Scalar scalar)
    {
        return scalar1 * scalar.ScalarValue;
    }

    
    public static double Times(this double scalar1, IFloat64Scalar scalar)
    {
        return scalar1 * scalar.ScalarValue;
    }

    
    
    public static double Divide(this IFloat64Scalar scalar1, IntegerSign scalar)
    {
        return scalar1.ScalarValue / scalar.ToFloat64();
    }
    
    
    public static double Divide(this IFloat64Scalar scalar1, double scalar)
    {
        return scalar1.ScalarValue / scalar;
    }
    
    
    public static double Divide(this IFloat64Scalar scalar1, IFloat64Scalar scalar)
    {
        return scalar1.ScalarValue / scalar.ScalarValue;
    }
    
    
    public static double Divide(this IntegerSign scalar1, IFloat64Scalar scalar)
    {
        return scalar1.ToFloat64() / scalar.ScalarValue;
    }

    
    public static double Divide(this int scalar1, IFloat64Scalar scalar)
    {
        return scalar1 / scalar.ScalarValue;
    }

    
    public static double Divide(this uint scalar1, IFloat64Scalar scalar)
    {
        return scalar1 / scalar.ScalarValue;
    }

    
    public static double Divide(this long scalar1, IFloat64Scalar scalar)
    {
        return scalar1 / scalar.ScalarValue;
    }

    
    public static double Divide(this ulong scalar1, IFloat64Scalar scalar)
    {
        return scalar1 / scalar.ScalarValue;
    }

    
    public static double Divide(this float scalar1, IFloat64Scalar scalar)
    {
        return scalar1 / scalar.ScalarValue;
    }

    
    public static double Divide(this double scalar1, IFloat64Scalar scalar)
    {
        return scalar1 / scalar.ScalarValue;
    }

    
    
    public static double NegativeTimes(this IFloat64Scalar scalar1, IntegerSign scalar)
    {
        return -scalar1.ScalarValue * scalar.ToFloat64();
    }
    
    
    public static double NegativeTimes(this IFloat64Scalar scalar1, double scalar)
    {
        return -scalar1.ScalarValue * scalar;
    }
    
    
    public static double NegativeTimes(this IFloat64Scalar scalar1, IFloat64Scalar scalar)
    {
        return -scalar1.ScalarValue * scalar.ScalarValue;
    }
    
    
    public static double NegativeTimes(this IntegerSign scalar1, IFloat64Scalar scalar)
    {
        return -scalar1.ToFloat64() * scalar.ScalarValue;
    }

    
    public static double NegativeTimes(this int scalar1, IFloat64Scalar scalar)
    {
        return -scalar1 * scalar.ScalarValue;
    }

    
    public static double NegativeTimes(this uint scalar1, IFloat64Scalar scalar)
    {
        return -scalar1 * scalar.ScalarValue;
    }

    
    public static double NegativeTimes(this long scalar1, IFloat64Scalar scalar)
    {
        return -scalar1 * scalar.ScalarValue;
    }

    
    public static double NegativeTimes(this ulong scalar1, IFloat64Scalar scalar)
    {
        return scalar1 * -scalar.ScalarValue;
    }

    
    public static double NegativeTimes(this float scalar1, IFloat64Scalar scalar)
    {
        return -scalar1 * scalar.ScalarValue;
    }

    
    public static double NegativeTimes(this double scalar1, IFloat64Scalar scalar)
    {
        return -scalar1 * scalar.ScalarValue;
    }

    
    
    public static double NegativeDivide(this IFloat64Scalar scalar1, IntegerSign scalar)
    {
        return -scalar1.ScalarValue / scalar.ToFloat64();
    }
    
    
    public static double NegativeDivide(this IFloat64Scalar scalar1, double scalar)
    {
        return -scalar1.ScalarValue / scalar;
    }
    
    
    public static double NegativeDivide(this IFloat64Scalar scalar1, IFloat64Scalar scalar)
    {
        return -scalar1.ScalarValue / scalar.ScalarValue;
    }
    
    
    public static double NegativeDivide(this IntegerSign scalar1, IFloat64Scalar scalar)
    {
        return -scalar1.ToFloat64() / scalar.ScalarValue;
    }

    
    public static double NegativeDivide(this int scalar1, IFloat64Scalar scalar)
    {
        return -scalar1 / scalar.ScalarValue;
    }

    
    public static double NegativeDivide(this uint scalar1, IFloat64Scalar scalar)
    {
        return -scalar1 / scalar.ScalarValue;
    }

    
    public static double NegativeDivide(this long scalar1, IFloat64Scalar scalar)
    {
        return -scalar1 / scalar.ScalarValue;
    }

    
    public static double NegativeDivide(this ulong scalar1, IFloat64Scalar scalar)
    {
        return scalar1 / -scalar.ScalarValue;
    }

    
    public static double NegativeDivide(this float scalar1, IFloat64Scalar scalar)
    {
        return -scalar1 / scalar.ScalarValue;
    }

    
    public static double NegativeDivide(this double scalar1, IFloat64Scalar scalar)
    {
        return -scalar1 / scalar.ScalarValue;
    }

    
    
    public static double Min(this IFloat64Scalar scalar1, IntegerSign scalar2)
    {
        return scalar1.ScalarValue < scalar2.ToFloat64() 
            ? scalar1.ScalarValue 
            : scalar2.ToFloat64();
    }

    
    public static double Min(this IFloat64Scalar scalar1, double scalar2)
    {
        return scalar1.ScalarValue < scalar2 
            ? scalar1.ScalarValue 
            : scalar2;
    }
    
    
    public static double Min(this IFloat64Scalar scalar1, IFloat64Scalar scalar2)
    {
        return scalar1.ScalarValue < scalar2.ScalarValue 
            ? scalar1.ScalarValue 
            : scalar2.ScalarValue;
    }
    
    
    public static double Min(this IntegerSign scalar1, IFloat64Scalar scalar2)
    {
        return scalar1.ToFloat64() < scalar2.ScalarValue 
            ? scalar1.ToFloat64() 
            : scalar2.ScalarValue;
    }

    
    public static double Min(this int scalar1, IFloat64Scalar scalar2)
    {
        return scalar1 < scalar2.ScalarValue 
            ? scalar1 
            : scalar2.ScalarValue;
    }

    
    public static double Min(this uint scalar1, IFloat64Scalar scalar2)
    {
        return scalar1 < scalar2.ScalarValue 
            ? scalar1 
            : scalar2.ScalarValue;
    }

    
    public static double Min(this long scalar1, IFloat64Scalar scalar2)
    {
        return scalar1 < scalar2.ScalarValue 
            ? scalar1 
            : scalar2.ScalarValue;
    }

    
    public static double Min(this ulong scalar1, IFloat64Scalar scalar2)
    {
        return scalar1 < scalar2.ScalarValue 
            ? scalar1 
            : scalar2.ScalarValue;
    }

    
    public static double Min(this float scalar1, IFloat64Scalar scalar2)
    {
        return scalar1 < scalar2.ScalarValue 
            ? scalar1 
            : scalar2.ScalarValue;
    }

    
    public static double Min(this double scalar1, IFloat64Scalar scalar2)
    {
        return scalar1 < scalar2.ScalarValue 
            ? scalar1
            : scalar2.ScalarValue;
    }
    
    
    
    public static double Max(this IFloat64Scalar scalar1, IntegerSign scalar2)
    {
        return scalar1.ScalarValue > scalar2.ToFloat64() 
            ? scalar1.ScalarValue 
            : scalar2.ToFloat64();
    }

    
    public static double Max(this IFloat64Scalar scalar1, double scalar2)
    {
        return scalar1.ScalarValue > scalar2 
            ? scalar1.ScalarValue 
            : scalar2;
    }
    
    
    public static double Max(this IFloat64Scalar scalar1, IFloat64Scalar scalar2)
    {
        return scalar1.ScalarValue > scalar2.ScalarValue 
            ? scalar1.ScalarValue 
            : scalar2.ScalarValue;
    }
    
    
    public static double Max(this IntegerSign scalar1, IFloat64Scalar scalar2)
    {
        return scalar1.ToFloat64() > scalar2.ScalarValue 
            ? scalar1.ToFloat64() 
            : scalar2.ScalarValue;
    }

    
    public static double Max(this int scalar1, IFloat64Scalar scalar2)
    {
        return scalar1 > scalar2.ScalarValue 
            ? scalar1 
            : scalar2.ScalarValue;
    }

    
    public static double Max(this uint scalar1, IFloat64Scalar scalar2)
    {
        return scalar1 > scalar2.ScalarValue 
            ? scalar1 
            : scalar2.ScalarValue;
    }

    
    public static double Max(this long scalar1, IFloat64Scalar scalar2)
    {
        return scalar1 > scalar2.ScalarValue 
            ? scalar1 
            : scalar2.ScalarValue;
    }

    
    public static double Max(this ulong scalar1, IFloat64Scalar scalar2)
    {
        return scalar1 > scalar2.ScalarValue 
            ? scalar1 
            : scalar2.ScalarValue;
    }

    
    public static double Max(this float scalar1, IFloat64Scalar scalar2)
    {
        return scalar1 > scalar2.ScalarValue 
            ? scalar1 
            : scalar2.ScalarValue;
    }

    
    public static double Max(this double scalar1, IFloat64Scalar scalar2)
    {
        return scalar1 > scalar2.ScalarValue 
            ? scalar1
            : scalar2.ScalarValue;
    }
    
}