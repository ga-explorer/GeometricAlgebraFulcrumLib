using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps.Outermorphisms;

/// <summary>
/// This class represents an outermorphism defined by a square diagonal vector mapping matrix.
/// Only the scalars on the diagonal of the vector mapping matrix are stored, all other basis
/// mappings are computed as needed.
/// </summary>
public sealed class RGaFloat64DiagonalOutermorphism : 
    IRGaFloat64Automorphism
{
    public RGaFloat64Processor Processor 
        => DiagonalVector.Processor;
        
    public RGaMetric Metric 
        => DiagonalVector.Metric;
        
    public RGaFloat64Vector DiagonalVector { get; }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaFloat64DiagonalOutermorphism(RGaFloat64Vector diagonalVector)
    {
        DiagonalVector = diagonalVector;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaFloat64Outermorphism GetOmAdjoint()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector OmMapBasisVector(int index)
    {
        var id = index.BasisVectorIndexToId();

        return DiagonalVector.TryGetBasisBladeScalarValue(id, out var scalar)
            ? Processor.VectorTerm(index, scalar)
            : Processor.VectorZero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector OmMapBasisBivector(int index1, int index2)
    {
        if (index1 < 0 || index1 >= index2)
            throw new InvalidOperationException();

        var id1 = index1.BasisVectorIndexToId();

        if (!DiagonalVector.TryGetBasisBladeScalarValue(id1, out var scalar1))
            return Processor.BivectorZero;

        var id2 = index2.BasisVectorIndexToId();

        return !DiagonalVector.TryGetBasisBladeScalarValue(id2, out var scalar2)
            ? Processor.BivectorZero
            : Processor.BivectorTerm(
                BasisBivectorUtils.IndexPairToBivectorId(index1, index2), 
                scalar1 * scalar2
            );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64KVector OmMapBasisBlade(ulong id)
    {
        var scalar = 1d;

        if (id == 0UL)
            return Processor.Scalar(scalar);

        if (id.IsBasisVector())
            return OmMapBasisVector(id.FirstOneBitPosition());

        if (id.IsBasisBivector())
            return OmMapBasisBivector(
                id.FirstOneBitPosition(),
                id.LastOneBitPosition()
            );

        foreach (var index in id.GetSetBitPositions())
        {
            if (!DiagonalVector.TryGetBasisBladeScalarValue(index.BasisVectorIndexToId(), out var s))
                return Processor.ScalarZero;

            scalar *= s;
        }
        
        return Processor.KVectorTerm(id, scalar);
    }
        

    public RGaFloat64Vector OmMap(RGaFloat64Vector vector)
    {
        var composer = Processor.CreateComposer();

        if (DiagonalVector.Count <= vector.Count)
        {
            foreach (var (id, mv) in DiagonalVector)
            {
                if (!vector.TryGetBasisBladeScalarValue(id, out var scalar))
                    continue;

                composer.AddTerm(id, mv, scalar);
            }
        }
        else
        {
            foreach (var (id, scalar) in vector)
            {
                if (!DiagonalVector.TryGetValue(id, out var mv))
                    continue;

                composer.AddTerm(id, mv, scalar);
            }
        }

        return composer.GetVector();
    }

    public RGaFloat64Bivector OmMap(RGaFloat64Bivector bivector)
    {
        var composer = Processor.CreateComposer();

        foreach (var (id, scalar) in bivector)
        {
            var bv = OmMapBasisBivector(
                id.FirstOneBitPosition(),
                id.LastOneBitPosition()
            );

            if (bv.IsZero)
                continue;

            composer.AddMultivector(bv, scalar);
        }

        return composer.GetBivector();
    }

    public RGaFloat64HigherKVector OmMap(RGaFloat64HigherKVector kVector)
    {
        var composer = Processor.CreateComposer();

        foreach (var (id, scalar) in kVector)
        {
            var mv = OmMapBasisBlade(id);

            if (mv.IsZero)
                continue;

            composer.AddMultivector(mv, scalar);
        }

        return composer.GetHigherKVector(kVector.Grade);
    }

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

        foreach (var (id, scalar) in multivector)
        {
            var mv = OmMapBasisBlade(id);

            if (mv.IsZero)
                continue;

            composer.AddMultivector(mv, scalar);
        }

        return composer.GetSimpleMultivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return true;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaFloat64UnilinearMap GetAdjoint()
    {
        return this;
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
        return DiagonalVector
            .Ids
            .Where(id => id.VSpaceDimensions() <= vSpaceDimensions)
            .Select(id => 
                new KeyValuePair<ulong, RGaFloat64Multivector>(
                    id, 
                    OmMapBasisBlade(id)
                )
            );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<ulong, RGaFloat64Vector>> GetOmMappedBasisVectors(int vSpaceDimensions)
    {
        return DiagonalVector
            .Ids
            .Where(id => id.VSpaceDimensions() <= vSpaceDimensions)
            .Select(id => 
                new KeyValuePair<ulong, RGaFloat64Vector>(
                    id, 
                    OmMapBasisVector(id.FirstOneBitPosition())
                )
            );
    }

    public LinFloat64UnilinearMap GetVectorMapPart(int vSpaceDimensions)
    {
        var indexVectorDictionary =
            DiagonalVector
                .Where(p => p.Key.FirstOneBitPosition() < vSpaceDimensions)
                .ToDictionary(
                    p => p.Key.FirstOneBitPosition(),
                    p => p.Key.FirstOneBitPosition().CreateLinVector(p.Value)
                );

        return indexVectorDictionary.ToLinUnilinearMap();
    }
        
}