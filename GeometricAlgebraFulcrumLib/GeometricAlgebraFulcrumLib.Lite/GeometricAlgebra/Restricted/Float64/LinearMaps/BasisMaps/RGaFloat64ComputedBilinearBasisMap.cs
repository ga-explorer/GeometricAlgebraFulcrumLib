using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.LinearMaps.BasisMaps;

public sealed class RGaFloat64ComputedBilinearBasisMap :
    IRGaFloat64BilinearBasisMap
{
    public RGaFloat64Processor Processor { get; }

    public RGaMetric Metric
        => Processor;

    public Func<ulong, ulong, RGaFloat64ScaledBasisBlade> BasisMappingFunc { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ComputedBilinearBasisMap(RGaFloat64Processor processor, Func<ulong, ulong, RGaFloat64ScaledBasisBlade> basisMappingFunc)
    {
        Processor = processor;
        BasisMappingFunc = basisMappingFunc;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Processor.IsValid();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade MapBasisBlades(ulong basisBladeId1, ulong basisBladeId2)
    {
        return BasisMappingFunc(basisBladeId1, basisBladeId2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<Pair<ulong>, RGaFloat64ScaledBasisBlade>> GetMappedBasisBlades(int vSpaceDimensions)
    {
        var idList = 
            Processor.GetBasisBladeIds(vSpaceDimensions).ToImmutableArray();

        foreach (var id1 in idList)
        foreach (var id2 in idList)
            yield return new KeyValuePair<Pair<ulong>, RGaFloat64ScaledBasisBlade>(
                new Pair<ulong>(id1, id2),
                BasisMappingFunc(id1, id2)
            );
    }
}