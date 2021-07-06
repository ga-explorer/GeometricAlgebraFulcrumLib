namespace GeometricAlgebraLib.Algebra.Outermorphisms
{
    //public abstract class GaNumMapUnilinear<TItem, TArray>
    //{
    //    protected TArray InternalMappingMatrix;


    //    public IGaImmutableScalarsArraysDomain<TItem, TArray> ArraysDomain { get; }

    //    public IGaImmutableScalarsDomain<TItem> ScalarProcessor 
    //        => ArraysDomain.ScalarProcessor;

    //    public GaMultivector<TItem> this[int grade1, ulong index1]
    //        => this[GaFrameUtils.BasisBladeId(grade1, index1)];

    //    public abstract GaMultivector<TItem> this[ulong id1] { get; }

    //    public abstract GaMultivector<TItem> this[GaMultivector<TItem> mv1] { get; }

    //    public TArray MappingMatrix
    //    {
    //        get
    //        {
    //            if (ReferenceEquals(InternalMappingMatrix, null))
    //                GetMappingMatrix();

    //            return InternalMappingMatrix;
    //        }
    //    }


    //    public virtual GaMultivector<TItem> MapPseudoScalar(int vSpaceDimension)
    //    {
    //        return this[(1UL << vSpaceDimension) - 1];
    //    }

    //    public virtual TArray GetMappingMatrix()
    //    {
    //        var matrixItems = ArraysDomain.CreateZeroArray(TargetGaSpaceDimension, DomainGaSpaceDimension);

    //        for (var col = 0UL; col < DomainGaSpaceDimension; col++)
    //        {
    //            var mv = this[col];

    //            if (mv.IsNullOrEmpty())
    //                continue;

    //            for (var row = 0UL; row < TargetGaSpaceDimension; row++)
    //                matrixItems[row, col] = mv[row];
    //        }

    //        InternalMappingMatrix = DenseMatrix.OfArray(matrixItems);

    //        return InternalMappingMatrix;
    //    }

    //    public virtual TArray GetVectorsMappingMatrix()
    //    {
    //        var matrixItems = new T[TargetVSpaceDimension, DomainVSpaceDimension];

    //        foreach (var pair in BasisVectorMaps())
    //        {
    //            var col = pair.Item1;

    //            foreach (var term in pair.Item2)
    //            {
    //                var row = term.BasisBladeId.BasisBladeIndex();

    //                matrixItems[row, col] = term.ScalarValue;
    //            }
    //        }

    //        return DenseMatrix.OfArray(matrixItems);
    //    }


    //    public virtual GaNumMapUnilinear<TItem, TArray> Adjoint()
    //    {
    //        var exprArray = this.ToScalarsArray();

    //        var resultMap = GaNumMapUnilinearTree.Create(
    //            TargetVSpaceDimension,
    //            DomainVSpaceDimension
    //        );

    //        for (var id = 0UL; id < TargetGaSpaceDimension; id++)
    //        {
    //            var mv = GaNumSarMultivector.CreateFromRow(exprArray, id);

    //            if (!mv.IsNullOrEmpty())
    //                resultMap.SetBasisBladeMap(id, mv);
    //        }

    //        return resultMap;
    //    }

    //    public virtual GaNumMapUnilinear<TItem, TArray> Inverse()
    //    {
    //        return (this.ToDenseMatrix().Inverse() as T[,]).ToTreeMap();
    //    }

    //    public virtual GaNumMapUnilinear<TItem, TArray> InverseAdjoint()
    //    {
    //        return (this.ToDenseMatrix().Inverse().Transpose() as T[,]).ToTreeMap();
    //    }


    //    public abstract IEnumerable<Tuple<ulong, GaMultivector<TItem>>> BasisBladeMaps();

    //    public virtual IEnumerable<Tuple<ulong, GaMultivector<TItem>>> BasisVectorMaps()
    //    {
    //        for (var index = 0UL; index < (ulong)DomainVSpaceDimension; index++)
    //        {
    //            var mv = this[GaFrameUtils.BasisBladeId(1, index)];

    //            if (!mv.IsNullOrEmpty())
    //                yield return new Tuple<ulong, GaMultivector<T>>(index, mv);
    //        }
    //    }
    //}
}
