namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms
{
    //public abstract class GeoNumMapUnilinear<TItem, TArray>
    //{
    //    protected TArray InternalMappingMatrix;


    //    public IGeoImmutableScalarsArraysDomain<TItem, TArray> ArraysDomain { get; }

    //    public IGeoImmutableScalarsDomain<TItem> ScalarProcessor 
    //        => ArraysDomain.ScalarProcessor;

    //    public Multivector<TItem> this[int grade1, ulong index1]
    //        => this[GeoFrameUtils.BasisBladeId(grade1, index1)];

    //    public abstract Multivector<TItem> this[ulong id1] { get; }

    //    public abstract Multivector<TItem> this[Multivector<TItem> mv1] { get; }

    //    public TArray MappingMatrix
    //    {
    //        get
    //        {
    //            if (ReferenceEquals(InternalMappingMatrix, null))
    //                GetMappingMatrix();

    //            return InternalMappingMatrix;
    //        }
    //    }


    //    public virtual Multivector<TItem> MapPseudoScalar(uint vSpaceDimension)
    //    {
    //        return this[(1UL << (int) vSpaceDimension) - 1];
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


    //    public virtual GeoNumMapUnilinear<TItem, TArray> Adjoint()
    //    {
    //        var exprArray = this.ToScalarsArray();

    //        var resultMap = GeoNumMapUnilinearTree.Create(
    //            TargetVSpaceDimension,
    //            DomainVSpaceDimension
    //        );

    //        for (var id = 0UL; id < TargetGaSpaceDimension; id++)
    //        {
    //            var mv = GeoNumSarMultivector.CreateFromRow(exprArray, id);

    //            if (!mv.IsNullOrEmpty())
    //                resultMap.SetBasisBladeMap(id, mv);
    //        }

    //        return resultMap;
    //    }

    //    public virtual GeoNumMapUnilinear<TItem, TArray> Inverse()
    //    {
    //        return (this.ToDenseMatrix().Inverse() as T[,]).ToTreeMap();
    //    }

    //    public virtual GeoNumMapUnilinear<TItem, TArray> InverseAdjoint()
    //    {
    //        return (this.ToDenseMatrix().Inverse().Transpose() as T[,]).ToTreeMap();
    //    }


    //    public abstract IEnumerable<Tuple<ulong, Multivector<TItem>>> BasisBladeMaps();

    //    public virtual IEnumerable<Tuple<ulong, Multivector<TItem>>> BasisVectorMaps()
    //    {
    //        for (var index = 0UL; index < (ulong)DomainVSpaceDimension; index++)
    //        {
    //            var mv = this[GeoFrameUtils.BasisBladeId(1, index)];

    //            if (!mv.IsNullOrEmpty())
    //                yield return new Tuple<ulong, Multivector<T>>(index, mv);
    //        }
    //    }
    //}
}
