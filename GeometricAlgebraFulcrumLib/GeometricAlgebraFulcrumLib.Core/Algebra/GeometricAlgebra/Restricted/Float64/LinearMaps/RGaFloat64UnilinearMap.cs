using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps;

public class RGaFloat64UnilinearMap :
    IRGaFloat64UnilinearMap,
    IReadOnlyDictionary<ulong, RGaFloat64Multivector>
{
    private readonly IReadOnlyDictionary<ulong, RGaFloat64Multivector> _basisMapDictionary;

    public RGaFloat64Processor Processor { get; }

    public RGaMetric Metric 
        => Processor;
        
    public int Count 
        => _basisMapDictionary.Count;
    
    public IEnumerable<ulong> Keys 
        => _basisMapDictionary.Keys;

    public IEnumerable<RGaFloat64Multivector> Values 
        => _basisMapDictionary.Values;
    
    public RGaFloat64Multivector this[ulong key] 
        => _basisMapDictionary.TryGetValue(key, out var mv)
            ? mv : Processor.ScalarZero;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaFloat64UnilinearMap(RGaFloat64Processor metric, IReadOnlyDictionary<ulong, RGaFloat64Multivector> idMultivectorDictionary)
    {
        Processor = metric;
        _basisMapDictionary = idMultivectorDictionary;

        Debug.Assert(
            IsValid()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return _basisMapDictionary.Values.All(
            d => d.IsValid()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsKey(ulong key)
    {
        return _basisMapDictionary.ContainsKey(key);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(ulong key, out RGaFloat64Multivector value)
    {
        return _basisMapDictionary.TryGetValue(key, out value);
    }
    
    public IRGaFloat64UnilinearMap GetAdjoint()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector MapBasisBlade(ulong id)
    {
        return _basisMapDictionary.TryGetValue(id, out var mv)
            ? mv
            : Processor.ScalarZero;
    }

    public RGaFloat64Multivector Map(RGaFloat64Multivector multivector)
    {
        var composer = Processor.CreateComposer();

        if (Count <= multivector.Count)
        {
            foreach (var (id, mv) in _basisMapDictionary)
            {
                if (!multivector.TryGetBasisBladeScalarValue(id, out var scalar))
                    continue;

                composer.AddMultivector(mv, scalar);
            }
        }
        else
        {
            foreach (var (id, scalar) in multivector)
            {
                if (!_basisMapDictionary.TryGetValue(id, out var mv))
                    continue;

                composer.AddMultivector(mv, scalar);
            }
        }

        return composer.GetSimpleMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<ulong, RGaFloat64Multivector>> GetMappedBasisBlades(int vSpaceDimensions)
    {
        return _basisMapDictionary
            .Where(p => p.Key.VSpaceDimensions() <= vSpaceDimensions)
            .Select(p => 
                new KeyValuePair<ulong, RGaFloat64Multivector>(p.Key, p.Value)
            );
    }
        
    public double[,] GetMultivectorMapArray(int rowCount, int colCount)
    {
        var mapArray = 
            new double[rowCount, colCount];

        if (_basisMapDictionary.Count == 0)
            return mapArray;

        var minRowCount = 
            _basisMapDictionary.Values.Max(v => v.VSpaceDimensions);

        if (rowCount < minRowCount)
            throw new InvalidOperationException();

        var minColCount = _basisMapDictionary.Keys.Max();

        if ((ulong) colCount < minColCount)
            throw new InvalidOperationException();

        foreach (var (colIndex, vector) in _basisMapDictionary)
        foreach (var (rowIndex, scalar) in vector)
            mapArray[rowIndex, colIndex] = scalar;

        return mapArray;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<KeyValuePair<ulong, RGaFloat64Multivector>> GetEnumerator()
    {
        return _basisMapDictionary.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}