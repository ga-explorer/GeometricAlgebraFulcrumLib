using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Spaces.Conformal;

public class XGaConformalOpnsHyperPlane<T> :
    XGaConformalBlade<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalOpnsHyperPlane<T> operator *(XGaConformalOpnsHyperPlane<T> mv, T s)
    {
        return new XGaConformalOpnsHyperPlane<T>(
            mv.Space,
            mv.Blade.Times(s)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalOpnsHyperPlane<T> operator *(T s, XGaConformalOpnsHyperPlane<T> mv)
    {
        return new XGaConformalOpnsHyperPlane<T>(
            mv.Space,
            mv.Blade.Times(s)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalOpnsHyperPlane<T> operator /(XGaConformalOpnsHyperPlane<T> mv, T s)
    {
        return new XGaConformalOpnsHyperPlane<T>(
            mv.Space,
            mv.Blade.Divide(s)
        );
    }
        
        
    public override XGaKVector<T> Blade { get; }
        

    internal XGaConformalOpnsHyperPlane(XGaConformalSpace<T> space, XGaKVector<T> vector)
        : base(space)
    {
        Blade = vector;
    }

        
    public override bool IsValid()
    {
        throw new NotImplementedException();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> Square()
    {
        return Blade.SpSquared();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalIpnsHyperPlane<T> ToIpnsHyperPlane()
    {
        return new XGaConformalIpnsHyperPlane<T>(
            Space,
            Blade.Dual(Space.VSpaceDimensions).GetVectorPart()
        );
    }
}