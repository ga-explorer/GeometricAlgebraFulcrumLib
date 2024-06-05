using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Spaces.Conformal;

public class XGaFloat64ConformalIpnsPoint :
    XGaFloat64ConformalIpnsVector
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64ConformalIpnsPoint operator *(XGaFloat64ConformalIpnsPoint mv, double s)
    {
        return new XGaFloat64ConformalIpnsPoint(
            mv.Space,
            mv.Vector.Times(s)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64ConformalIpnsPoint operator *(double s, XGaFloat64ConformalIpnsPoint mv)
    {
        return new XGaFloat64ConformalIpnsPoint(
            mv.Space,
            mv.Vector.Times(s)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64ConformalIpnsPoint operator /(XGaFloat64ConformalIpnsPoint mv, double s)
    {
        return new XGaFloat64ConformalIpnsPoint(
            mv.Space,
            mv.Vector.Divide(s)
        );
    }


    internal XGaFloat64ConformalIpnsPoint(XGaFloat64ConformalSpace space, XGaFloat64Vector vector)
        : base(space, vector)
    {
    }

    internal XGaFloat64ConformalIpnsPoint(XGaFloat64ConformalSpace space, XGaFloat64Vector vector, bool assumeUnitWeight)
        : base(space, vector, assumeUnitWeight)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        // TODO: Add one more condition to ensure this is a round point
        return Vector.IsValid() && 
               Vector.VSpaceDimensions <= Space.VSpaceDimensions;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector GetPosition()
    {
        return Vector
            .GetVectorPart(index => index < Space.VSpaceDimensions - 2)
            .Divide(Weight());
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64ConformalIpnsPoint GetNormalizedPoint()
    {
        if (AssumeUnitWeight)
            return this;
            
        return new XGaFloat64ConformalIpnsPoint(
            Space, 
            Vector.Divide(Weight().ScalarValue), 
            true
        );
    }
}