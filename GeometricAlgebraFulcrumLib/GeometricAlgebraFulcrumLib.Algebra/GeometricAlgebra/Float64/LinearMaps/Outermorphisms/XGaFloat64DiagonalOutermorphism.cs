using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.LinearMaps.Outermorphisms;

/// <summary>
/// This class represents an outermorphism defined by a square diagonal vector mapping matrix.
/// Only the scalars on the diagonal of the vector mapping matrix are stored, all other basis
/// mappings are computed as needed.
/// </summary>
public sealed class XGaFloat64DiagonalOutermorphism : 
    IXGaFloat64Automorphism
{
    public XGaMetric Metric 
        => DiagonalVector.Processor;
        
    public XGaFloat64Processor Processor 
        => DiagonalVector.Processor;
        
    public XGaFloat64Vector DiagonalVector { get; }
        
    public int VSpaceDimensions 
        => DiagonalVector.VSpaceDimensions;
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaFloat64DiagonalOutermorphism(XGaFloat64Vector diagonalVector)
    {
        DiagonalVector = diagonalVector;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaFloat64Outermorphism GetOmAdjoint()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector OmMapBasisVector(int index)
    {
        var id = index.ToUnitIndexSet();

        return DiagonalVector.TryGetBasisBladeScalarValue(id, out var scalar)
            ? Processor.VectorTerm(index, scalar)
            : Processor.VectorZero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector OmMapBasisBivector(int index1, int index2)
    {
        if (index1 < 0 || index1 >= index2)
            throw new InvalidOperationException();

        var id1 = index1.ToUnitIndexSet();

        if (!DiagonalVector.TryGetBasisBladeScalarValue(id1, out var scalar1))
            return Processor.BivectorZero;

        var id2 = index2.ToUnitIndexSet();

        return !DiagonalVector.TryGetBasisBladeScalarValue(id2, out var scalar2)
            ? Processor.BivectorZero
            : Processor.BivectorTerm(
                IndexSet.CreatePair(index1, index2), 
                scalar1 * scalar2
            );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector OmMapBasisBlade(IndexSet id)
    {
        var scalar = 1d;

        if (id.IsEmptySet)
            return Processor.Scalar(scalar);

        if (id.IsUnitSet)
            return OmMapBasisVector(id.FirstIndex);

        if (id.IsPairSet)
            return OmMapBasisBivector(
                id.FirstIndex,
                id.LastIndex
            );

        foreach (var index in id)
        {
            if (!DiagonalVector.TryGetBasisBladeScalarValue(index.ToUnitIndexSet(), out var s))
                return Processor.ScalarZero;

            scalar *= s;
        }
            
        return Processor.KVectorTerm(id, scalar);
    }
        

    public XGaFloat64Vector OmMap(XGaFloat64Vector vector)
    {
        var composer = Processor.CreateVectorComposer();

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

    public XGaFloat64Bivector OmMap(XGaFloat64Bivector bivector)
    {
        var composer = Processor.CreateBivectorComposer();

        foreach (var (id, scalar) in bivector)
        {
            var bv = OmMapBasisBivector(
                id.FirstIndex,
                id.LastIndex
            );

            if (bv.IsZero)
                continue;

            composer.AddKVectorScaled(bv, scalar);
        }

        return composer.GetBivector();
    }

    public XGaFloat64HigherKVector OmMap(XGaFloat64HigherKVector kVector)
    {
        var composer = Processor.CreateKVectorComposer(kVector.Grade);

        foreach (var (id, scalar) in kVector)
        {
            var mv = OmMapBasisBlade(id);

            if (mv.IsZero)
                continue;

            composer.AddKVectorScaled(mv, scalar);
        }

        return composer.GetHigherKVector();
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
        var composer = Processor.CreateMultivectorComposer();

        foreach (var (id, scalar) in multivector)
        {
            var mv = OmMapBasisBlade(id);

            if (mv.IsZero)
                continue;

            composer.AddMultivectorScaled(mv, scalar);
        }

        return composer.GetSimpleMultivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaFloat64UnilinearMap GetAdjoint()
    {
        return this;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector MapBasisBlade(IndexSet id)
    {
        return OmMapBasisBlade(id);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector Map(XGaFloat64Multivector multivector)
    {
        return OmMap(multivector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<IndexSet, XGaFloat64Multivector>> GetMappedBasisBlades(int vSpaceDimensions)
    {
        return DiagonalVector
            .Ids
            .Where(id => id.VSpaceDimensions() <= vSpaceDimensions)
            .Select(id => 
                new KeyValuePair<IndexSet, XGaFloat64Multivector>(
                    id, 
                    OmMapBasisBlade(id)
                )
            );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<IndexSet, XGaFloat64Vector>> GetOmMappedBasisVectors(int vSpaceDimensions)
    {
        return DiagonalVector
            .Ids
            .Where(id => id.VSpaceDimensions() <= vSpaceDimensions)
            .Select(id => 
                new KeyValuePair<IndexSet, XGaFloat64Vector>(
                    id, 
                    OmMapBasisVector(id.FirstIndex)
                )
            );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64UnilinearMap GetVectorMapPart(int vSpaceDimensions)
    {
        var indexVectorDictionary =
            DiagonalVector
                .Where(p => p.Key.FirstIndex < vSpaceDimensions)
                .ToDictionary(
                    p => p.Key.FirstIndex,
                    p => p.Key.FirstIndex.CreateLinVector(p.Value)
                );

        return indexVectorDictionary.ToLinUnilinearMap();
    }
        
}