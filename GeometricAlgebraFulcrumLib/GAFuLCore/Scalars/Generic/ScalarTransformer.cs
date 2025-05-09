using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic
{
    public class ScalarTransformer<T> :
        IReadOnlyList<Func<T, T>>
    {
        private readonly List<Func<T, T>> _mapFunctionList 
            = new List<Func<T, T>>();
        
        
        public int Count 
            => _mapFunctionList.Count;

        public Func<T, T> this[int index]
        {
            get => _mapFunctionList[index];
            set => _mapFunctionList[index] = value ?? throw new ArgumentNullException(nameof(value));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarTransformer<T> Clear()
        {
            _mapFunctionList.Clear();

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarTransformer<T> Remove(int index)
        {
            _mapFunctionList.RemoveAt(index);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarTransformer<T> Append(Func<T, T> mapFunc)
        {
            _mapFunctionList.Add(mapFunc);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarTransformer<T> Prepend(Func<T, T> mapFunc)
        {
            _mapFunctionList.Insert(0, mapFunc);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarTransformer<T> Insert(Func<T, T> mapFunc, int index)
        {
            _mapFunctionList.Insert(index, mapFunc);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T MapScalarValue(T inScalar)
        {
            return _mapFunctionList.Aggregate(
                inScalar, 
                (scalar, mapFunc) => mapFunc(scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T1 MapScalarValue<T1>(T inScalar, Func<T, T1> mapFunc)
        {
            return mapFunc(
                MapScalarValue(inScalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<Func<T, T>> GetEnumerator()
        {
            return _mapFunctionList.GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
