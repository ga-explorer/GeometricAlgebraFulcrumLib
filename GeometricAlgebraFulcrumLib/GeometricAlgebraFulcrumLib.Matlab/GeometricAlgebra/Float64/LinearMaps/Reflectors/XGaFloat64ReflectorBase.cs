using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Versors;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Reflectors;

public abstract class XGaFloat64ReflectorBase : 
    XGaFloat64VersorBase, 
    IXGaFloat64Reflector
{
    
    protected XGaFloat64ReflectorBase(XGaFloat64Processor metric)
        : base(metric)
    {
    }


    public abstract IXGaFloat64Reflector GetReflectorInverse();

    
    public override IXGaFloat64Versor GetVersorInverse()
    {
        return GetReflectorInverse();
    }
}