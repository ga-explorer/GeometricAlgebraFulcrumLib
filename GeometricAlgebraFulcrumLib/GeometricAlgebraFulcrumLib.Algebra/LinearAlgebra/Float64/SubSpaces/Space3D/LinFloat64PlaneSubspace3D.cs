using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.SubSpaces.Space3D;

public sealed class LinFloat64PlaneSubspace3D :
    ILinFloat64Subspace3D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PlaneSubspace3D CreateFromVectors(ILinFloat64Vector3D vector1, ILinFloat64Vector3D vector2)
    {
        var u = vector1.ToUnitLinVector3D();
        var v = vector2.RejectOnUnitVector(u).ToUnitLinVector3D();

        return new LinFloat64PlaneSubspace3D(u, v);

    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PlaneSubspace3D CreateFromUnitVectors(ILinFloat64Vector3D vector1, ILinFloat64Vector3D vector2)
    {
        return new LinFloat64PlaneSubspace3D(
            vector1.ToLinVector3D(),
            vector2.RejectOnUnitVector(vector1).ToUnitLinVector3D()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PlaneSubspace3D CreateFromOrthogonalVectors(ILinFloat64Vector3D vector1, ILinFloat64Vector3D vector2)
    {
        return new LinFloat64PlaneSubspace3D(
            vector1.ToUnitLinVector3D(),
            vector2.ToUnitLinVector3D()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PlaneSubspace3D CreateFromOrthonormalVectors(ILinFloat64Vector3D vector1, ILinFloat64Vector3D vector2)
    {
        return new LinFloat64PlaneSubspace3D(
            vector1.ToLinVector3D(),
            vector2.ToLinVector3D()
        );
    }


    public int VSpaceDimensions
        => 3;

    public int SubspaceDimensions
        => 2;

    public IEnumerable<LinFloat64Vector3D> BasisVectors
    {
        get
        {
            yield return BasisVector1;
            yield return BasisVector2;
        }
    }

    public LinFloat64Vector3D BasisVector1 { get; }

    public LinFloat64Vector3D BasisVector2 { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64PlaneSubspace3D(LinFloat64Vector3D vector1, LinFloat64Vector3D vector2)
    {
        Debug.Assert(
            vector1.IsNearOrthonormalWith(vector2)
        );

        BasisVector1 = vector1;
        BasisVector2 = vector2;
    }


    public bool NearContains(ILinFloat64Vector3D vector, double epsilon = 1E-12D)
    {
        if (vector.IsNearZero(epsilon))
            return true;

        // Project vector on subspace plane and compare with original vector

        var (xuDot, xvDot) = vector.VectorESp(BasisVector1, BasisVector2);

        var diffNorm = (vector - (xuDot * BasisVector1 + xvDot * BasisVector2)).VectorENormSquared();

        return diffNorm < epsilon;

        //var rank = Matrix<double>.Build.DenseOfColumnArrays(
        //    vector,
        //    BasisVector1,
        //    BasisVector2
        //).Rank();

        //Debug.Assert(
        //    rank is 2 or 3
        //);

        //return rank == 2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool NearContains(ILinFloat64Subspace3D subspace, double epsilon = 1E-12)
    {
        return subspace.VSpaceDimensions <= VSpaceDimensions &&
               subspace.BasisVectors.All(v => NearContains(v, epsilon));
    }

    public bool IsValid()
    {
        throw new NotImplementedException();
    }

    public LinFloat64Vector3D GetVectorProjection(ILinFloat64Vector3D vector)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarAngle GetVectorProjectionPolarAngle(ILinFloat64Vector3D vector)
    {
        var (u1Dot, u2Dot) =
            vector.VectorESp(BasisVector1, BasisVector2);

        return LinFloat64PolarAngle.CreateFromVector(u1Dot, u2Dot);
    }

    public LinFloat64Vector3D GetVectorRejection(ILinFloat64Vector3D vector)
    {
        throw new NotImplementedException();
    }

}