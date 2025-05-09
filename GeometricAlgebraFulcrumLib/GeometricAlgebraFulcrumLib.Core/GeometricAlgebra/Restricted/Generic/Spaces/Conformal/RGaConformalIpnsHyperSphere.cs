using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Spaces.Conformal;

public class RGaConformalIpnsHyperSphere<T> :
    RGaConformalIpnsVector<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalIpnsHyperSphere<T> operator *(RGaConformalIpnsHyperSphere<T> mv, T s)
    {
        return new RGaConformalIpnsHyperSphere<T>(
            mv.Space,
            mv.Vector.Times(s)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalIpnsHyperSphere<T> operator *(T s, RGaConformalIpnsHyperSphere<T> mv)
    {
        return new RGaConformalIpnsHyperSphere<T>(
            mv.Space,
            mv.Vector.Times(s)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalIpnsHyperSphere<T> operator /(RGaConformalIpnsHyperSphere<T> mv, T s)
    {
        return new RGaConformalIpnsHyperSphere<T>(
            mv.Space,
            mv.Vector.Divide(s)
        );
    }


    internal RGaConformalIpnsHyperSphere(RGaConformalSpace<T> space, RGaVector<T> vector)
        : base(space, vector)
    {
    }

    internal RGaConformalIpnsHyperSphere(RGaConformalSpace<T> space, RGaVector<T> vector, bool assumeUnitWeight)
        : base(space, vector, assumeUnitWeight)
    {
    }
        
        
    public override bool IsValid()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> GetCenter()
    {
        return Vector.GetVectorPart(index => index < Space.VSpaceDimensions - 2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetRadiusSquared()
    {
        return Vector.SpSquared();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetRadius()
    {
        return GetRadiusSquared().SqrtOfAbs();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalIpnsHyperSphere<T> ToOpnsHyperSphere()
    {
        return new RGaConformalIpnsHyperSphere<T>(
            Space,
            Vector.UnDual(Space.VSpaceDimensions).GetVectorPart()
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalIpnsHyperSphere<T> GetNormalizedSphere()
    {
        if (AssumeUnitWeight)
            return this;

        var vector = Vector.Divide(Weight());

        return new RGaConformalIpnsHyperSphere<T>(
            Space, 
            vector, 
            true
        );
    }
}