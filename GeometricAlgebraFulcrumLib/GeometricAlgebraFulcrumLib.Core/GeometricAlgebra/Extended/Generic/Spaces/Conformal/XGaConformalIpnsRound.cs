using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Spaces.Conformal;

public class XGaConformalIpnsRound<T> :
    XGaConformalBlade<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalIpnsRound<T> operator *(XGaConformalIpnsRound<T> mv, T s)
    {
        return new XGaConformalIpnsRound<T>(
            mv.Space,
            mv.Blade.Times(s)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalIpnsRound<T> operator *(T s, XGaConformalIpnsRound<T> mv)
    {
        return new XGaConformalIpnsRound<T>(
            mv.Space,
            mv.Blade.Times(s)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalIpnsRound<T> operator /(XGaConformalIpnsRound<T> mv, T s)
    {
        return new XGaConformalIpnsRound<T>(
            mv.Space,
            mv.Blade.Divide(s)
        );
    }

        
    public override XGaKVector<T> Blade { get; }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaConformalIpnsRound(XGaConformalSpace<T> space, XGaKVector<T> blade)
        : base(space)
    {
        Blade = blade;
    }

        
    public override bool IsValid()
    {
        throw new NotImplementedException();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Square()
    {
        return Blade.SpSquared();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaConformalOpnsRound<T> ToOpnsRound()
    {
        return new XGaConformalOpnsRound<T>(
            Space,
            Blade.UnDual(Space.VSpaceDimensions)
        );
    }
}