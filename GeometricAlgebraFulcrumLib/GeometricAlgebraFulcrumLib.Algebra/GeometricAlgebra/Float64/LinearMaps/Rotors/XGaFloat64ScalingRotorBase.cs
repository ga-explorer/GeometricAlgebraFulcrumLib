using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.LinearMaps.Versors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.LinearMaps.Rotors;

public abstract class XGaFloat64ScalingRotorBase : 
    XGaFloat64VersorBase, 
    IXGaFloat64ScalingRotor
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected XGaFloat64ScalingRotorBase(XGaFloat64Processor metric)
        : base(metric)
    {
    }


    public abstract double GetScalingFactor();

    public abstract IXGaFloat64ScalingRotor GetScalingRotorInverse();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IXGaFloat64Versor GetVersorInverse()
    {
        return GetScalingRotorInverse();
    }
}