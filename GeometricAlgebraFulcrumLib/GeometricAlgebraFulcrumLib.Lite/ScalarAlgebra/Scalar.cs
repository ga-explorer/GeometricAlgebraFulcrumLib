﻿using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;

namespace GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

public sealed record Scalar<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> CreateZero(IScalarProcessor<T> processor)
    {
        return new Scalar<T>(processor, processor.ScalarZero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> CreateOne(IScalarProcessor<T> processor)
    {
        return new Scalar<T>(processor, processor.ScalarOne);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> CreateMinusOne(IScalarProcessor<T> processor)
    {
        return new Scalar<T>(processor, processor.ScalarMinusOne);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Create(IScalarProcessor<T> processor, T scalar)
    {
        return new Scalar<T>(processor, scalar);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator T(Scalar<T> d)
    {
        return d.ScalarValue;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator -(Scalar<T> s1)
    {
        return new Scalar<T>(s1.ScalarProcessor, s1.ScalarProcessor.Negative(s1.ScalarValue));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator +(Scalar<T> s1, Scalar<T> s2)
    {
        return new Scalar<T>(s1.ScalarProcessor, s1.ScalarProcessor.Add(s1.ScalarValue, s2.ScalarValue));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator +(Scalar<T> s1, int s2)
    {
        return new Scalar<T>(
            s1.ScalarProcessor,
            s1.ScalarProcessor.Add(
                s1.ScalarValue,
                s1.ScalarProcessor.GetScalarFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator +(int s1, Scalar<T> s2)
    {
        return new Scalar<T>(
            s2.ScalarProcessor,
            s2.ScalarProcessor.Add(
                s2.ScalarProcessor.GetScalarFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator +(Scalar<T> s1, uint s2)
    {
        return new Scalar<T>(
            s1.ScalarProcessor,
            s1.ScalarProcessor.Add(
                s1.ScalarValue,
                s1.ScalarProcessor.GetScalarFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator +(uint s1, Scalar<T> s2)
    {
        return new Scalar<T>(
            s2.ScalarProcessor,
            s2.ScalarProcessor.Add(
                s2.ScalarProcessor.GetScalarFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator +(Scalar<T> s1, long s2)
    {
        return new Scalar<T>(
            s1.ScalarProcessor,
            s1.ScalarProcessor.Add(
                s1.ScalarValue,
                s1.ScalarProcessor.GetScalarFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator +(long s1, Scalar<T> s2)
    {
        return new Scalar<T>(
            s2.ScalarProcessor,
            s2.ScalarProcessor.Add(
                s2.ScalarProcessor.GetScalarFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator +(Scalar<T> s1, ulong s2)
    {
        return new Scalar<T>(
            s1.ScalarProcessor,
            s1.ScalarProcessor.Add(
                s1.ScalarValue,
                s1.ScalarProcessor.GetScalarFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator +(ulong s1, Scalar<T> s2)
    {
        return new Scalar<T>(
            s2.ScalarProcessor,
            s2.ScalarProcessor.Add(
                s2.ScalarProcessor.GetScalarFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator +(Scalar<T> s1, float s2)
    {
        return new Scalar<T>(
            s1.ScalarProcessor,
            s1.ScalarProcessor.Add(
                s1.ScalarValue,
                s1.ScalarProcessor.GetScalarFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator +(float s1, Scalar<T> s2)
    {
        return new Scalar<T>(
            s2.ScalarProcessor,
            s2.ScalarProcessor.Add(
                s2.ScalarProcessor.GetScalarFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator +(Scalar<T> s1, double s2)
    {
        return new Scalar<T>(
            s1.ScalarProcessor,
            s1.ScalarProcessor.Add(
                s1.ScalarValue,
                s1.ScalarProcessor.GetScalarFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator +(double s1, Scalar<T> s2)
    {
        return new Scalar<T>(
            s2.ScalarProcessor,
            s2.ScalarProcessor.Add(
                s2.ScalarProcessor.GetScalarFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator +(Scalar<T> s1, T s2)
    {
        return new Scalar<T>(s1.ScalarProcessor, s1.ScalarProcessor.Add(s1.ScalarValue, s2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator +(T s1, Scalar<T> s2)
    {
        return new Scalar<T>(s2.ScalarProcessor, s2.ScalarProcessor.Add(s1, s2.ScalarValue));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator -(Scalar<T> s1, Scalar<T> s2)
    {
        return new Scalar<T>(s1.ScalarProcessor, s1.ScalarProcessor.Subtract(s1.ScalarValue, s2.ScalarValue));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator -(Scalar<T> s1, int s2)
    {
        return new Scalar<T>(
            s1.ScalarProcessor,
            s1.ScalarProcessor.Subtract(
                s1.ScalarValue,
                s1.ScalarProcessor.GetScalarFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator -(int s1, Scalar<T> s2)
    {
        return new Scalar<T>(
            s2.ScalarProcessor,
            s2.ScalarProcessor.Subtract(
                s2.ScalarProcessor.GetScalarFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator -(Scalar<T> s1, uint s2)
    {
        return new Scalar<T>(
            s1.ScalarProcessor,
            s1.ScalarProcessor.Subtract(
                s1.ScalarValue,
                s1.ScalarProcessor.GetScalarFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator -(uint s1, Scalar<T> s2)
    {
        return new Scalar<T>(
            s2.ScalarProcessor,
            s2.ScalarProcessor.Subtract(
                s2.ScalarProcessor.GetScalarFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator -(Scalar<T> s1, long s2)
    {
        return new Scalar<T>(
            s1.ScalarProcessor,
            s1.ScalarProcessor.Subtract(
                s1.ScalarValue,
                s1.ScalarProcessor.GetScalarFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator -(long s1, Scalar<T> s2)
    {
        return new Scalar<T>(
            s2.ScalarProcessor,
            s2.ScalarProcessor.Subtract(
                s2.ScalarProcessor.GetScalarFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator -(Scalar<T> s1, ulong s2)
    {
        return new Scalar<T>(
            s1.ScalarProcessor,
            s1.ScalarProcessor.Subtract(
                s1.ScalarValue,
                s1.ScalarProcessor.GetScalarFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator -(ulong s1, Scalar<T> s2)
    {
        return new Scalar<T>(
            s2.ScalarProcessor,
            s2.ScalarProcessor.Subtract(
                s2.ScalarProcessor.GetScalarFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator -(Scalar<T> s1, float s2)
    {
        return new Scalar<T>(
            s1.ScalarProcessor,
            s1.ScalarProcessor.Subtract(
                s1.ScalarValue,
                s1.ScalarProcessor.GetScalarFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator -(float s1, Scalar<T> s2)
    {
        return new Scalar<T>(
            s2.ScalarProcessor,
            s2.ScalarProcessor.Subtract(
                s2.ScalarProcessor.GetScalarFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator -(Scalar<T> s1, double s2)
    {
        return new Scalar<T>(
            s1.ScalarProcessor,
            s1.ScalarProcessor.Subtract(
                s1.ScalarValue,
                s1.ScalarProcessor.GetScalarFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator -(double s1, Scalar<T> s2)
    {
        return new Scalar<T>(
            s2.ScalarProcessor,
            s2.ScalarProcessor.Subtract(
                s2.ScalarProcessor.GetScalarFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator -(Scalar<T> s1, T s2)
    {
        return new Scalar<T>(s1.ScalarProcessor, s1.ScalarProcessor.Subtract(s1.ScalarValue, s2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator -(T s1, Scalar<T> s2)
    {
        return new Scalar<T>(s2.ScalarProcessor, s2.ScalarProcessor.Subtract(s1, s2.ScalarValue));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator *(Scalar<T> s1, Scalar<T> s2)
    {
        return new Scalar<T>(s1.ScalarProcessor, s1.ScalarProcessor.Times(s1.ScalarValue, s2.ScalarValue));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator *(Scalar<T> s1, IntegerSign s2)
    {
        return new Scalar<T>(
            s1.ScalarProcessor,
            s1.ScalarProcessor.Times(
                s2,
                s1.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator *(IntegerSign s1, Scalar<T> s2)
    {
        return new Scalar<T>(
            s2.ScalarProcessor,
            s2.ScalarProcessor.Times(
                s1,
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator *(Scalar<T> s1, int s2)
    {
        return new Scalar<T>(
            s1.ScalarProcessor,
            s1.ScalarProcessor.Times(
                s1.ScalarValue,
                s1.ScalarProcessor.GetScalarFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator *(int s1, Scalar<T> s2)
    {
        return new Scalar<T>(
            s2.ScalarProcessor,
            s2.ScalarProcessor.Times(
                s2.ScalarProcessor.GetScalarFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator *(Scalar<T> s1, uint s2)
    {
        return new Scalar<T>(
            s1.ScalarProcessor,
            s1.ScalarProcessor.Times(
                s1.ScalarValue,
                s1.ScalarProcessor.GetScalarFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator *(uint s1, Scalar<T> s2)
    {
        return new Scalar<T>(
            s2.ScalarProcessor,
            s2.ScalarProcessor.Times(
                s2.ScalarProcessor.GetScalarFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator *(Scalar<T> s1, long s2)
    {
        return new Scalar<T>(
            s1.ScalarProcessor,
            s1.ScalarProcessor.Times(
                s1.ScalarValue,
                s1.ScalarProcessor.GetScalarFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator *(long s1, Scalar<T> s2)
    {
        return new Scalar<T>(
            s2.ScalarProcessor,
            s2.ScalarProcessor.Times(
                s2.ScalarProcessor.GetScalarFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator *(Scalar<T> s1, ulong s2)
    {
        return new Scalar<T>(
            s1.ScalarProcessor,
            s1.ScalarProcessor.Times(
                s1.ScalarValue,
                s1.ScalarProcessor.GetScalarFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator *(ulong s1, Scalar<T> s2)
    {
        return new Scalar<T>(
            s2.ScalarProcessor,
            s2.ScalarProcessor.Times(
                s2.ScalarProcessor.GetScalarFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator *(Scalar<T> s1, float s2)
    {
        return new Scalar<T>(
            s1.ScalarProcessor,
            s1.ScalarProcessor.Times(
                s1.ScalarValue,
                s1.ScalarProcessor.GetScalarFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator *(float s1, Scalar<T> s2)
    {
        return new Scalar<T>(
            s2.ScalarProcessor,
            s2.ScalarProcessor.Times(
                s2.ScalarProcessor.GetScalarFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator *(Scalar<T> s1, double s2)
    {
        return new Scalar<T>(
            s1.ScalarProcessor,
            s1.ScalarProcessor.Times(
                s1.ScalarValue,
                s1.ScalarProcessor.GetScalarFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator *(double s1, Scalar<T> s2)
    {
        return new Scalar<T>(
            s2.ScalarProcessor,
            s2.ScalarProcessor.Times(
                s2.ScalarProcessor.GetScalarFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator *(Scalar<T> s1, T s2)
    {
        return new Scalar<T>(s1.ScalarProcessor, s1.ScalarProcessor.Times(s1.ScalarValue, s2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator *(T s1, Scalar<T> s2)
    {
        return new Scalar<T>(s2.ScalarProcessor, s2.ScalarProcessor.Times(s1, s2.ScalarValue));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator /(Scalar<T> s1, Scalar<T> s2)
    {
        return new Scalar<T>(s1.ScalarProcessor, s1.ScalarProcessor.Divide(s1.ScalarValue, s2.ScalarValue));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator /(Scalar<T> s1, int s2)
    {
        return new Scalar<T>(
            s1.ScalarProcessor,
            s1.ScalarProcessor.Divide(
                s1.ScalarValue,
                s1.ScalarProcessor.GetScalarFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator /(int s1, Scalar<T> s2)
    {
        return new Scalar<T>(
            s2.ScalarProcessor,
            s2.ScalarProcessor.Divide(
                s2.ScalarProcessor.GetScalarFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator /(Scalar<T> s1, uint s2)
    {
        return new Scalar<T>(
            s1.ScalarProcessor,
            s1.ScalarProcessor.Divide(
                s1.ScalarValue,
                s1.ScalarProcessor.GetScalarFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator /(uint s1, Scalar<T> s2)
    {
        return new Scalar<T>(
            s2.ScalarProcessor,
            s2.ScalarProcessor.Divide(
                s2.ScalarProcessor.GetScalarFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator /(Scalar<T> s1, long s2)
    {
        return new Scalar<T>(
            s1.ScalarProcessor,
            s1.ScalarProcessor.Divide(
                s1.ScalarValue,
                s1.ScalarProcessor.GetScalarFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator /(long s1, Scalar<T> s2)
    {
        return new Scalar<T>(
            s2.ScalarProcessor,
            s2.ScalarProcessor.Divide(
                s2.ScalarProcessor.GetScalarFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator /(Scalar<T> s1, ulong s2)
    {
        return new Scalar<T>(
            s1.ScalarProcessor,
            s1.ScalarProcessor.Divide(
                s1.ScalarValue,
                s1.ScalarProcessor.GetScalarFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator /(ulong s1, Scalar<T> s2)
    {
        return new Scalar<T>(
            s2.ScalarProcessor,
            s2.ScalarProcessor.Divide(
                s2.ScalarProcessor.GetScalarFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator /(Scalar<T> s1, float s2)
    {
        return new Scalar<T>(
            s1.ScalarProcessor,
            s1.ScalarProcessor.Divide(
                s1.ScalarValue,
                s1.ScalarProcessor.GetScalarFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator /(float s1, Scalar<T> s2)
    {
        return new Scalar<T>(
            s2.ScalarProcessor,
            s2.ScalarProcessor.Divide(
                s2.ScalarProcessor.GetScalarFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator /(Scalar<T> s1, double s2)
    {
        return new Scalar<T>(
            s1.ScalarProcessor,
            s1.ScalarProcessor.Divide(
                s1.ScalarValue,
                s1.ScalarProcessor.GetScalarFromNumber(s2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator /(double s1, Scalar<T> s2)
    {
        return new Scalar<T>(
            s2.ScalarProcessor,
            s2.ScalarProcessor.Divide(
                s2.ScalarProcessor.GetScalarFromNumber(s1),
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator /(Scalar<T> s1, T s2)
    {
        return new Scalar<T>(s1.ScalarProcessor, s1.ScalarProcessor.Divide(s1.ScalarValue, s2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> operator /(T s1, Scalar<T> s2)
    {
        return new Scalar<T>(s2.ScalarProcessor, s2.ScalarProcessor.Divide(s1, s2.ScalarValue));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Scalar<T> s1, int s2)
    {
        var processor = s1.ScalarProcessor;

        return processor.IsZero(
            processor.Subtract(
                s1.ScalarValue,
                s2
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Scalar<T> s1, int s2)
    {
        var processor = s1.ScalarProcessor;

        return !processor.IsZero(
            processor.Subtract(
                s1.ScalarValue,
                s2
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(int s1, Scalar<T> s2)
    {
        var processor = s2.ScalarProcessor;

        return processor.IsZero(
            processor.Subtract(
                s1,
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(int s1, Scalar<T> s2)
    {
        var processor = s2.ScalarProcessor;

        return !processor.IsZero(
            processor.Subtract(
                s1,
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Scalar<T> s1, double s2)
    {
        var processor = s1.ScalarProcessor;

        return processor.IsZero(
            processor.Subtract(
                s1.ScalarValue,
                s2
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Scalar<T> s1, double s2)
    {
        var processor = s1.ScalarProcessor;

        return !processor.IsZero(
            processor.Subtract(
                s1.ScalarValue,
                s2
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(double s1, Scalar<T> s2)
    {
        var processor = s2.ScalarProcessor;

        return processor.IsZero(
            processor.Subtract(
                s1,
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(double s1, Scalar<T> s2)
    {
        var processor = s2.ScalarProcessor;

        return !processor.IsZero(
            processor.Subtract(
                s1,
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(Scalar<T> s1, Scalar<T> s2)
    {
        var processor = s1.ScalarProcessor;

        return processor.IsNegative(
            processor.Subtract(
                s1.ScalarValue,
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(Scalar<T> s1, Scalar<T> s2)
    {
        var processor = s1.ScalarProcessor;

        return processor.IsPositive(
            processor.Subtract(
                s1.ScalarValue,
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(Scalar<T> s1, int s2)
    {
        var processor = s1.ScalarProcessor;

        return processor.IsNegative(
            processor.Subtract(
                s1.ScalarValue,
                s2
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(Scalar<T> s1, int s2)
    {
        var processor = s1.ScalarProcessor;

        return processor.IsPositive(
            processor.Subtract(
                s1.ScalarValue,
                s2
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(int s1, Scalar<T> s2)
    {
        var processor = s2.ScalarProcessor;

        return processor.IsNegative(
            processor.Subtract(
                s1,
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(int s1, Scalar<T> s2)
    {
        var processor = s2.ScalarProcessor;

        return processor.IsPositive(
            processor.Subtract(
                s1,
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(Scalar<T> s1, double s2)
    {
        var processor = s1.ScalarProcessor;

        return processor.IsNegative(
            processor.Subtract(
                s1.ScalarValue,
                s2
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(Scalar<T> s1, double s2)
    {
        var processor = s1.ScalarProcessor;

        return processor.IsPositive(
            processor.Subtract(
                s1.ScalarValue,
                s2
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(double s1, Scalar<T> s2)
    {
        var processor = s2.ScalarProcessor;

        return processor.IsNegative(
            processor.Subtract(
                s1,
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(double s1, Scalar<T> s2)
    {
        var processor = s2.ScalarProcessor;

        return processor.IsPositive(
            processor.Subtract(
                s1,
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(Scalar<T> s1, Scalar<T> s2)
    {
        var processor = s1.ScalarProcessor;

        return !processor.IsPositive(
            processor.Subtract(
                s1.ScalarValue,
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(Scalar<T> s1, Scalar<T> s2)
    {
        var processor = s1.ScalarProcessor;

        return !processor.IsNegative(
            processor.Subtract(
                s1.ScalarValue,
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(Scalar<T> s1, int s2)
    {
        var processor = s1.ScalarProcessor;

        return !processor.IsPositive(
            processor.Subtract(
                s1.ScalarValue,
                s2
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(Scalar<T> s1, int s2)
    {
        var processor = s1.ScalarProcessor;

        return !processor.IsNegative(
            processor.Subtract(
                s1.ScalarValue,
                s2
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(int s1, Scalar<T> s2)
    {
        var processor = s2.ScalarProcessor;

        return !processor.IsPositive(
            processor.Subtract(
                s1,
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(int s1, Scalar<T> s2)
    {
        var processor = s2.ScalarProcessor;

        return !processor.IsNegative(
            processor.Subtract(
                s1,
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(Scalar<T> s1, double s2)
    {
        var processor = s1.ScalarProcessor;

        return !processor.IsPositive(
            processor.Subtract(
                s1.ScalarValue,
                s2
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(Scalar<T> s1, double s2)
    {
        var processor = s1.ScalarProcessor;

        return !processor.IsNegative(
            processor.Subtract(
                s1.ScalarValue,
                s2
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(double s1, Scalar<T> s2)
    {
        var processor = s2.ScalarProcessor;

        return !processor.IsPositive(
            processor.Subtract(
                s1,
                s2.ScalarValue
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(double s1, Scalar<T> s2)
    {
        var processor = s2.ScalarProcessor;

        return !processor.IsNegative(
            processor.Subtract(
                s1,
                s2.ScalarValue
            )
        );
    }


    public IScalarProcessor<T> ScalarProcessor { get; }

    [NotNull]
    public T ScalarValue { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Scalar(IScalarProcessor<T> scalarProcessor, [NotNull] T scalarValue)
    {
        ScalarProcessor = scalarProcessor;
        ScalarValue = scalarValue;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return ScalarProcessor.ToText(ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        return ScalarProcessor.IsZero(ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearZero()
    {
        return ScalarProcessor.IsNearZero(ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNumeric()
    {
        return ScalarProcessor.IsNumeric;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsSymbolic()
    {
        return ScalarProcessor.IsSymbolic;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return ScalarProcessor.IsValid(ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsOne()
    {
        return ScalarProcessor.IsOne(ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsMinusOne()
    {
        return ScalarProcessor.IsMinusOne(ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsPositive()
    {
        return ScalarProcessor.IsPositive(ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNegative()
    {
        return ScalarProcessor.IsNegative(ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNotNearPositive()
    {
        return ScalarProcessor.IsNotNearPositive(ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNotNearNegative()
    {
        return ScalarProcessor.IsNotNearNegative(ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> MapScalar(Func<T, T> scalarMapping)
    {
        return new Scalar<T>(
            ScalarProcessor,
            scalarMapping(ScalarValue)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Scalar<T>? other)
    {
        if (ReferenceEquals(other, null)) return false;
        if (ReferenceEquals(this, other)) return true;
        return EqualityComparer<T>.Default.Equals(ScalarValue, other.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        return ScalarValue.GetHashCode();
    }
}