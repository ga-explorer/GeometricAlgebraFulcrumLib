using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Lite.Maps.Space2D;

public sealed class IdentityMap2D : 
    IAffineMap2D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix3 GetSquareMatrix3()
    {
        return SquareMatrix3.CreateIdentityMatrix();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[,] GetArray2D()
    {
        throw new NotImplementedException();
    }

    public bool SwapsHandedness 
        => false;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector2D MapPoint(IFloat64Vector2D point)
    {
        return point.ToVector2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector2D MapVector(IFloat64Vector2D vector)
    {
        return vector.ToVector2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector2D MapNormal(IFloat64Vector2D normal)
    {
        return normal.ToVector2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IAffineMap2D GetInverseAffineMap()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return true;
    }
}