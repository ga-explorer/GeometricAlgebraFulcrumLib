using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.SubSpaces.SpaceND;

public sealed record LinFloat64NullSubspace :
    ILinFloat64Subspace
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64NullSubspace Create(int dimensions)
    {
        return new LinFloat64NullSubspace(dimensions);
    }


    public int VSpaceDimensions { get; }

    public int SubspaceDimensions
        => 0;

    public IEnumerable<LinFloat64Vector> BasisVectors
        => [];


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64NullSubspace(int dimensions)
    {
        if (dimensions < 1)
            throw new ArgumentOutOfRangeException(nameof(dimensions));

        VSpaceDimensions = dimensions;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool NearContains(LinFloat64Vector vector, double tolerance = 1E-12D)
    {
        return vector.IsNearZero(tolerance);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool NearContains(ILinFloat64Subspace subspace, double tolerance = Float64Utils.DefaultTolerance)
    {
        return subspace.SubspaceDimensions == 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector GetVectorProjection(LinFloat64Vector vector)
    {
        return LinFloat64Vector.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarAngle GetVectorProjectionPolarAngle(LinFloat64Vector vector)
    {
        return LinFloat64PolarAngle.Angle0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector GetVectorRejection(LinFloat64Vector vector)
    {
        return vector;
    }
}