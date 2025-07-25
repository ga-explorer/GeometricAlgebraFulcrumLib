﻿using System;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Spaces.Conformal;

public class XGaFloat64ConformalOpnsHyperPlane :
    XGaFloat64ConformalBlade
{
    
    public static XGaFloat64ConformalOpnsHyperPlane operator *(XGaFloat64ConformalOpnsHyperPlane mv, double s)
    {
        return new XGaFloat64ConformalOpnsHyperPlane(
            mv.Space,
            mv.Blade.Times(s)
        );
    }

    
    public static XGaFloat64ConformalOpnsHyperPlane operator *(double s, XGaFloat64ConformalOpnsHyperPlane mv)
    {
        return new XGaFloat64ConformalOpnsHyperPlane(
            mv.Space,
            mv.Blade.Times(s)
        );
    }


    
    public static XGaFloat64ConformalOpnsHyperPlane operator /(XGaFloat64ConformalOpnsHyperPlane mv, double s)
    {
        return new XGaFloat64ConformalOpnsHyperPlane(
            mv.Space,
            mv.Blade.Divide(s)
        );
    }
        
        
    public override XGaFloat64KVector Blade { get; }
        

    internal XGaFloat64ConformalOpnsHyperPlane(XGaFloat64ConformalSpace space, XGaFloat64KVector vector)
        : base(space)
    {
        Blade = vector;
    }

        
    public override bool IsValid()
    {
        throw new NotImplementedException();
    }
        
    
    public double Square()
    {
        return Blade.SpSquared();
    }

    
    public XGaFloat64ConformalIpnsHyperPlane ToIpnsHyperPlane()
    {
        return new XGaFloat64ConformalIpnsHyperPlane(
            Space,
            Blade.Dual(Space.VSpaceDimensions).GetVectorPart()
        );
    }
}