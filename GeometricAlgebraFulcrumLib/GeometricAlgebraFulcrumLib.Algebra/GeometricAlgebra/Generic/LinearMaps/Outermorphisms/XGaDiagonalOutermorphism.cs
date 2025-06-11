using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.LinearMaps;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Outermorphisms;

/// <summary>
/// This class represents an outermorphism defined by a square diagonal vector mapping matrix.
/// Only the scalars on the diagonal of the vector mapping matrix are stored, all other basis
/// mappings are computed as needed.
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class XGaDiagonalOutermorphism<T> : 
    IXGaAutomorphism<T>
{
    public XGaProcessor<T> Processor 
        => DiagonalVector.Processor;

    public XGaMetric Metric 
        => DiagonalVector.Metric;

    public IScalarProcessor<T> ScalarProcessor 
        => DiagonalVector.ScalarProcessor;

    public XGaVector<T> DiagonalVector { get; }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaDiagonalOutermorphism(XGaVector<T> diagonalVector)
    {
        DiagonalVector = diagonalVector;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaOutermorphism<T> GetOmAdjoint()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> OmMapBasisVector(int index)
    {
        var id = index.ToUnitIndexSet();

        return DiagonalVector.TryGetBasisBladeScalarValue(id, out var scalar)
            ? Processor.VectorTerm(index, scalar)
            : Processor.VectorZero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> OmMapBasisBivector(int index1, int index2)
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
                ScalarProcessor.Times(scalar1, scalar2)
            );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> OmMapBasisBlade(IndexSet id)
    {
        var scalar = ScalarProcessor.OneValue;

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

            scalar = ScalarProcessor.Times(scalar, s).ScalarValue;
        }
            
        return Processor.KVectorTerm(id, scalar);
    }
        

    public XGaVector<T> OmMap(XGaVector<T> vector)
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

    public XGaBivector<T> OmMap(XGaBivector<T> bivector)
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

    public XGaHigherKVector<T> OmMap(XGaHigherKVector<T> kVector)
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
        var composer = Processor.CreateMultivectorComposer();

        foreach (var (id, scalar) in multivector)
        {
            var mv = OmMapBasisBlade(id);

            if (mv.IsZero)
                continue;

            composer.AddKVectorScaled(mv, scalar);
        }

        return composer.GetSimpleMultivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return true;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaUnilinearMap<T> GetAdjoint()
    {
        return this;
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
        return DiagonalVector
            .Ids
            .Where(id => id.VSpaceDimensions() <= vSpaceDimensions)
            .Select(id => 
                new KeyValuePair<IndexSet, XGaMultivector<T>>(
                    id, 
                    OmMapBasisBlade(id)
                )
            );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<IndexSet, XGaVector<T>>> GetOmMappedBasisVectors(int vSpaceDimensions)
    {
        return DiagonalVector
            .Ids
            .Where(id => id.VSpaceDimensions() <= vSpaceDimensions)
            .Select(id => 
                new KeyValuePair<IndexSet, XGaVector<T>>(
                    id, 
                    OmMapBasisVector(id.FirstIndex)
                )
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMap<T> GetVectorMapPart(int vSpaceDimensions)
    {
        var indexVectorDictionary =
            DiagonalVector
                .Where(p => p.Key.FirstIndex < vSpaceDimensions)
                .ToDictionary(
                    p => p.Key.FirstIndex,
                    p => ScalarProcessor.CreateLinVector(p.Key.FirstIndex, p.Value)
                );

        return ScalarProcessor.CreateLinUnilinearMap(indexVectorDictionary);
    }
}