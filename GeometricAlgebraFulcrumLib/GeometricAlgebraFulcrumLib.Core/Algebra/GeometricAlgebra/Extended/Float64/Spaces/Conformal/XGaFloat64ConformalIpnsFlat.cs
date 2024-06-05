using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Spaces.Conformal;

public class XGaFloat64ConformalIpnsFlat :
    XGaFloat64ConformalBlade
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64ConformalIpnsFlat operator *(XGaFloat64ConformalIpnsFlat mv, double s)
    {
        return new XGaFloat64ConformalIpnsFlat(
            mv.Space,
            mv.Blade.Times(s)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64ConformalIpnsFlat operator *(double s, XGaFloat64ConformalIpnsFlat mv)
    {
        return new XGaFloat64ConformalIpnsFlat(
            mv.Space,
            mv.Blade.Times(s)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64ConformalIpnsFlat operator /(XGaFloat64ConformalIpnsFlat mv, double s)
    {
        return new XGaFloat64ConformalIpnsFlat(
            mv.Space,
            mv.Blade.Divide(s)
        );
    }

        
    public override XGaFloat64KVector Blade { get; }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaFloat64ConformalIpnsFlat(XGaFloat64ConformalSpace space, XGaFloat64KVector blade)
        : base(space)
    {
        Blade = blade;
    }


    public override bool IsValid()
    {
        throw new NotImplementedException();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Square()
    {
        return Blade.SpSquared();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64ConformalOpnsFlat ToOpnsFlat()
    {
        return new XGaFloat64ConformalOpnsFlat(
            Space,
            Blade.UnDual(Space.VSpaceDimensions)
        );
    }
}