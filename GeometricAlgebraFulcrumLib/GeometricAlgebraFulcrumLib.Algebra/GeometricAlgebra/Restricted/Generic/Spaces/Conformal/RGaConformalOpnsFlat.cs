using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Spaces.Conformal;

public class RGaConformalOpnsFlat<T> :
    RGaConformalBlade<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalOpnsFlat<T> operator *(RGaConformalOpnsFlat<T> mv, T s)
    {
        return new RGaConformalOpnsFlat<T>(
            mv.Space,
            mv.Blade.Times(s)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalOpnsFlat<T> operator *(T s, RGaConformalOpnsFlat<T> mv)
    {
        return new RGaConformalOpnsFlat<T>(
            mv.Space,
            mv.Blade.Times(s)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalOpnsFlat<T> operator /(RGaConformalOpnsFlat<T> mv, T s)
    {
        return new RGaConformalOpnsFlat<T>(
            mv.Space,
            mv.Blade.Divide(s)
        );
    }

        
    public override RGaKVector<T> Blade { get; }
        

    internal RGaConformalOpnsFlat(RGaConformalSpace<T> space, RGaKVector<T> blade)
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
    public RGaConformalOpnsFlat<T> ToIpnsFlat()
    {
        return new RGaConformalOpnsFlat<T>(
            Space,
            Blade.Dual(Space.VSpaceDimensions)
        );
    }
}