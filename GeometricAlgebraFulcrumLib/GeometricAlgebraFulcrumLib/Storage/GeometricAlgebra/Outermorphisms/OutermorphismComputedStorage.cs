namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Outermorphisms
{
    //public sealed class OutermorphismComputedStorage<T> :
    //    IOutermorphismStorage<T>
    //{
    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //    public static OutermorphismComputedStorage<T> Create(ILinearAlgebraProcessor<T> matrixProcessor, ILinMatrixStorage<T> indexToVectorMatrix)
    //    {
    //        return new OutermorphismComputedStorage<T>(
    //            matrixProcessor, 
    //            indexToVectorMatrix
    //        );
    //    }


    //    public ILinearAlgebraProcessor<T> MatrixProcessor { get; }

    //    public ILinMatrixStorage<T> IndexToVectorMatrix { get; }


    //    private OutermorphismComputedStorage([NotNull] ILinearAlgebraProcessor<T> matrixProcessor, [NotNull] ILinMatrixStorage<T> indexToVectorMatrix)
    //    {
    //        MatrixProcessor = matrixProcessor;
    //        IndexToVectorMatrix = indexToVectorMatrix;
    //    }


    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //    public VectorStorage<T> GetMappedBasisVector(ulong index)
    //    {
    //        return IndexToVectorMatrix
    //            .GetColumn(index)
    //            .CreateVectorStorage();
    //    }

    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //    public BivectorStorage<T> GetMappedBasisBivector(ulong index)
    //    {
    //        var (index1, index2) =
    //            index.BasisBivectorIndexToVectorIndices();

    //        return MatrixProcessor.VectorsOp(
    //            IndexToVectorMatrix.GetColumn(index1),
    //            IndexToVectorMatrix.GetColumn(index2)
    //        );
    //    }

    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //    public BivectorStorage<T> GetMappedBasisBivector(ulong index1, ulong index2)
    //    {
    //        if (index1 == index2)
    //            return BivectorStorage<T>.ZeroBivector;

    //        var v1 = IndexToVectorMatrix.GetColumn(index1);
    //        var v2 = IndexToVectorMatrix.GetColumn(index2);

    //        return index1 < index2
    //            ? MatrixProcessor.VectorsOp(v1, v2)
    //            : MatrixProcessor.VectorsOp(v2, v1);
    //    }

    //    public KVectorStorage<T> GetMappedBasisBlade(ulong id)
    //    {
    //        if (id == 0)
    //            return MatrixProcessor.CreateKVectorBasisScalarStorage();

    //        if (id.IsBasisVectorId())
    //            return IndexToVectorMatrix
    //                .GetColumn(id.BasisVectorIdToIndex())
    //                .CreateVectorStorage();

    //        return MatrixProcessor.Op(
    //            id
    //                .BasisBladeIdToBasisVectorIndices()
    //                .Select(GetMappedBasisVector)
    //                .ToArray()
    //        );
    //    }

    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //    public KVectorStorage<T> GetMappedBasisBlade(uint grade, ulong index)
    //    {
    //        return grade switch
    //        {
    //            0 => MatrixProcessor.CreateKVectorBasisScalarStorage(),
    //            1 => IndexToVectorMatrix.GetColumn(index).CreateVectorStorage(),
    //            _ => GetMappedBasisBlade(BasisBladeUtils.BasisBladeGradeIndexToId(grade, index))
    //        };
    //    }

    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //    public IEnumerable<ulong> GetMappedIds()
    //    {
    //        return IndexToVectorMatrix
    //            .GetIndices2()
    //            .Select(index => index.BasisVectorIndexToId());
    //    }

    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //    public IEnumerable<ulong> GetMappedIds(uint grade)
    //    {
    //        return grade == 1
    //            ? GetMappedIds() 
    //            : Enumerable.Empty<ulong>();
    //    }

    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //    public IEnumerable<ulong> GetMappedIndices(uint grade)
    //    {
    //        return grade == 1
    //            ? IndexToVectorMatrix.GetIndices2()
    //            : Enumerable.Empty<ulong>();
    //    }

    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //    public IEnumerable<GradeIndexRecord> GetMappedGradeIndexRecords()
    //    {
    //        return IndexToVectorMatrix
    //            .GetIndices2()
    //            .Select(index => new GradeIndexRecord(1, index));
    //    }

    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //    public IEnumerable<GradeIndexRecord> GetMappedGradeIndexRecords(uint grade)
    //    {
    //        return grade == 1
    //            ? GetMappedGradeIndexRecords() 
    //            : Enumerable.Empty<GradeIndexRecord>();
    //    }

    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //    public IEnumerable<IndexVectorStorageRecord<T>> GetMappedIdKVectorRecords()
    //    {
    //        return IndexToVectorMatrix
    //            .GetIndices2()
    //            .Select(index => 
    //                new IndexVectorStorageRecord<T>(
    //                    index.BasisVectorIndexToId(),
    //                    IndexToVectorMatrix.GetColumn(index)
    //                )
    //            );
    //    }

    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //    public IEnumerable<IndexVectorStorageRecord<T>> GetMappedIdKVectorRecords(uint grade)
    //    {
    //        return grade == 1
    //            ? GetMappedIdKVectorRecords()
    //            : Enumerable.Empty<IndexVectorStorageRecord<T>>();
    //    }

    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //    public IEnumerable<GradeIndexVectorStorageRecord<T>> GetMappedGradeIndexKVectorRecords()
    //    {
    //        return IndexToVectorMatrix
    //            .GetIndices2()
    //            .Select(index => 
    //                new GradeIndexVectorStorageRecord<T>(
    //                    1,
    //                    index,
    //                    IndexToVectorMatrix.GetColumn(index)
    //                )
    //            );
    //    }

    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //    public IEnumerable<GradeIndexVectorStorageRecord<T>> GetMappedGradeIndexKVectorRecords(uint grade)
    //    {
    //        return grade == 1
    //            ? GetMappedGradeIndexKVectorRecords()
    //            : Enumerable.Empty<GradeIndexVectorStorageRecord<T>>();
    //    }

    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //    public IEnumerable<IndexVectorStorageRecord<T>> GetMappedIndexKVectorRecords(uint grade)
    //    {
    //        return grade == 1
    //            ? IndexToVectorMatrix
    //                .GetIndices2()
    //                .Select(index => 
    //                    new IndexVectorStorageRecord<T>(
    //                        index,
    //                        IndexToVectorMatrix.GetColumn(index)
    //                    )
    //                )
    //            : Enumerable.Empty<IndexVectorStorageRecord<T>>();
    //    }
    //}
}