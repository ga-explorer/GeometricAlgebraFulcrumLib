using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps;

public class XGaComputedUnilinearMap<T> :
    IXGaUnilinearMap<T>
{
    public Func<IndexSet, XGaMultivector<T>> BasisMapFunc { get; }

    public XGaProcessor<T> Processor { get; }
        
    public XGaMetric Metric 
        => Processor;

    public IScalarProcessor<T> ScalarProcessor 
        => Processor.ScalarProcessor;

    public XGaMultivector<T> this[IndexSet key] 
        => BasisMapFunc(key);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaComputedUnilinearMap(XGaProcessor<T> processor, Func<IndexSet, XGaMultivector<T>> basisMapFunc)
    {
        Processor = processor;
        BasisMapFunc = basisMapFunc;

        Debug.Assert(
            IsValid()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return true;
    }
    
    public IXGaUnilinearMap<T> GetAdjoint()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> MapBasisBlade(IndexSet id)
    {
        return BasisMapFunc(id);
    }

    public XGaMultivector<T> Map(XGaMultivector<T> multivector)
    {
        var composer = Processor.CreateMultivectorComposer();

        foreach (var (id, scalar) in multivector)
        {
            var mv = BasisMapFunc(id);

            if (mv.IsZero)
                continue;

            composer.AddMultivectorScaled(mv, scalar);
        }

        return composer.GetMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<IndexSet, XGaMultivector<T>>> GetMappedBasisBlades(int vSpaceDimensions)
    {
        return Processor
            .GetBasisBladeIds(vSpaceDimensions)
            .Select(id => 
                new KeyValuePair<IndexSet, XGaMultivector<T>>(id, BasisMapFunc(id))
            ).Where(p => !p.Value.IsZero);
    }
        
    //public T[,] GetMultivectorMapArray(int rowCount, int colCount)
    //{
    //    var mapArray = 
    //        new T[rowCount, colCount];

    //    if (_basisMapDictionary.Count == 0)
    //        return mapArray;

    //    var minRowCount = 
    //        _basisMapDictionary.Values.Max(v => v.VSpaceDimensions);

    //    if (rowCount < minRowCount)
    //        throw new InvalidOperationException();

    //    var maxId = _basisMapDictionary.Keys.Max() ?? IndexSet.EmptySet;

    //    if (!maxId.TryGetUInt64BitPattern(out var minColCount))
    //        throw new InvalidOperationException();

    //    if ((ulong) colCount < minColCount)
    //        throw new InvalidOperationException();

    //    foreach (var (colId, vector) in _basisMapDictionary)
    //    {
    //        var colIndex = colId.ToInt32();

    //        foreach (var (rowId, scalar) in vector)
    //        {
    //            var rowIndex = rowId.ToInt32();

    //            mapArray[rowIndex, colIndex] = scalar;
    //        }
    //    }

    //    return mapArray;
    //}
    
}