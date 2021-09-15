using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra
{
    public sealed class OutermorphismStorage<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static OutermorphismStorage<T> Create(ILinMatrixGradedStorage<T> gradeIndexToKVectorMatrix)
        {
            return new OutermorphismStorage<T>(gradeIndexToKVectorMatrix);
        }


        public ILinMatrixGradedStorage<T> GradeIndexToKVectorMatrix { get; }


        private OutermorphismStorage([NotNull] ILinMatrixGradedStorage<T> gradeIndexToKVectorMatrix)
        {
            GradeIndexToKVectorMatrix = gradeIndexToKVectorMatrix;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T> GetMappedBasisVector(ulong index)
        {
            return GradeIndexToKVectorMatrix
                .GetMatrixStorage(1)
                .GetColumn(index)
                .CreateVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BivectorStorage<T> GetMappedBasisBivector(ulong index)
        {
            return GradeIndexToKVectorMatrix
                .GetMatrixStorage(2)
                .GetColumn(index)
                .CreateBivectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BivectorStorage<T> GetMappedBasisBivector(ulong index1, ulong index2)
        {
            var index = 
                BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(index1, index2);

            return GradeIndexToKVectorMatrix
                .GetMatrixStorage(2)
                .GetColumn(index)
                .CreateBivectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetMappedBasisBlade(ulong id)
        {
            var (grade, index) = 
                id.BasisBladeIdToGradeIndex();

            return GradeIndexToKVectorMatrix
                .GetMatrixStorage(grade)
                .GetColumn(index)
                .CreateKVectorStorage(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetMappedBasisBlade(uint grade, ulong index)
        {
            return GradeIndexToKVectorMatrix
                .GetMatrixStorage(grade)
                .GetColumn(index)
                .CreateKVectorStorage(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetVectorMappingMatrix()
        {
            return GradeIndexToKVectorMatrix.GetMatrixStorage(1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetBivectorMappingMatrix()
        {
            return GradeIndexToKVectorMatrix.GetMatrixStorage(2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetKVectorMappingMatrix(uint grade)
        {
            return GradeIndexToKVectorMatrix.GetMatrixStorage(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetMultivectorMappingMatrix()
        {
            return GradeIndexToKVectorMatrix
                .ToMatrixStorage(BasisBladeUtils.BasisBladeGradeIndexToId);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixGradedStorage<T> GetMultivectorGradedMappingMatrix()
        {
            return GradeIndexToKVectorMatrix;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetMappedIds()
        {
            return GradeIndexToKVectorMatrix
                .GetGradeIndexRecords()
                .Select(r => 
                    BasisBladeUtils.BasisBladeGradeIndexToId(r.Grade, r.Index2)
                ).Distinct();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetMappedIds(uint grade)
        {
            return GradeIndexToKVectorMatrix
                .GetMatrixStorage(grade)
                .GetIndices2()
                .Select(index => 
                    BasisBladeUtils.BasisBladeGradeIndexToId(grade, index)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetMappedIndices(uint grade)
        {
            return GradeIndexToKVectorMatrix
                .GetMatrixStorage(grade)
                .GetIndices2();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GradeIndexRecord> GetMappedGradeIndexRecords()
        {
            return GradeIndexToKVectorMatrix
                .GetGradeIndexRecords()
                .Select(r => 
                    new GradeIndexRecord(r.Grade, r.Index2)
                ).Distinct();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GradeIndexRecord> GetMappedGradeIndexRecords(uint grade)
        {
            return GradeIndexToKVectorMatrix
                .GetMatrixStorage(grade)
                .GetIndices2()
                .Select(index => new GradeIndexRecord(grade, index));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IdKVectorStorageRecord<T>> GetMappedBasisBladesById()
        {
            return GradeIndexToKVectorMatrix
                .GetColumns()
                .Select(r => 
                    new IdKVectorStorageRecord<T>(
                        r.Index.BasisBladeIndexToId(r.Grade),
                        r.Storage.CreateKVectorStorage(r.Grade)
                    )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IdKVectorStorageRecord<T>> GetMappedBasisBladesById(uint grade)
        {
            return GradeIndexToKVectorMatrix
                .GetMatrixStorage(grade)
                .GetColumns()
                .Select(r => 
                    new IdKVectorStorageRecord<T>(
                        r.Index.BasisBladeIndexToId(grade),
                        r.Storage.CreateKVectorStorage(grade)
                    )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GradeIndexKVectorStorageRecord<T>> GetMappedBasisBlades()
        {
            return GradeIndexToKVectorMatrix
                .GetColumns()
                .Select(r => 
                    new GradeIndexKVectorStorageRecord<T>(
                        r.Grade,
                        r.Index,
                        r.Storage.CreateKVectorStorage(r.Grade)
                    )
                );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OutermorphismStorage<T> GetTranspose()
        {
            return new OutermorphismStorage<T>(
                GradeIndexToKVectorMatrix.GetTranspose()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexVectorStorageRecord<T>> GetMappedBasisVectors()
        {
            return GradeIndexToKVectorMatrix
                .GetMatrixStorage(1)
                .GetColumns()
                .Select(r => 
                    new IndexVectorStorageRecord<T>(
                        r.Index, 
                        r.Storage.CreateVectorStorage()
                    )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexBivectorStorageRecord<T>> GetMappedBasisBivectors()
        {
            return GradeIndexToKVectorMatrix
                .GetMatrixStorage(2)
                .GetColumns()
                .Select(r => 
                    new IndexBivectorStorageRecord<T>(
                        r.Index, 
                        r.Storage.CreateBivectorStorage()
                    )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexKVectorStorageRecord<T>> GetMappedBasisBlades(uint grade)
        {
            return GradeIndexToKVectorMatrix
                .GetMatrixStorage(grade)
                .GetColumns()
                .Select(r => 
                    new IndexKVectorStorageRecord<T>(
                        r.Index, 
                        r.Storage.CreateKVectorStorage(grade)
                    )
                );
        }
    }
}