using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.SubSpaces.SpaceND;

public sealed class LinFloat64PlaneSubspace :
    ILinFloat64Subspace
{
    
    public static LinFloat64PlaneSubspace CreateFromSpanningVectors(LinFloat64Vector vector1, LinFloat64Vector vector2)
    {
        var u = vector1.ToUnitLinVector();
        var v = vector2.RejectOnUnitVector(u).DivideByENorm();

        return new LinFloat64PlaneSubspace(u, v);
    }

    
    public static LinFloat64PlaneSubspace CreateFromUnitVectors(LinFloat64Vector vector1, LinFloat64Vector vector2)
    {
        return new LinFloat64PlaneSubspace(
            vector1,
            vector2.RejectOnUnitVector(vector1).DivideByENorm()
        );
    }

    
    public static LinFloat64PlaneSubspace CreateFromOrthogonalVectors(LinFloat64Vector vector1, LinFloat64Vector vector2)
    {
        return new LinFloat64PlaneSubspace(
            vector1.ToUnitLinVector(),
            vector2.ToUnitLinVector()
        );
    }

    
    public static LinFloat64PlaneSubspace CreateFromOrthonormalVectors(LinFloat64Vector vector1, LinFloat64Vector vector2)
    {
        return new LinFloat64PlaneSubspace(
            vector1,
            vector2
        );
    }


    public int VSpaceDimensions
        => BasisVector1.VSpaceDimensions;

    public int SubspaceDimensions
        => 2;

    public IEnumerable<LinFloat64Vector> BasisVectors
    {
        get
        {
            yield return BasisVector1;
            yield return BasisVector2;
        }
    }

    public LinFloat64Vector BasisVector1 { get; }

    public LinFloat64Vector BasisVector2 { get; }


    
    private LinFloat64PlaneSubspace(LinFloat64Vector vector1, LinFloat64Vector vector2)
    {
        BasisVector1 = vector1;
        BasisVector2 = vector2;

        Debug.Assert(IsValid());
    }


    public bool NearContains(LinFloat64Vector vector, double zeroEpsilon = 1E-12D)
    {
        if (vector.IsNearZero(zeroEpsilon))
            return true;

        // Project vector on subspace plane and compare with original vector

        var (xuDot, xvDot) = vector.VectorDot(BasisVector1, BasisVector2);

        var diffNorm = (vector - (xuDot * BasisVector1 + xvDot * BasisVector2)).ENormSquared();

        return diffNorm < zeroEpsilon;

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

    
    public bool NearContains(ILinFloat64Subspace subspace, double zeroEpsilon = 1E-12)
    {
        return subspace.VSpaceDimensions <= VSpaceDimensions &&
               subspace.BasisVectors.All(v => NearContains(v, zeroEpsilon));
    }

    
    public bool IsValid()
    {
        return BasisVector1.IsValid() &&
               BasisVector2.IsValid() &&
               BasisVector1.IsNearOrthonormalWith(BasisVector2);
    }

    
    public LinFloat64Vector GetVectorProjection(LinFloat64Vector vector)
    {
        var (u1Dot, u2Dot) =
            vector.VectorDot(BasisVector1, BasisVector2);

        return LinFloat64VectorComposer
            .Create()
            .SetVector(BasisVector1, u1Dot)
            .AddVector(BasisVector2, u2Dot)
            .GetVector();
    }

    
    public LinFloat64PolarAngle GetVectorProjectionPolarAngle(LinFloat64Vector vector)
    {
        return LinFloat64PolarAngle.CreateFromVector(
            vector.VectorDot(BasisVector1, BasisVector2)
        );
    }

    
    public LinFloat64Vector GetVectorRejection(LinFloat64Vector vector)
    {
        var (u1Dot, u2Dot) =
            vector.VectorDot(BasisVector1, BasisVector2);

        return LinFloat64VectorComposer
            .Create()
            .SetVector(vector)
            .AddVector(BasisVector1, -u1Dot)
            .AddVector(BasisVector2, -u2Dot)
            .GetVector();
    }
}