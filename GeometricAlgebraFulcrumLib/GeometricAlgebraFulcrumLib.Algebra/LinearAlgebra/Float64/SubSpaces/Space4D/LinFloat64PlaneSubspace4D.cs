using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.SubSpaces.Space4D;

public sealed class LinFloat64PlaneSubspace4D :
    ILinFloat64Subspace4D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PlaneSubspace4D CreateFromVectors(ILinFloat64Vector4D vector1, ILinFloat64Vector4D vector2)
    {
        var u = vector1.ToUnitLinVector4D();
        var v = vector2.RejectOnUnitVector(u).ToUnitLinVector4D();

        return new LinFloat64PlaneSubspace4D(u, v);

    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PlaneSubspace4D CreateFromUnitVectors(ILinFloat64Vector4D vector1, ILinFloat64Vector4D vector2)
    {
        return new LinFloat64PlaneSubspace4D(
            LinFloat64Vector4DUtils.ToLinVector4D(vector1),
            vector2.RejectOnUnitVector(vector1).ToUnitLinVector4D()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PlaneSubspace4D CreateFromOrthogonalVectors(ILinFloat64Vector4D vector1, ILinFloat64Vector4D vector2)
    {
        return new LinFloat64PlaneSubspace4D(
            vector1.ToUnitLinVector4D(),
            vector2.ToUnitLinVector4D()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PlaneSubspace4D CreateFromOrthonormalVectors(ILinFloat64Vector4D vector1, ILinFloat64Vector4D vector2)
    {
        return new LinFloat64PlaneSubspace4D(
            LinFloat64Vector4DUtils.ToLinVector4D(vector1),
            LinFloat64Vector4DUtils.ToLinVector4D(vector2)
        );
    }


    public int VSpaceDimensions
        => 4;

    public int SubspaceDimensions
        => 2;

    public IEnumerable<LinFloat64Vector4D> BasisVectors
    {
        get
        {
            yield return BasisVector1;
            yield return BasisVector2;
        }
    }

    public LinFloat64Vector4D BasisVector1 { get; }

    public LinFloat64Vector4D BasisVector2 { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64PlaneSubspace4D(LinFloat64Vector4D vector1, LinFloat64Vector4D vector2)
    {
        Debug.Assert(
            vector1.IsNearOrthonormalWith(vector2)
        );

        BasisVector1 = vector1;
        BasisVector2 = vector2;
    }


    public bool NearContains(ILinFloat64Vector4D vector, double epsilon = 1E-12D)
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