using System;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Processors.TupleAlgebra
{
    public interface ITuple<T>
    {
        IScalarAlgebraProcessor<T> ScalarProcessor { get; }

        T this[int key] { get; }

        bool ContainsKey(int key);

        bool TryGetValue(int key, out T value);

        ITuple<T> Add(ITuple<T> scalarsTuple2);

        ITuple<T> Subtract(ITuple<T> scalarsTuple2);

        ITuple<T> Times(ITuple<T> scalarsTuple2);

        ITuple<T> Divide(ITuple<T> scalarsTuple2);

        ITuple<T> Negative();

        ITuple<T> MapScalars(Func<T, T> mappingFunc);

        ITuple<T> MapScalars(ITuple<T> scalarsTuple2, Func<T, T, T> mappingFunc);

        bool IsValid();

        bool IsZero();

        bool IsNearZero();
    }
}