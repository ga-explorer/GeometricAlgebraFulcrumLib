using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.GradedMatrices
{
    public sealed record LaMatrixSingleGradeStorage<T> :
        LaMatrixSingleGradeStorageBase<T>
    {
        
        public override ILaMatrixEvenStorage<T> EvenStorage { get; }


        internal LaMatrixSingleGradeStorage(uint grade)
            : base(grade)
        {
            EvenStorage = LaMatrixEmptyStorage<T>.EmptyMatrix;
        }

        internal LaMatrixSingleGradeStorage(uint grade, [NotNull] ILaMatrixEvenStorage<T> evenGrid)
            : base(grade)
        {
            EvenStorage = evenGrid.IsNullOrEmpty() 
                ? LaMatrixEmptyStorage<T>.EmptyMatrix
                : evenGrid;
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
                ? EvenStorage.GetScalar(index1, index2)
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(GradeIndexPairRecord gradeKey)
        {
            var (grade, index1, index2) = gradeKey;

            return grade == Grade
                ? EvenStorage.GetScalar(index1, index2)
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(uint grade, IndexPairRecord index)
        {
            return grade == Grade
                ? EvenStorage.GetScalar(index)
                : throw new KeyNotFoundException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsIndex(uint grade, ulong index1, ulong index2)
        {
            return grade == Grade
                ? EvenStorage.ContainsIndex(index1, index2)
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsIndex(uint grade, IndexPairRecord index)
        {
            return grade == Grade
                ? EvenStorage.ContainsIndex(index)
                : throw new KeyNotFoundException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetScalar(uint grade, IndexPairRecord index, out T value)
        {
            if (grade == Grade)
                return EvenStorage.TryGetScalar(index, out value);

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetScalar(uint grade, ulong index1, ulong index2, out T value)
        {
            if (grade == Grade)
                return EvenStorage.TryGetScalar(index1, index2, out value);

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<GradeIndexPairRecord> GetGradeIndexRecords()
        {
            return EvenStorage.GetIndices().Select(
                index => new GradeIndexPairRecord(Grade, index.Index1, index.Index2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<GradeIndexPairScalarRecord<T>> GetGradeIndexScalarRecords()
        {
            return EvenStorage.GetIndexScalarRecords().Select(
                indexValueRecord =>
                {
                    var (index1, index2, value) = indexValueRecord;

                    return new GradeIndexPairScalarRecord<T>(Grade, index1, index2, value);
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixGradedStorage<T> GetCopy()
        {
            return new LaMatrixSingleGradeStorage<T>(
                Grade,
                EvenStorage.GetCopy()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixGradedStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            return new LaMatrixSingleGradeStorage<T2>(
                Grade,
                EvenStorage.MapScalars(valueMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixGradedStorage<T2> MapScalars<T2>(Func<ulong, ulong, T, T2> indexValueMapping)
        {
            return new LaMatrixSingleGradeStorage<T2>(
                Grade,
                EvenStorage.MapScalars(indexValueMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixGradedStorage<T2> MapScalars<T2>(Func<uint, ulong, ulong, T, T2> gradeKeyValueMapping)
        {
            return new LaMatrixSingleGradeStorage<T2>(
                Grade,
                EvenStorage.MapScalars((index1, index2, value) => 
                    gradeKeyValueMapping(Grade, index1, index2, value)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixGradedStorage<T> FilterByIndex(Func<ulong, ulong, bool> indexFilter)
        {
            var evenGrid = EvenStorage.FilterByIndex(indexFilter);

            return evenGrid.IsEmpty()
                ? LaMatrixEmptyGradedStorage<T>.EmptyGrid
                : new LaMatrixSingleGradeStorage<T>(Grade, evenGrid);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixGradedStorage<T> FilterByGradeIndex(Func<uint, ulong, ulong, bool> gradeKeyFilter)
        {
            var evenGrid = EvenStorage.FilterByIndex(
                (index1, index2) => 
                    gradeKeyFilter(Grade, index1, index2)
            );
            
            return evenGrid.IsEmpty()
                ? LaMatrixEmptyGradedStorage<T>.EmptyGrid
                : new LaMatrixSingleGradeStorage<T>(Grade, evenGrid);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixGradedStorage<T> FilterByScalar(Func<T, bool> valueFilter)
        {
            var evenGrid = EvenStorage.FilterByScalar(valueFilter);
            
            return evenGrid.IsEmpty()
                ? LaMatrixEmptyGradedStorage<T>.EmptyGrid
                : new LaMatrixSingleGradeStorage<T>(Grade, evenGrid);

            
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixGradedStorage<T> FilterByGradeScalar(Func<uint, T, bool> gradeValueFilter)
        {
            var evenGrid = EvenStorage.FilterByScalar(
                value => gradeValueFilter(Grade, value)
            );
            
            return evenGrid.IsEmpty()
                ? LaMatrixEmptyGradedStorage<T>.EmptyGrid
                : new LaMatrixSingleGradeStorage<T>(Grade, evenGrid);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixGradedStorage<T> FilterByIndexScalar(Func<ulong, ulong, T, bool> indexValueFilter)
        {
            
            var evenGrid = EvenStorage.FilterByIndexScalar(indexValueFilter);
            
            return evenGrid.IsEmpty()
                ? LaMatrixEmptyGradedStorage<T>.EmptyGrid
                : new LaMatrixSingleGradeStorage<T>(Grade, evenGrid);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixGradedStorage<T> FilterByGradeIndexScalar(Func<uint, ulong, ulong, T, bool> gradeKeyValueFilter)
        {
            var evenGrid = EvenStorage.FilterByIndexScalar(
                (index1,index2, value) => 
                    gradeKeyValueFilter(Grade, index1, index2, value)
            );
            
            return evenGrid.IsEmpty()
                ? LaMatrixEmptyGradedStorage<T>.EmptyGrid
                : new LaMatrixSingleGradeStorage<T>(Grade, evenGrid);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixEvenStorage<T> ToEvenStorage(Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            return EvenStorage.MapIndices(
                (index1, index2) => 
                    new IndexPairRecord(
                        gradeKeyToEvenKeyMapping(Grade, index1),
                        gradeKeyToEvenKeyMapping(Grade, index2)
                    )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixEvenStorage<T> ToEvenStorage(Func<uint, ulong, ulong, IndexPairRecord> gradeKeyToEvenKeyMapping)
        {
            return EvenStorage.MapIndices(
                (index1, index2) => 
                    gradeKeyToEvenKeyMapping(Grade, index1, index2)
            );
        }
    }
}