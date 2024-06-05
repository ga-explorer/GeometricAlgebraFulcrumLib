using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Spaces.Conformal;

public class RGaConformalIpnsPoint<T> :
    RGaConformalIpnsVector<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalIpnsPoint<T> operator *(RGaConformalIpnsPoint<T> mv, T s)
    {
        return new RGaConformalIpnsPoint<T>(
            mv.Space,
            mv.Vector.Times(s)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalIpnsPoint<T> operator *(T s, RGaConformalIpnsPoint<T> mv)
    {
        return new RGaConformalIpnsPoint<T>(
            mv.Space,
            mv.Vector.Times(s)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalIpnsPoint<T> operator /(RGaConformalIpnsPoint<T> mv, T s)
    {
        return new RGaConformalIpnsPoint<T>(
            mv.Space,
            mv.Vector.Divide(s)
        );
    }


    internal RGaConformalIpnsPoint(RGaConformalSpace<T> space, RGaVector<T> vector)
        : base(space, vector)
    {
    }

    internal RGaConformalIpnsPoint(RGaConformalSpace<T> space, RGaVector<T> vector, bool assumeUnitWeight)
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
    public RGaVector<T> GetPosition()
    {
        return Vector
            .GetVectorPart(index => index < Space.VSpaceDimensions - 2)
            .Divide(Weight());
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalIpnsPoint<T> GetNormalizedPoint()
    {
        if (AssumeUnitWeight)
            return this;
            
        return new RGaConformalIpnsPoint<T>(
            Space, 
            Vector.Divide(Weight().ScalarValue), 
            true
        );
    }
}