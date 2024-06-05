using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Spaces.Conformal;

public class RGaFloat64ConformalOpnsDirection :
    RGaFloat64ConformalBlade
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ConformalOpnsDirection operator *(RGaFloat64ConformalOpnsDirection mv, double s)
    {
        return new RGaFloat64ConformalOpnsDirection(
            mv.Space,
            mv.Blade.Times(s)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ConformalOpnsDirection operator *(double s, RGaFloat64ConformalOpnsDirection mv)
    {
        return new RGaFloat64ConformalOpnsDirection(
            mv.Space,
            mv.Blade.Times(s)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ConformalOpnsDirection operator /(RGaFloat64ConformalOpnsDirection mv, double s)
    {
        return new RGaFloat64ConformalOpnsDirection(
            mv.Space,
            mv.Blade.Divide(s)
        );
    }

        
    public override RGaFloat64KVector Blade { get; }
        

    internal RGaFloat64ConformalOpnsDirection(RGaFloat64ConformalSpace space, RGaFloat64KVector blade)
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
    public RGaFloat64ConformalOpnsDirection ToIpnsDirection()
    {
        return new RGaFloat64ConformalOpnsDirection(
            Space,
            Blade.Dual(Space.VSpaceDimensions)
        );
    }
}