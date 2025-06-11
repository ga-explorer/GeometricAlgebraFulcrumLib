using System;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Spaces.Conformal;

public class XGaFloat64ConformalIpnsFlat :
    XGaFloat64ConformalBlade
{
    
    public static XGaFloat64ConformalIpnsFlat operator *(XGaFloat64ConformalIpnsFlat mv, double s)
    {
        return new XGaFloat64ConformalIpnsFlat(
            mv.Space,
            mv.Blade.Times(s)
        );
    }

    
    public static XGaFloat64ConformalIpnsFlat operator *(double s, XGaFloat64ConformalIpnsFlat mv)
    {
        return new XGaFloat64ConformalIpnsFlat(
            mv.Space,
            mv.Blade.Times(s)
        );
    }
        
    
    public static XGaFloat64ConformalIpnsFlat operator /(XGaFloat64ConformalIpnsFlat mv, double s)
    {
        return new XGaFloat64ConformalIpnsFlat(
            mv.Space,
            mv.Blade.Divide(s)
        );
    }

        
    public override XGaFloat64KVector Blade { get; }
        

    
    internal XGaFloat64ConformalIpnsFlat(XGaFloat64ConformalSpace space, XGaFloat64KVector blade)
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

    
    public XGaFloat64ConformalOpnsFlat ToOpnsFlat()
    {
        return new XGaFloat64ConformalOpnsFlat(
            Space,
            Blade.UnDual(Space.VSpaceDimensions)
        );
    }
}