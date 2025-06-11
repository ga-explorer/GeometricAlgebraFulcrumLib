using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.LinearMaps;

public class XGaFloat64ComputedUnilinearMap :
    IXGaFloat64UnilinearMap
{
    public Func<IndexSet, XGaFloat64Multivector> BasisMapFunc { get; }

    public XGaFloat64Processor Processor { get; }
        
    public XGaMetric Metric 
        => Processor;
        
    public XGaFloat64Multivector this[IndexSet key] 
        => BasisMapFunc(key);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaFloat64ComputedUnilinearMap(XGaFloat64Processor processor, Func<IndexSet, XGaFloat64Multivector> basisMapFunc)
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
    
    public IXGaFloat64UnilinearMap GetAdjoint()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector MapBasisBlade(IndexSet id)
    {
        return BasisMapFunc(id);
    }

    public XGaFloat64Multivector Map(XGaFloat64Multivector multivector)
    {
        var composer = Processor.CreateMultivectorComposer();

        foreach (var (id, scalar) in multivector)
        {
            var mv = BasisMapFunc(id);

            if (mv.IsZero)
                continue;

            composer.AddMultivectorScaled(mv, scalar);
        }

        return composer.GetSimpleMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<IndexSet, XGaFloat64Multivector>> GetMappedBasisBlades(int vSpaceDimensions)
    {
        return Processor
            .GetBasisBladeIds(vSpaceDimensions)
            .Select(id => 
                new KeyValuePair<IndexSet, XGaFloat64Multivector>(id, BasisMapFunc(id))
            ).Where(p => !p.Value.IsZero);
    }
        
    //public double[,] GetMultivectorMapArray(int rowCount, int colCount)
    //{
    //    var mapArray = 
    //        new double[rowCount, colCount];

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