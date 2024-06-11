using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Euclidean.Space2D.Objects;

public sealed record E2DVector<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DVector<T> operator -(E2DVector<T> v1)
    {
        var processor = v1.ScalarProcessor;

        return new E2DVector<T>(

            processor.Negative(v1.X),
            processor.Negative(v1.Y),
            v1.AssumeUnit
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DVector<T> operator +(E2DVector<T> v1, E2DVector<T> v2)
    {
        var processor = v1.ScalarProcessor;

        return new E2DVector<T>(

            processor.Add(v1.X, v2.X),
            processor.Add(v1.Y, v2.Y)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DVector<T> operator -(E2DVector<T> v1, E2DVector<T> v2)
    {
        var processor = v1.ScalarProcessor;

        return new E2DVector<T>(

            processor.Subtract(v1.X, v2.X),
            processor.Subtract(v1.Y, v2.Y)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DVector<T> operator *(int v1, E2DVector<T> v2)
    {
        var processor = v2.ScalarProcessor;


        return new E2DVector<T>(

            processor.Times(v1, v2.X),
            processor.Times(v1, v2.Y)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DVector<T> operator *(E2DVector<T> v1, int v2)
    {
        var processor = v1.ScalarProcessor;


        return new E2DVector<T>(

            processor.Times(v1.X, v2),
            processor.Times(v1.Y, v2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DVector<T> operator *(uint v1, E2DVector<T> v2)
    {
        var processor = v2.ScalarProcessor;


        return new E2DVector<T>(

            processor.Times(v1, v2.X),
            processor.Times(v1, v2.Y)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DVector<T> operator *(E2DVector<T> v1, uint v2)
    {
        var processor = v1.ScalarProcessor;


        return new E2DVector<T>(

            processor.Times(v1.X, v2),
            processor.Times(v1.Y, v2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DVector<T> operator *(float v1, E2DVector<T> v2)
    {
        var processor = v2.ScalarProcessor;


        return new E2DVector<T>(

            processor.Times(v1, v2.X),
            processor.Times(v1, v2.Y)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DVector<T> operator *(E2DVector<T> v1, long v2)
    {
        var processor = v1.ScalarProcessor;


        return new E2DVector<T>(

            processor.Times(v1.X, v2),
            processor.Times(v1.Y, v2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DVector<T> operator *(long v1, E2DVector<T> v2)
    {
        var processor = v2.ScalarProcessor;


        return new E2DVector<T>(

            processor.Times(v1, v2.X),
            processor.Times(v1, v2.Y)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DVector<T> operator *(ulong v1, E2DVector<T> v2)
    {
        var processor = v2.ScalarProcessor;


        return new E2DVector<T>(

            processor.Times(v1, v2.X),
            processor.Times(v1, v2.Y)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DVector<T> operator *(E2DVector<T> v1, ulong v2)
    {
        var processor = v1.ScalarProcessor;


        return new E2DVector<T>(

            processor.Times(v1.X, v2),
            processor.Times(v1.Y, v2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DVector<T> operator *(E2DVector<T> v1, float v2)
    {
        var processor = v1.ScalarProcessor;


        return new E2DVector<T>(

            processor.Times(v1.X, v2),
            processor.Times(v1.Y, v2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DVector<T> operator *(double v1, E2DVector<T> v2)
    {
        var processor = v2.ScalarProcessor;


        return new E2DVector<T>(

            processor.Times(v1, v2.X),
            processor.Times(v1, v2.Y)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DVector<T> operator *(E2DVector<T> v1, double v2)
    {
        var processor = v1.ScalarProcessor;


        return new E2DVector<T>(

            processor.Times(v1.X, v2),
            processor.Times(v1.Y, v2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DVector<T> operator *(T v1, E2DVector<T> v2)
    {
        var processor = v2.ScalarProcessor;

        return new E2DVector<T>(

            processor.Times(v1, v2.X),
            processor.Times(v1, v2.Y)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DVector<T> operator *(E2DVector<T> v1, T v2)
    {
        var processor = v1.ScalarProcessor;

        return new E2DVector<T>(

            processor.Times(v1.X, v2),
            processor.Times(v1.Y, v2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DVector<T> operator /(E2DVector<T> v1, int v2)
    {
        var processor = v1.ScalarProcessor;


        return new E2DVector<T>(

            processor.Divide(v1.X, v2),
            processor.Divide(v1.Y, v2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DVector<T> operator /(E2DVector<T> v1, uint v2)
    {
        var processor = v1.ScalarProcessor;


        return new E2DVector<T>(

            processor.Divide(v1.X, v2),
            processor.Divide(v1.Y, v2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DVector<T> operator /(E2DVector<T> v1, long v2)
    {
        var processor = v1.ScalarProcessor;


        return new E2DVector<T>(

            processor.Divide(v1.X, v2),
            processor.Divide(v1.Y, v2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DVector<T> operator /(E2DVector<T> v1, ulong v2)
    {
        var processor = v1.ScalarProcessor;


        return new E2DVector<T>(

            processor.Divide(v1.X, v2),
            processor.Divide(v1.Y, v2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DVector<T> operator /(E2DVector<T> v1, float v2)
    {
        var processor = v1.ScalarProcessor;


        return new E2DVector<T>(

            processor.Divide(v1.X, v2),
            processor.Divide(v1.Y, v2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DVector<T> operator /(E2DVector<T> v1, double v2)
    {
        var processor = v1.ScalarProcessor;


        return new E2DVector<T>(

            processor.Divide(v1.X, v2),
            processor.Divide(v1.Y, v2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E2DVector<T> operator /(E2DVector<T> v1, T v2)
    {
        var processor = v1.ScalarProcessor;

        return new E2DVector<T>(

            processor.Divide(v1.X, v2),
            processor.Divide(v1.Y, v2)
        );
    }


    public IScalarProcessor<T> ScalarProcessor { get; }

    public T X { get; }

    public T Y { get; }

    public Scalar<T> XScalar
        => ScalarProcessor.ScalarFromValue(X);

    public Scalar<T> YScalar
        => ScalarProcessor.ScalarFromValue(Y);

    public bool AssumeUnit { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal E2DVector(IScalarProcessor<T> scalarProcessor, T x, T y, bool assumeUnit = false)
    {
        ScalarProcessor = scalarProcessor;
        X = x;
        Y = y;
        AssumeUnit = assumeUnit;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal E2DVector(IScalar<T> x, IScalar<T> y, bool assumeUnit = false)
    {
        ScalarProcessor = x.ScalarProcessor;
        X = x.ScalarValue;
        Y = y.ScalarValue;
        AssumeUnit = assumeUnit;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetNorm()
    {
        if (AssumeUnit)
            return ScalarProcessor.OneValue.ScalarFromValue(ScalarProcessor);

        return (ScalarProcessor.Square(X) + ScalarProcessor.Square(Y)).Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetNormSquared()
    {
        if (AssumeUnit)
            return ScalarProcessor.OneValue.ScalarFromValue(ScalarProcessor);

        return ScalarProcessor.Square(X) + ScalarProcessor.Square(Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E2DVector<T> GetUnitVector()
    {
        if (AssumeUnit)
            return this;

        var norm = (ScalarProcessor.Square(X) + ScalarProcessor.Square(Y)).Sqrt().ScalarValue;

        return this / norm;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E2DVector<T> GetInverseVector()
    {
        if (AssumeUnit)
            return this;

        var normSquared = (ScalarProcessor.Square(X) + ScalarProcessor.Square(Y)).ScalarValue;

        return this / normSquared;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E2DPoint<T> ToE2DPoint()
    {
        return new E2DPoint<T>(ScalarProcessor, X, Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Dot(E2DVector<T> vector)
    {
        return ScalarProcessor.Add(
            ScalarProcessor.Times(X, vector.X),
            ScalarProcessor.Times(Y, vector.Y)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetCosAngle(E2DVector<T> vector)
    {
        var norm1 = GetNorm().ScalarValue;
        var norm2 = vector.GetNorm().ScalarValue;

        return (ScalarProcessor.Times(X, vector.X) + ScalarProcessor.Times(Y, vector.Y)) / ScalarProcessor.Times(norm1, norm2);
    }

    public E2DBivector<T> Op(E2DVector<T> vector)
    {
        var xy =
            ScalarProcessor.Times(X, vector.Y) -
            ScalarProcessor.Times(Y, vector.X);

        return new E2DBivector<T>(ScalarProcessor, xy.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E2DVector<T> MapScalars(Func<T, T> scalarMapping)
    {
        return new E2DVector<T>(
            ScalarProcessor,
            scalarMapping(X),
            scalarMapping(Y)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E2DVector<T2> MapScalars<T2>(Func<T, T2> scalarMapping, IScalarProcessor<T2> scalarProcessor)
    {
        return new E2DVector<T2>(
            scalarProcessor,
            scalarMapping(X),
            scalarMapping(Y)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E2DLineTangent<T> CreateE2DLineTangent(E2DPoint<T> origin)
    {
        return new E2DLineTangent<T>(origin, this);
    }


}