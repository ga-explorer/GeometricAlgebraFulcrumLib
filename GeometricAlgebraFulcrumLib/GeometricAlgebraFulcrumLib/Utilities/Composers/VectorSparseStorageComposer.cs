﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Composers
{
    public class VectorSparseStorageComposer<T> :
        VectorStorageComposerBase<T>
    {
        public Dictionary<ulong, T> KeyValueDictionary { get; }
            = new Dictionary<ulong, T>();

        public override int Count 
            => KeyValueDictionary.Count;


        internal VectorSparseStorageComposer([NotNull] IScalarAlgebraProcessor<T> scalarProcessor)
            : base(scalarProcessor)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override VectorStorageComposerBase<T> Clear()
        {
            KeyValueDictionary.Clear();

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override VectorStorageComposerBase<T> RemoveTerm(ulong index)
        {
            KeyValueDictionary.Remove(index);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override VectorStorageComposerBase<T> RemoveZeroTerms()
        {
            var indexsList = KeyValueDictionary
                .Where(pair => ScalarProcessor.IsZero(pair.Value))
                .Select(pair => pair.Key)
                .ToArray();

            return RemoveTerms(indexsList);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override VectorStorageComposerBase<T> SetTerm(ulong index, [NotNull] T value)
        {
            if (KeyValueDictionary.ContainsKey(index))
                KeyValueDictionary[index] = value;
            else
                KeyValueDictionary.Add(index, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override VectorStorageComposerBase<T> AddTerm(ulong index, [NotNull] T value)
        {
            if (KeyValueDictionary.TryGetValue(index, out var value1))
                KeyValueDictionary[index] = ScalarProcessor.Add(value1, value);
            else
                KeyValueDictionary.Add(index, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override VectorStorageComposerBase<T> SubtractTerm(ulong index, [NotNull] T value)
        {
            if (KeyValueDictionary.TryGetValue(index, out var value1))
                KeyValueDictionary[index] = ScalarProcessor.Subtract(value1, value);
            else
                KeyValueDictionary.Add(index, ScalarProcessor.Negative(value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override VectorStorageComposerBase<T> MapScalars(Func<T, T> valueMapping)
        {
            foreach (var (index, value) in KeyValueDictionary)
                KeyValueDictionary[index] = valueMapping(value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override VectorStorageComposerBase<T> MapScalars(Func<ulong, T, T> valueMapping)
        {
            foreach (var (index, value) in KeyValueDictionary)
                KeyValueDictionary[index] = valueMapping(index, value);

            return this;
        }
        
        public override ILinVectorStorage<T> CreateLinVectorStorage()
        {
            if (KeyValueDictionary.Count == 0)
                return LinVectorEmptyStorage<T>.EmptyStorage;

            if (KeyValueDictionary.Count == 1)
            {
                var (index, value) = KeyValueDictionary.First();

                return value.CreateLinVectorSingleScalarStorage(index);
            }

            var denseCount = 
                1UL + KeyValueDictionary.Keys.Max();

            if ((denseCount / 2) < (ulong) KeyValueDictionary.Count || denseCount > int.MaxValue)
                return KeyValueDictionary.CreateLinVectorStorage();

            var array = ScalarProcessor.CreateArrayZero1D((int) denseCount);

            foreach (var (index, scalar) in KeyValueDictionary)
                array[index] = scalar;

            return array.CreateLinVectorArrayStorage();
        }

        public override ILinVectorDenseStorage<T> CreateLinVectorDenseStorage()
        {
            if (KeyValueDictionary.Count == 0)
                return LinVectorEmptyStorage<T>.EmptyStorage;

            var denseCount = 
                1UL + KeyValueDictionary.Keys.Max();

            if (denseCount > int.MaxValue)
                throw new InvalidOperationException();

            var array = ScalarProcessor.CreateArrayZero1D((int) denseCount);

            foreach (var (index, scalar) in KeyValueDictionary)
                array[index] = scalar;

            return array.CreateLinVectorArrayStorage();
        }
    }
}