using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Spaces.Conformal;

public class XGaConformalIpnsFlat<T> :
    XGaConformalBlade<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalIpnsFlat<T> operator *(XGaConformalIpnsFlat<T> mv, T s)
    {
        return new XGaConformalIpnsFlat<T>(
            mv.Space,
            mv.Blade.Times(s)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalIpnsFlat<T> operator *(T s, XGaConformalIpnsFlat<T> mv)
    {
        return new XGaConformalIpnsFlat<T>(
            mv.Space,
            mv.Blade.Times(s)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalIpnsFlat<T> operator /(XGaConformalIpnsFlat<T> mv, T s)
    {
        return new XGaConformalIpnsFlat<T>(
            mv.Space,
            mv.Blade.Divide(s)
        );
    }

        
    public override XGaKVector<T> Blade { get; }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaConformalIpnsFlat(XGaConformalSpace<T> space, XGaKVector<T> blade)
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
    public XGaConformalOpnsFlat<T> ToOpnsFlat()
    {
        return new XGaConformalOpnsFlat<T>(
            Space,
            Blade.UnDual(Space.VSpaceDimensions)
        );
    }
}