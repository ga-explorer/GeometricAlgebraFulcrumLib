using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Spaces.Conformal;

public class XGaFloat64ConformalOpnsHyperPlane :
    XGaFloat64ConformalBlade
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64ConformalOpnsHyperPlane operator *(XGaFloat64ConformalOpnsHyperPlane mv, double s)
    {
        return new XGaFloat64ConformalOpnsHyperPlane(
            mv.Space,
            mv.Blade.Times(s)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64ConformalOpnsHyperPlane operator *(double s, XGaFloat64ConformalOpnsHyperPlane mv)
    {
        return new XGaFloat64ConformalOpnsHyperPlane(
            mv.Space,
            mv.Blade.Times(s)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64ConformalOpnsHyperPlane operator /(XGaFloat64ConformalOpnsHyperPlane mv, double s)
    {
        return new XGaFloat64ConformalOpnsHyperPlane(
            mv.Space,
            mv.Blade.Divide(s)
        );
    }
        
        
    public override XGaFloat64KVector Blade { get; }
        

    internal XGaFloat64ConformalOpnsHyperPlane(XGaFloat64ConformalSpace space, XGaFloat64KVector vector)
        : base(space)
    {
        Blade = vector;
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
    public XGaFloat64ConformalIpnsHyperPlane ToIpnsHyperPlane()
    {
        return new XGaFloat64ConformalIpnsHyperPlane(
            Space,
            Blade.Dual(Space.VSpaceDimensions).GetVectorPart()
        );
    }
}