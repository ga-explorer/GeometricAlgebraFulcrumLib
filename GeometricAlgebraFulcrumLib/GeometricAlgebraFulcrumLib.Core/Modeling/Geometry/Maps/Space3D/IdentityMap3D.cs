using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Maps.Space3D;

public sealed record IdentityMap3D :
    IRotateMap3D
{
    public static IdentityMap3D DefaultMap { get; }
        = new IdentityMap3D();


    public bool SwapsHandedness
        => false;


    private IdentityMap3D()
    {
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix4 GetSquareMatrix4()
    {
        return SquareMatrix4.CreateIdentityMatrix();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Matrix4x4 GetMatrix4x4()
    {
        return Matrix4x4.Identity;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[,] GetArray2D()
    {
        var array = new double[4, 4];

        array[0, 0] = 1d;
        array[1, 1] = 1d;
        array[2, 2] = 1d;
        array[3, 3] = 1d;

        return array;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D MapPoint(ILinFloat64Vector3D point)
    {
        return point.ToLinVector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D MapVector(ILinFloat64Vector3D vector)
    {
        return vector.ToLinVector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D MapNormal(ILinFloat64Vector3D normal)
    {
        return normal.ToLinVector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IAffineMap3D GetInverseAffineMap()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRotateMap3D InverseRotateMap()
    {
        return this;
    }
}