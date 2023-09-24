using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.LinearMaps;

public class XGaFloat64ComputedUnilinearMap :
    IXGaFloat64UnilinearMap
{
    public Func<IIndexSet, XGaFloat64Multivector> BasisMapFunc { get; }

    public XGaFloat64Processor Processor { get; }
        
    public XGaMetric Metric 
        => Processor;
        
    public XGaFloat64Multivector this[IIndexSet key] 
        => BasisMapFunc(key);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaFloat64ComputedUnilinearMap(XGaFloat64Processor processor, Func<IIndexSet, XGaFloat64Multivector> basisMapFunc)
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
    public XGaFloat64Multivector MapBasisBlade(IIndexSet id)
    {
        return BasisMapFunc(id);
    }

    public XGaFloat64Multivector Map(XGaFloat64Multivector multivector)
    {
        var composer = Processor.CreateComposer();

        foreach (var (id, scalar) in multivector)
        {
            var mv = BasisMapFunc(id);

            if (mv.IsZero)
                continue;

            composer.AddMultivector(mv, scalar);
        }

        return composer.GetSimpleMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<IIndexSet, XGaFloat64Multivector>> GetMappedBasisBlades(int vSpaceDimensions)
    {
        return Processor
            .GetBasisBladeIds(vSpaceDimensions)
            .Select(id => 
                new KeyValuePair<IIndexSet, XGaFloat64Multivector>(id, BasisMapFunc(id))
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

    //    var maxId = _basisMapDictionary.Keys.Max() ?? EmptyIndexSet.Instance;

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