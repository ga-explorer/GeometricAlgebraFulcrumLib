using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.LinearMaps;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.LinearMaps.Outermorphisms;

/// <summary>
/// See Projective Geometric Algebra as a Sub-algebra of Conformal Geometric algebra
/// https://link.springer.com/article/10.1007/s00006-021-01118-7
/// </summary>
public sealed class RGaMusicalAutomorphism<T> :
    IRGaOutermorphism<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMusicalAutomorphism<T> Create(RGaConformalProcessor<T> conformalProcessor)
    {
        return new RGaMusicalAutomorphism<T>(conformalProcessor);
    }


    public RGaConformalProcessor<T> ConformalProcessor { get; }

    public RGaProcessor<T> Processor 
        => ConformalProcessor;

    public RGaMetric Metric 
        => ConformalProcessor;

    public IScalarProcessor<T> ScalarProcessor 
        => ConformalProcessor.ScalarProcessor;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RGaMusicalAutomorphism(RGaConformalProcessor<T> conformalProcessor)
    {
        ConformalProcessor = conformalProcessor;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaUnilinearMap<T> GetAdjoint()
    {
        return GetOmAdjoint();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T> MapBasisBlade(ulong id)
    {
        return OmMapBasisBlade(id);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T> Map(RGaMultivector<T> multivector)
    {
        return OmMap(multivector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<ulong, RGaMultivector<T>>> GetMappedBasisBlades(int vSpaceDimensions)
    {
        return Processor
            .GetBasisBladeIds(vSpaceDimensions)
            .Select(id => 
                new KeyValuePair<ulong, RGaMultivector<T>>(
                    id, 
                    OmMapBasisBlade(id)
                )
            );
    }

    public IRGaOutermorphism<T> GetOmAdjoint()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> OmMapBasisVector(int index)
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
    public RGaBivector<T> OmMapBasisBivector(int index1, int index2)
    {
        var v1 = OmMapBasisVector(index1);
        var v2 = OmMapBasisVector(index2);

        return v1.Op(v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaKVector<T> OmMapBasisBlade(ulong id)
    {
        if (id == 0UL)
            return Processor.ScalarOne;

        return id.IsBasisVector() 
            ? OmMapBasisVector(id.FirstOneBitPosition())
            : id.PatternToPositions().Select(OmMapBasisVector).Op(Processor);
    }

    public RGaVector<T> OmMap(RGaVector<T> vector)
    {
        var composer = Processor.CreateComposer();

        foreach (var (id, scalar) in vector.IdScalarPairs)
            composer.AddMultivector(
                OmMapBasisVector(id.FirstOneBitPosition()),
                scalar
            );
            
        return composer.GetVector();
    }

    public RGaBivector<T> OmMap(RGaBivector<T> bivector)
    {
        var composer = Processor.CreateComposer();

        foreach (var (id, scalar) in bivector.IdScalarPairs)
            composer.AddMultivector(
                OmMapBasisBivector(id.FirstOneBitPosition(), id.LastOneBitPosition()),
                scalar
            );
            
        return composer.GetBivector();
    }

    public RGaHigherKVector<T> OmMap(RGaHigherKVector<T> kVector)
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
    public RGaKVector<T> OmMap(RGaKVector<T> kVector)
    {
        return kVector switch
        {
            RGaScalar<T> s => s,
            RGaVector<T> v => OmMap(v),
            RGaBivector<T> bv => OmMap(bv),
            RGaHigherKVector<T> kv => OmMap(kv),
            _ => throw new InvalidOperationException()
        };
    }

    public RGaMultivector<T> OmMap(RGaMultivector<T> multivector)
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
    public IEnumerable<KeyValuePair<ulong, RGaVector<T>>> GetOmMappedBasisVectors(int vSpaceDimensions)
    {
        return vSpaceDimensions
            .GetRange(index => 
                new KeyValuePair<ulong, RGaVector<T>>(
                    index.BasisVectorIndexToId(), 
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