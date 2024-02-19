using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.SubSpaces.SpaceND;

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

    public IEnumerable<Float64Vector> BasisVectors
        => Enumerable.Empty<Float64Vector>();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64NullSubspace(int dimensions)
    {
        if (dimensions < 1)
            throw new ArgumentOutOfRangeException(nameof(dimensions));

        VSpaceDimensions = dimensions;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool NearContains(Float64Vector vector, double epsilon = 1E-12D)
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
    public Float64Vector GetVectorProjection(Float64Vector vector)
    {
        return Float64Vector.ZeroVector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64PlanarAngle GetVectorProjectionPolarAngle(Float64Vector vector)
    {
        return Float64PlanarAngle.Angle0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector GetVectorRejection(Float64Vector vector)
    {
        return vector;
    }
}