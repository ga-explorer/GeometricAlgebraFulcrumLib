using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Spaces.Conformal;

public class RGaConformalIpnsHyperPlane<T> :
    RGaConformalIpnsVector<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalIpnsHyperPlane<T> operator *(RGaConformalIpnsHyperPlane<T> mv, T s)
    {
        return new RGaConformalIpnsHyperPlane<T>(
            mv.Space,
            mv.Vector.Times(s)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalIpnsHyperPlane<T> operator *(T s, RGaConformalIpnsHyperPlane<T> mv)
    {
        return new RGaConformalIpnsHyperPlane<T>(
            mv.Space,
            mv.Vector.Times(s)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalIpnsHyperPlane<T> operator /(RGaConformalIpnsHyperPlane<T> mv, T s)
    {
        return new RGaConformalIpnsHyperPlane<T>(
            mv.Space,
            mv.Vector.Divide(s)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaConformalIpnsHyperPlane(RGaConformalSpace<T> space, RGaVector<T> vector)
        : base(space, vector)
    {
    }

        
        
    public override bool IsValid()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> GetNormal()
    {
        return Vector.GetVectorPart(index => index < Space.VSpaceDimensions - 2);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalOpnsHyperPlane<T> ToOpnsHyperPlane()
    {
        return new RGaConformalOpnsHyperPlane<T>(
            Space,
            Vector.UnDual(Space.VSpaceDimensions)
        );
    }
}