using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.LinearMaps;

public sealed class XGaFloat64DiagonalUnilinearMap :
    IXGaFloat64UnilinearMap,
    IReadOnlyDictionary<IndexSet, double>
{
    public XGaFloat64Multivector DiagonalMultivector { get; }
        
    public XGaFloat64Processor Processor 
        => DiagonalMultivector.Processor;
        
    public XGaMetric Metric 
        => DiagonalMultivector.Metric;

    public int Count 
        => DiagonalMultivector.Count;

    public IEnumerable<IndexSet> Keys 
        => DiagonalMultivector.Ids;

    public IEnumerable<double> Values 
        => DiagonalMultivector.Scalars;

    public double this[IndexSet key] 
        => DiagonalMultivector[key];


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaFloat64DiagonalUnilinearMap(XGaFloat64Multivector diagonalMultivector)
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
    public bool TryGetValue(IndexSet key, out double value)
    {
        return DiagonalMultivector.TryGetValue(key, out value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaFloat64UnilinearMap GetAdjoint()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector MapBasisBlade(IndexSet id)
    {
        return DiagonalMultivector.TryGetValue(id, out var scalar)
            ? Processor.KVectorTerm(id, scalar)
            : Processor.ScalarZero;
    }

    public XGaFloat64Multivector Map(XGaFloat64Multivector multivector)
    {
        var composer = Processor.CreateMultivectorComposer();

        if (Count <= multivector.Count)
        {
            foreach (var (id, mv) in DiagonalMultivector)
            {
                if (!multivector.TryGetBasisBladeScalarValue(id, out var scalar))
                    continue;

                composer.AddTerm(id, mv * scalar);
            }
        }
        else
        {
            foreach (var (id, scalar) in multivector)
            {
                if (!DiagonalMultivector.TryGetValue(id, out var mv))
                    continue;

                composer.AddTerm(id, mv * scalar);
            }
        }

        return composer.GetMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<IndexSet, XGaFloat64Multivector>> GetMappedBasisBlades(int vSpaceDimensions)
    {
        return DiagonalMultivector
            .Where(p => p.Key.VSpaceDimensions() <= vSpaceDimensions)
            .Select(p => 
                new KeyValuePair<IndexSet, XGaFloat64Multivector>(
                    p.Key, 
                    Processor.KVectorTerm(p.Key, p.Value)
                )
            );
    }
        
    public double[,] GetMultivectorMapArray(int rowCount, int colCount)
    {
        var minSize = DiagonalMultivector.VSpaceDimensions;

        if (rowCount < minSize || colCount < minSize)
            throw new InvalidOperationException();
            
        var mapArray = new double[rowCount, colCount];
            
        foreach (var (id, scalar) in DiagonalMultivector)
        {
            var index = id.ToInt32();

            mapArray[index, index] = scalar;
        }

        return mapArray;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<KeyValuePair<IndexSet, double>> GetEnumerator()
    {
        return DiagonalMultivector.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}