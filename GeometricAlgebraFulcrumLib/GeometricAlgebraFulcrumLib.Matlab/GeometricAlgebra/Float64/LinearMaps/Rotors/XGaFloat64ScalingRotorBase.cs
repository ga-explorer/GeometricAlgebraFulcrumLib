using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Versors;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Rotors;

public abstract class XGaFloat64ScalingRotorBase : 
    XGaFloat64VersorBase, 
    IXGaFloat64ScalingRotor
{
    
    protected XGaFloat64ScalingRotorBase(XGaFloat64Processor metric)
        : base(metric)
    {
    }


    public abstract double GetScalingFactor();

    public abstract IXGaFloat64ScalingRotor GetScalingRotorInverse();

    
    public override IXGaFloat64Versor GetVersorInverse()
    {
        return GetScalingRotorInverse();
    }
}