using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Utilities.Composers
{
    public sealed class VectorDenseStorageComposer<T> :
        VectorStorageComposerBase<T>
    {
        public T[] TermsArray { get; private set; }

        public override int Count 
            => TermsArray.Length;


        internal VectorDenseStorageComposer(IScalarAlgebraProcessor<T> scalarProcessor, int count)
            : base(scalarProcessor)
        {
            TermsArray = scalarProcessor.CreateArrayZero1D(count);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override VectorStorageComposerBase<T> Clear()
        {
            TermsArray = ScalarProcessor.CreateArrayZero1D(TermsArray.Length);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorDenseStorageComposer<T> Reset(int count)
        {
            TermsArray = ScalarProcessor.CreateArrayZero1D(count);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override VectorStorageComposerBase<T> RemoveTerm(ulong index)
        {
            TermsArray[index] = ScalarProcessor.ScalarZero;

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override VectorStorageComposerBase<T> RemoveZeroTerms()
        {
            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override VectorStorageComposerBase<T> SetTerm(ulong index, [NotNull] T value)
        {
            TermsArray[index] = value;

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override VectorStorageComposerBase<T> AddTerm(ulong index, [NotNull] T value)
        {
            TermsArray[index] = ScalarProcessor.Add(TermsArray[index], value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override VectorStorageComposerBase<T> SubtractTerm(ulong index, [NotNull] T value)
        {
            TermsArray[index] = ScalarProcessor.Subtract(TermsArray[index], value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override VectorStorageComposerBase<T> MapScalars(Func<T, T> valueMapping)
        {
            for (var index = 0; index < Count; index++)
                TermsArray[index] = valueMapping(TermsArray[index]);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override VectorStorageComposerBase<T> MapScalars(Func<ulong, T, T> valueMapping)
        {
            for (var index = 0; index < Count; index++)
                TermsArray[index] = valueMapping((ulong) index, TermsArray[index]);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<IndexScalarRecord<T>> GetIndexScalarRecords()
        {
            for (var index = 0; index < Count; index++)
                yield return new IndexScalarRecord<T>((ulong) index, TermsArray[index]);
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