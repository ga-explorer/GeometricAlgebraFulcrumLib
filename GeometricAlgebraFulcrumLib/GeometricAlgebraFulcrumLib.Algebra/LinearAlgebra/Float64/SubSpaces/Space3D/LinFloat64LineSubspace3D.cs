using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.SubSpaces.Space3D;

public sealed class LinFloat64LineSubspace3D :
    ILinFloat64Subspace3D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64LineSubspace3D CreateFromVector(LinFloat64Vector3D vector)
    {
        var length =
            LinFloat64Vector3DUtils.L2Norm(vector);

        var unitVector =
            length.IsNearOne() ? vector : vector / length;

        return new LinFloat64LineSubspace3D(unitVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64LineSubspace3D CreateFromUnitVector(LinFloat64Vector3D vector)
    {
        return new LinFloat64LineSubspace3D(vector);
    }


    public int VSpaceDimensions
        => 3;

    public int SubspaceDimensions
        => 1;

    public LinFloat64Vector3D BasisVector { get; }

    public IEnumerable<LinFloat64Vector3D> BasisVectors
    {
        get
        {
            yield return BasisVector;
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64LineSubspace3D(LinFloat64Vector3D vector)
    {
        Debug.Assert(
            vector.IsNearUnit()
        );

        BasisVector = vector;
    }


    public LinFloat64PolarAngle GetVectorProjectionPolarAngle(LinFloat64Vector3D vector)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool NearContains(LinFloat64Vector3D vector, double tolerance = 1E-12D)
    {
        return vector.IsNearZero(tolerance) ||
               vector.IsNearParallelToUnit(BasisVector, tolerance);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool NearContains(ILinFloat64Subspace3D subspace, double tolerance = Float64Utils.DefaultTolerance)
    {
        return subspace.VSpaceDimensions <= VSpaceDimensions &&
               subspace.BasisVectors.All(v => NearContains(v, tolerance));
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