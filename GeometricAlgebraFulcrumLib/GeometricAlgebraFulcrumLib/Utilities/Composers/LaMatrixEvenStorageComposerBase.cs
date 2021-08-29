using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices;

namespace GeometricAlgebraFulcrumLib.Utilities.Composers
{
    public abstract class LaMatrixEvenStorageComposerBase<T>
    {
        public IScalarProcessor<T> ScalarProcessor { get; }

        public abstract int Count { get; }
        

        protected LaMatrixEvenStorageComposerBase([NotNull] IScalarProcessor<T> scalarProcessor)
        {
            ScalarProcessor = scalarProcessor;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return Count == 0;
        }

        public abstract LaMatrixEvenStorageComposerBase<T> Clear();

        public abstract LaMatrixEvenStorageComposerBase<T> RemoveTerm(IndexPairRecord index);

        public abstract LaMatrixEvenStorageComposerBase<T> SetTerm(IndexPairRecord index, [NotNull] T value);
        
        public abstract LaMatrixEvenStorageComposerBase<T> AddTerm(IndexPairRecord index, [NotNull] T value);

        public abstract LaMatrixEvenStorageComposerBase<T> SubtractTerm(IndexPairRecord index, [NotNull] T value);

        public abstract LaMatrixEvenStorageComposerBase<T> MapValues(Func<T, T> valueMapping);

        public abstract LaMatrixEvenStorageComposerBase<T> MapValues(Func<ulong, ulong, T, T> valueMapping);

        public abstract ILaMatrixEvenStorage<T> CreateEvenGrid();

        public abstract ILaMatrixDenseEvenStorage<T> CreateDenseEvenGrid();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixEvenStorageComposerBase<T> RemoveTerm(ulong index1, ulong index2)
        {
            return RemoveTerm(new IndexPairRecord(index1, index2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixEvenStorageComposerBase<T> RemoveTerms(params IndexPairRecord[] indexsList)
        {
            foreach (var index in indexsList)
                RemoveTerm(index);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixEvenStorageComposerBase<T> RemoveTerms(IEnumerable<IndexPairRecord> indexsList)
        {
            foreach (var index in indexsList.ToArray())
                RemoveTerm(index);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract LaMatrixEvenStorageComposerBase<T> RemoveZeroTerms();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixEvenStorageComposerBase<T> SetTerm(ulong index1, ulong index2, [NotNull] T value)
        {
            return SetTerm(new IndexPairRecord(index1, index2), value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixEvenStorageComposerBase<T> SetTerms(ILaMatrixDenseEvenStorage<T> evenGrid)
        {
            var count1 = (ulong) evenGrid.Count1;
            var count2 = (ulong) evenGrid.Count2;

            for (var index1 = 0UL; index1 < count1; index1++)
            for (var index2 = 0UL; index2 < count2; index2++)
                SetTerm(index1, index2, evenGrid.GetScalar(index1, index2));

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixEvenStorageComposerBase<T> SetTerms(ILaMatrixEvenStorage<T> evenGrid)
        {
            foreach (var (index1, index2, value) in evenGrid.GetIndexScalarRecords())
                SetTerm(index1, index2, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixEvenStorageComposerBase<T> SetTerms(IEnumerable<IndexPairScalarRecord<T>> indexTermRecords)
        {
            foreach (var (index1, index2, value) in indexTermRecords)
                SetTerm(new IndexPairRecord(index1, index2), value);

            return this;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixEvenStorageComposerBase<T> AddTerm(ulong index1, ulong index2, [NotNull] T value)
        {
            return AddTerm(new IndexPairRecord(index1, index2), value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixEvenStorageComposerBase<T> AddTerms(ILaMatrixDenseEvenStorage<T> evenGrid)
        {
            var count1 = (ulong) evenGrid.Count1;
            var count2 = (ulong) evenGrid.Count2;

            for (var index1 = 0UL; index1 < count1; index1++)
            for (var index2 = 0UL; index2 < count2; index2++)
                AddTerm(index1, index2, evenGrid.GetScalar(index1, index2));

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixEvenStorageComposerBase<T> AddTerms(ILaMatrixEvenStorage<T> evenGrid)
        {
            foreach (var (index1, index2, value) in evenGrid.GetIndexScalarRecords())
                AddTerm(index1, index2, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixEvenStorageComposerBase<T> AddTerms(IEnumerable<IndexPairScalarRecord<T>> indexTermRecords)
        {
            foreach (var (index1, index2, value) in indexTermRecords)
                AddTerm(new IndexPairRecord(index1, index2), value);

            return this;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixEvenStorageComposerBase<T> SubtractTerm(ulong index1, ulong index2, [NotNull] T value)
        {
            return SubtractTerm(new IndexPairRecord(index1, index2), value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixEvenStorageComposerBase<T> SubtractTerms(ILaMatrixDenseEvenStorage<T> evenGrid)
        {
            var count1 = (ulong) evenGrid.Count1;
            var count2 = (ulong) evenGrid.Count2;

            for (var index1 = 0UL; index1 < count1; index1++)
            for (var index2 = 0UL; index2 < count2; index2++)
                SubtractTerm(index1, index2, evenGrid.GetScalar(index1, index2));

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixEvenStorageComposerBase<T> SubtractTerms(ILaMatrixEvenStorage<T> evenGrid)
        {
            foreach (var (index1, index2, value) in evenGrid.GetIndexScalarRecords())
                SubtractTerm(index1, index2, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixEvenStorageComposerBase<T> SubtractTerms(IEnumerable<IndexPairScalarRecord<T>> indexTermRecords)
        {
            foreach (var (index1, index2, value) in indexTermRecords)
                SubtractTerm(new IndexPairRecord(index1, index2), value);

            return this;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixEvenStorageComposerBase<T> Negative()
        {
            return MapValues(ScalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixEvenStorageComposerBase<T> Times(T scalingFactor)
        {
            return MapValues(scalar => ScalarProcessor.Times(scalar, scalingFactor));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixEvenStorageComposerBase<T> Divide(T scalingFactor)
        {
            return MapValues(scalar => ScalarProcessor.Divide(scalar, scalingFactor));
        }
    }
}