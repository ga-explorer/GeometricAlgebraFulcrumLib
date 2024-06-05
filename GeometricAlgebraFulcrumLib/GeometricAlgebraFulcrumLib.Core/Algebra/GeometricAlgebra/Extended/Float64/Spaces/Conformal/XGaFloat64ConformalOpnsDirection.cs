using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Spaces.Conformal;

public class XGaFloat64ConformalOpnsDirection :
    XGaFloat64ConformalBlade
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64ConformalOpnsDirection operator *(XGaFloat64ConformalOpnsDirection mv, double s)
    {
        return new XGaFloat64ConformalOpnsDirection(
            mv.Space,
            mv.Blade.Times(s)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64ConformalOpnsDirection operator *(double s, XGaFloat64ConformalOpnsDirection mv)
    {
        return new XGaFloat64ConformalOpnsDirection(
            mv.Space,
            mv.Blade.Times(s)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64ConformalOpnsDirection operator /(XGaFloat64ConformalOpnsDirection mv, double s)
    {
        return new XGaFloat64ConformalOpnsDirection(
            mv.Space,
            mv.Blade.Divide(s)
        );
    }

        
    public override XGaFloat64KVector Blade { get; }
        

    internal XGaFloat64ConformalOpnsDirection(XGaFloat64ConformalSpace space, XGaFloat64KVector blade)
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
    public XGaFloat64ConformalOpnsDirection ToIpnsDirection()
    {
        return new XGaFloat64ConformalOpnsDirection(
            Space,
            Blade.Dual(Space.VSpaceDimensions)
        );
    }
}