using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.LinearMaps;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Core.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.LinearMaps.Outermorphisms;

/// <summary>
/// This class represents an outermorphism defined by a square diagonal vector mapping matrix.
/// Only the scalars on the diagonal of the vector mapping matrix are stored, all other basis
/// mappings are computed as needed.
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class RGaDiagonalOutermorphism<T> : 
    IRGaAutomorphism<T>
{
    public RGaProcessor<T> Processor 
        => DiagonalVector.Processor;

    public RGaMetric Metric 
        => DiagonalVector.Metric;

    public IScalarProcessor<T> ScalarProcessor 
        => DiagonalVector.ScalarProcessor;

    public RGaVector<T> DiagonalVector { get; }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaDiagonalOutermorphism(RGaVector<T> diagonalVector)
    {
        DiagonalVector = diagonalVector;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaOutermorphism<T> GetOmAdjoint()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> OmMapBasisVector(int index)
    {
        var id = index.BasisVectorIndexToId();

        return DiagonalVector.TryGetBasisBladeScalarValue(id, out var scalar)
            ? Processor.VectorTerm(index, scalar)
            : Processor.VectorZero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaBivector<T> OmMapBasisBivector(int index1, int index2)
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
                ScalarProcessor.Times(scalar1, scalar2)
            );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaKVector<T> OmMapBasisBlade(ulong id)
    {
        var scalar = ScalarProcessor.OneValue;

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

            scalar = ScalarProcessor.Times(scalar, s).ScalarValue;
        }
            
        return Processor.KVectorTerm(id, scalar);
    }
        

    public RGaVector<T> OmMap(RGaVector<T> vector)
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

    public RGaBivector<T> OmMap(RGaBivector<T> bivector)
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

    public RGaHigherKVector<T> OmMap(RGaHigherKVector<T> kVector)
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
    public IRGaUnilinearMap<T> GetAdjoint()
    {
        return this;
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
        return DiagonalVector
            .Ids
            .Where(id => id.VSpaceDimensions() <= vSpaceDimensions)
            .Select(id => 
                new KeyValuePair<ulong, RGaMultivector<T>>(
                    id, 
                    OmMapBasisBlade(id)
                )
            );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<ulong, RGaVector<T>>> GetOmMappedBasisVectors(int vSpaceDimensions)
    {
        return DiagonalVector
            .Ids
            .Where(id => id.VSpaceDimensions() <= vSpaceDimensions)
            .Select(id => 
                new KeyValuePair<ulong, RGaVector<T>>(
                    id, 
                    OmMapBasisVector(id.FirstOneBitPosition())
                )
            );
    }

    public LinUnilinearMap<T> GetVectorMapPart(int vSpaceDimensions)
    {
        var indexVectorDictionary =
            DiagonalVector
                .Where(p => p.Key.FirstOneBitPosition() < vSpaceDimensions)
                .ToDictionary(
                    p => p.Key.FirstOneBitPosition(),
                    p => ScalarProcessor.CreateLinVector(p.Key.FirstOneBitPosition(), p.Value)
                );

        return ScalarProcessor.CreateLinUnilinearMap(indexVectorDictionary);
    }
}