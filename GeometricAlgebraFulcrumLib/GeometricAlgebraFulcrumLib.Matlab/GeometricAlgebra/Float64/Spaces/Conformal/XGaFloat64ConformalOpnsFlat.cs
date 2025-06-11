using System;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Spaces.Conformal;

public class XGaFloat64ConformalOpnsFlat :
    XGaFloat64ConformalBlade
{
    
    public static XGaFloat64ConformalOpnsFlat operator *(XGaFloat64ConformalOpnsFlat mv, double s)
    {
        return new XGaFloat64ConformalOpnsFlat(
            mv.Space,
            mv.Blade.Times(s)
        );
    }

    
    public static XGaFloat64ConformalOpnsFlat operator *(double s, XGaFloat64ConformalOpnsFlat mv)
    {
        return new XGaFloat64ConformalOpnsFlat(
            mv.Space,
            mv.Blade.Times(s)
        );
    }


    
    public static XGaFloat64ConformalOpnsFlat operator /(XGaFloat64ConformalOpnsFlat mv, double s)
    {
        return new XGaFloat64ConformalOpnsFlat(
            mv.Space,
            mv.Blade.Divide(s)
        );
    }

        
    public override XGaFloat64KVector Blade { get; }
        

    internal XGaFloat64ConformalOpnsFlat(XGaFloat64ConformalSpace space, XGaFloat64KVector blade)
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

    
    public XGaFloat64ConformalOpnsFlat ToIpnsFlat()
    {
        return new XGaFloat64ConformalOpnsFlat(
            Space,
            Blade.Dual(Space.VSpaceDimensions)
        );
    }
}