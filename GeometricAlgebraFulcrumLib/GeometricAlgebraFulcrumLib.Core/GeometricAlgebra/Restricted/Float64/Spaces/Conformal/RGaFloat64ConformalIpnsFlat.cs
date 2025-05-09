using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Spaces.Conformal;

public class RGaFloat64ConformalIpnsFlat :
    RGaFloat64ConformalBlade
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ConformalIpnsFlat operator *(RGaFloat64ConformalIpnsFlat mv, double s)
    {
        return new RGaFloat64ConformalIpnsFlat(
            mv.Space,
            mv.Blade.Times(s)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ConformalIpnsFlat operator *(double s, RGaFloat64ConformalIpnsFlat mv)
    {
        return new RGaFloat64ConformalIpnsFlat(
            mv.Space,
            mv.Blade.Times(s)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ConformalIpnsFlat operator /(RGaFloat64ConformalIpnsFlat mv, double s)
    {
        return new RGaFloat64ConformalIpnsFlat(
            mv.Space,
            mv.Blade.Divide(s)
        );
    }

        
    public override RGaFloat64KVector Blade { get; }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaFloat64ConformalIpnsFlat(RGaFloat64ConformalSpace space, RGaFloat64KVector blade)
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
    public RGaFloat64ConformalOpnsFlat ToOpnsFlat()
    {
        return new RGaFloat64ConformalOpnsFlat(
            Space,
            Blade.UnDual(Space.VSpaceDimensions)
        );
    }
}