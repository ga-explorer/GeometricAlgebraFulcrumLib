using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Dense;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Utilities.Composers
{
    public abstract class MatrixStorageComposerBase<T> :
        IMatrixStorageComposer<T>
    {
        public IScalarAlgebraProcessor<T> ScalarProcessor { get; }

        public abstract int Count { get; }
        

        protected MatrixStorageComposerBase([NotNull] IScalarAlgebraProcessor<T> scalarProcessor)
        {
            ScalarProcessor = scalarProcessor;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return Count == 0;
        }

        public abstract MatrixStorageComposerBase<T> Clear();

        public abstract MatrixStorageComposerBase<T> RemoveTerm(IndexPairRecord index);

        public abstract MatrixStorageComposerBase<T> SetTerm(IndexPairRecord index, [NotNull] T value);
        
        public abstract MatrixStorageComposerBase<T> AddTerm(IndexPairRecord index, [NotNull] T value);

        public abstract MatrixStorageComposerBase<T> SubtractTerm(IndexPairRecord index, [NotNull] T value);

        public abstract MatrixStorageComposerBase<T> MapValues(Func<T, T> valueMapping);

        public abstract MatrixStorageComposerBase<T> MapValues(Func<ulong, ulong, T, T> valueMapping);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixStorageComposerBase<T> RemoveTerm(ulong index1, ulong index2)
        {
            return RemoveTerm(new IndexPairRecord(index1, index2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixStorageComposerBase<T> RemoveTerms(params IndexPairRecord[] indexsList)
        {
            foreach (var index in indexsList)
                RemoveTerm(index);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixStorageComposerBase<T> RemoveTerms(IEnumerable<IndexPairRecord> indexsList)
        {
            foreach (var index in indexsList.ToArray())
                RemoveTerm(index);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract MatrixStorageComposerBase<T> RemoveZeroTerms();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixStorageComposerBase<T> SetTerm(ulong index1, ulong index2, [NotNull] T value)
        {
            return SetTerm(new IndexPairRecord(index1, index2), value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixStorageComposerBase<T> SetTerms(ILinMatrixDenseStorage<T> matrixStorage)
        {
            var count1 = (ulong) matrixStorage.Count1;
            var count2 = (ulong) matrixStorage.Count2;

            for (var index1 = 0UL; index1 < count1; index1++)
            for (var index2 = 0UL; index2 < count2; index2++)
                SetTerm(index1, index2, matrixStorage.GetScalar(index1, index2));

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixStorageComposerBase<T> SetTerms(ILinMatrixStorage<T> matrixStorage)
        {
            foreach (var (index1, index2, value) in matrixStorage.GetIndexScalarRecords())
                SetTerm(index1, index2, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixStorageComposerBase<T> SetTerms(IEnumerable<IndexPairScalarRecord<T>> indexTermRecords)
        {
            foreach (var (index1, index2, value) in indexTermRecords)
                SetTerm(new IndexPairRecord(index1, index2), value);

            return this;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixStorageComposerBase<T> AddTerm(ulong index1, ulong index2, [NotNull] T value)
        {
            return AddTerm(new IndexPairRecord(index1, index2), value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixStorageComposerBase<T> AddTerms(ILinMatrixDenseStorage<T> matrixStorage)
        {
            var count1 = (ulong) matrixStorage.Count1;
            var count2 = (ulong) matrixStorage.Count2;

            for (var index1 = 0UL; index1 < count1; index1++)
            for (var index2 = 0UL; index2 < count2; index2++)
                AddTerm(index1, index2, matrixStorage.GetScalar(index1, index2));

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixStorageComposerBase<T> AddTerms(ILinMatrixStorage<T> matrixStorage)
        {
            foreach (var (index1, index2, value) in matrixStorage.GetIndexScalarRecords())
                AddTerm(index1, index2, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixStorageComposerBase<T> AddTerms(IEnumerable<IndexPairScalarRecord<T>> indexTermRecords)
        {
            foreach (var (index1, index2, value) in indexTermRecords)
                AddTerm(new IndexPairRecord(index1, index2), value);

            return this;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixStorageComposerBase<T> SubtractTerm(ulong index1, ulong index2, [NotNull] T value)
        {
            return SubtractTerm(new IndexPairRecord(index1, index2), value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixStorageComposerBase<T> SubtractTerms(ILinMatrixDenseStorage<T> matrixStorage)
        {
            var count1 = (ulong) matrixStorage.Count1;
            var count2 = (ulong) matrixStorage.Count2;

            for (var index1 = 0UL; index1 < count1; index1++)
            for (var index2 = 0UL; index2 < count2; index2++)
                SubtractTerm(index1, index2, matrixStorage.GetScalar(index1, index2));

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixStorageComposerBase<T> SubtractTerms(ILinMatrixStorage<T> matrixStorage)
        {
            foreach (var (index1, index2, value) in matrixStorage.GetIndexScalarRecords())
                SubtractTerm(index1, index2, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixStorageComposerBase<T> SubtractTerms(IEnumerable<IndexPairScalarRecord<T>> indexTermRecords)
        {
            foreach (var (index1, index2, value) in indexTermRecords)
                SubtractTerm(new IndexPairRecord(index1, index2), value);

            return this;
        }

        
        public abstract ILinMatrixStorage<T> CreateLinMatrixStorage();

        public abstract ILinMatrixDenseStorage<T> CreateLinMatrixDenseStorage();

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public ILinMatrixGradedStorage<T> CreateLinMatrixGradedStorage()
        //{
        //    return CreateLinMatrixStorage().ToMatrixGradedStorage();
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixStorageComposerBase<T> Negative()
        {
            return MapValues(ScalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixStorageComposerBase<T> Times(T scalingFactor)
        {
            return MapValues(scalar => ScalarProcessor.Times(scalar, scalingFactor));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixStorageComposerBase<T> Divide(T scalingFactor)
        {
            return MapValues(scalar => ScalarProcessor.Divide(scalar, scalingFactor));
        }
    }
}