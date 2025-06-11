using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps;

public class XGaUnilinearMap<T> :
    IXGaUnilinearMap<T>,
    IReadOnlyDictionary<IndexSet, XGaMultivector<T>>
{
    private readonly IReadOnlyDictionary<IndexSet, XGaMultivector<T>> _idMultivectorDictionary;

    public XGaProcessor<T> Processor { get; }

    public XGaMetric Metric 
        => Processor;

    public IScalarProcessor<T> ScalarProcessor 
        => Processor.ScalarProcessor;

    public int Count 
        => _idMultivectorDictionary.Count;
    
    public IEnumerable<IndexSet> Keys 
        => _idMultivectorDictionary.Keys;

    public IEnumerable<XGaMultivector<T>> Values 
        => _idMultivectorDictionary.Values;
    
    public XGaMultivector<T> this[IndexSet key] 
        => _idMultivectorDictionary.TryGetValue(key, out var mv)
            ? mv : Processor.ScalarZero;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaUnilinearMap(XGaProcessor<T> processor, IReadOnlyDictionary<IndexSet, XGaMultivector<T>> idMultivectorDictionary)
    {
        Processor = processor;
        _idMultivectorDictionary = idMultivectorDictionary;

        Debug.Assert(
            IsValid()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return _idMultivectorDictionary.Values.All(
            d => d.IsValid()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsKey(IndexSet key)
    {
        return _idMultivectorDictionary.ContainsKey(key);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(IndexSet key, out XGaMultivector<T> value)
    {
        return _idMultivectorDictionary.TryGetValue(key, out value);
    }
    
    public IXGaUnilinearMap<T> GetAdjoint()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> MapBasisBlade(IndexSet id)
    {
        return _idMultivectorDictionary.TryGetValue(id, out var mv)
            ? mv
            : Processor.ScalarZero;
    }

    public XGaMultivector<T> Map(XGaMultivector<T> multivector)
    {
        var composer = Processor.CreateMultivectorComposer();

        if (Count <= multivector.Count)
        {
            foreach (var (id, mv) in _idMultivectorDictionary)
            {
                if (!multivector.TryGetBasisBladeScalarValue(id, out var scalar))
                    continue;

                composer.AddMultivectorScaled(mv, scalar);
            }
        }
        else
        {
            foreach (var (id, scalar) in multivector)
            {
                if (!_idMultivectorDictionary.TryGetValue(id, out var mv))
                    continue;

                composer.AddMultivectorScaled(mv, scalar);
            }
        }

        return composer.GetSimpleMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<IndexSet, XGaMultivector<T>>> GetMappedBasisBlades(int vSpaceDimensions)
    {
        return _idMultivectorDictionary
            .Where(p => p.Key.VSpaceDimensions() <= vSpaceDimensions)
            .Select(p => 
                new KeyValuePair<IndexSet, XGaMultivector<T>>(p.Key, p.Value)
            );
    }
        
    public T[,] GetMultivectorMapArray(int rowCount, int colCount)
    {
        var mapArray = 
            ScalarProcessor.CreateArrayZero2D(rowCount, colCount);

        if (_idMultivectorDictionary.Count == 0)
            return mapArray;

        var minRowCount = 
            _idMultivectorDictionary.Values.Max(v => v.VSpaceDimensions);

        if (rowCount < minRowCount)
            throw new InvalidOperationException();

        var maxId = _idMultivectorDictionary.Keys.Max();

        var minColCount = maxId.ToUInt64();

        if ((ulong) colCount < minColCount)
            throw new InvalidOperationException();

        foreach (var (colId, vector) in _idMultivectorDictionary)
        {
            var colIndex = colId.ToInt32();

            foreach (var (rowId, scalar) in vector)
            {
                var rowIndex = rowId.ToInt32();

                mapArray[rowIndex, colIndex] = scalar;
            }
        }

        return mapArray;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<KeyValuePair<IndexSet, XGaMultivector<T>>> GetEnumerator()
    {
        return _idMultivectorDictionary.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}