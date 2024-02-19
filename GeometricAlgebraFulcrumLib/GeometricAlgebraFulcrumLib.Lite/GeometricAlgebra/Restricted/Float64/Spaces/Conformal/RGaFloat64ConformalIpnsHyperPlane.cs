using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Spaces.Conformal;

public class RGaFloat64ConformalIpnsHyperPlane :
    RGaFloat64ConformalIpnsVector
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ConformalIpnsHyperPlane operator *(RGaFloat64ConformalIpnsHyperPlane mv, double s)
    {
        return new RGaFloat64ConformalIpnsHyperPlane(
            mv.Space,
            mv.Vector.Times(s)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ConformalIpnsHyperPlane operator *(double s, RGaFloat64ConformalIpnsHyperPlane mv)
    {
        return new RGaFloat64ConformalIpnsHyperPlane(
            mv.Space,
            mv.Vector.Times(s)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ConformalIpnsHyperPlane operator /(RGaFloat64ConformalIpnsHyperPlane mv, double s)
    {
        return new RGaFloat64ConformalIpnsHyperPlane(
            mv.Space,
            mv.Vector.Divide(s)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaFloat64ConformalIpnsHyperPlane(RGaFloat64ConformalSpace space, RGaFloat64Vector vector)
        : base(space, vector)
    {
    }

        
        
    public override bool IsValid()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector GetNormal()
    {
        return Vector.GetVectorPart((int index) => index < Space.VSpaceDimensions - 2);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ConformalOpnsHyperPlane ToOpnsHyperPlane()
    {
        return new RGaFloat64ConformalOpnsHyperPlane(
            Space,
            Vector.UnDual(Space.VSpaceDimensions)
        );
    }
}