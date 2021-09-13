using System;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Processors.TupleAlgebra
{
    public interface IGeoTuple<T>
    {
        IScalarAlgebraProcessor<T> ScalarProcessor { get; }

        T this[int key] { get; }

        bool ContainsKey(int key);

        bool TryGetValue(int key, out T value);

        IGeoTuple<T> Add(IGeoTuple<T> scalarsTuple2);

        IGeoTuple<T> Subtract(IGeoTuple<T> scalarsTuple2);

        IGeoTuple<T> Times(IGeoTuple<T> scalarsTuple2);

        IGeoTuple<T> Divide(IGeoTuple<T> scalarsTuple2);

        IGeoTuple<T> Negative();

        IGeoTuple<T> MapScalars(Func<T, T> mappingFunc);

        IGeoTuple<T> MapScalars(IGeoTuple<T> scalarsTuple2, Func<T, T, T> mappingFunc);

        bool IsValid();

        bool IsZero();

        bool IsNearZero();
    }
}