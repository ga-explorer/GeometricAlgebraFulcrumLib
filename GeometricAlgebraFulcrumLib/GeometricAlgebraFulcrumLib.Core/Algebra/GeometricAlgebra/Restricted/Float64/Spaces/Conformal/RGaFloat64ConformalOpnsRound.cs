using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Spaces.Conformal;

public class RGaFloat64ConformalOpnsRound
    : RGaFloat64ConformalBlade
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ConformalOpnsRound operator *(RGaFloat64ConformalOpnsRound mv, double s)
    {
        return new RGaFloat64ConformalOpnsRound(
            mv.Space,
            mv.Blade.Times(s)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ConformalOpnsRound operator *(double s, RGaFloat64ConformalOpnsRound mv)
    {
        return new RGaFloat64ConformalOpnsRound(
            mv.Space,
            mv.Blade.Times(s)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ConformalOpnsRound operator /(RGaFloat64ConformalOpnsRound mv, double s)
    {
        return new RGaFloat64ConformalOpnsRound(
            mv.Space,
            mv.Blade.Divide(s)
        );
    }

        
    public override RGaFloat64KVector Blade { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaFloat64ConformalOpnsRound(RGaFloat64ConformalSpace space, RGaFloat64KVector blade)
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
    public RGaFloat64ConformalIpnsRound ToIpnsRound()
    {
        return new RGaFloat64ConformalIpnsRound(
            Space,
            Blade.Dual(Space.VSpaceDimensions)
        );
    }
}