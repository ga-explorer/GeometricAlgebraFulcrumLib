using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Sparse
{
    public class LinVectorSparseStorageComposer<T> :
        LinVectorStorageComposerBase<T>
    {
        public Dictionary<ulong, T> IndexScalarDictionary { get; }
            = new Dictionary<ulong, T>();

        public override int Count
            => IndexScalarDictionary.Count;


        internal LinVectorSparseStorageComposer(IScalarProcessor<T> scalarProcessor)
            : base(scalarProcessor)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinVectorStorageComposerBase<T> Clear()
        {
            IndexScalarDictionary.Clear();

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinVectorStorageComposerBase<T> RemoveTerm(ulong index)
        {
            IndexScalarDictionary.Remove(index);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinVectorStorageComposerBase<T> RemoveZeroTerms()
        {
            var indexsList = IndexScalarDictionary
                .Where(pair => ScalarProcessor.IsZero(pair.Value))
                .Select(pair => pair.Key)
                .ToArray();

            return RemoveTerms(indexsList);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinVectorStorageComposerBase<T> SetTerm(ulong index, T value)
        {
            if (IndexScalarDictionary.ContainsKey(index))
                IndexScalarDictionary[index] = value;
            else
                IndexScalarDictionary.Add(index, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinVectorStorageComposerBase<T> AddTerm(ulong index, T value)
        {
            if (IndexScalarDictionary.TryGetValue(index, out var value1))
                IndexScalarDictionary[index] = ScalarProcessor.Add(value1, value);
            else
                IndexScalarDictionary.Add(index, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinVectorStorageComposerBase<T> SubtractTerm(ulong index, T value)
        {
            if (IndexScalarDictionary.TryGetValue(index, out var value1))
                IndexScalarDictionary[index] = ScalarProcessor.Subtract(value1, value);
            else
                IndexScalarDictionary.Add(index, ScalarProcessor.Negative(value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinVectorStorageComposerBase<T> MapScalars(Func<T, T> valueMapping)
        {
            foreach (var (index, value) in IndexScalarDictionary)
                IndexScalarDictionary[index] = valueMapping(value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinVectorStorageComposerBase<T> MapScalars(Func<ulong, T, T> valueMapping)
        {
            foreach (var (index, value) in IndexScalarDictionary)
                IndexScalarDictionary[index] = valueMapping(index, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<RGaKvIndexScalarRecord<T>> GetIndexScalarRecords()
        {
            return IndexScalarDictionary.Select(
                p => new RGaKvIndexScalarRecord<T>(p.Key, p.Value)
            );
        }

        public override ILinVectorStorage<T> CreateLinVectorStorage()
        {
            if (IndexScalarDictionary.Count == 0)
                return LinVectorEmptyStorage<T>.EmptyStorage;

            if (IndexScalarDictionary.Count == 1)
            {
                var (index, value) = IndexScalarDictionary.First();

                return value.CreateLinVectorSingleScalarStorage(index);
            }

            var denseCount =
                1UL + IndexScalarDictionary.Keys.Max();

            if (denseCount / 2 < (ulong)IndexScalarDictionary.Count || denseCount > int.MaxValue)
                return IndexScalarDictionary.CreateLinVectorStorage();

            var array = ScalarProcessor.CreateArrayZero1D((int)denseCount);

            foreach (var (index, scalar) in IndexScalarDictionary)
                array[index] = scalar;

            return array.CreateLinVectorArrayStorage();
        }

        public override ILinVectorDenseStorage<T> CreateLinVectorDenseStorage()
        {
            if (IndexScalarDictionary.Count == 0)
                return LinVectorEmptyStorage<T>.EmptyStorage;

            var denseCount =
                1UL + IndexScalarDictionary.Keys.Max();

            if (denseCount > int.MaxValue)
                throw new InvalidOperationException();

            var array = ScalarProcessor.CreateArrayZero1D((int)denseCount);

            foreach (var (index, scalar) in IndexScalarDictionary)
                array[index] = scalar;

            return array.CreateLinVectorArrayStorage();
        }
    }
}
