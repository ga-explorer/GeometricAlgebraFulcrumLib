using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded
{
    public sealed record LinMatrixSingleGradeStorage<T> :
        LinMatrixSingleGradeStorageBase<T>
    {
        
        public override ILinMatrixStorage<T> MatrixStorage { get; }


        internal LinMatrixSingleGradeStorage(uint grade)
            : base(grade)
        {
            MatrixStorage = LinMatrixEmptyStorage<T>.EmptyStorage;
        }

        internal LinMatrixSingleGradeStorage(uint grade, [NotNull] ILinMatrixStorage<T> matrixStorage)
            : base(grade)
        {
            MatrixStorage = matrixStorage.IsNullOrEmpty() 
                ? LinMatrixEmptyStorage<T>.EmptyStorage
                : matrixStorage;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsEmpty()
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(uint grade, ulong index1, ulong index2)
        {
            return grade == Grade
                ? MatrixStorage.GetScalar(index1, index2)
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(GradeIndexPairRecord gradeKey)
        {
            var (grade, index1, index2) = gradeKey;

            return grade == Grade
                ? MatrixStorage.GetScalar(index1, index2)
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(uint grade, IndexPairRecord index)
        {
            return grade == Grade
                ? MatrixStorage.GetScalar(index)
                : throw new KeyNotFoundException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsIndex(uint grade, ulong index1, ulong index2)
        {
            return grade == Grade
                ? MatrixStorage.ContainsIndex(index1, index2)
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsIndex(uint grade, IndexPairRecord index)
        {
            return grade == Grade
                ? MatrixStorage.ContainsIndex(index)
                : throw new KeyNotFoundException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetScalar(uint grade, IndexPairRecord index, out T value)
        {
            if (grade == Grade)
                return MatrixStorage.TryGetScalar(index, out value);

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetScalar(uint grade, ulong index1, ulong index2, out T value)
        {
            if (grade == Grade)
                return MatrixStorage.TryGetScalar(index1, index2, out value);

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<GradeIndexPairRecord> GetGradeIndexRecords()
        {
            return MatrixStorage.GetIndices().Select(
                index => new GradeIndexPairRecord(Grade, index.Index1, index.Index2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<GradeIndexPairScalarRecord<T>> GetGradeIndexScalarRecords()
        {
            return MatrixStorage.GetIndexScalarRecords().Select(
                indexValueRecord =>
                {
                    var (index1, index2, value) = indexValueRecord;

                    return new GradeIndexPairScalarRecord<T>(Grade, index1, index2, value);
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixGradedStorage<T> GetCopy()
        {
            return new LinMatrixSingleGradeStorage<T>(
                Grade,
                MatrixStorage.GetCopy()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixGradedStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            return new LinMatrixSingleGradeStorage<T2>(
                Grade,
                MatrixStorage.MapScalars(valueMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixGradedStorage<T2> MapScalars<T2>(Func<ulong, ulong, T, T2> indexValueMapping)
        {
            return new LinMatrixSingleGradeStorage<T2>(
                Grade,
                MatrixStorage.MapScalars(indexValueMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixGradedStorage<T2> MapScalars<T2>(Func<uint, ulong, ulong, T, T2> gradeKeyValueMapping)
        {
            return new LinMatrixSingleGradeStorage<T2>(
                Grade,
                MatrixStorage.MapScalars((index1, index2, value) => 
                    gradeKeyValueMapping(Grade, index1, index2, value)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixGradedStorage<T> FilterByIndex(Func<ulong, ulong, bool> indexFilter)
        {
            var matrixStorage = MatrixStorage.FilterByIndex(indexFilter);

            return matrixStorage.IsEmpty()
                ? LinMatrixEmptyGradedStorage<T>.EmptyStorage
                : new LinMatrixSingleGradeStorage<T>(Grade, matrixStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixGradedStorage<T> FilterByGradeIndex(Func<uint, ulong, ulong, bool> gradeKeyFilter)
        {
            var matrixStorage = MatrixStorage.FilterByIndex(
                (index1, index2) => 
                    gradeKeyFilter(Grade, index1, index2)
            );
            
            return matrixStorage.IsEmpty()
                ? LinMatrixEmptyGradedStorage<T>.EmptyStorage
                : new LinMatrixSingleGradeStorage<T>(Grade, matrixStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixGradedStorage<T> FilterByScalar(Func<T, bool> valueFilter)
        {
            var matrixStorage = MatrixStorage.FilterByScalar(valueFilter);
            
            return matrixStorage.IsEmpty()
                ? LinMatrixEmptyGradedStorage<T>.EmptyStorage
                : new LinMatrixSingleGradeStorage<T>(Grade, matrixStorage);

            
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixGradedStorage<T> FilterByGradeScalar(Func<uint, T, bool> gradeValueFilter)
        {
            var matrixStorage = MatrixStorage.FilterByScalar(
                value => gradeValueFilter(Grade, value)
            );
            
            return matrixStorage.IsEmpty()
                ? LinMatrixEmptyGradedStorage<T>.EmptyStorage
                : new LinMatrixSingleGradeStorage<T>(Grade, matrixStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixGradedStorage<T> FilterByIndexScalar(Func<ulong, ulong, T, bool> indexValueFilter)
        {
            
            var matrixStorage = MatrixStorage.FilterByIndexScalar(indexValueFilter);
            
            return matrixStorage.IsEmpty()
                ? LinMatrixEmptyGradedStorage<T>.EmptyStorage
                : new LinMatrixSingleGradeStorage<T>(Grade, matrixStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixGradedStorage<T> FilterByGradeIndexScalar(Func<uint, ulong, ulong, T, bool> gradeKeyValueFilter)
        {
            var matrixStorage = MatrixStorage.FilterByIndexScalar(
                (index1,index2, value) => 
                    gradeKeyValueFilter(Grade, index1, index2, value)
            );
            
            return matrixStorage.IsEmpty()
                ? LinMatrixEmptyGradedStorage<T>.EmptyStorage
                : new LinMatrixSingleGradeStorage<T>(Grade, matrixStorage);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixStorage<T> ToMatrixStorage(Func<uint, ulong, ulong> gradeIndexToIndexMapping)
        {
            return MatrixStorage.GetPermutation(
                (index1, index2) => 
                    new IndexPairRecord(
                        gradeIndexToIndexMapping(Grade, index1),
                        gradeIndexToIndexMapping(Grade, index2)
                    )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixStorage<T> ToMatrixStorage(Func<uint, ulong, ulong, IndexPairRecord> gradeIndexToIndexMapping)
        {
            return MatrixStorage.GetPermutation(
                (index1, index2) => 
                    gradeIndexToIndexMapping(Grade, index1, index2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixGradedStorage<T> GetTranspose()
        {
            return new LinMatrixSingleGradeStorage<T>(Grade, MatrixStorage.GetTranspose());
        }
    }
}