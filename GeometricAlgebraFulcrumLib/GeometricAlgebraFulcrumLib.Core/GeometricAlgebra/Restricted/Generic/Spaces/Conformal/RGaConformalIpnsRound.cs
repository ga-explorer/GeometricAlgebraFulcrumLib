using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Spaces.Conformal;

public class RGaConformalIpnsRound<T> :
    CGaFloat64Blade<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalIpnsRound<T> operator *(RGaConformalIpnsRound<T> mv, T s)
    {
        return new RGaConformalIpnsRound<T>(
            mv.Space,
            mv.Blade.Times(s)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalIpnsRound<T> operator *(T s, RGaConformalIpnsRound<T> mv)
    {
        return new RGaConformalIpnsRound<T>(
            mv.Space,
            mv.Blade.Times(s)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalIpnsRound<T> operator /(RGaConformalIpnsRound<T> mv, T s)
    {
        return new RGaConformalIpnsRound<T>(
            mv.Space,
            mv.Blade.Divide(s)
        );
    }

        
    public override RGaKVector<T> Blade { get; }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaConformalIpnsRound(RGaConformalSpace<T> space, RGaKVector<T> blade)
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
    public RGaConformalOpnsRound<T> ToOpnsRound()
    {
        return new RGaConformalOpnsRound<T>(
            Space,
            Blade.UnDual(Space.VSpaceDimensions)
        );
    }
}