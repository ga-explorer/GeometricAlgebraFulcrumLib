using System;
using GeometricAlgebraLib.Processors.Scalars;

namespace GeometricAlgebraLib.Processors.Tuples
{
    public interface IGaTuple<T>
    {
        IGaScalarProcessor<T> ScalarProcessor { get; }

        T this[int key] { get; }

        bool ContainsKey(int key);

        bool TryGetValue(int key, out T value);

        IGaTuple<T> Add(IGaTuple<T> scalarsTuple2);

        IGaTuple<T> Subtract(IGaTuple<T> scalarsTuple2);

        IGaTuple<T> Times(IGaTuple<T> scalarsTuple2);

        IGaTuple<T> Divide(IGaTuple<T> scalarsTuple2);

        IGaTuple<T> Negative();

        IGaTuple<T> MapScalars(Func<T, T> mappingFunc);

        IGaTuple<T> MapScalars(IGaTuple<T> scalarsTuple2, Func<T, T, T> mappingFunc);

        bool IsValid();

        bool IsZero();

        bool IsNearZero();
    }
}