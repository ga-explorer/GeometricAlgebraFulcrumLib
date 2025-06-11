using System;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Spaces.Conformal;

public class XGaFloat64ConformalIpnsRound :
    XGaFloat64ConformalBlade
{
    
    public static XGaFloat64ConformalIpnsRound operator *(XGaFloat64ConformalIpnsRound mv, double s)
    {
        return new XGaFloat64ConformalIpnsRound(
            mv.Space,
            mv.Blade.Times(s)
        );
    }

    
    public static XGaFloat64ConformalIpnsRound operator *(double s, XGaFloat64ConformalIpnsRound mv)
    {
        return new XGaFloat64ConformalIpnsRound(
            mv.Space,
            mv.Blade.Times(s)
        );
    }
        
    
    public static XGaFloat64ConformalIpnsRound operator /(XGaFloat64ConformalIpnsRound mv, double s)
    {
        return new XGaFloat64ConformalIpnsRound(
            mv.Space,
            mv.Blade.Divide(s)
        );
    }

        
    public override XGaFloat64KVector Blade { get; }
        

    
    internal XGaFloat64ConformalIpnsRound(XGaFloat64ConformalSpace space, XGaFloat64KVector blade)
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

    
    public XGaFloat64ConformalOpnsRound ToOpnsRound()
    {
        return new XGaFloat64ConformalOpnsRound(
            Space,
            Blade.UnDual(Space.VSpaceDimensions)
        );
    }
}