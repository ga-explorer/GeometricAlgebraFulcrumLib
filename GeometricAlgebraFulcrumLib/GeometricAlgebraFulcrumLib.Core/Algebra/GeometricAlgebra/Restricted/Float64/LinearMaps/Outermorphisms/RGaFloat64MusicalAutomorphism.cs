using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps.Outermorphisms;

/// <summary>
/// See Projective Geometric Algebra as a Sub-algebra of Conformal Geometric algebra
/// https://link.springer.com/article/10.1007/s00006-021-01118-7
/// </summary>
public sealed class RGaFloat64MusicalAutomorphism :
    IRGaFloat64Outermorphism
{
    public static RGaFloat64MusicalAutomorphism Instance { get; }
        = new RGaFloat64MusicalAutomorphism();


    public RGaFloat64ConformalProcessor ConformalProcessor 
        => RGaFloat64ConformalProcessor.Instance;

    public RGaMetric Metric 
        => ConformalProcessor;

    public RGaFloat64Processor Processor 
        => ConformalProcessor;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RGaFloat64MusicalAutomorphism()
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaFloat64UnilinearMap GetAdjoint()
    {
        return GetOmAdjoint();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector MapBasisBlade(ulong id)
    {
        return OmMapBasisBlade(id);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector Map(RGaFloat64Multivector multivector)
    {
        return OmMap(multivector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<ulong, RGaFloat64Multivector>> GetMappedBasisBlades(int vSpaceDimensions)
    {
        return Processor
            .GetBasisBladeIds(vSpaceDimensions)
            .Select(id => 
                new KeyValuePair<ulong, RGaFloat64Multivector>(
                    id, 
                    OmMapBasisBlade(id)
                )
            );
    }

    public IRGaFloat64Outermorphism GetOmAdjoint()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector OmMapBasisVector(int index)
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
    public RGaFloat64Bivector OmMapBasisBivector(int index1, int index2)
    {
        var v1 = OmMapBasisVector(index1);
        var v2 = OmMapBasisVector(index2);

        return v1.Op(v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64KVector OmMapBasisBlade(ulong id)
    {
        if (id == 0UL)
            return Processor.ScalarOne;

        return id.IsBasisVector() 
            ? OmMapBasisVector(id.FirstOneBitPosition())
            : id.PatternToPositions().Select(OmMapBasisVector).Op(Processor);
    }

    public RGaFloat64Vector OmMap(RGaFloat64Vector vector)
    {
        var composer = Processor.CreateComposer();

        foreach (var (id, scalar) in vector.IdScalarPairs)
            composer.AddMultivector(
                OmMapBasisVector(id.FirstOneBitPosition()),
                scalar
            );
            
        return composer.GetVector();
    }

    public RGaFloat64Bivector OmMap(RGaFloat64Bivector bivector)
    {
        var composer = Processor.CreateComposer();

        foreach (var (id, scalar) in bivector.IdScalarPairs)
            composer.AddMultivector(
                OmMapBasisBivector(id.FirstOneBitPosition(), id.LastOneBitPosition()),
                scalar
            );
            
        return composer.GetBivector();
    }

    public RGaFloat64HigherKVector OmMap(RGaFloat64HigherKVector kVector)
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
    public RGaFloat64KVector OmMap(RGaFloat64KVector kVector)
    {
        return kVector switch
        {
            RGaFloat64Scalar s => s,
            RGaFloat64Vector v => OmMap(v),
            RGaFloat64Bivector bv => OmMap(bv),
            RGaFloat64HigherKVector kv => OmMap(kv),
            _ => throw new InvalidOperationException()
        };
    }

    public RGaFloat64Multivector OmMap(RGaFloat64Multivector multivector)
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
    public IEnumerable<KeyValuePair<ulong, RGaFloat64Vector>> GetOmMappedBasisVectors(int vSpaceDimensions)
    {
        return vSpaceDimensions
            .GetRange(index => 
                new KeyValuePair<ulong, RGaFloat64Vector>(
                    index.BasisVectorIndexToId(), 
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