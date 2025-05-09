using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.LinearMaps;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Core.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.LinearMaps.Outermorphisms;

/// <summary>
/// See Projective Geometric Algebra as a Sub-algebra of Conformal Geometric algebra
/// https://link.springer.com/article/10.1007/s00006-021-01118-7
/// </summary>
public sealed class XGaMusicalAutomorphism<T> :
    IXGaOutermorphism<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMusicalAutomorphism<T> Create(XGaConformalProcessor<T> conformalProcessor)
    {
        return new XGaMusicalAutomorphism<T>(conformalProcessor);
    }


    public XGaConformalProcessor<T> ConformalProcessor { get; }

    public XGaProcessor<T> Processor 
        => ConformalProcessor;

    public XGaMetric Metric 
        => ConformalProcessor;

    public IScalarProcessor<T> ScalarProcessor 
        => ConformalProcessor.ScalarProcessor;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private XGaMusicalAutomorphism(XGaConformalProcessor<T> conformalProcessor)
    {
        ConformalProcessor = conformalProcessor;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaUnilinearMap<T> GetAdjoint()
    {
        return GetOmAdjoint();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> MapBasisBlade(IndexSet id)
    {
        return OmMapBasisBlade(id);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> Map(XGaMultivector<T> multivector)
    {
        return OmMap(multivector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<IndexSet, XGaMultivector<T>>> GetMappedBasisBlades(int vSpaceDimensions)
    {
        return Processor
            .GetBasisBladeIds(vSpaceDimensions)
            .Select(id => 
                new KeyValuePair<IndexSet, XGaMultivector<T>>(
                    id, 
                    OmMapBasisBlade(id)
                )
            );
    }

    public IXGaOutermorphism<T> GetOmAdjoint()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> OmMapBasisVector(int index)
    {
        return index switch
        {
            < 0 => throw new ArgumentOutOfRangeException(nameof(index)),

            0 => ConformalProcessor.Vector(
                ScalarProcessor.Divide(-5, 4),
                ScalarProcessor.Divide(3, 4)
            ), // (-1.25, 0.75),

            1 => ConformalProcessor.Vector(
                ScalarProcessor.Divide(-3, 4),
                ScalarProcessor.Divide(5, 4)
            ), // (-0.75, 1.25),

            _ => ConformalProcessor.VectorTerm(index)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> OmMapBasisBivector(int index1, int index2)
    {
        var v1 = OmMapBasisVector(index1);
        var v2 = OmMapBasisVector(index2);

        return v1.Op(v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> OmMapBasisBlade(IndexSet id)
    {
        if (id.IsEmptySet)
            return Processor.ScalarOne;

        return id.IsBasisVector() 
            ? OmMapBasisVector(id.FirstIndex)
            : id.Select(OmMapBasisVector).Op(Processor);
    }

    public XGaVector<T> OmMap(XGaVector<T> vector)
    {
        var composer = Processor.CreateComposer();

        foreach (var (id, scalar) in vector.IdScalarPairs)
            composer.AddMultivector(
                OmMapBasisVector(id.FirstIndex),
                scalar
            );
            
        return composer.GetVector();
    }

    public XGaBivector<T> OmMap(XGaBivector<T> bivector)
    {
        var composer = Processor.CreateComposer();

        foreach (var (id, scalar) in bivector.IdScalarPairs)
            composer.AddMultivector(
                OmMapBasisBivector(id.FirstIndex, id.LastIndex),
                scalar
            );
            
        return composer.GetBivector();
    }

    public XGaHigherKVector<T> OmMap(XGaHigherKVector<T> kVector)
    {
        var composer = Processor.CreateComposer();

        foreach (var (id, scalar) in kVector.IdScalarPairs)
            composer.AddMultivector(
                OmMapBasisBlade(id),
                scalar
            );
            
        return composer.GetHigherKVector(kVector.Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> OmMap(XGaKVector<T> kVector)
    {
        return kVector switch
        {
            XGaScalar<T> s => s,
            XGaVector<T> v => OmMap(v),
            XGaBivector<T> bv => OmMap(bv),
            XGaHigherKVector<T> kv => OmMap(kv),
            _ => throw new InvalidOperationException()
        };
    }

    public XGaMultivector<T> OmMap(XGaMultivector<T> multivector)
    {
        var composer = Processor.CreateComposer();

        foreach (var (id, scalar) in multivector.IdScalarPairs)
            composer.AddMultivector(
                OmMapBasisBlade(id),
                scalar
            );
            
        return composer.GetMultivector();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<IndexSet, XGaVector<T>>> GetOmMappedBasisVectors(int vSpaceDimensions)
    {
        return vSpaceDimensions
            .GetRange(index => 
                new KeyValuePair<IndexSet, XGaVector<T>>(
                    IndexSetUtils.IndexToIndexSet(index), 
                    OmMapBasisVector(index)
                )
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMap<T> GetVectorMapPart(int vSpaceDimensions)
    {
        return vSpaceDimensions.RangeToDictionary(
            index => OmMapBasisVector(index).ToLinVector()
        ).ToLinUnilinearMap(ScalarProcessor);
    }
}