using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Spaces.Conformal;

public class RGaFloat64ConformalIpnsPoint :
    RGaFloat64ConformalIpnsVector
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ConformalIpnsPoint operator *(RGaFloat64ConformalIpnsPoint mv, double s)
    {
        return new RGaFloat64ConformalIpnsPoint(
            mv.Space,
            mv.Vector.Times(s)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ConformalIpnsPoint operator *(double s, RGaFloat64ConformalIpnsPoint mv)
    {
        return new RGaFloat64ConformalIpnsPoint(
            mv.Space,
            mv.Vector.Times(s)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ConformalIpnsPoint operator /(RGaFloat64ConformalIpnsPoint mv, double s)
    {
        return new RGaFloat64ConformalIpnsPoint(
            mv.Space,
            mv.Vector.Divide(s)
        );
    }


    internal RGaFloat64ConformalIpnsPoint(RGaFloat64ConformalSpace space, RGaFloat64Vector vector)
        : base(space, vector)
    {
    }

    internal RGaFloat64ConformalIpnsPoint(RGaFloat64ConformalSpace space, RGaFloat64Vector vector, bool assumeUnitWeight)
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
    public RGaFloat64Vector GetPosition()
    {
        return Vector
            .GetVectorPart((int index) => index < Space.VSpaceDimensions - 2)
            .Divide(Weight());
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ConformalIpnsPoint GetNormalizedPoint()
    {
        if (AssumeUnitWeight)
            return this;
            
        return new RGaFloat64ConformalIpnsPoint(
            Space, 
            Vector.Divide(Weight().ScalarValue), 
            true
        );
    }
}