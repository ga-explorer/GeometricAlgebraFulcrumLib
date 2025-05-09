namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;

public static class XGaScalarUtils
{
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsEqualTo<T>(this XGaScalar<T> scalar1, int scalar2)
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
    //public static bool IsEqualTo<T>(this int scalar1, XGaScalar<T> scalar2)
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
    //public static bool IsEqualTo<T>(this XGaScalar<T> scalar1, uint scalar2)
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
    //public static bool IsEqualTo<T>(this uint scalar1, XGaScalar<T> scalar2)
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
    //public static bool IsEqualTo<T>(this XGaScalar<T> scalar1, long scalar2)
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
    //public static bool IsEqualTo<T>(this long scalar1, XGaScalar<T> scalar2)
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
    //public static bool IsEqualTo<T>(this XGaScalar<T> scalar1, ulong scalar2)
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
    //public static bool IsEqualTo<T>(this ulong scalar1, XGaScalar<T> scalar2)
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
    //public static bool IsEqualTo<T>(this XGaScalar<T> scalar1, float scalar2)
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
    //public static bool IsEqualTo<T>(this float scalar1, XGaScalar<T> scalar2)
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
    //public static bool IsEqualTo<T>(this XGaScalar<T> scalar1, double scalar2)
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
    //public static bool IsEqualTo<T>(this double scalar1, XGaScalar<T> scalar2)
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
    //public static bool IsEqualTo<T>(this XGaScalar<T> scalar1, T scalar2)
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
    //public static bool IsEqualTo<T>(this T scalar1, XGaScalar<T> scalar2)
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
    //public static bool IsEqualTo<T>(this XGaScalar<T> scalar1, string scalar2)
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
    //public static bool IsEqualTo<T>(this string scalar1, XGaScalar<T> scalar2)
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
    //public static bool IsEqualTo<T>(this XGaScalar<T> scalar1, object scalar2)
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
    //public static bool IsEqualTo<T>(this object scalar1, XGaScalar<T> scalar2)
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
    //public static bool IsEqualTo<T>(this XGaScalar<T> scalar1, XGaScalar<T> scalar2)
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
    //public static XGaScalar<T> Inverse<T>(this XGaScalar<T> scalar)
    //{
    //    var processor = scalar.ScalarProcessor;

    //    return scalar.Processor.Scalar(
    //        processor.Inverse(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaScalar<T> Abs<T>(this XGaScalar<T> scalar)
    //{
    //    var processor = scalar.ScalarProcessor;

    //    return scalar.Processor.Scalar(
    //        processor.Abs(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaScalar<T> Square<T>(this XGaScalar<T> scalar)
    //{
    //    var processor = scalar.ScalarProcessor;

    //    return scalar.Processor.Scalar(
    //        processor.Square(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaScalar<T> Cube<T>(this XGaScalar<T> scalar)
    //{
    //    var processor = scalar.ScalarProcessor;

    //    return scalar.Processor.Scalar(
    //        processor.Cube(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaScalar<T> Sign<T>(this XGaScalar<T> scalar)
    //{
    //    var processor = scalar.ScalarProcessor;

    //    return scalar.Processor.Scalar(
    //        processor.Sign(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaScalar<T> Sqrt<T>(this XGaScalar<T> scalar)
    //{
    //    var processor = scalar.ScalarProcessor;

    //    return scalar.Processor.Scalar(
    //        processor.Sqrt(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaScalar<T> SqrtOfAbs<T>(this XGaScalar<T> scalar)
    //{
    //    var processor = scalar.ScalarProcessor;

    //    return scalar.Processor.Scalar(
    //        processor.SqrtOfAbs(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaScalar<T> Power<T>(this XGaScalar<T> scalar, int exponentScalar)
    //{
    //    var processor = scalar.ScalarProcessor;

    //    return scalar.Processor.Scalar(
    //        processor.Power(scalar.ScalarValue, exponentScalar)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaScalar<T> Power<T>(this XGaScalar<T> scalar, double exponentScalar)
    //{
    //    var processor = scalar.ScalarProcessor;

    //    return scalar.Processor.Scalar(
    //        processor.Power(scalar.ScalarValue, exponentScalar)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaScalar<T> Power<T>(this XGaScalar<T> scalar, T exponentScalar)
    //{
    //    var processor = scalar.ScalarProcessor;

    //    return scalar.Processor.Scalar(
    //        processor.Power(scalar.ScalarValue, exponentScalar)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaScalar<T> Exp<T>(this XGaScalar<T> scalar)
    //{
    //    var processor = scalar.ScalarProcessor;

    //    return scalar.Processor.Scalar(
    //        processor.Exp(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaScalar<T> Log<T>(this XGaScalar<T> scalar)
    //{
    //    var processor = scalar.ScalarProcessor;

    //    return scalar.Processor.Scalar(
    //        processor.LogE(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaScalar<T> Log2<T>(this XGaScalar<T> scalar)
    //{
    //    var processor = scalar.ScalarProcessor;

    //    return scalar.Processor.Scalar(
    //        processor.Log2(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaScalar<T> Log10<T>(this XGaScalar<T> scalar)
    //{
    //    var processor = scalar.ScalarProcessor;

    //    return scalar.Processor.Scalar(
    //        processor.Log10(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaScalar<T> Log<T>(this XGaScalar<T> scalar, T baseScalar)
    //{
    //    var processor = scalar.ScalarProcessor;

    //    return scalar.Processor.Scalar(
    //        processor.Log(baseScalar, scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaScalar<T> Log<T>(this XGaScalar<T> scalar, XGaScalar<T> baseScalar)
    //{
    //    var processor = scalar.ScalarProcessor;

    //    return scalar.Processor.Scalar(
    //        processor.Log(baseScalar.ScalarValue, scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaScalar<T> Cos<T>(this XGaScalar<T> scalar)
    //{
    //    var processor = scalar.ScalarProcessor;

    //    return scalar.Processor.Scalar(
    //        processor.Cos(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaScalar<T> Sin<T>(this XGaScalar<T> scalar)
    //{
    //    var processor = scalar.ScalarProcessor;

    //    return scalar.Processor.Scalar(
    //        processor.Sin(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaScalar<T> Tan<T>(this XGaScalar<T> scalar)
    //{
    //    var processor = scalar.ScalarProcessor;

    //    return scalar.Processor.Scalar(
    //        processor.Tan(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaScalar<T> ArcCos<T>(this XGaScalar<T> scalar)
    //{
    //    var processor = scalar.ScalarProcessor;
            
    //    return scalar.Processor.Scalar(
    //        processor.ArcCos(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaScalar<T> ArcSin<T>(this XGaScalar<T> scalar)
    //{
    //    var processor = scalar.ScalarProcessor;

    //    return scalar.Processor.Scalar(
    //        processor.ArcSin(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaScalar<T> ArcTan<T>(this XGaScalar<T> scalar)
    //{
    //    var processor = scalar.ScalarProcessor;

    //    return scalar.Processor.Scalar(
    //        processor.ArcTan(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaScalar<T> ArcTan2<T>(this XGaScalar<T> scalarX, T scalarY)
    //{
    //    var processor = scalarX.ScalarProcessor;

    //    return scalarX.Processor.Scalar(
    //        processor.ArcTan2(scalarX.ScalarValue, scalarY)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaScalar<T> ArcTan2<T>(this XGaScalar<T> scalarX, XGaScalar<T> scalarY)
    //{
    //    var processor = scalarX.ScalarProcessor;

    //    return scalarX.Processor.Scalar(
    //        processor.ArcTan2(scalarX.ScalarValue, scalarY.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaScalar<T> Cosh<T>(this XGaScalar<T> scalar)
    //{
    //    var processor = scalar.ScalarProcessor;
            
    //    return scalar.Processor.Scalar(
    //        processor.Cosh(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaScalar<T> Sinh<T>(this XGaScalar<T> scalar)
    //{
    //    var processor = scalar.ScalarProcessor;
            
    //    return scalar.Processor.Scalar(
    //        processor.Sinh(scalar.ScalarValue)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaScalar<T> Tanh<T>(this XGaScalar<T> scalar)
    //{
    //    var processor = scalar.ScalarProcessor;
            
    //    return scalar.Processor.Scalar(
    //        processor.Tanh(scalar.ScalarValue)
    //    );
    //}
}