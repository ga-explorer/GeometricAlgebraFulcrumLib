using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense
{
    public sealed class LinVectorDenseStorageComposer<T> :
        LinVectorStorageComposerBase<T>
    {
        public T[] TermsArray { get; private set; }

        public override int Count
            => TermsArray.Length;


        internal LinVectorDenseStorageComposer(IScalarAlgebraProcessor<T> scalarProcessor, int count)
            : base(scalarProcessor)
        {
            TermsArray = scalarProcessor.CreateArrayZero1D(count);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinVectorStorageComposerBase<T> Clear()
        {
            TermsArray = ScalarProcessor.CreateArrayZero1D(TermsArray.Length);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorDenseStorageComposer<T> Reset(int count)
        {
            TermsArray = ScalarProcessor.CreateArrayZero1D(count);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinVectorStorageComposerBase<T> RemoveTerm(ulong index)
        {
            TermsArray[index] = ScalarProcessor.ScalarZero;

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinVectorStorageComposerBase<T> RemoveZeroTerms()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinVectorStorageComposerBase<T> SetTerm(ulong index, [NotNull] T value)
        {
            TermsArray[index] = value;

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinVectorStorageComposerBase<T> AddTerm(ulong index, [NotNull] T value)
        {
            TermsArray[index] = ScalarProcessor.Add(TermsArray[index], value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinVectorStorageComposerBase<T> SubtractTerm(ulong index, [NotNull] T value)
        {
            TermsArray[index] = ScalarProcessor.Subtract(TermsArray[index], value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinVectorStorageComposerBase<T> MapScalars(Func<T, T> valueMapping)
        {
            for (var index = 0; index < Count; index++)
                TermsArray[index] = valueMapping(TermsArray[index]);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinVectorStorageComposerBase<T> MapScalars(Func<ulong, T, T> valueMapping)
        {
            for (var index = 0; index < Count; index++)
                TermsArray[index] = valueMapping((ulong)index, TermsArray[index]);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<IndexScalarRecord<T>> GetIndexScalarRecords()
        {
            for (var index = 0; index < Count; index++)
                yield return new IndexScalarRecord<T>((ulong)index, TermsArray[index]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorStorage<T> CreateLinVectorStorage()
        {
            return CreateLinVectorDenseStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorDenseStorage<T> CreateLinVectorDenseStorage()
        {
            return Count switch
            {
                0 => LinVectorEmptyStorage<T>.EmptyStorage,
                1 => TermsArray[0].CreateLinVectorSingleScalarDenseStorage(),
                _ => TermsArray.CreateLinVectorArrayStorage()
            };
        }
    }
}