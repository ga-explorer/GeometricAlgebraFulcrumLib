using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps.BasisMaps;

public sealed class RGaFloat64ComputedUnilinearBasisMap :
    IRGaFloat64UnilinearBasisMap
{
    public RGaFloat64Processor Processor { get; }

    public RGaMetric Metric
        => Processor;

    public Func<ulong, RGaFloat64ScaledBasisBlade> BasisMappingFunc { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ComputedUnilinearBasisMap(RGaFloat64Processor processor, Func<ulong, RGaFloat64ScaledBasisBlade> basisMappingFunc)
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
    public RGaFloat64ScaledBasisBlade MapBasisBlade(ulong basisBladeId)
    {
        return BasisMappingFunc(basisBladeId);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<ulong, RGaFloat64ScaledBasisBlade>> GetMappedBasisBlades(int vSpaceDimensions)
    {
        return Processor.GetBasisBladeIds(vSpaceDimensions).Select(
            id => new KeyValuePair<ulong, RGaFloat64ScaledBasisBlade>(
                id, 
                BasisMappingFunc(id)
            )
        );
    }
}