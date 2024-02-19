using System.Runtime.CompilerServices;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic.LinearMaps;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Outermorphisms;

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
        var id = index.IndexToIndexSet();

        return DiagonalVector.TryGetBasisBladeScalarValue(id, out var scalar)
            ? Processor.CreateTermVector(index, scalar)
            : Processor.CreateZeroVector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> OmMapBasisBivector(int index1, int index2)
    {
        if (index1 < 0 || index1 >= index2)
            throw new InvalidOperationException();

        var id1 = index1.IndexToIndexSet();

        if (!DiagonalVector.TryGetBasisBladeScalarValue(id1, out var scalar1))
            return Processor.CreateZeroBivector();

        var id2 = index2.IndexToIndexSet();

        return !DiagonalVector.TryGetBasisBladeScalarValue(id2, out var scalar2)
            ? Processor.CreateZeroBivector()
            : Processor.CreateTermBivector(
                IndexSetUtils.IndexPairToIndexSet(index1, index2), 
                ScalarProcessor.Times(scalar1, scalar2)
            );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> OmMapBasisBlade(IIndexSet id)
    {
        var scalar = ScalarProcessor.ScalarOne;

        if (id.IsEmptySet)
            return Processor.CreateScalar(scalar);

        if (id.IsSingleIndexSet)
            return OmMapBasisVector(id.FirstIndex);

        if (id.IsIndexPairSet)
            return OmMapBasisBivector(
                id.FirstIndex,
                id.LastIndex
            );

        foreach (var index in id)
        {
            if (!DiagonalVector.TryGetBasisBladeScalarValue(index.IndexToIndexSet(), out var s))
                return Processor.CreateZeroScalar();

            scalar = ScalarProcessor.Times(scalar, s);
        }
            
        return Processor.CreateTermKVector(id, scalar);
    }
        

    public XGaVector<T> OmMap(XGaVector<T> vector)
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

    public XGaBivector<T> OmMap(XGaBivector<T> bivector)
    {
        var composer = Processor.CreateComposer();

        foreach (var (id, scalar) in bivector)
        {
            var bv = OmMapBasisBivector(
                id.FirstIndex,
                id.LastIndex
            );

            if (bv.IsZero)
                continue;

            composer.AddMultivector(bv, scalar);
        }

        return composer.GetBivector();
    }

    public XGaHigherKVector<T> OmMap(XGaHigherKVector<T> kVector)
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
    public IXGaUnilinearMap<T> GetAdjoint()
    {
        return this;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> MapBasisBlade(IIndexSet id)
    {
        return OmMapBasisBlade(id);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> Map(XGaMultivector<T> multivector)
    {
        return OmMap(multivector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<IIndexSet, XGaMultivector<T>>> GetMappedBasisBlades(int vSpaceDimensions)
    {
        return DiagonalVector
            .Ids
            .Where(id => id.VSpaceDimensions() <= vSpaceDimensions)
            .Select(id => 
                new KeyValuePair<IIndexSet, XGaMultivector<T>>(
                    id, 
                    OmMapBasisBlade(id)
                )
            );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<IIndexSet, XGaVector<T>>> GetOmMappedBasisVectors(int vSpaceDimensions)
    {
        return DiagonalVector
            .Ids
            .Where(id => id.VSpaceDimensions() <= vSpaceDimensions)
            .Select(id => 
                new KeyValuePair<IIndexSet, XGaVector<T>>(
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