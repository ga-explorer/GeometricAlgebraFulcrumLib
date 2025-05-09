using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

public static class Float64ScalarUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValid(this IFloat64Scalar scalar)
    {
        return scalar.ScalarValue.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPositive(this IFloat64Scalar scalar)
    {
        return scalar.ScalarValue > 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNegative(this IFloat64Scalar scalar)
    {
        return scalar.ScalarValue < 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotZeroOrNearPositive(this IFloat64Scalar scalar, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return scalar.ScalarValue < -zeroEpsilon;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotZeroOrNearNegative(this IFloat64Scalar scalar, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return scalar.ScalarValue > zeroEpsilon;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZeroOrPositive(this IFloat64Scalar scalar)
    {
        return scalar.ScalarValue >= 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZeroOrNegative(this IFloat64Scalar scalar)
    {
        return scalar.ScalarValue <= 0;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsFinite(this IFloat64Scalar scalar)
    {
        return scalar.ScalarValue.IsFinite();
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero(this IFloat64Scalar scalar)
    {
        return scalar.ScalarValue.IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearZero(this IFloat64Scalar scalar, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return scalar.ScalarValue.IsNearZero(zeroEpsilon);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOne(this IFloat64Scalar scalar)
    {
        return (scalar.ScalarValue - 1).IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOne(this IFloat64Scalar scalar, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return (scalar.ScalarValue - 1).IsNearZero(zeroEpsilon);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMinusOne(this IFloat64Scalar scalar)
    {
        return (scalar.ScalarValue + 1).IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearMinusOne(this IFloat64Scalar scalar, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return (scalar.ScalarValue + 1).IsNearZero(zeroEpsilon);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEqualTo(this IFloat64Scalar scalar1, double scalar2)
    {
        return (scalar1.ScalarValue - scalar2).IsZero();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEqualTo(this IFloat64Scalar scalar1, IFloat64Scalar scalar2)
    {
        return (scalar1.ScalarValue - scalar2.ScalarValue).IsZero();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEqualTo(this int scalar1, IFloat64Scalar scalar2)
    {
        return (scalar1 - scalar2.ScalarValue).IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEqualTo(this uint scalar1, IFloat64Scalar scalar2)
    {
        return (scalar1 - scalar2.ScalarValue).IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEqualTo(this long scalar1, IFloat64Scalar scalar2)
    {
        return (scalar1 - scalar2.ScalarValue).IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEqualTo(this ulong scalar1, IFloat64Scalar scalar2)
    {
        return (scalar1 - scalar2.ScalarValue).IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEqualTo(this float scalar1, IFloat64Scalar scalar2)
    {
        return (scalar1 - scalar2.ScalarValue).IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEqualTo(this double scalar1, IFloat64Scalar scalar2)
    {
        return (scalar1 - scalar2.ScalarValue).IsZero();
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearEqualTo(this IFloat64Scalar scalar1, double scalar2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return (scalar1.ScalarValue - scalar2).IsNearZero(zeroEpsilon);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearEqualTo(this IFloat64Scalar scalar1, IFloat64Scalar scalar2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return (scalar1.ScalarValue - scalar2.ScalarValue).IsNearZero(zeroEpsilon);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearEqualTo(this int scalar1, IFloat64Scalar scalar2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return (scalar1 - scalar2.ScalarValue).IsNearZero(zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearEqualTo(this uint scalar1, IFloat64Scalar scalar2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return (scalar1 - scalar2.ScalarValue).IsNearZero(zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearEqualTo(this long scalar1, IFloat64Scalar scalar2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return (scalar1 - scalar2.ScalarValue).IsNearZero(zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearEqualTo(this ulong scalar1, IFloat64Scalar scalar2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return (scalar1 - scalar2.ScalarValue).IsNearZero(zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearEqualTo(this float scalar1, IFloat64Scalar scalar2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return (scalar1 - scalar2.ScalarValue).IsNearZero(zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearEqualTo(this double scalar1, IFloat64Scalar scalar2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return (scalar1 - scalar2.ScalarValue).IsNearZero(zeroEpsilon);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThan(this IFloat64Scalar scalar1, double scalar2)
    {
        return scalar1.ScalarValue < scalar2;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThan(this IFloat64Scalar scalar1, IFloat64Scalar scalar2)
    {
        return scalar1.ScalarValue < scalar2.ScalarValue;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThan(this double scalar1, IFloat64Scalar scalar2)
    {
        return scalar1 < scalar2.ScalarValue;
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThanOrEqualTo(this IFloat64Scalar scalar1, double scalar2)
    {
        return scalar1.ScalarValue <= scalar2;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThanOrEqualTo(this IFloat64Scalar scalar1, IFloat64Scalar scalar2)
    {
        return scalar1.ScalarValue <= scalar2.ScalarValue;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThanOrEqualTo(this double scalar1, IFloat64Scalar scalar2)
    {
        return scalar1 <= scalar2.ScalarValue;
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThan(this IFloat64Scalar scalar1, double scalar2)
    {
        return scalar1.ScalarValue > scalar2;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThan(this IFloat64Scalar scalar1, IFloat64Scalar scalar2)
    {
        return scalar1.ScalarValue > scalar2.ScalarValue;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThan(this double scalar1, IFloat64Scalar scalar2)
    {
        return scalar1 > scalar2.ScalarValue;
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThanOrEqualTo(this IFloat64Scalar scalar1, double scalar2)
    {
        return scalar1.ScalarValue >= scalar2;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThanOrEqualTo(this IFloat64Scalar scalar1, IFloat64Scalar scalar2)
    {
        return scalar1.ScalarValue >= scalar2.ScalarValue;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThanOrEqualTo(this double scalar1, IFloat64Scalar scalar2)
    {
        return scalar1 >= scalar2.ScalarValue;
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Scalar3D GetScalar3D(this Random random)
    {
        return LinFloat64Scalar3D.Create(
            random.NextDouble()
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar MapScalar(this IFloat64Scalar scalar, Func<double, double> scalarMapping)
    {
        return Float64Scalar.Create(
            scalarMapping(scalar.ScalarValue)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar NaNToZero(this IFloat64Scalar number)
    {
        return number.ScalarValue.NaNToZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar NaNInfinityToZero(this IFloat64Scalar number)
    {
        return number.ScalarValue.NaNInfinityToZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar NearZeroToZero(this IFloat64Scalar number, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return number.ScalarValue.NearZeroToZero(zeroEpsilon);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Positive(this IFloat64Scalar scalar)
    {
        return scalar.ToScalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Negative(this IFloat64Scalar scalar)
    {
        return -scalar.ScalarValue;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Inverse(this IFloat64Scalar scalar)
    {
        return 1d / scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Abs(this IFloat64Scalar scalar)
    {
        return Math.Abs(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Square(this IFloat64Scalar scalar)
    {
        return Float64Scalar.Create(scalar.ScalarValue * scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Cube(this IFloat64Scalar scalar)
    {
        return Float64Scalar.Create(scalar.ScalarValue * scalar.ScalarValue * scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sign(this IFloat64Scalar scalar)
    {
        return Math.Sign(scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar UnitStep(this IFloat64Scalar scalar)
    {
        return scalar.ScalarValue >= 0 ? 1 : 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sqrt(this IFloat64Scalar scalar)
    {
        return Math.Sqrt(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar SqrtOfNegative(this IFloat64Scalar scalar)
    {
        return Math.Sqrt(scalar.Negative().ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar SqrtOfAbs(this IFloat64Scalar scalar)
    {
        return Math.Sqrt(Math.Abs(scalar.ScalarValue));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Exp(this IFloat64Scalar scalar)
    {
        return Math.Exp(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar LogE(this IFloat64Scalar scalar)
    {
        return Math.Log(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Log2(this IFloat64Scalar scalar)
    {
        return Math.Log(scalar.ScalarValue, 2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Log10(this IFloat64Scalar scalar)
    {
        return Math.Log10(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Cos(this IFloat64Scalar scalar)
    {
        return Math.Cos(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sin(this IFloat64Scalar scalar)
    {
        return Math.Sin(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Tan(this IFloat64Scalar scalar)
    {
        return Math.Tan(scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sec(this IFloat64Scalar scalar)
    {
        return 1 / (
            Math.Cos(scalar.ScalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Csc(this IFloat64Scalar scalar)
    {
        return 1 / (
            Math.Sin(scalar.ScalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Cot(this IFloat64Scalar scalar)
    {
        return 1 / (
            Math.Tan(scalar.ScalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Cosh(this IFloat64Scalar scalar)
    {
        return Math.Cosh(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sinh(this IFloat64Scalar scalar)
    {
        return Math.Sinh(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Tanh(this IFloat64Scalar scalar)
    {
        return Math.Tanh(scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sech(this IFloat64Scalar scalar)
    {
        return 1 / Math.Cosh(scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Csch(this IFloat64Scalar scalar)
    {
        return 1 / Math.Sinh(scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Coth(this IFloat64Scalar scalar)
    {
        return 1 / Math.Tanh(scalar.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle ArcCos(this IFloat64Scalar scalar)
    {
        return scalar.ScalarValue.CosToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle ArcSin(this IFloat64Scalar scalar)
    {
        return scalar.ScalarValue.SinToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle ArcTan(this IFloat64Scalar scalar)
    {
        return scalar.ScalarValue.TanToPolarAngle();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle ArcSec(this IFloat64Scalar scalar)
    {
        return (1 / scalar.ScalarValue).CosToPolarAngle();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle ArcCsc(this IFloat64Scalar scalar)
    {
        return (1 / scalar.ScalarValue).SinToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle ArcCot(this IFloat64Scalar scalar)
    {
        return (1 / scalar.ScalarValue).TanToPolarAngle();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle ArcTan2(this IFloat64Scalar scalarX, int scalarY)
    {
        return scalarX.ArcTan2(
            Float64Scalar.Create(scalarY)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle ArcTan2(this IFloat64Scalar scalarX, double scalarY)
    {
        return scalarX.ArcTan2(
            Float64Scalar.Create(scalarY)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle ArcTan2(this IFloat64Scalar scalarX, Float64Scalar scalarY)
    {
        return LinFloat64PolarAngle.CreateFromVector(scalarX.ScalarValue, scalarY.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle ArcTan2(this IFloat64Scalar scalarX, IFloat64Scalar scalarY)
    {
        return LinFloat64PolarAngle.CreateFromVector(scalarX.ScalarValue, scalarY.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngle ArcTan2(this double scalarX, IFloat64Scalar scalarY)
    {
        return Float64Scalar.Create(scalarX).ArcTan2(scalarY);
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Power(this IFloat64Scalar scalar, IntegerSign exponentScalar)
    {
        return scalar.Power(
            Float64Scalar.Create(exponentScalar).ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Power(this IFloat64Scalar scalar, int exponentScalar)
    {
        return scalar.Power(
            Float64Scalar.Create(exponentScalar).ScalarValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Power(this IFloat64Scalar scalar, double exponentScalar)
    {
        return Math.Pow(scalar.ScalarValue, exponentScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Power(this IFloat64Scalar scalar, Float64Scalar exponentScalar)
    {
        return scalar.Power(exponentScalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Power(this IFloat64Scalar scalar, IFloat64Scalar exponentScalar)
    {
        return scalar.Power(exponentScalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Power(this IntegerSign scalar, IFloat64Scalar exponentScalar)
    {
        return Float64Scalar.Create(scalar).Power(exponentScalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Power(this int scalar, int exponentScalar)
    {
        return Float64Scalar.Create(scalar).Power(exponentScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Power(this int scalar, double exponentScalar)
    {
        return Float64Scalar.Create(scalar).Power(exponentScalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Power(this int scalar, Float64Scalar exponentScalar)
    {
        return Float64Scalar.Create(scalar).Power(exponentScalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Power(this int scalar, IFloat64Scalar exponentScalar)
    {
        return Float64Scalar.Create(scalar).Power(exponentScalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Power(this uint scalar, IFloat64Scalar exponentScalar)
    {
        return Float64Scalar.Create(scalar).Power(exponentScalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Power(this long scalar, IFloat64Scalar exponentScalar)
    {
        return Float64Scalar.Create(scalar).Power(exponentScalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Power(this ulong scalar, IFloat64Scalar exponentScalar)
    {
        return Float64Scalar.Create(scalar).Power(exponentScalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Power(this float scalar, IFloat64Scalar exponentScalar)
    {
        return Float64Scalar.Create(scalar).Power(exponentScalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Power(this double scalar, IFloat64Scalar exponentScalar)
    {
        return Float64Scalar.Create(scalar).Power(exponentScalar.ScalarValue);
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Log(this IFloat64Scalar scalar, int baseScalar)
    {
        return scalar.Log(
            Float64Scalar.Create(baseScalar)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Log(this IFloat64Scalar scalar, double baseScalar)
    {
        return scalar.Log(
            Float64Scalar.Create(baseScalar)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Log(this IFloat64Scalar scalar, Float64Scalar baseScalar)
    {
        return scalar.Log(baseScalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Log(this IFloat64Scalar scalar, IFloat64Scalar baseScalar)
    {
        return scalar.Log(baseScalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Log(this int scalar, IFloat64Scalar baseScalar)
    {
        return Float64Scalar.Create(scalar).Log(baseScalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Log(this uint scalar, IFloat64Scalar baseScalar)
    {
        return Float64Scalar.Create(scalar).Log(baseScalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Log(this long scalar, IFloat64Scalar baseScalar)
    {
        return Float64Scalar.Create(scalar).Log(baseScalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Log(this ulong scalar, IFloat64Scalar baseScalar)
    {
        return Float64Scalar.Create(scalar).Log(baseScalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Log(this float scalar, IFloat64Scalar baseScalar)
    {
        return Float64Scalar.Create(scalar).Log(baseScalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Log(this double scalar, IFloat64Scalar baseScalar)
    {
        return Float64Scalar.Create(scalar).Log(baseScalar.ScalarValue);
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Add(this IFloat64Scalar scalar1, IntegerSign scalar)
    {
        return scalar1.ScalarValue + scalar.ToFloat64();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Add(this IFloat64Scalar scalar1, double scalar)
    {
        return scalar1.ScalarValue + scalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Add(this IFloat64Scalar scalar1, Float64Scalar scalar)
    {
        return scalar1.ScalarValue + scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Add(this IFloat64Scalar scalar1, IFloat64Scalar scalar)
    {
        return scalar1.ScalarValue + scalar.ScalarValue;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Add(this IntegerSign scalar1, IFloat64Scalar scalar)
    {
        return scalar1.ToFloat64() + scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Add(this int scalar1, IFloat64Scalar scalar)
    {
        return scalar1 + scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Add(this uint scalar1, IFloat64Scalar scalar)
    {
        return scalar1 + scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Add(this long scalar1, IFloat64Scalar scalar)
    {
        return scalar1 + scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Add(this ulong scalar1, IFloat64Scalar scalar)
    {
        return scalar1 + scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Add(this float scalar1, IFloat64Scalar scalar)
    {
        return scalar1 + scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Add(this double scalar1, IFloat64Scalar scalar)
    {
        return scalar1 + scalar.ScalarValue;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Subtract(this IFloat64Scalar scalar1, IntegerSign scalar)
    {
        return scalar1.ScalarValue - scalar.ToFloat64();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Subtract(this IFloat64Scalar scalar1, double scalar)
    {
        return scalar1.ScalarValue - scalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Subtract(this IFloat64Scalar scalar1, Float64Scalar scalar)
    {
        return scalar1.ScalarValue - scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Subtract(this IFloat64Scalar scalar1, IFloat64Scalar scalar)
    {
        return scalar1.ScalarValue - scalar.ScalarValue;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Subtract(this IntegerSign scalar1, IFloat64Scalar scalar)
    {
        return scalar1.ToFloat64() - scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Subtract(this int scalar1, IFloat64Scalar scalar)
    {
        return scalar1 - scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Subtract(this uint scalar1, IFloat64Scalar scalar)
    {
        return scalar1 - scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Subtract(this long scalar1, IFloat64Scalar scalar)
    {
        return scalar1 - scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Subtract(this ulong scalar1, IFloat64Scalar scalar)
    {
        return scalar1 - scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Subtract(this float scalar1, IFloat64Scalar scalar)
    {
        return scalar1 - scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Subtract(this double scalar1, IFloat64Scalar scalar)
    {
        return scalar1 - scalar.ScalarValue;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Times(this IFloat64Scalar scalar1, IntegerSign scalar)
    {
        return scalar1.ScalarValue * scalar.ToFloat64();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Times(this IFloat64Scalar scalar1, double scalar)
    {
        return scalar1.ScalarValue * scalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Times(this IFloat64Scalar scalar1, Float64Scalar scalar)
    {
        return scalar1.ScalarValue * scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Times(this IFloat64Scalar scalar1, IFloat64Scalar scalar)
    {
        return scalar1.ScalarValue * scalar.ScalarValue;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Times(this IntegerSign scalar1, IFloat64Scalar scalar)
    {
        return scalar1.ToFloat64() * scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Times(this int scalar1, IFloat64Scalar scalar)
    {
        return scalar1 * scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Times(this uint scalar1, IFloat64Scalar scalar)
    {
        return scalar1 * scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Times(this long scalar1, IFloat64Scalar scalar)
    {
        return scalar1 * scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Times(this ulong scalar1, IFloat64Scalar scalar)
    {
        return scalar1 * scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Times(this float scalar1, IFloat64Scalar scalar)
    {
        return scalar1 * scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Times(this double scalar1, IFloat64Scalar scalar)
    {
        return scalar1 * scalar.ScalarValue;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Divide(this IFloat64Scalar scalar1, IntegerSign scalar)
    {
        return scalar1.ScalarValue / scalar.ToFloat64();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Divide(this IFloat64Scalar scalar1, double scalar)
    {
        return scalar1.ScalarValue / scalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Divide(this IFloat64Scalar scalar1, Float64Scalar scalar)
    {
        return scalar1.ScalarValue / scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Divide(this IFloat64Scalar scalar1, IFloat64Scalar scalar)
    {
        return scalar1.ScalarValue / scalar.ScalarValue;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Divide(this IntegerSign scalar1, IFloat64Scalar scalar)
    {
        return scalar1.ToFloat64() / scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Divide(this int scalar1, IFloat64Scalar scalar)
    {
        return scalar1 / scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Divide(this uint scalar1, IFloat64Scalar scalar)
    {
        return scalar1 / scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Divide(this long scalar1, IFloat64Scalar scalar)
    {
        return scalar1 / scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Divide(this ulong scalar1, IFloat64Scalar scalar)
    {
        return scalar1 / scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Divide(this float scalar1, IFloat64Scalar scalar)
    {
        return scalar1 / scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Divide(this double scalar1, IFloat64Scalar scalar)
    {
        return scalar1 / scalar.ScalarValue;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar NegativeTimes(this IFloat64Scalar scalar1, IntegerSign scalar)
    {
        return -scalar1.ScalarValue * scalar.ToFloat64();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar NegativeTimes(this IFloat64Scalar scalar1, double scalar)
    {
        return -scalar1.ScalarValue * scalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar NegativeTimes(this IFloat64Scalar scalar1, Float64Scalar scalar)
    {
        return -scalar1.ScalarValue * scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar NegativeTimes(this IFloat64Scalar scalar1, IFloat64Scalar scalar)
    {
        return -scalar1.ScalarValue * scalar.ScalarValue;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar NegativeTimes(this IntegerSign scalar1, IFloat64Scalar scalar)
    {
        return -scalar1.ToFloat64() * scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar NegativeTimes(this int scalar1, IFloat64Scalar scalar)
    {
        return -scalar1 * scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar NegativeTimes(this uint scalar1, IFloat64Scalar scalar)
    {
        return -scalar1 * scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar NegativeTimes(this long scalar1, IFloat64Scalar scalar)
    {
        return -scalar1 * scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar NegativeTimes(this ulong scalar1, IFloat64Scalar scalar)
    {
        return scalar1 * -scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar NegativeTimes(this float scalar1, IFloat64Scalar scalar)
    {
        return -scalar1 * scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar NegativeTimes(this double scalar1, IFloat64Scalar scalar)
    {
        return -scalar1 * scalar.ScalarValue;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar NegativeDivide(this IFloat64Scalar scalar1, IntegerSign scalar)
    {
        return -scalar1.ScalarValue / scalar.ToFloat64();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar NegativeDivide(this IFloat64Scalar scalar1, double scalar)
    {
        return -scalar1.ScalarValue / scalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar NegativeDivide(this IFloat64Scalar scalar1, Float64Scalar scalar)
    {
        return -scalar1.ScalarValue / scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar NegativeDivide(this IFloat64Scalar scalar1, IFloat64Scalar scalar)
    {
        return -scalar1.ScalarValue / scalar.ScalarValue;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar NegativeDivide(this IntegerSign scalar1, IFloat64Scalar scalar)
    {
        return -scalar1.ToFloat64() / scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar NegativeDivide(this int scalar1, IFloat64Scalar scalar)
    {
        return -scalar1 / scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar NegativeDivide(this uint scalar1, IFloat64Scalar scalar)
    {
        return -scalar1 / scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar NegativeDivide(this long scalar1, IFloat64Scalar scalar)
    {
        return -scalar1 / scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar NegativeDivide(this ulong scalar1, IFloat64Scalar scalar)
    {
        return scalar1 / -scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar NegativeDivide(this float scalar1, IFloat64Scalar scalar)
    {
        return -scalar1 / scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar NegativeDivide(this double scalar1, IFloat64Scalar scalar)
    {
        return -scalar1 / scalar.ScalarValue;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Min(this IFloat64Scalar scalar1, IntegerSign scalar2)
    {
        return scalar1.ScalarValue < scalar2.ToFloat64() 
            ? scalar1.ScalarValue 
            : scalar2.ToFloat64();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Min(this IFloat64Scalar scalar1, double scalar2)
    {
        return scalar1.ScalarValue < scalar2 
            ? scalar1.ScalarValue 
            : scalar2;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Min(this IFloat64Scalar scalar1, Float64Scalar scalar2)
    {
        return scalar1.ScalarValue < scalar2.ScalarValue 
            ? scalar1.ScalarValue 
            : scalar2.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Min(this IFloat64Scalar scalar1, IFloat64Scalar scalar2)
    {
        return scalar1.ScalarValue < scalar2.ScalarValue 
            ? scalar1.ScalarValue 
            : scalar2.ScalarValue;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Min(this IntegerSign scalar1, IFloat64Scalar scalar2)
    {
        return scalar1.ToFloat64() < scalar2.ScalarValue 
            ? scalar1.ToFloat64() 
            : scalar2.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Min(this int scalar1, IFloat64Scalar scalar2)
    {
        return scalar1 < scalar2.ScalarValue 
            ? scalar1 
            : scalar2.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Min(this uint scalar1, IFloat64Scalar scalar2)
    {
        return scalar1 < scalar2.ScalarValue 
            ? scalar1 
            : scalar2.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Min(this long scalar1, IFloat64Scalar scalar2)
    {
        return scalar1 < scalar2.ScalarValue 
            ? scalar1 
            : scalar2.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Min(this ulong scalar1, IFloat64Scalar scalar2)
    {
        return scalar1 < scalar2.ScalarValue 
            ? scalar1 
            : scalar2.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Min(this float scalar1, IFloat64Scalar scalar2)
    {
        return scalar1 < scalar2.ScalarValue 
            ? scalar1 
            : scalar2.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Min(this double scalar1, IFloat64Scalar scalar2)
    {
        return scalar1 < scalar2.ScalarValue 
            ? scalar1
            : scalar2.ScalarValue;
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Max(this IFloat64Scalar scalar1, IntegerSign scalar2)
    {
        return scalar1.ScalarValue > scalar2.ToFloat64() 
            ? scalar1.ScalarValue 
            : scalar2.ToFloat64();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Max(this IFloat64Scalar scalar1, double scalar2)
    {
        return scalar1.ScalarValue > scalar2 
            ? scalar1.ScalarValue 
            : scalar2;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Max(this IFloat64Scalar scalar1, Float64Scalar scalar2)
    {
        return scalar1.ScalarValue > scalar2.ScalarValue 
            ? scalar1.ScalarValue 
            : scalar2.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Max(this IFloat64Scalar scalar1, IFloat64Scalar scalar2)
    {
        return scalar1.ScalarValue > scalar2.ScalarValue 
            ? scalar1.ScalarValue 
            : scalar2.ScalarValue;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Max(this IntegerSign scalar1, IFloat64Scalar scalar2)
    {
        return scalar1.ToFloat64() > scalar2.ScalarValue 
            ? scalar1.ToFloat64() 
            : scalar2.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Max(this int scalar1, IFloat64Scalar scalar2)
    {
        return scalar1 > scalar2.ScalarValue 
            ? scalar1 
            : scalar2.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Max(this uint scalar1, IFloat64Scalar scalar2)
    {
        return scalar1 > scalar2.ScalarValue 
            ? scalar1 
            : scalar2.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Max(this long scalar1, IFloat64Scalar scalar2)
    {
        return scalar1 > scalar2.ScalarValue 
            ? scalar1 
            : scalar2.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Max(this ulong scalar1, IFloat64Scalar scalar2)
    {
        return scalar1 > scalar2.ScalarValue 
            ? scalar1 
            : scalar2.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Max(this float scalar1, IFloat64Scalar scalar2)
    {
        return scalar1 > scalar2.ScalarValue 
            ? scalar1 
            : scalar2.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Max(this double scalar1, IFloat64Scalar scalar2)
    {
        return scalar1 > scalar2.ScalarValue 
            ? scalar1
            : scalar2.ScalarValue;
    }
    
}