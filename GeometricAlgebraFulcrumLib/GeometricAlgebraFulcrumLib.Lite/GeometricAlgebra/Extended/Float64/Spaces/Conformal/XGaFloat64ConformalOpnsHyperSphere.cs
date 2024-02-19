using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Spaces.Conformal;

public class XGaFloat64ConformalOpnsHyperSphere :
    XGaFloat64ConformalBlade
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64ConformalOpnsHyperSphere operator *(XGaFloat64ConformalOpnsHyperSphere mv, double s)
    {
        return new XGaFloat64ConformalOpnsHyperSphere(
            mv.Space,
            mv.Blade.Times(s)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64ConformalOpnsHyperSphere operator *(double s, XGaFloat64ConformalOpnsHyperSphere mv)
    {
        return new XGaFloat64ConformalOpnsHyperSphere(
            mv.Space,
            mv.Blade.Times(s)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar Square()
    {
        return Blade.SpSquared();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64ConformalIpnsHyperSphere ToIpnsHyperSphere()
    {
        return new XGaFloat64ConformalIpnsHyperSphere(
            Space,
            Blade.Dual(Space.VSpaceDimensions).GetVectorPart()
        );
    }
}