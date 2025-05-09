using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Spaces.Conformal;

public class RGaFloat64ConformalOpnsHyperSphere :
    RGaFloat64ConformalBlade
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ConformalOpnsHyperSphere operator *(RGaFloat64ConformalOpnsHyperSphere mv, double s)
    {
        return new RGaFloat64ConformalOpnsHyperSphere(
            mv.Space,
            mv.Blade.Times(s)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ConformalOpnsHyperSphere operator *(double s, RGaFloat64ConformalOpnsHyperSphere mv)
    {
        return new RGaFloat64ConformalOpnsHyperSphere(
            mv.Space,
            mv.Blade.Times(s)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ConformalOpnsHyperSphere operator /(RGaFloat64ConformalOpnsHyperSphere mv, double s)
    {
        return new RGaFloat64ConformalOpnsHyperSphere(
            mv.Space,
            mv.Blade.Divide(s)
        );
    }

        
    public override RGaFloat64KVector Blade { get; }
        

    internal RGaFloat64ConformalOpnsHyperSphere(RGaFloat64ConformalSpace space, RGaFloat64KVector vector)
        : base(space)
    {
        Blade = vector;
    }

        
    public override bool IsValid()
    {
        throw new NotImplementedException();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Square()
    {
        return Blade.SpSquared();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ConformalIpnsHyperSphere ToIpnsHyperSphere()
    {
        return new RGaFloat64ConformalIpnsHyperSphere(
            Space,
            Blade.Dual(Space.VSpaceDimensions).GetVectorPart()
        );
    }
}