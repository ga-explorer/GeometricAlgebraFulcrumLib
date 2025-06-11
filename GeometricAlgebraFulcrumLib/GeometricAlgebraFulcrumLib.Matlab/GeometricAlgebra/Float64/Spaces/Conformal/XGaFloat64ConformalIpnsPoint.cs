using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Spaces.Conformal;

public class XGaFloat64ConformalIpnsPoint :
    XGaFloat64ConformalIpnsVector
{
    
    public static XGaFloat64ConformalIpnsPoint operator *(XGaFloat64ConformalIpnsPoint mv, double s)
    {
        return new XGaFloat64ConformalIpnsPoint(
            mv.Space,
            mv.Vector.Times(s)
        );
    }

    
    public static XGaFloat64ConformalIpnsPoint operator *(double s, XGaFloat64ConformalIpnsPoint mv)
    {
        return new XGaFloat64ConformalIpnsPoint(
            mv.Space,
            mv.Vector.Times(s)
        );
    }
        
    
    public static XGaFloat64ConformalIpnsPoint operator /(XGaFloat64ConformalIpnsPoint mv, double s)
    {
        return new XGaFloat64ConformalIpnsPoint(
            mv.Space,
            mv.Vector.Divide(s)
        );
    }


    internal XGaFloat64ConformalIpnsPoint(XGaFloat64ConformalSpace space, XGaFloat64Vector vector)
        : base(space, vector)
    {
    }

    internal XGaFloat64ConformalIpnsPoint(XGaFloat64ConformalSpace space, XGaFloat64Vector vector, bool assumeUnitWeight)
        : base(space, vector, assumeUnitWeight)
    {
    }


    
    public override bool IsValid()
    {
        // TODO: Add one more condition to ensure this is a round point
        return Vector.IsValid() && 
               Vector.VSpaceDimensions <= Space.VSpaceDimensions;
    }

    
    public XGaFloat64Vector GetPosition()
    {
        return Vector
            .GetVectorPart((int index) => index < Space.VSpaceDimensions - 2)
            .Divide(Weight());
    }
        
    
    public XGaFloat64ConformalIpnsPoint GetNormalizedPoint()
    {
        if (AssumeUnitWeight)
            return this;
            
        return new XGaFloat64ConformalIpnsPoint(
            Space, 
            Vector.Divide(Weight().ScalarValue), 
            true
        );
    }
}