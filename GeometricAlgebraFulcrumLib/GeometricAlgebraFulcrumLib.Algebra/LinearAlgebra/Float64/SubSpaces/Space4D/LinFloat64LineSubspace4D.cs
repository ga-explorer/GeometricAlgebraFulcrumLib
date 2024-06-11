using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.SubSpaces.Space4D;

public sealed class LinFloat64LineSubspace4D :
    ILinFloat64Subspace4D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64LineSubspace4D CreateFromVector(LinFloat64Vector4D vector)
    {
        var length =
            vector.VectorENorm();

        var unitVector =
            length.IsNearOne() ? vector : vector / length;

        return new LinFloat64LineSubspace4D(unitVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64LineSubspace4D CreateFromUnitVector(LinFloat64Vector4D vector)
    {
        return new LinFloat64LineSubspace4D(vector);
    }


    public int VSpaceDimensions
        => 4;

    public int SubspaceDimensions
        => 1;

    public LinFloat64Vector4D BasisVector { get; }

    public IEnumerable<LinFloat64Vector4D> BasisVectors
    {
        get
        {
            yield return BasisVector;
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64LineSubspace4D(LinFloat64Vector4D vector)
    {
        Debug.Assert(
            vector.IsNearUnit()
        );

        BasisVector = vector;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool NearContains(ILinFloat64Vector4D vector, double epsilon = 1E-12D)
    {
        return vector.IsNearZero(epsilon) ||
               vector.IsNearParallelToUnit(BasisVector, epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool NearContains(ILinFloat64Subspace4D subspace, double epsilon = 1E-12)
    {
        return subspace.VSpaceDimensions <= VSpaceDimensions &&
               subspace.BasisVectors.All(v => NearContains(v, epsilon));
    }

    public bool IsValid()
    {
        throw new NotImplementedException();
    }
}