using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.LinearMaps;

public sealed class XGaDiagonalUnilinearMap<T> :
    IXGaUnilinearMap<T>,
    IReadOnlyDictionary<IndexSet, T>
{
    public XGaMultivector<T> DiagonalMultivector { get; }
        
    public XGaProcessor<T> Processor 
        => DiagonalMultivector.Processor;

    public XGaMetric Metric 
        => DiagonalMultivector.Metric;

    public IScalarProcessor<T> ScalarProcessor 
        => DiagonalMultivector.ScalarProcessor;

    public int Count 
        => DiagonalMultivector.Count;

    public IEnumerable<IndexSet> Keys 
        => DiagonalMultivector.Ids;

    public IEnumerable<T> Values 
        => DiagonalMultivector.Scalars;

    public T this[IndexSet key] 
        => DiagonalMultivector[key].ScalarValue;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaDiagonalUnilinearMap(XGaMultivector<T> diagonalMultivector)
    {
        DiagonalMultivector = diagonalMultivector;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return DiagonalMultivector.IsValid();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsKey(IndexSet key)
    {
        return DiagonalMultivector.ContainsKey(key);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(IndexSet key, out T value)
    {
        return DiagonalMultivector.TryGetValue(key, out value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaUnilinearMap<T> GetAdjoint()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> MapBasisBlade(IndexSet id)
    {
        return DiagonalMultivector.TryGetValue(id, out var scalar)
            ? Processor.KVectorTerm(id, scalar)
            : Processor.ScalarZero;
    }

    public XGaMultivector<T> Map(XGaMultivector<T> multivector)
    {
        var composer = Processor.CreateComposer();

        if (Count <= multivector.Count)
        {
            foreach (var (id, mv) in DiagonalMultivector)
            {
                if (!multivector.TryGetBasisBladeScalarValue(id, out var scalar))
                    continue;

                composer.AddTerm(id, mv, scalar);
            }
        }
        else
        {
            foreach (var (id, scalar) in multivector)
            {
                if (!DiagonalMultivector.TryGetValue(id, out var mv))
                    continue;

                composer.AddTerm(id, mv, scalar);
            }
        }

        return composer.GetSimpleMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<IndexSet, XGaMultivector<T>>> GetMappedBasisBlades(int vSpaceDimensions)
    {
        return DiagonalMultivector
            .Where(p => p.Key.VSpaceDimensions() <= vSpaceDimensions)
            .Select(p => 
                new KeyValuePair<IndexSet, XGaMultivector<T>>(
                    p.Key, 
                    Processor.KVectorTerm(p.Key, p.Value)
                )
            );
    }
        
    public T[,] GetMultivectorMapArray(int rowCount, int colCount)
    {
        var minSize = DiagonalMultivector.VSpaceDimensions;

        if (rowCount < minSize || colCount < minSize)
            throw new InvalidOperationException();

        var mapArray = 
            ScalarProcessor.CreateArrayZero2D(rowCount, colCount);
            
        foreach (var (id, scalar) in DiagonalMultivector)
        {
            var index = id.ToInt32();

            mapArray[index, index] = scalar;
        }

        return mapArray;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<KeyValuePair<IndexSet, T>> GetEnumerator()
    {
        return DiagonalMultivector.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}