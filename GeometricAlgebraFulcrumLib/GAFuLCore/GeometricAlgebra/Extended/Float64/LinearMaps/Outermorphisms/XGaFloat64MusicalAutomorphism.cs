using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.LinearMaps.Outermorphisms;

/// <summary>
/// See Projective Geometric Algebra as a Sub-algebra of Conformal Geometric algebra
/// https://link.springer.com/article/10.1007/s00006-021-01118-7
/// </summary>
public sealed class XGaFloat64MusicalAutomorphism :
    IXGaFloat64Outermorphism
{
    public static XGaFloat64MusicalAutomorphism Instance { get; }
        = new XGaFloat64MusicalAutomorphism();


    public XGaFloat64ConformalProcessor ConformalProcessor 
        => XGaFloat64ConformalProcessor.Instance;

    public XGaMetric Metric 
        => ConformalProcessor;

    public XGaFloat64Processor Processor 
        => ConformalProcessor;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private XGaFloat64MusicalAutomorphism()
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaFloat64UnilinearMap GetAdjoint()
    {
        return GetOmAdjoint();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector MapBasisBlade(IIndexSet id)
    {
        return OmMapBasisBlade(id);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector Map(XGaFloat64Multivector multivector)
    {
        return OmMap(multivector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<IIndexSet, XGaFloat64Multivector>> GetMappedBasisBlades(int vSpaceDimensions)
    {
        return Processor
            .GetBasisBladeIds(vSpaceDimensions)
            .Select(id => 
                new KeyValuePair<IIndexSet, XGaFloat64Multivector>(
                    id, 
                    OmMapBasisBlade(id)
                )
            );
    }

    public IXGaFloat64Outermorphism GetOmAdjoint()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector OmMapBasisVector(int index)
    {
        return index switch
        {
            < 0 => throw new ArgumentOutOfRangeException(nameof(index)),
            0 => ConformalProcessor.Vector(-1.25, 0.75),
            1 => ConformalProcessor.Vector(-0.75, 1.25),
            _ => ConformalProcessor.VectorTerm(index)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector OmMapBasisBivector(int index1, int index2)
    {
        var v1 = OmMapBasisVector(index1);
        var v2 = OmMapBasisVector(index2);

        return v1.Op(v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector OmMapBasisBlade(IIndexSet id)
    {
        if (id.IsEmptySet)
            return Processor.ScalarOne;

        return id.IsBasisVector() 
            ? OmMapBasisVector(id.FirstIndex)
            : id.Select(OmMapBasisVector).Op(Processor);
    }

    public XGaFloat64Vector OmMap(XGaFloat64Vector vector)
    {
        var composer = Processor.CreateComposer();

        foreach (var (id, scalar) in vector.IdScalarPairs)
            composer.AddMultivector(
                OmMapBasisVector(id.FirstIndex),
                scalar
            );
            
        return composer.GetVector();
    }

    public XGaFloat64Bivector OmMap(XGaFloat64Bivector bivector)
    {
        var composer = Processor.CreateComposer();

        foreach (var (id, scalar) in bivector.IdScalarPairs)
            composer.AddMultivector(
                OmMapBasisBivector(id.FirstIndex, id.LastIndex),
                scalar
            );
            
        return composer.GetBivector();
    }

    public XGaFloat64HigherKVector OmMap(XGaFloat64HigherKVector kVector)
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
    public XGaFloat64KVector OmMap(XGaFloat64KVector kVector)
    {
        return kVector switch
        {
            XGaFloat64Scalar s => s,
            XGaFloat64Vector v => OmMap(v),
            XGaFloat64Bivector bv => OmMap(bv),
            XGaFloat64HigherKVector kv => OmMap(kv),
            _ => throw new InvalidOperationException()
        };
    }

    public XGaFloat64Multivector OmMap(XGaFloat64Multivector multivector)
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
    public IEnumerable<KeyValuePair<IIndexSet, XGaFloat64Vector>> GetOmMappedBasisVectors(int vSpaceDimensions)
    {
        return vSpaceDimensions
            .GetRange(index => 
                new KeyValuePair<IIndexSet, XGaFloat64Vector>(
                    index.IndexToSingleIndexSet(), 
                    OmMapBasisVector(index)
                )
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64UnilinearMap GetVectorMapPart(int vSpaceDimensions)
    {
        return vSpaceDimensions.RangeToDictionary(
            index => OmMapBasisVector(index).ToLinVector()
        ).ToLinUnilinearMap();
    }
}