using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Spaces.Conformal;

public class XGaFloat64ConformalIpnsRound :
    XGaFloat64ConformalBlade
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64ConformalIpnsRound operator *(XGaFloat64ConformalIpnsRound mv, double s)
    {
        return new XGaFloat64ConformalIpnsRound(
            mv.Space,
            mv.Blade.Times(s)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64ConformalIpnsRound operator *(double s, XGaFloat64ConformalIpnsRound mv)
    {
        return new XGaFloat64ConformalIpnsRound(
            mv.Space,
            mv.Blade.Times(s)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64ConformalIpnsRound operator /(XGaFloat64ConformalIpnsRound mv, double s)
    {
        return new XGaFloat64ConformalIpnsRound(
            mv.Space,
            mv.Blade.Divide(s)
        );
    }

        
    public override XGaFloat64KVector Blade { get; }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaFloat64ConformalIpnsRound(XGaFloat64ConformalSpace space, XGaFloat64KVector blade)
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
    public XGaFloat64ConformalOpnsRound ToOpnsRound()
    {
        return new XGaFloat64ConformalOpnsRound(
            Space,
            Blade.UnDual(Space.VSpaceDimensions)
        );
    }
}