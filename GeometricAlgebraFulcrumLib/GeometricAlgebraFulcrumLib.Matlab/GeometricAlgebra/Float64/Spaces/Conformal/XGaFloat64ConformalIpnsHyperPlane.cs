﻿using System;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Spaces.Conformal;

public class XGaFloat64ConformalIpnsHyperPlane :
    XGaFloat64ConformalIpnsVector
{
    
    public static XGaFloat64ConformalIpnsHyperPlane operator *(XGaFloat64ConformalIpnsHyperPlane mv, double s)
    {
        return new XGaFloat64ConformalIpnsHyperPlane(
            mv.Space,
            mv.Vector.Times(s)
        );
    }

    
    public static XGaFloat64ConformalIpnsHyperPlane operator *(double s, XGaFloat64ConformalIpnsHyperPlane mv)
    {
        return new XGaFloat64ConformalIpnsHyperPlane(
            mv.Space,
            mv.Vector.Times(s)
        );
    }


    
    public static XGaFloat64ConformalIpnsHyperPlane operator /(XGaFloat64ConformalIpnsHyperPlane mv, double s)
    {
        return new XGaFloat64ConformalIpnsHyperPlane(
            mv.Space,
            mv.Vector.Divide(s)
        );
    }


    
    internal XGaFloat64ConformalIpnsHyperPlane(XGaFloat64ConformalSpace space, XGaFloat64Vector vector)
        : base(space, vector)
    {
    }

        
        
    public override bool IsValid()
    {
        throw new NotImplementedException();
    }

    
    public XGaFloat64Vector GetNormal()
    {
        return Vector.GetVectorPart((int index) => index < Space.VSpaceDimensions - 2);
    }
        
    
    public XGaFloat64ConformalOpnsHyperPlane ToOpnsHyperPlane()
    {
        return new XGaFloat64ConformalOpnsHyperPlane(
            Space,
            Vector.UnDual(Space.VSpaceDimensions)
        );
    }
}