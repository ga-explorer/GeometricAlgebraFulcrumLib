using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Spaces.Conformal;

public class RGaConformalOpnsRound<T>
    : RGaConformalBlade<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalOpnsRound<T> operator *(RGaConformalOpnsRound<T> mv, T s)
    {
        return new RGaConformalOpnsRound<T>(
            mv.Space,
            mv.Blade.Times(s)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalOpnsRound<T> operator *(T s, RGaConformalOpnsRound<T> mv)
    {
        return new RGaConformalOpnsRound<T>(
            mv.Space,
            mv.Blade.Times(s)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalOpnsRound<T> operator /(RGaConformalOpnsRound<T> mv, T s)
    {
        return new RGaConformalOpnsRound<T>(
            mv.Space,
            mv.Blade.Divide(s)
        );
    }

        
    public override RGaKVector<T> Blade { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaConformalOpnsRound(RGaConformalSpace<T> space, RGaKVector<T> blade)
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
    public RGaConformalIpnsRound<T> ToIpnsRound()
    {
        return new RGaConformalIpnsRound<T>(
            Space,
            Blade.Dual(Space.VSpaceDimensions)
        );
    }
}