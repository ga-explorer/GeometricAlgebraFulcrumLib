namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors;

public static class RGaScalarUtils
{
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsEqualTo<T>(this RGaScalar<T> scalar1, int scalar2)
    //{
    //    var processor = scalar1.ScalarProcessor;

    //    return processor.IsZero(
    //        processor.Subtract(
    //            scalar1.ScalarValue,
    //            processor.ScalarFromNumber(scalar2)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsEqualTo<T>(this int scalar1, RGaScalar<T> scalar2)
    //{
    //    var processor = scalar2.ScalarProcessor;

    //    return processor.IsZero(
    //        processor.Subtract(
    //            processor.ScalarFromNumber(scalar1),
    //            scalar2.ScalarValue
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsEqualTo<T>(this RGaScalar<T> scalar1, uint scalar2)
    //{
    //    var processor = scalar1.ScalarProcessor;

    //    return processor.IsZero(
    //        processor.Subtract(
    //            scalar1.ScalarValue,
    //            processor.ScalarFromNumber(scalar2)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsEqualTo<T>(this uint scalar1, RGaScalar<T> scalar2)
    //{
    //    var processor = scalar2.ScalarProcessor;

    //    return processor.IsZero(
    //        processor.Subtract(
    //            processor.ScalarFromNumber(scalar1),
    //            scalar2.ScalarValue
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsEqualTo<T>(this RGaScalar<T> scalar1, long scalar2)
    //{
    //    var processor = scalar1.ScalarProcessor;

    //    return processor.IsZero(
    //        processor.Subtract(
    //            scalar1.ScalarValue,
    //            processor.ScalarFromNumber(scalar2)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsEqualTo<T>(this long scalar1, RGaScalar<T> scalar2)
    //{
    //    var processor = scalar2.ScalarProcessor;

    //    return processor.IsZero(
    //        processor.Subtract(
    //            processor.ScalarFromNumber(scalar1),
    //            scalar2.ScalarValue
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsEqualTo<T>(this RGaScalar<T> scalar1, ulong scalar2)
    //{
    //    var processor = scalar1.ScalarProcessor;

    //    return processor.IsZero(
    //        processor.Subtract(
    //            scalar1.ScalarValue,
    //            processor.ScalarFromNumber(scalar2)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsEqualTo<T>(this ulong scalar1, RGaScalar<T> scalar2)
    //{
    //    var processor = scalar2.ScalarProcessor;

    //    return processor.IsZero(
    //        processor.Subtract(
    //            processor.ScalarFromNumber(scalar1),
    //            scalar2.ScalarValue
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsEqualTo<T>(this RGaScalar<T> scalar1, float scalar2)
    //{
    //    var processor = scalar1.ScalarProcessor;

    //    return processor.IsZero(
    //        processor.Subtract(
    //            scalar1.ScalarValue,
    //            processor.ScalarFromNumber(scalar2)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsEqualTo<T>(this float scalar1, RGaScalar<T> scalar2)
    //{
    //    var processor = scalar2.ScalarProcessor;

    //    return processor.IsZero(
    //        processor.Subtract(
    //            processor.ScalarFromNumber(scalar1),
    //            scalar2.ScalarValue
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsEqualTo<T>(this RGaScalar<T> scalar1, double scalar2)
    //{
    //    var processor = scalar1.ScalarProcessor;

    //    return processor.IsZero(
    //        processor.Subtract(
    //            scalar1.ScalarValue,
    //            processor.ScalarFromNumber(scalar2)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsEqualTo<T>(this double scalar1, RGaScalar<T> scalar2)
    //{
    //    var processor = scalar2.ScalarProcessor;

    //    return processor.IsZero(
    //        processor.Subtract(
    //            processor.ScalarFromNumber(scalar1),
    //            scalar2.ScalarValue
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsEqualTo<T>(this RGaScalar<T> scalar1, T scalar2)
    //{
    //    var processor = scalar1.ScalarProcessor;

    //    return processor.IsZero(
    //        processor.Subtract(
    //            scalar1.ScalarValue,
    //            scalar2
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsEqualTo<T>(this T scalar1, RGaScalar<T> scalar2)
    //{
    //    var processor = scalar2.ScalarProcessor;

    //    return processor.IsZero(
    //        processor.Subtract(
    //            scalar1,
    //            scalar2.ScalarValue
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsEqualTo<T>(this RGaScalar<T> scalar1, string scalar2)
    //{
    //    var processor = scalar1.ScalarProcessor;

    //    return processor.IsZero(
    //        processor.Subtract(
    //            scalar1.ScalarValue,
    //            processor.ScalarFromText(scalar2)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsEqualTo<T>(this string scalar1, RGaScalar<T> scalar2)
    //{
    //    var processor = scalar2.ScalarProcessor;

    //    return processor.IsZero(
    //        processor.Subtract(
    //            processor.ScalarFromText(scalar1),
    //            scalar2.ScalarValue
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsEqualTo<T>(this RGaScalar<T> scalar1, object scalar2)
    //{
    //    var processor = scalar1.ScalarProcessor;

    //    return processor.IsZero(
    //        processor.Subtract(
    //            scalar1.ScalarValue,
    //            processor.ScalarFromObject(scalar2)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsEqualTo<T>(this object scalar1, RGaScalar<T> scalar2)
    //{
    //    var processor = scalar2.ScalarProcessor;

    //    return processor.IsZero(
    //        processor.Subtract(
    //            processor.ScalarFromObject(scalar1),
    //            scalar2.ScalarValue
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsEqualTo<T>(this RGaScalar<T> scalar1, RGaScalar<T> scalar2)
    //{
    //    var processor = scalar1.ScalarProcessor;

    //    return processor.IsZero(
    //        processor.Subtract(
    //            scalar1.ScalarValue,
    //            scalar2.ScalarValue
    //        )
    //    );
    //}


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaScalar<T> Inverse<T>(this RGaScalar<T> scalar)
    //{
    //    var metric = scalar.Processor;
    //    var processor = scalar.ScalarProcessor;

    //    return metric.Scalar(
                
    //        processor.Inverse(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaScalar<T> Abs<T>(this RGaScalar<T> scalar)
    //{
    //    var metric = scalar.Processor;
    //    var processor = scalar.ScalarProcessor;

    //    return metric.Scalar(
                
    //        processor.Abs(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaScalar<T> Square<T>(this RGaScalar<T> scalar)
    //{
    //    var metric = scalar.Processor;
    //    var processor = scalar.ScalarProcessor;

    //    return metric.Scalar(
                
    //        processor.Square(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaScalar<T> Cube<T>(this RGaScalar<T> scalar)
    //{
    //    var metric = scalar.Processor;
    //    var processor = scalar.ScalarProcessor;

    //    return metric.Scalar(
                
    //        processor.Cube(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaScalar<T> Sign<T>(this RGaScalar<T> scalar)
    //{
    //    var metric = scalar.Processor;
    //    var processor = scalar.ScalarProcessor;

    //    return metric.Scalar(
                
    //        processor.Sign(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaScalar<T> Sqrt<T>(this RGaScalar<T> scalar)
    //{
    //    var metric = scalar.Processor;
    //    var processor = scalar.ScalarProcessor;

    //    return metric.Scalar(
                
    //        processor.Sqrt(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaScalar<T> SqrtOfAbs<T>(this RGaScalar<T> scalar)
    //{
    //    var metric = scalar.Processor;
    //    var processor = scalar.ScalarProcessor;

    //    return metric.Scalar(
                
    //        processor.SqrtOfAbs(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaScalar<T> Power<T>(this RGaScalar<T> scalar, int exponentScalar)
    //{
    //    var metric = scalar.Processor;
    //    var processor = scalar.ScalarProcessor;

    //    return metric.Scalar(
                
    //        processor.Power(scalar.ScalarValue, exponentScalar)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaScalar<T> Power<T>(this RGaScalar<T> scalar, double exponentScalar)
    //{
    //    var metric = scalar.Processor;
    //    var processor = scalar.ScalarProcessor;

    //    return metric.Scalar(
                
    //        processor.Power(scalar.ScalarValue, exponentScalar)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaScalar<T> Power<T>(this RGaScalar<T> scalar, T exponentScalar)
    //{
    //    var metric = scalar.Processor;
    //    var processor = scalar.ScalarProcessor;

    //    return metric.Scalar(
                
    //        processor.Power(scalar.ScalarValue, exponentScalar)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaScalar<T> Exp<T>(this RGaScalar<T> scalar)
    //{
    //    var metric = scalar.Processor;
    //    var processor = scalar.ScalarProcessor;

    //    return metric.Scalar(
                
    //        processor.Exp(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaScalar<T> Log<T>(this RGaScalar<T> scalar)
    //{
    //    var metric = scalar.Processor;
    //    var processor = scalar.ScalarProcessor;

    //    return metric.Scalar(
                
    //        processor.LogE(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaScalar<T> Log2<T>(this RGaScalar<T> scalar)
    //{
    //    var metric = scalar.Processor;
    //    var processor = scalar.ScalarProcessor;

    //    return metric.Scalar(
                
    //        processor.Log2(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaScalar<T> Log10<T>(this RGaScalar<T> scalar)
    //{
    //    var metric = scalar.Processor;
    //    var processor = scalar.ScalarProcessor;

    //    return metric.Scalar(
                
    //        processor.Log10(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaScalar<T> Log<T>(this RGaScalar<T> scalar, T baseScalar)
    //{
    //    var metric = scalar.Processor;
    //    var processor = scalar.ScalarProcessor;

    //    return metric.Scalar(
                
    //        processor.Log(baseScalar, scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaScalar<T> Log<T>(this RGaScalar<T> scalar, RGaScalar<T> baseScalar)
    //{
    //    var metric = scalar.Processor;
    //    var processor = scalar.ScalarProcessor;

    //    return metric.Scalar(
                
    //        processor.Log(baseScalar.ScalarValue, scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaScalar<T> Cos<T>(this RGaScalar<T> scalar)
    //{
    //    var metric = scalar.Processor;
    //    var processor = scalar.ScalarProcessor;

    //    return metric.Scalar(
                
    //        processor.Cos(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaScalar<T> Sin<T>(this RGaScalar<T> scalar)
    //{
    //    var metric = scalar.Processor;
    //    var processor = scalar.ScalarProcessor;

    //    return metric.Scalar(
                
    //        processor.Sin(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaScalar<T> Tan<T>(this RGaScalar<T> scalar)
    //{
    //    var metric = scalar.Processor;
    //    var processor = scalar.ScalarProcessor;

    //    return metric.Scalar(
                
    //        processor.Tan(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaScalar<T> ArcCos<T>(this RGaScalar<T> scalar)
    //{
    //    var metric = scalar.Processor;
    //    var processor = scalar.ScalarProcessor;

    //    return metric.Scalar(
                
    //        processor.ArcCos(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaScalar<T> ArcSin<T>(this RGaScalar<T> scalar)
    //{
    //    var metric = scalar.Processor;
    //    var processor = scalar.ScalarProcessor;

    //    return metric.Scalar(
                
    //        processor.ArcSin(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaScalar<T> ArcTan<T>(this RGaScalar<T> scalar)
    //{
    //    var metric = scalar.Processor;
    //    var processor = scalar.ScalarProcessor;

    //    return metric.Scalar(
                
    //        processor.ArcTan(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaScalar<T> ArcTan2<T>(this RGaScalar<T> scalarX, T scalarY)
    //{
    //    var metric = scalarX.Processor;
    //    var processor = scalarX.ScalarProcessor;

    //    return metric.Scalar(
                
    //        processor.ArcTan2(scalarX.ScalarValue, scalarY)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaScalar<T> ArcTan2<T>(this RGaScalar<T> scalarX, RGaScalar<T> scalarY)
    //{
    //    var metric = scalarX.Processor;
    //    var processor = scalarX.ScalarProcessor;

    //    return metric.Scalar(
                
    //        processor.ArcTan2(scalarX.ScalarValue, scalarY.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaScalar<T> Cosh<T>(this RGaScalar<T> scalar)
    //{
    //    var metric = scalar.Processor;
    //    var processor = scalar.ScalarProcessor;

    //    return metric.Scalar(
                
    //        processor.Cosh(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaScalar<T> Sinh<T>(this RGaScalar<T> scalar)
    //{
    //    var metric = scalar.Processor;
    //    var processor = scalar.ScalarProcessor;

    //    return metric.Scalar(
                
    //        processor.Sinh(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaScalar<T> Tanh<T>(this RGaScalar<T> scalar)
    //{
    //    var metric = scalar.Processor;
    //    var processor = scalar.ScalarProcessor;

    //    return metric.Scalar(
                
    //        processor.Tanh(scalar.ScalarValue)
    //    );
    //}
}