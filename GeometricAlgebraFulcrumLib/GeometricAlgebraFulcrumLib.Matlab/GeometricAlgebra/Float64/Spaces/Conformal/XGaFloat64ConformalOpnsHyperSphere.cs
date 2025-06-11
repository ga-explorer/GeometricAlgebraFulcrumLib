using System;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Spaces.Conformal;

public class XGaFloat64ConformalOpnsHyperSphere :
    XGaFloat64ConformalBlade
{
    
    public static XGaFloat64ConformalOpnsHyperSphere operator *(XGaFloat64ConformalOpnsHyperSphere mv, double s)
    {
        return new XGaFloat64ConformalOpnsHyperSphere(
            mv.Space,
            mv.Blade.Times(s)
        );
    }

    
    public static XGaFloat64ConformalOpnsHyperSphere operator *(double s, XGaFloat64ConformalOpnsHyperSphere mv)
    {
        return new XGaFloat64ConformalOpnsHyperSphere(
            mv.Space,
            mv.Blade.Times(s)
        );
    }


    
    public static XGaFloat64ConformalOpnsHyperSphere operator /(XGaFloat64ConformalOpnsHyperSphere mv, double s)
    {
        return new XGaFloat64ConformalOpnsHyperSphere(
            mv.Space,
            mv.Blade.Divide(s)
        );
    }

        
    public override XGaFloat64KVector Blade { get; }
        

    internal XGaFloat64ConformalOpnsHyperSphere(XGaFloat64ConformalSpace space, XGaFloat64KVector vector)
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

    
    public XGaFloat64ConformalIpnsHyperSphere ToIpnsHyperSphere()
    {
        return new XGaFloat64ConformalIpnsHyperSphere(
            Space,
            Blade.Dual(Space.VSpaceDimensions).GetVectorPart()
        );
    }
}