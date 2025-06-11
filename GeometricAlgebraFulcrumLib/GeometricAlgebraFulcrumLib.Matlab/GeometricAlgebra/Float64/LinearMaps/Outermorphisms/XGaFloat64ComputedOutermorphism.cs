using System;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Outermorphisms;

public sealed class XGaFloat64ComputedOutermorphism :
    XGaFloat64OutermorphismBasisVectorOpBase
{
    public Func<int, XGaFloat64Vector> BasisMapFunc { get; }

    public override XGaFloat64Processor Processor { get; }


    
    internal XGaFloat64ComputedOutermorphism(Func<int, XGaFloat64Vector> basisMapFunc, XGaFloat64Processor processor)
    {
        BasisMapFunc = basisMapFunc;
        Processor = processor;
    }

    
    
    public override bool IsValid()
    {
        return true;
    }


    public override IXGaFloat64Outermorphism GetOmAdjoint()
    {
        throw new NotImplementedException();
    }

    
    public override XGaFloat64Vector OmMapBasisVector(int index)
    {
        return BasisMapFunc(index);
    }

    
}