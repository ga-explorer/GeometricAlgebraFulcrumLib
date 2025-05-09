using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Spaces.Conformal;

public class RGaConformalIpnsFlat<T> :
    CGaFloat64Blade<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalIpnsFlat<T> operator *(RGaConformalIpnsFlat<T> mv, T s)
    {
        return new RGaConformalIpnsFlat<T>(
            mv.Space,
            mv.Blade.Times(s)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalIpnsFlat<T> operator *(T s, RGaConformalIpnsFlat<T> mv)
    {
        return new RGaConformalIpnsFlat<T>(
            mv.Space,
            mv.Blade.Times(s)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalIpnsFlat<T> operator /(RGaConformalIpnsFlat<T> mv, T s)
    {
        return new RGaConformalIpnsFlat<T>(
            mv.Space,
            mv.Blade.Divide(s)
        );
    }

        
    public override RGaKVector<T> Blade { get; }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaConformalIpnsFlat(RGaConformalSpace<T> space, RGaKVector<T> blade)
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
    public RGaConformalOpnsFlat<T> ToOpnsFlat()
    {
        return new RGaConformalOpnsFlat<T>(
            Space,
            Blade.UnDual(Space.VSpaceDimensions)
        );
    }
}