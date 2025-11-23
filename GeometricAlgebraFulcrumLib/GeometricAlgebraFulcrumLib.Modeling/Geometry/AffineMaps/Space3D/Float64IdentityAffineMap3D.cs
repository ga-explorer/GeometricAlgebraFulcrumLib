using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;

public sealed record Float64IdentityAffineMap3D :
    IFloat64RotateAffineMap3D
{
    public static Float64IdentityAffineMap3D Instance { get; }
        = new Float64IdentityAffineMap3D();


    public bool SwapsHandedness
        => false;


    private Float64IdentityAffineMap3D()
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return true;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsIdentity()
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearIdentity(double tolerance = Float64Utils.DefaultTolerance)
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
    public Matrix<double> GetMatrix()
    {
        return DenseMatrix.CreateIdentity(4);
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
    public IFloat64AffineMap3D GetInverseAffineMap()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IFloat64RotateAffineMap3D InverseRotateMap()
    {
        return this;
    }
}