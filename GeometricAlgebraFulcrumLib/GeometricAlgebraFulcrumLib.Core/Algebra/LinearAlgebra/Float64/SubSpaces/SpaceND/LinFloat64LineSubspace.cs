using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.SubSpaces.SpaceND;

public sealed class LinFloat64LineSubspace :
    ILinFloat64Subspace
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64LineSubspace CreateFromVector(LinFloat64Vector vector)
    {
        var length =
            vector.ENorm();

        var unitVector =
            length.IsNearOne() ? vector : vector / length;

        return new LinFloat64LineSubspace(unitVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64LineSubspace CreateFromUnitVector(LinFloat64Vector vector)
    {
        return new LinFloat64LineSubspace(vector);
    }


    public int VSpaceDimensions
        => BasisVector.VSpaceDimensions;

    public int SubspaceDimensions
        => 1;

    public LinFloat64Vector BasisVector { get; }

    public IEnumerable<LinFloat64Vector> BasisVectors
    {
        get
        {
            yield return BasisVector;
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64LineSubspace(LinFloat64Vector vector)
    {
        Debug.Assert(
            vector.IsNearUnit()
        );

        BasisVector = vector;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool NearContains(LinFloat64Vector vector, double epsilon = 1E-12D)
    {
        return vector.IsNearZero(epsilon) ||
               vector.IsNearParallelToUnit(BasisVector, epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool NearContains(ILinFloat64Subspace subspace, double epsilon = 1E-12)
    {
        return subspace.VSpaceDimensions <= VSpaceDimensions &&
               subspace.BasisVectors.All(v => NearContains(v, epsilon));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return BasisVector.IsValid() &&
               BasisVector.IsNearUnit();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector GetVectorProjection(LinFloat64Vector vector)
    {
        return vector.ProjectOnUnitVector(BasisVector);
    }

    public LinFloat64PolarAngle GetVectorProjectionPolarAngle(LinFloat64Vector vector)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector GetVectorRejection(LinFloat64Vector vector)
    {
        return vector.RejectOnUnitVector(BasisVector);
    }

}