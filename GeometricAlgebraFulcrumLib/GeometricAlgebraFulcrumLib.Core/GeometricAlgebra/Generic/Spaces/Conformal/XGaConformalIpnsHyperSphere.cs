using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Spaces.Conformal;

public class XGaConformalIpnsHyperSphere<T> :
    XGaConformalIpnsVector<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalIpnsHyperSphere<T> operator *(XGaConformalIpnsHyperSphere<T> mv, T s)
    {
        return new XGaConformalIpnsHyperSphere<T>(
            mv.Space,
            mv.Vector.Times(s)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalIpnsHyperSphere<T> operator *(T s, XGaConformalIpnsHyperSphere<T> mv)
    {
        return new XGaConformalIpnsHyperSphere<T>(
            mv.Space,
            mv.Vector.Times(s)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalIpnsHyperSphere<T> operator /(XGaConformalIpnsHyperSphere<T> mv, T s)
    {
        return new XGaConformalIpnsHyperSphere<T>(
            mv.Space,
            mv.Vector.Divide(s)
        );
    }


    internal XGaConformalIpnsHyperSphere(XGaConformalSpace<T> space, XGaVector<T> vector)
        : base(space, vector)
    {
    }

    internal XGaConformalIpnsHyperSphere(XGaConformalSpace<T> space, XGaVector<T> vector, bool assumeUnitWeight)
        : base(space, vector, assumeUnitWeight)
    {
    }
        
        
    public override bool IsValid()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> GetCenter()
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
    public XGaConformalIpnsHyperSphere<T> ToOpnsHyperSphere()
    {
        return new XGaConformalIpnsHyperSphere<T>(
            Space,
            Vector.UnDual(Space.VSpaceDimensions).GetVectorPart()
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalIpnsHyperSphere<T> GetNormalizedSphere()
    {
        if (AssumeUnitWeight)
            return this;

        var vector = Vector.Divide(Weight());

        return new XGaConformalIpnsHyperSphere<T>(
            Space, 
            vector, 
            true
        );
    }
}