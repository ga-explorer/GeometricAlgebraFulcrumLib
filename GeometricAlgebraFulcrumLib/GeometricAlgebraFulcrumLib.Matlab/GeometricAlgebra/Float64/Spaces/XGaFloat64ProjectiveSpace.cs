using System;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Spaces;

public class XGaFloat64ProjectiveSpace :
    XGaFloat64Space
{
    
    public static XGaFloat64ProjectiveSpace Create(int vSpaceDimensions)
    {
        return new XGaFloat64ProjectiveSpace(vSpaceDimensions);
    }


    public override int VSpaceDimensions { get; }

    public XGaFloat64ProjectiveProcessor ProjectiveProcessor 
        => XGaFloat64Processor.Projective;

    public override XGaFloat64Processor Processor 
        => XGaFloat64Processor.Projective;


    internal XGaFloat64ProjectiveSpace(int vSpaceDimensions)
    {
        if (vSpaceDimensions < 0)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        VSpaceDimensions = vSpaceDimensions;
    }


    
    public XGaFloat64ProjectiveSpace CreateSpace(int vSpaceDimensions)
    {
        return new XGaFloat64ProjectiveSpace(vSpaceDimensions);
    }
}