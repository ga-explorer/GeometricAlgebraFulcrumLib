using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Utilities.Composers
{
    public class VectorSparseStorageComposer<T> :
        VectorStorageComposerBase<T>
    {
        public Dictionary<ulong, T> IndexScalarDictionary { get; }
            = new Dictionary<ulong, T>();

        public override int Count 
            => IndexScalarDictionary.Count;


        internal VectorSparseStorageComposer([NotNull] IScalarAlgebraProcessor<T> scalarProcessor)
            : base(scalarProcessor)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override VectorStorageComposerBase<T> Clear()
        {
            IndexScalarDictionary.Clear();

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override VectorStorageComposerBase<T> RemoveTerm(ulong index)
        {
            IndexScalarDictionary.Remove(index);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override VectorStorageComposerBase<T> RemoveZeroTerms()
        {
            var indexsList = IndexScalarDictionary
                .Where(pair => ScalarProcessor.IsZero(pair.Value))
                .Select(pair => pair.Key)
                .ToArray();

            return RemoveTerms(indexsList);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override VectorStorageComposerBase<T> SetTerm(ulong index, [NotNull] T value)
        {
            if (IndexScalarDictionary.ContainsKey(index))
                IndexScalarDictionary[index] = value;
            else
                IndexScalarDictionary.Add(index, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override VectorStorageComposerBase<T> AddTerm(ulong index, [NotNull] T value)
        {
            if (IndexScalarDictionary.TryGetValue(index, out var value1))
                IndexScalarDictionary[index] = ScalarProcessor.Add(value1, value);
            else
                IndexScalarDictionary.Add(index, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override VectorStorageComposerBase<T> SubtractTerm(ulong index, [NotNull] T value)
        {
            if (IndexScalarDictionary.TryGetValue(index, out var value1))
                IndexScalarDictionary[index] = ScalarProcessor.Subtract(value1, value);
            else
                IndexScalarDictionary.Add(index, ScalarProcessor.Negative(value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override VectorStorageComposerBase<T> MapScalars(Func<T, T> valueMapping)
        {
            foreach (var (index, value) in IndexScalarDictionary)
                IndexScalarDictionary[index] = valueMapping(value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override VectorStorageComposerBase<T> MapScalars(Func<ulong, T, T> valueMapping)
        {
            foreach (var (index, value) in IndexScalarDictionary)
                IndexScalarDictionary[index] = valueMapping(index, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<IndexScalarRecord<T>> GetIndexScalarRecords()
        {
            return IndexScalarDictionary.Select(
                p => new IndexScalarRecord<T>(p.Key, p.Value)
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

            if ((denseCount / 2) < (ulong) IndexScalarDictionary.Count || denseCount > int.MaxValue)
                return IndexScalarDictionary.CreateLinVectorStorage();

            var array = ScalarProcessor.CreateArrayZero1D((int) denseCount);

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

            var array = ScalarProcessor.CreateArrayZero1D((int) denseCount);

            foreach (var (index, scalar) in IndexScalarDictionary)
                array[index] = scalar;

            return array.CreateLinVectorArrayStorage();
        }
    }
}
