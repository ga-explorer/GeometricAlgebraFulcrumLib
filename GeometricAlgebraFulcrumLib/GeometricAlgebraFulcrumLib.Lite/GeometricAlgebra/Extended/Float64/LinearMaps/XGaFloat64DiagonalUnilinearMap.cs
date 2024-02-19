using System.Collections;
using System.Runtime.CompilerServices;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.LinearMaps;

public sealed class XGaFloat64DiagonalUnilinearMap :
    IXGaFloat64UnilinearMap,
    IReadOnlyDictionary<IIndexSet, double>
{
    public XGaFloat64Multivector DiagonalMultivector { get; }
        
    public XGaFloat64Processor Processor 
        => DiagonalMultivector.Processor;
        
    public XGaMetric Metric 
        => DiagonalMultivector.Metric;

    public int Count 
        => DiagonalMultivector.Count;

    public IEnumerable<IIndexSet> Keys 
        => DiagonalMultivector.Ids;

    public IEnumerable<double> Values 
        => DiagonalMultivector.Scalars;

    public double this[IIndexSet key] 
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
    public bool ContainsKey(IIndexSet key)
    {
        return DiagonalMultivector.ContainsKey(key);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(IIndexSet key, out double value)
    {
        return DiagonalMultivector.TryGetValue(key, out value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaFloat64UnilinearMap GetAdjoint()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector MapBasisBlade(IIndexSet id)
    {
        return DiagonalMultivector.TryGetValue(id, out var scalar)
            ? Processor.CreateTermKVector(id, scalar)
            : Processor.CreateZeroScalar();
    }

    public XGaFloat64Multivector Map(XGaFloat64Multivector multivector)
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
    public IEnumerable<KeyValuePair<IIndexSet, XGaFloat64Multivector>> GetMappedBasisBlades(int vSpaceDimensions)
    {
        return DiagonalMultivector
            .Where(p => p.Key.VSpaceDimensions() <= vSpaceDimensions)
            .Select(p => 
                new KeyValuePair<IIndexSet, XGaFloat64Multivector>(
                    p.Key, 
                    Processor.CreateTermKVector(p.Key, p.Value)
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
    public IEnumerator<KeyValuePair<IIndexSet, double>> GetEnumerator()
    {
        return DiagonalMultivector.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}