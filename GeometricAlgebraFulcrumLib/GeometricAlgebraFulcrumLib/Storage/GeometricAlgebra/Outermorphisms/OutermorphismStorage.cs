using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Dense;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Outermorphisms
{
    public sealed class OutermorphismStorage<T> :
        IOutermorphismStorage<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static OutermorphismStorage<T> Create(ILinMatrixStorage<T> idToKVectorMatrix)
        {
            return new OutermorphismStorage<T>(idToKVectorMatrix);
        }


        public ILinMatrixStorage<T> IdToKVectorMatrix { get; }
        

        private OutermorphismStorage([NotNull] ILinMatrixStorage<T> idToKVectorMatrix)
        {
            IdToKVectorMatrix = idToKVectorMatrix;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T> GetMappedBasisVector(ulong index)
        {
            return IdToKVectorMatrix
                .GetColumn(index.BasisVectorIndexToId())
                .FilterByIndex(id => id.IsBasisVectorId())
                .GetPermutation(id => id.BasisVectorIdToIndex())
                .CreateVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BivectorStorage<T> GetMappedBasisBivector(ulong index)
        {
            return IdToKVectorMatrix
                .GetColumn(index.BasisBivectorIndexToId())
                .FilterByIndex(id => id.IsBasisBivectorId())
                .GetPermutation(id => id.BasisBivectorIdToIndex())
                .CreateBivectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BivectorStorage<T> GetMappedBasisBivector(ulong index1, ulong index2)
        {
            return index1 < index2
                ? IdToKVectorMatrix
                    .GetColumn(BasisBivectorUtils.BasisVectorIndicesToBivectorId(index1, index2))
                    .FilterByIndex(id => id.IsBasisBivectorId())
                    .GetPermutation(id => id.BasisBivectorIdToIndex())
                    .CreateBivectorStorage()
                : throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetMappedBasisBlade(ulong id)
        {
            var (grade, index) = 
                id.BasisBladeIdToGradeIndex();

            return IdToKVectorMatrix
                .GetColumn(index.BasisBladeIndexToId(grade))
                .FilterByIndex(basisBladeId => basisBladeId.IsBasisBladeOfGrade(grade))
                .GetPermutation(basisBladeId => basisBladeId.BasisBladeIdToIndex())
                .CreateKVectorStorage(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetMappedBasisBlade(uint grade, ulong index)
        {
            return IdToKVectorMatrix
                .GetColumn(index.BasisBladeIndexToId(grade))
                .FilterByIndex(id => id.IsBasisBladeOfGrade(grade))
                .GetPermutation(id => id.BasisBladeIdToIndex())
                .CreateKVectorStorage(grade);
        }

        public ILinMatrixStorage<T> GetVectorMappingMatrix()
        {
            if (IdToKVectorMatrix is not ILinMatrixDenseStorage<T> denseMatrix)
                return IdToKVectorMatrix
                    .FilterByIndex((rowId, colId) =>
                        rowId.IsBasisVectorId() &&
                        colId.IsBasisVectorId()
                    )
                    .GetPermutation((rowId, colId) =>
                        new IndexPairRecord(
                            rowId.BasisVectorIdToIndex(),
                            colId.BasisVectorIdToIndex()
                        )
                    );

            var count2 = (ulong) denseMatrix.Count2;

            if (count2 == 0)
                return LinMatrixEmptyStorage<T>.EmptyStorage;

            return denseMatrix
                .GetDenseColumns(count2.GetBasisVectorIds())
                .OrderBy(r => r.Index)
                .Select(r => 
                    r.Storage
                        .FilterByIndex(vid => vid.IsBasisVectorId())
                        .GetPermutation(BasisVectorUtils.BasisVectorIdToIndex)
                )
                .CreateLinVectorStorage()
                .CreateLinMatrixColumnsListStorage();
        }

        public ILinMatrixStorage<T> GetBivectorMappingMatrix()
        {
            if (IdToKVectorMatrix is not ILinMatrixDenseStorage<T> denseMatrix)
                return IdToKVectorMatrix
                    .FilterByIndex((rowId, colId) =>
                        rowId.IsBasisBivectorId() &&
                        colId.IsBasisBivectorId()
                    )
                    .GetPermutation((rowId, colId) =>
                        new IndexPairRecord(
                            rowId.BasisBivectorIdToIndex(),
                            colId.BasisBivectorIdToIndex()
                        )
                    );

            var count2 = (ulong) denseMatrix.Count2;

            if (count2 == 0)
                return LinMatrixEmptyStorage<T>.EmptyStorage;

            return denseMatrix
                .GetDenseColumns(count2.GetBasisBivectorIds())
                .OrderBy(r => r.Index)
                .Select(r => 
                    r.Storage
                        .FilterByIndex(vid => vid.IsBasisBivectorId())
                        .GetPermutation(BasisBivectorUtils.BasisBivectorIdToIndex)
                )
                .CreateLinVectorStorage()
                .CreateLinMatrixColumnsListStorage();
        }

        public ILinMatrixStorage<T> GetKVectorMappingMatrix(uint grade)
        {
            if (IdToKVectorMatrix is not ILinMatrixDenseStorage<T> denseMatrix)
                return IdToKVectorMatrix
                    .FilterByIndex((rowId, colId) =>
                        rowId.IsBasisBladeOfGrade(grade) &&
                        colId.IsBasisBladeOfGrade(grade)
                    )
                    .GetPermutation((rowId, colId) =>
                        new IndexPairRecord(
                            rowId.BasisBladeIdToIndex(),
                            colId.BasisBladeIdToIndex()
                        )
                    );

            var count2 = (ulong) denseMatrix.Count2;

            if (count2 == 0)
                return LinMatrixEmptyStorage<T>.EmptyStorage;

            return denseMatrix
                .GetDenseColumns(count2.GetBasisBladeIds(grade))
                .OrderBy(r => r.Index)
                .Select(r => 
                    r.Storage
                        .FilterByIndex(vid => vid.IsBasisBladeOfGrade(grade))
                        .GetPermutation(BasisBladeUtils.BasisBladeIdToIndex)
                )
                .CreateLinVectorStorage()
                .CreateLinMatrixColumnsListStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetMultivectorMappingMatrix()
        {
            return IdToKVectorMatrix;
        }

        public ILinMatrixGradedStorage<T> GetMultivectorGradedMappingMatrix()
        {
            //if (IdToKVectorMatrix is not ILinMatrixDenseStorage<T> denseMatrix)
            return IdToKVectorMatrix
                .FilterByIndex((id1, id2) =>
                    id1.IsBasisBladeOfGrade(id2.BasisBladeIdToGrade())
                )
                .GetIndexScalarRecords()
                .Select(r =>
                    new GradeIndexPairScalarRecord<T>(
                        r.Index1.BasisBladeIdToGrade(),
                        r.Index1.BasisVectorIdToIndex(),
                        r.Index2.BasisVectorIdToIndex(),
                        r.Scalar
                    )
                ).CreateLinMatrixGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetMappedIds()
        {
            return IdToKVectorMatrix.GetIndices2();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetMappedIds(uint grade)
        {
            return IdToKVectorMatrix
                .GetIndices2()
                .Where(id => id.IsBasisBladeOfGrade(grade));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetMappedIndices(uint grade)
        {
            return IdToKVectorMatrix
                .GetIndices2()
                .Where(id => id.IsBasisBladeOfGrade(grade))
                .Select(id => id.BasisBladeIdToIndex());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GradeIndexRecord> GetMappedGradeIndexRecords()
        {
            return IdToKVectorMatrix
                .GetIndices2()
                .Select(id => id.BasisBladeIdToGradeIndex());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GradeIndexRecord> GetMappedGradeIndexRecords(uint grade)
        {
            return IdToKVectorMatrix
                .GetIndices2()
                .Where(id => id.IsBasisBladeOfGrade(grade))
                .Select(id => id.BasisBladeIdToGradeIndex());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IdKVectorStorageRecord<T>> GetMappedBasisBladesById()
        {
            return GetMappedIds()
                .Select(colId =>
                    {
                        var grade = colId.BasisBladeIdToGrade();

                        return new IdKVectorStorageRecord<T>(
                            colId,
                            IdToKVectorMatrix
                                .GetColumn(colId)
                                .FilterByIndex(rowId => rowId.IsBasisBladeOfGrade(grade))
                                .GetPermutation(rowId => rowId.BasisBladeIdToIndex())
                                .CreateKVectorStorage(colId.BasisBladeIdToGrade())
                        );
                    }
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IdKVectorStorageRecord<T>> GetMappedBasisBladesById(uint grade)
        {
            return GetMappedIds(grade)
                .Select(colId =>
                    new IdKVectorStorageRecord<T>(
                        colId,
                        IdToKVectorMatrix
                            .GetColumn(colId)
                            .FilterByIndex(rowId => rowId.IsBasisBladeOfGrade(grade))
                            .GetPermutation(rowId => rowId.BasisBladeIdToIndex())
                            .CreateKVectorStorage(grade)
                    )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GradeIndexKVectorStorageRecord<T>> GetMappedBasisBlades()
        {
            return GetMappedIds()
                .Select(colId =>
                    {
                        var (grade, index) = colId.BasisBladeIdToGradeIndex();

                        return new GradeIndexKVectorStorageRecord<T>(
                            grade,
                            index,
                            IdToKVectorMatrix
                                .GetColumn(colId)
                                .FilterByIndex(rowId => rowId.IsBasisBladeOfGrade(grade))
                                .GetPermutation(rowId => rowId.BasisBladeIdToIndex())
                                .CreateKVectorStorage(grade)
                        );
                    }
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IOutermorphismStorage<T> GetTranspose()
        {
            return new OutermorphismStorage<T>(
                IdToKVectorMatrix.GetTranspose()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexVectorStorageRecord<T>> GetMappedBasisVectors()
        {
            return GetMappedIds(1)
                .Select(colId => new IndexVectorStorageRecord<T>(
                        colId.BasisBladeIdToIndex(),
                        IdToKVectorMatrix
                            .GetColumn(colId)
                            .FilterByIndex(rowId => rowId.IsBasisVectorId())
                            .GetPermutation(rowId => rowId.BasisVectorIdToIndex())
                            .CreateVectorStorage()
                    )
                ).OrderBy(r => r.Index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexBivectorStorageRecord<T>> GetMappedBasisBivectors()
        {
            return GetMappedIds(2)
                .Select(colId => new IndexBivectorStorageRecord<T>(
                        colId.BasisBladeIdToIndex(),
                        IdToKVectorMatrix
                            .GetColumn(colId)
                            .FilterByIndex(rowId => rowId.IsBasisBivectorId())
                            .GetPermutation(rowId => rowId.BasisBivectorIdToIndex())
                            .CreateBivectorStorage()
                    )
                ).OrderBy(r => r.Index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexKVectorStorageRecord<T>> GetMappedBasisBlades(uint grade)
        {
            return GetMappedIds(grade)
                .Select(colId => new IndexKVectorStorageRecord<T>(
                    colId.BasisBladeIdToIndex(),
                    IdToKVectorMatrix
                        .GetColumn(colId)
                        .FilterByIndex(rowId => rowId.IsBasisBladeOfGrade(grade))
                        .GetPermutation(rowId => rowId.BasisBladeIdToIndex())
                        .CreateKVectorStorage(grade)
                )
            ).OrderBy(r => r.Index);
        }
    }
}