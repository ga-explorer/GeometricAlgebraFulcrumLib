using System;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Spaces.Conformal;

public class XGaFloat64ConformalIpnsHyperSphere :
    XGaFloat64ConformalIpnsVector
{
    
    public static XGaFloat64ConformalIpnsHyperSphere operator *(XGaFloat64ConformalIpnsHyperSphere mv, double s)
    {
        return new XGaFloat64ConformalIpnsHyperSphere(
            mv.Space,
            mv.Vector.Times(s)
        );
    }

    
    public static XGaFloat64ConformalIpnsHyperSphere operator *(double s, XGaFloat64ConformalIpnsHyperSphere mv)
    {
        return new XGaFloat64ConformalIpnsHyperSphere(
            mv.Space,
            mv.Vector.Times(s)
        );
    }


    
    public static XGaFloat64ConformalIpnsHyperSphere operator /(XGaFloat64ConformalIpnsHyperSphere mv, double s)
    {
        return new XGaFloat64ConformalIpnsHyperSphere(
            mv.Space,
            mv.Vector.Divide(s)
        );
    }


    internal XGaFloat64ConformalIpnsHyperSphere(XGaFloat64ConformalSpace space, XGaFloat64Vector vector)
        : base(space, vector)
    {
    }

    internal XGaFloat64ConformalIpnsHyperSphere(XGaFloat64ConformalSpace space, XGaFloat64Vector vector, bool assumeUnitWeight)
        : base(space, vector, assumeUnitWeight)
    {
    }
        
        
    public override bool IsValid()
    {
        throw new NotImplementedException();
    }

    
    public XGaFloat64Vector GetCenter()
    {
        return Vector.GetVectorPart((int index) => index < Space.VSpaceDimensions - 2);
    }

    
    public double GetRadiusSquared()
    {
        return Vector.SpSquared();
    }

    
    public double GetRadius()
    {
        return GetRadiusSquared().SqrtOfAbs();
    }
        
    
    public XGaFloat64ConformalIpnsHyperSphere ToOpnsHyperSphere()
    {
        return new XGaFloat64ConformalIpnsHyperSphere(
            Space,
            Vector.UnDual(Space.VSpaceDimensions).GetVectorPart()
        );
    }
        
    
    public XGaFloat64ConformalIpnsHyperSphere GetNormalizedSphere()
    {
        if (AssumeUnitWeight)
            return this;

        var vector = Vector.Divide(Weight());

        return new XGaFloat64ConformalIpnsHyperSphere(
            Space, 
            vector, 
            true
        );
    }
}