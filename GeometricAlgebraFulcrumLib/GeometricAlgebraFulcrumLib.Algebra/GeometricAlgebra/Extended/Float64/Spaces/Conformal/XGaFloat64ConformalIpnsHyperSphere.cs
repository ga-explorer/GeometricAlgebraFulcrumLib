using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Spaces.Conformal;

public class XGaFloat64ConformalIpnsHyperSphere :
    XGaFloat64ConformalIpnsVector
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64ConformalIpnsHyperSphere operator *(XGaFloat64ConformalIpnsHyperSphere mv, double s)
    {
        return new XGaFloat64ConformalIpnsHyperSphere(
            mv.Space,
            mv.Vector.Times(s)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64ConformalIpnsHyperSphere operator *(double s, XGaFloat64ConformalIpnsHyperSphere mv)
    {
        return new XGaFloat64ConformalIpnsHyperSphere(
            mv.Space,
            mv.Vector.Times(s)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64ConformalIpnsHyperSphere operator /(XGaFloat64ConformalIpnsHyperSphere mv, double s)
    {
        return new XGaFloat64ConformalIpnsHyperSphere(
            mv.Space,
            mv.Vector.Divide(s)
        );
    }


    internal XGaFloat64ConformalIpnsHyperSphere(XGaFloat64ConformalSpace space, XGaFloat64Vector vector)
        : base(space, vector)
    {
    }

    internal XGaFloat64ConformalIpnsHyperSphere(XGaFloat64ConformalSpace space, XGaFloat64Vector vector, bool assumeUnitWeight)
        : base(space, vector, assumeUnitWeight)
    {
    }
        
        
    public override bool IsValid()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector GetCenter()
    {
        return Vector.GetVectorPart(index => index < Space.VSpaceDimensions - 2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetRadiusSquared()
    {
        return Vector.SpSquared().ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetRadius()
    {
        return GetRadiusSquared().SqrtOfAbs();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64ConformalIpnsHyperSphere ToOpnsHyperSphere()
    {
        return new XGaFloat64ConformalIpnsHyperSphere(
            Space,
            Vector.UnDual(Space.VSpaceDimensions).GetVectorPart()
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64ConformalIpnsHyperSphere GetNormalizedSphere()
    {
        if (AssumeUnitWeight)
            return this;

        var vector = Vector.Divide(Weight());

        return new XGaFloat64ConformalIpnsHyperSphere(
            Space, 
            vector, 
            true
        );
    }
}