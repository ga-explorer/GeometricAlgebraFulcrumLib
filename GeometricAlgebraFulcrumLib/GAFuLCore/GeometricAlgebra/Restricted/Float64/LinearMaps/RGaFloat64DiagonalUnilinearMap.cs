using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps;

public sealed class RGaFloat64DiagonalUnilinearMap :
    IRGaFloat64UnilinearMap,
    IReadOnlyDictionary<ulong, double>
{
    public RGaFloat64Multivector DiagonalMultivector { get; }
        
    public RGaFloat64Processor Processor 
        => DiagonalMultivector.Processor;

    public RGaMetric Metric 
        => DiagonalMultivector.Metric;
        
    public int Count 
        => DiagonalMultivector.Count;

    public IEnumerable<ulong> Keys 
        => DiagonalMultivector.Ids;

    public IEnumerable<double> Values 
        => DiagonalMultivector.Scalars;

    public double this[ulong key] 
        => DiagonalMultivector.GetBasisBladeScalar(key);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaFloat64DiagonalUnilinearMap(RGaFloat64Multivector diagonalMultivector)
    {
        DiagonalMultivector = diagonalMultivector;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return DiagonalMultivector.IsValid();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsKey(ulong key)
    {
        return DiagonalMultivector.ContainsKey(key);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(ulong key, out double value)
    {
        return DiagonalMultivector.TryGetValue(key, out value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaFloat64UnilinearMap GetAdjoint()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector MapBasisBlade(ulong id)
    {
        return DiagonalMultivector.TryGetValue(id, out var scalar)
            ? Processor.KVectorTerm(id, scalar)
            : Processor.ScalarZero;
    }

    public RGaFloat64Multivector Map(RGaFloat64Multivector multivector)
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
    public IEnumerable<KeyValuePair<ulong, RGaFloat64Multivector>> GetMappedBasisBlades(int vSpaceDimensions)
    {
        return DiagonalMultivector
            .Where(p => p.Key.VSpaceDimensions() <= vSpaceDimensions)
            .Select(p => 
                new KeyValuePair<ulong, RGaFloat64Multivector>(
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

        var mapArray = 
            new double[rowCount, colCount];
            
        foreach (var (index, scalar) in DiagonalMultivector)
            mapArray[index, index] = scalar;

        return mapArray;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<KeyValuePair<ulong, double>> GetEnumerator()
    {
        return DiagonalMultivector.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}