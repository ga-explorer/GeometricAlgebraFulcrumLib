using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.SubSpaces.SpaceND;

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
        => Enumerable.Empty<LinFloat64Vector>();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64NullSubspace(int dimensions)
    {
        if (dimensions < 1)
            throw new ArgumentOutOfRangeException(nameof(dimensions));

        VSpaceDimensions = dimensions;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool NearContains(LinFloat64Vector vector, double epsilon = 1E-12D)
    {
        return vector.IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool NearContains(ILinFloat64Subspace subspace, double epsilon = 1E-12)
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
        return LinFloat64Vector.VectorZero;
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