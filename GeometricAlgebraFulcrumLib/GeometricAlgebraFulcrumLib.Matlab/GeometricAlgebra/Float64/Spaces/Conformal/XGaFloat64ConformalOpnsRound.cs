using System;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Spaces.Conformal;

public class XGaFloat64ConformalOpnsRound
    : XGaFloat64ConformalBlade
{
    
    public static XGaFloat64ConformalOpnsRound operator *(XGaFloat64ConformalOpnsRound mv, double s)
    {
        return new XGaFloat64ConformalOpnsRound(
            mv.Space,
            mv.Blade.Times(s)
        );
    }

    
    public static XGaFloat64ConformalOpnsRound operator *(double s, XGaFloat64ConformalOpnsRound mv)
    {
        return new XGaFloat64ConformalOpnsRound(
            mv.Space,
            mv.Blade.Times(s)
        );
    }
        
    
    public static XGaFloat64ConformalOpnsRound operator /(XGaFloat64ConformalOpnsRound mv, double s)
    {
        return new XGaFloat64ConformalOpnsRound(
            mv.Space,
            mv.Blade.Divide(s)
        );
    }

        
    public override XGaFloat64KVector Blade { get; }


    
    internal XGaFloat64ConformalOpnsRound(XGaFloat64ConformalSpace space, XGaFloat64KVector blade)
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

    
    public XGaFloat64ConformalIpnsRound ToIpnsRound()
    {
        return new XGaFloat64ConformalIpnsRound(
            Space,
            Blade.Dual(Space.VSpaceDimensions)
        );
    }
}