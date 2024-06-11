namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;

public static class RGaFloat64ScalarUtils
{
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsEqualTo(this RGaFloat64Scalar scalar1, int scalar2)
    //{
    //    return (scalar1.ScalarValue - scalar2).IsZero();
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsEqualTo(this int scalar1, RGaFloat64Scalar scalar2)
    //{
    //    return (scalar1 - scalar2.ScalarValue).IsZero();
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsEqualTo(this RGaFloat64Scalar scalar1, uint scalar2)
    //{
    //    return (scalar1.ScalarValue - scalar2).IsZero();
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsEqualTo(this uint scalar1, RGaFloat64Scalar scalar2)
    //{
    //    return (scalar1 - scalar2.ScalarValue).IsZero();
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsEqualTo(this RGaFloat64Scalar scalar1, long scalar2)
    //{
    //    return (scalar1.ScalarValue - scalar2).IsZero();
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsEqualTo(this long scalar1, RGaFloat64Scalar scalar2)
    //{
    //    return (scalar1 - scalar2.ScalarValue).IsZero();
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsEqualTo(this RGaFloat64Scalar scalar1, ulong scalar2)
    //{
    //    return (scalar1.ScalarValue - scalar2).IsZero();
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsEqualTo(this ulong scalar1, RGaFloat64Scalar scalar2)
    //{
    //    return (scalar1 - scalar2.ScalarValue).IsZero();
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsEqualTo(this RGaFloat64Scalar scalar1, float scalar2)
    //{
    //    return (scalar1.ScalarValue - scalar2).IsZero();
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsEqualTo(this float scalar1, RGaFloat64Scalar scalar2)
    //{
    //    return (scalar1 - scalar2.ScalarValue).IsZero();
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsEqualTo(this RGaFloat64Scalar scalar1, double scalar2)
    //{
    //    return (scalar1.ScalarValue - scalar2).IsZero();
    //}
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsEqualTo(this double scalar1, RGaFloat64Scalar scalar2)
    //{
    //    return (scalar1 - scalar2.ScalarValue).IsZero();
    //}
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsEqualTo(this RGaFloat64Scalar scalar1, RGaFloat64Scalar scalar2)
    //{
    //    return (scalar1 - scalar2).IsZero;
    //}


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaFloat64Scalar Inverse(this RGaFloat64Scalar scalar)
    //{
    //    var metric = scalar.Processor;

    //    return metric.Scalar(1 / scalar.ScalarValue);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaFloat64Scalar Abs(this RGaFloat64Scalar scalar)
    //{
    //    var metric = scalar.Processor;
            
    //    return metric.Scalar(
    //        scalar.ScalarValue.Abs()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaFloat64Scalar Square(this RGaFloat64Scalar scalar)
    //{
    //    var metric = scalar.Processor;
            
    //    return metric.Scalar(
    //        scalar.ScalarValue.Square()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaFloat64Scalar Cube(this RGaFloat64Scalar scalar)
    //{
    //    var metric = scalar.Processor;
            
    //    return metric.Scalar(
    //        scalar.ScalarValue.Cube()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaFloat64Scalar Sign(this RGaFloat64Scalar scalar)
    //{
    //    var metric = scalar.Processor;

    //    return metric.Scalar(
    //        scalar.ScalarValue.Sign().ToFloat64()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaFloat64Scalar Sqrt(this RGaFloat64Scalar scalar)
    //{
    //    var metric = scalar.Processor;
            
    //    return metric.Scalar(
    //        scalar.ScalarValue.Sqrt()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaFloat64Scalar SqrtOfAbs(this RGaFloat64Scalar scalar)
    //{
    //    var metric = scalar.Processor;
            
    //    return metric.Scalar(
    //        scalar.ScalarValue.SqrtOfAbs()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaFloat64Scalar Power(this RGaFloat64Scalar scalar, int exponentScalar)
    //{
    //    var metric = scalar.Processor;
            
    //    return metric.Scalar(
    //        scalar.ScalarValue.Power(exponentScalar)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaFloat64Scalar Power(this RGaFloat64Scalar scalar, double exponentScalar)
    //{
    //    var metric = scalar.Processor;
            
    //    return metric.Scalar(
    //        scalar.ScalarValue.Power(exponentScalar)
    //    );
    //}
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaFloat64Scalar Exp(this RGaFloat64Scalar scalar)
    //{
    //    var metric = scalar.Processor;
            
    //    return metric.Scalar(
    //        scalar.ScalarValue.Exp()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaFloat64Scalar Log(this RGaFloat64Scalar scalar)
    //{
    //    var metric = scalar.Processor;
            
    //    return metric.Scalar(
    //        scalar.ScalarValue.LogE()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaFloat64Scalar Log2(this RGaFloat64Scalar scalar)
    //{
    //    var metric = scalar.Processor;
            
    //    return metric.Scalar(
    //        scalar.ScalarValue.Log2()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaFloat64Scalar Log10(this RGaFloat64Scalar scalar)
    //{
    //    var metric = scalar.Processor;
            
    //    return metric.Scalar(
    //        scalar.ScalarValue.Log10()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaFloat64Scalar Log(this RGaFloat64Scalar scalar, double baseScalar)
    //{
    //    var metric = scalar.Processor;
            
    //    return metric.Scalar(
    //        baseScalar.Log(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaFloat64Scalar Log(this RGaFloat64Scalar scalar, RGaFloat64Scalar baseScalar)
    //{
    //    var metric = scalar.Processor;
            
    //    return metric.Scalar(
    //        baseScalar.ScalarValue.Log(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaFloat64Scalar Cos(this RGaFloat64Scalar scalar)
    //{
    //    var metric = scalar.Processor;
            
    //    return metric.Scalar(
    //        scalar.ScalarValue.Cos()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaFloat64Scalar Sin(this RGaFloat64Scalar scalar)
    //{
    //    var metric = scalar.Processor;
            
    //    return metric.Scalar(
    //        scalar.ScalarValue.Sin()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaFloat64Scalar Tan(this RGaFloat64Scalar scalar)
    //{
    //    var metric = scalar.Processor;
            
    //    return metric.Scalar(
    //        scalar.ScalarValue.Tan()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64PolarAngle ArcCos(this RGaFloat64Scalar scalar)
    //{
    //    return LinFloat64PolarAngle.CreateFromCos(scalar.ScalarValue);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64PolarAngle ArcSin(this RGaFloat64Scalar scalar)
    //{
    //    return LinFloat64PolarAngle.CreateFromSin(scalar.ScalarValue);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64PolarAngle ArcTan(this RGaFloat64Scalar scalar)
    //{
    //    return LinFloat64PolarAngle.CreateFromTan(scalar.ScalarValue);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64PolarAngle ArcTan2(this RGaFloat64Scalar scalarX, double scalarY)
    //{
    //    return LinFloat64PolarAngle.CreateFromVector(scalarX.ScalarValue, scalarY);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64PolarAngle ArcTan2(this RGaFloat64Scalar scalarX, RGaFloat64Scalar scalarY)
    //{
    //    return LinFloat64PolarAngle.CreateFromVector(scalarX.ScalarValue, scalarY.ScalarValue);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaFloat64Scalar Cosh(this RGaFloat64Scalar scalar)
    //{
    //    var metric = scalar.Processor;
            
    //    return metric.Scalar(
    //        scalar.ScalarValue.Cosh()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaFloat64Scalar Sinh(this RGaFloat64Scalar scalar)
    //{
    //    var metric = scalar.Processor;
            
    //    return metric.Scalar(
    //        scalar.ScalarValue.Sinh()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaFloat64Scalar Tanh(this RGaFloat64Scalar scalar)
    //{
    //    var metric = scalar.Processor;
            
    //    return metric.Scalar(
    //        scalar.ScalarValue.Tanh()
    //    );
    //}
}