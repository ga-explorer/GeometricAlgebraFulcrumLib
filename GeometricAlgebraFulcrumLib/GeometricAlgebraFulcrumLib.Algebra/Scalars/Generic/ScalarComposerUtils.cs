using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

public static class ScalarComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T ValueFromNumber<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign sign)
    {
        if (sign.IsZero)
            return scalarProcessor.ZeroValue;

        return sign.IsPositive
            ? scalarProcessor.OneValue
            : scalarProcessor.MinusOneValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T ValueFromNumber<T>(this IScalarProcessor<T> scalarProcessor, int number)
    {
        return scalarProcessor.ScalarFromNumber(number).ScalarValue;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T ValueFromNumber<T>(this IScalarProcessor<T> scalarProcessor, uint number)
    {
        return scalarProcessor.ScalarFromNumber(number).ScalarValue;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T ValueFromNumber<T>(this IScalarProcessor<T> scalarProcessor, long number)
    {
        return scalarProcessor.ScalarFromNumber(number).ScalarValue;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T ValueFromNumber<T>(this IScalarProcessor<T> scalarProcessor, ulong number)
    {
        return scalarProcessor.ScalarFromNumber(number).ScalarValue;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T ValueFromNumber<T>(this IScalarProcessor<T> scalarProcessor, float number)
    {
        return scalarProcessor.ScalarFromNumber(number).ScalarValue;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T ValueFromNumber<T>(this IScalarProcessor<T> scalarProcessor, double number)
    {
        return scalarProcessor.ScalarFromNumber(number).ScalarValue;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T ValueFromText<T>(this IScalarProcessor<T> scalarProcessor, string number)
    {
        return scalarProcessor.ScalarFromText(number).ScalarValue;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T ValueFromObject<T>(this IScalarProcessor<T> scalarProcessor, object number)
    {
        return scalarProcessor.ScalarFromObject(number).ScalarValue;
    }


    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T ValueFromNumber<T>(this IntegerSign sign, IScalarProcessor<T> scalarProcessor)
    {
        if (sign.IsZero)
            return scalarProcessor.ZeroValue;

        return sign.IsPositive
            ? scalarProcessor.OneValue
            : scalarProcessor.MinusOneValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T ValueFromNumber<T>(this int scalar, IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.ValueFromNumber(scalar);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T ValueFromNumber<T>(this uint scalar, IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.ValueFromNumber(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T ValueFromNumber<T>(this long scalar, IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.ValueFromNumber(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T ValueFromNumber<T>(this ulong scalar, IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.ValueFromNumber(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T ValueFromNumber<T>(this float scalar, IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.ValueFromNumber(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T ValueFromNumber<T>(this double scalar, IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.ValueFromNumber(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T ValueFromText<T>(this string scalar, IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.ValueFromText(scalar);
    }



    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> ScalarFromNumber<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign sign)
    {
        if (sign.IsZero)
            return scalarProcessor.Zero;

        return sign.IsPositive
            ? scalarProcessor.One
            : scalarProcessor.MinusOne;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> ScalarFromValue<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        return Scalar<T>.Create(scalarProcessor, scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> ScalarFromObject<T>(this IScalarProcessor<T> scalarProcessor, object valueObject)
    {
        return valueObject switch
        {
            int v => scalarProcessor.ScalarFromNumber(v),
            uint v => scalarProcessor.ScalarFromNumber(v),
            long v => scalarProcessor.ScalarFromNumber(v),
            ulong v => scalarProcessor.ScalarFromNumber(v),
            float v => scalarProcessor.ScalarFromNumber(v),
            double v => scalarProcessor.ScalarFromNumber(v),
            string v => scalarProcessor.ScalarFromText(v),
            T v => scalarProcessor.ScalarFromValue(v),
            IScalar<T> v => v.ToScalar(),
            _ => throw new InvalidOperationException()
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> ScalarFromNumber<T>(this IntegerSign sign, IScalarProcessor<T> scalarProcessor)
    {
        if (sign.IsZero)
            return scalarProcessor.Zero;

        return sign.IsPositive
            ? scalarProcessor.One
            : scalarProcessor.MinusOne;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> ScalarFromNumber<T>(this int scalar, IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.ScalarFromNumber(scalar);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> ScalarFromNumber<T>(this uint scalar, IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.ScalarFromNumber(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> ScalarFromNumber<T>(this long scalar, IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.ScalarFromNumber(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> ScalarFromNumber<T>(this ulong scalar, IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.ScalarFromNumber(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> ScalarFromNumber<T>(this float scalar, IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.ScalarFromNumber(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> ScalarFromNumber<T>(this double scalar, IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.ScalarFromNumber(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> ScalarFromText<T>(this string scalar, IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.ScalarFromText(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> ScalarFromValue<T>(this T scalar, IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.ScalarFromValue(scalar);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Scalar<T>> ScalarsFromNumbers<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<int> valuesList)
    {
        return valuesList.Select(scalarProcessor.ScalarFromNumber);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Scalar<T>> ScalarsFromNumbers<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<uint> valuesList)
    {
        return valuesList.Select(scalarProcessor.ScalarFromNumber);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Scalar<T>> ScalarsFromNumbers<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<long> valuesList)
    {
        return valuesList.Select(scalarProcessor.ScalarFromNumber);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Scalar<T>> ScalarsFromNumbers<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<ulong> valuesList)
    {
        return valuesList.Select(scalarProcessor.ScalarFromNumber);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Scalar<T>> ScalarsFromNumbers<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<float> valuesList)
    {
        return valuesList.Select(scalarProcessor.ScalarFromNumber);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Scalar<T>> ScalarsFromNumbers<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<double> valuesList)
    {
        return valuesList.Select(scalarProcessor.ScalarFromNumber);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Scalar<T>> ScalarsFromText<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<string> valuesList)
    {
        return valuesList.Select(scalarProcessor.ScalarFromText);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Scalar<T>> ScalarsFromObjects<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<object> valuesList)
    {
        return valuesList.Select(scalarProcessor.ScalarFromObject);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Scalar<T>> ScalarsFrom<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<Scalar<T>> valuesList)
    {
        return valuesList;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Scalar<T>> ScalarsFrom<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<IScalar<T>> valuesList)
    {
        return valuesList.Select(value => value.ToScalar());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> GetScalarValues<T>(this IEnumerable<Scalar<T>> valuesList)
    {
        return valuesList.Select(value => value.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> GetScalarValues<T>(this IEnumerable<IScalar<T>> valuesList)
    {
        return valuesList.Select(value => value.ScalarValue);
    }

    
}