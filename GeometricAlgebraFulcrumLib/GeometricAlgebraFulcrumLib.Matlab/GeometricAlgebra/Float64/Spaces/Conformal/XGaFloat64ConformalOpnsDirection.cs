using System;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Spaces.Conformal;

public class XGaFloat64ConformalOpnsDirection :
    XGaFloat64ConformalBlade
{
    
    public static XGaFloat64ConformalOpnsDirection operator *(XGaFloat64ConformalOpnsDirection mv, double s)
    {
        return new XGaFloat64ConformalOpnsDirection(
            mv.Space,
            mv.Blade.Times(s)
        );
    }

    
    public static XGaFloat64ConformalOpnsDirection operator *(double s, XGaFloat64ConformalOpnsDirection mv)
    {
        return new XGaFloat64ConformalOpnsDirection(
            mv.Space,
            mv.Blade.Times(s)
        );
    }


    
    public static XGaFloat64ConformalOpnsDirection operator /(XGaFloat64ConformalOpnsDirection mv, double s)
    {
        return new XGaFloat64ConformalOpnsDirection(
            mv.Space,
            mv.Blade.Divide(s)
        );
    }

        
    public override XGaFloat64KVector Blade { get; }
        

    internal XGaFloat64ConformalOpnsDirection(XGaFloat64ConformalSpace space, XGaFloat64KVector blade)
        : base(space)
    {
        Blade = blade;
    }

        
    public override bool IsValid()
    {
        throw new NotImplementedException();
    }
        
    
    public double Square()
    {
        return Blade.SpSquared();
    }

    
    public XGaFloat64ConformalOpnsDirection ToIpnsDirection()
    {
        return new XGaFloat64ConformalOpnsDirection(
            Space,
            Blade.Dual(Space.VSpaceDimensions)
        );
    }
}