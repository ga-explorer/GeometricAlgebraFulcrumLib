using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Spaces.Conformal;

public class XGaFloat64ConformalOpnsRound
    : XGaFloat64ConformalBlade
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64ConformalOpnsRound operator *(XGaFloat64ConformalOpnsRound mv, double s)
    {
        return new XGaFloat64ConformalOpnsRound(
            mv.Space,
            mv.Blade.Times(s)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64ConformalOpnsRound operator *(double s, XGaFloat64ConformalOpnsRound mv)
    {
        return new XGaFloat64ConformalOpnsRound(
            mv.Space,
            mv.Blade.Times(s)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64ConformalOpnsRound operator /(XGaFloat64ConformalOpnsRound mv, double s)
    {
        return new XGaFloat64ConformalOpnsRound(
            mv.Space,
            mv.Blade.Divide(s)
        );
    }

        
    public override XGaFloat64KVector Blade { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaFloat64ConformalOpnsRound(XGaFloat64ConformalSpace space, XGaFloat64KVector blade)
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
    public XGaFloat64ConformalIpnsRound ToIpnsRound()
    {
        return new XGaFloat64ConformalIpnsRound(
            Space,
            Blade.Dual(Space.VSpaceDimensions)
        );
    }
}