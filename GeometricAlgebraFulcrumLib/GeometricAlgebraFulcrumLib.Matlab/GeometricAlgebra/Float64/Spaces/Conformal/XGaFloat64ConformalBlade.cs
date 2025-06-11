using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Spaces.Conformal;

public abstract class XGaFloat64ConformalBlade :
    IAlgebraicElement
{
    public XGaFloat64ConformalSpace Space { get; }

    public XGaFloat64ConformalProcessor Processor 
        => XGaFloat64Processor.Conformal;

    public abstract XGaFloat64KVector Blade { get; }

    
    
    protected XGaFloat64ConformalBlade(XGaFloat64ConformalSpace space)
    {
        Space = space;
    }


    public abstract bool IsValid();
}