using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.SubSpaces.Space3D;

public sealed record LinFloat64NullSubspace3D :
    ILinFloat64Subspace3D
{
    public static LinFloat64NullSubspace3D Instance { get; }
        = new LinFloat64NullSubspace3D();


    public int VSpaceDimensions
        => 3;

    public int SubspaceDimensions
        => 0;

    public IEnumerable<LinFloat64Vector3D> BasisVectors
        => [];


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64NullSubspace3D()
    {
    }


    public LinFloat64PolarAngle GetVectorProjectionPolarAngle(LinFloat64Vector3D vector)
    {
        return LinFloat64PolarAngle.Angle0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool NearContains(LinFloat64Vector3D vector, double tolerance = 1E-12D)
    {
        return vector.L2Norm().IsNearZero(tolerance);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool NearContains(ILinFloat64Subspace3D subspace, double tolerance = Float64Utils.DefaultTolerance)
    {
        return subspace.SubspaceDimensions == 0;
    }

    public bool IsValid()
    {
        throw new NotImplementedException();
    }

    public LinFloat64Vector3D GetVectorProjection(LinFloat64Vector3D vector)
    {
        throw new NotImplementedException();
    }

    public LinFloat64Vector3D GetVectorRejection(LinFloat64Vector3D vector)
    {
        throw new NotImplementedException();
    }

}