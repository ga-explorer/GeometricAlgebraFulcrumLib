using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Versors;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Rotors;

public abstract class XGaFloat64RotorBase : 
    XGaFloat64ScalingRotorBase, 
    IXGaFloat64Rotor
{
    protected XGaFloat64RotorBase(XGaFloat64Processor metric)
        : base(metric)
    {
    }


    public abstract IXGaFloat64Rotor GetRotorInverse();

    
    public override IXGaFloat64Versor GetVersorInverse()
    {
        return GetRotorInverse();
    }

    
    public override IXGaFloat64ScalingRotor GetScalingRotorInverse()
    {
        return GetRotorInverse();
    }
}